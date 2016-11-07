using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using welcome.Services;
using welcome.Data;
using welcome.Models;
using System.Globalization;

namespace welcome.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly WelcomeContext _context;

        private readonly IStringLocalizer<ReservationsController> _localizer;

        private UserAccessInfo _userinfo;

        public ReservationsController(IStringLocalizer<ReservationsController> localizer, WelcomeContext context, UserAccessInfo UserInfo)
        {
            _localizer = localizer;
            _context = context;
            _userinfo = UserInfo;
        }

        public IActionResult Index(string shortby = "Date", bool asc = true)
        {
            ViewBag.Asceding = asc;
            IQueryable<Reservation> reservations = _context.Reservations.Include(r => r.Hotel).Include(r => r.StayRooms).ThenInclude(stayroom => stayroom.Agent).AsNoTracking();

            //switch (shortby)
            //{
            //    case "AA":
            //        if (asc) { reservations = reservations.OrderBy(r => r.AA); } else { reservations = reservations.OrderByDescending(r => r.AA); }
            //        break;
            //    case "Reservation":
            //        if (asc) { reservations = reservations.OrderBy(r => r.GuestOrGroup); } else { reservations = reservations.OrderByDescending(r => r.GuestOrGroup); }
            //        break;
            //    case "Rooms":
            //        if (asc) { reservations = reservations.OrderBy(r => r.StayRooms.Count()); } else { reservations = reservations.OrderByDescending(r => r.StayRooms.Count()); }
            //        break;
            //    case "Agent":
            //        if (asc) { reservations = reservations.OrderBy(r => r.StayRooms.OrderBy(r1 => r1.Agent.Name ?? "").FirstOrDefault().Agent.Name ?? ""); } else { reservations = reservations.OrderByDescending(r => r.StayRooms.OrderByDescending(r1 => r1.Agent.Name ?? "").FirstOrDefault().Agent.Name ?? ""); }
            //        break;
            //    default: 
            //        if (asc) { reservations = reservations.OrderBy(r => r.StayRooms.OrderBy(r1 => r1.Arrival).FirstOrDefault().Arrival); } else { reservations = reservations.OrderByDescending(r => r.StayRooms.OrderByDescending(r1 => r1.Arrival).FirstOrDefault().Arrival); }
            //        break;
            //}
            ViewBag.FirstReservation = reservations.FirstOrDefault();
            return View(reservations.ToList());
        }

        public async Task<PartialViewResult> GetReservationContactInfo(Guid id)
        {
            var reservation = await _context.Reservations.Include(r => r.StayRooms)
                .ThenInclude(stayroom => stayroom.StayPersons)
                .ThenInclude(stayperson => stayperson.Customer)
                    .SingleOrDefaultAsync(r => r.ReservationID == id);
            var customer = reservation.StayRooms.FirstOrDefault().StayPersons.FirstOrDefault().Customer;
            ViewData["CustomerID"] = customer.CustomerID;
            var model = await _context.Customers.AsNoTracking().SingleOrDefaultAsync(s => s.CustomerID == customer.CustomerID);
            return PartialView("_ContactInfo", model);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.SingleOrDefaultAsync(m => m.ReservationID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        public IActionResult Create()
        {
            ViewData["HotelID"] = new SelectList(_context.Hotels, "HotelID", "Name");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,AA,AskPrePay,AskPrePayDate,AskPrePayRemarks,GuestOrGroup,HotelID,Remarks")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                reservation.ReservationID = Guid.NewGuid();
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["HotelID"] = new SelectList(_context.Hotels, "HotelID", "Name", reservation.HotelID);
            return View(reservation);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.SingleOrDefaultAsync(m => m.ReservationID == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reservation = await _context.Reservations.SingleOrDefaultAsync(m => m.ReservationID == id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<PartialViewResult> EditReservation(string id)
        {
            //.Include(r => r.StayRooms).ThenInclude(stayroom => stayroom.Agent)
            var reservation = await _context.Reservations.Include(r => r.StayRooms).ThenInclude(stayroom => stayroom.Agent).Include(r => r.StayRooms).ThenInclude(stayroom => stayroom.Pricelist).AsNoTracking().SingleOrDefaultAsync(m => m.ReservationID == Guid.Parse(id));
            ViewData["HotelID"] = new SelectList(_context.Hotels, "HotelID", "Name", reservation.HotelID);
            ViewData["AgentID"] = reservation.StayRooms.FirstOrDefault().Agent?.AgentID ?? Guid.Empty;
            ViewData["AgentCode"] = reservation.StayRooms.FirstOrDefault().Agent?.Code ?? "";
            ViewData["AgentName"] = reservation.StayRooms.FirstOrDefault().Agent?.Name ?? "";
            ViewData["PricelistID"] = reservation.StayRooms.FirstOrDefault().Pricelist?.PricelistID ?? Guid.Empty;
            ViewData["PricelistName"] = reservation.StayRooms.FirstOrDefault().Pricelist?.Name ?? "";
            ViewData["Arrival"] = reservation.StayRooms.FirstOrDefault().Arrival;
            ViewData["Departure"] = reservation.StayRooms.FirstOrDefault().Departure;
            return PartialView("EditReservation", reservation);
        }

        [HttpPost, ActionName("EditReservation")]
        public async Task<IActionResult> EditReservationPost(string id, StayRoom[] SR, Customer Customer)
        {
            var valueProvider = await Microsoft.AspNetCore.Mvc.ModelBinding.CompositeValueProvider.CreateAsync(ControllerContext);

            var reservationtoupdate = await _context.Reservations.SingleOrDefaultAsync(r => r.ReservationID == Guid.Parse(id));

            var Arrival = valueProvider.GetValue("Arrival").Values[0];
            var Departure = valueProvider.GetValue("Departure").Values[0];
            var PricelistID = valueProvider.GetValue("PricelistID").Values[0];
            var AgentID = valueProvider.GetValue("AgentID").Values[0];
            foreach (var item in SR)
            {
                item.AgentID = Guid.Parse(AgentID);
                item.PricelistID = Guid.Parse(PricelistID);
                item.Arrival = DateTime.Parse(Arrival, CultureInfo.CurrentCulture);
                item.Departure = DateTime.Parse(Departure, CultureInfo.CurrentCulture);
                _context.Entry(item).State = EntityState.Modified;
            }
            _context.Entry(Customer).State = EntityState.Modified;
            if (await TryUpdateModelAsync(reservationtoupdate, "", s => s.AA, s => s.GuestOrGroup))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is StayRoom)
                        {
                            var databaseEntity = _context.StayRooms.AsNoTracking().Single(p => p.StayRoomID == ((StayRoom)entry.Entity).StayRoomID);
                            var databaseEntry = _context.Entry(databaseEntity);

                            foreach (var property in entry.Metadata.GetProperties())
                            {
                                var proposedValue = entry.Property(property.Name).CurrentValue;
                                var originalValue = entry.Property(property.Name).OriginalValue;
                                var databaseValue = databaseEntry.Property(property.Name).CurrentValue;

                                entry.Property(property.Name).OriginalValue = databaseEntry.Property(property.Name).CurrentValue;
                            }
                        }
                        else
                        {
                            throw new NotSupportedException("Don't know how to handle concurrency conflicts for " + entry.Metadata.Name);
                        }
                    }
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return PartialView(reservationtoupdate);
        }

        public async Task<PartialViewResult> EditStayRooms(Guid id)
        {
            IQueryable<StayRoom> stayrooms = _context.StayRooms.Include(r => r.ChargeRoomType).OrderBy(r => r.ChargeRoomType.DisplayOrder).Where(r => r.ReservationID == id).AsNoTracking();
            return PartialView(await stayrooms.ToListAsync());
        }

        [HttpPost]
        public IActionResult EditStayRooms(IEnumerable<StayRoom> stayrooms)
        {

            return null;// RedirectToAction("Index");
        }

        public async Task<PartialViewResult> EditReservationContactInfo(Guid id)
        {
            StayRoom stayroom = await _context.StayRooms.SingleOrDefaultAsync(r => r.ReservationID == id);
            Customer tempcustomer = stayroom.StayPersons.SingleOrDefault(r => r.Customer.HasAddress).Customer;
            Customer customer = await _context.Customers.SingleOrDefaultAsync(r => r.CustomerID == tempcustomer.CustomerID);
            return PartialView(customer);
        }

        private bool ReservationExists(Guid id)
        {
            return _context.Reservations.Any(e => e.ReservationID == id);
        }
    }
}
