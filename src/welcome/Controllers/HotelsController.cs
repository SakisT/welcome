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

namespace welcome.Controllers
{
    public class HotelsController : Controller
    {
        private readonly WelcomeContext _context;

        private readonly IStringLocalizer<HotelsController> _localizer;

        private UserAccessInfo _userinfo;

        public HotelsController(IStringLocalizer<HotelsController> localizer, WelcomeContext context, UserAccessInfo UserInfo)
        {
            _localizer = localizer;
            _context = context;
            _userinfo = UserInfo;
        }

        // GET: Hotels
        public async Task<IActionResult> Index(Guid? id)
        {
            if (id == null)
            {
                Hotel activehotel = _context.Hotels.SingleOrDefault(s => s.id == _userinfo.GetActiveHotels().FirstOrDefault());
                IQueryable<Hotel> userhotels = _context.Hotels.Include(h => h.HotelGroup).Where(s => s.HotelGroupID == activehotel.HotelGroupID);
                if (!User.IsInRole("Administrator"))
                {
                    userhotels = userhotels.Where(r => _userinfo.HotelIDs.Any(s => s == r.id));
                }
                else
                {
                        _userinfo.HotelIDs=new Guid[] { id.Value }.ToList();
                        _userinfo.SetActiveHotels(new Guid[] { id.Value});
                }
                return View(await userhotels.ToListAsync());
            }
            else
            {
                ViewBag.ActiveHotelGroupID = id;
                if (User.IsInRole("Administrator"))
                {
                    _userinfo.HotelIDs = new Guid[] { id.Value }.ToList();
                    _userinfo.SetActiveHotels(new Guid[] { id.Value });
                }
            }
            var welcomeContext = _context.Hotels.Include(h => h.HotelGroup);
            return View(await welcomeContext.ToListAsync());
        }

        // GET: Hotels/Details/5
        public async Task<IActionResult>
            Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels.SingleOrDefaultAsync(m => m.id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // GET: Hotels/Create
        public async Task<IActionResult> Create(Guid id)
        {
            HotelGroup hotelgroup = await _context.HotelGroups.SingleOrDefaultAsync(s => s.id == id);
            if (hotelgroup == null)
            {
                return NotFound();
            }
            Hotel newhotel = new Hotel { HotelGroupID = id, HotelDate = DateTime.Today, ExpirationDate = DateTime.Today.AddMonths(6), Dealer = "Netera Software", Name = hotelgroup.Name };
            return View(newhotel);
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Create([Bind("Dealer,ExpirationDate,HotelDate,HotelGroupID,IsPayingSupport,Name")] Hotel hotel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    hotel.id = Guid.NewGuid();
                    _context.Add(hotel);

                    HotelVardataPlan hotelvardataplan = new HotelVardataPlan { Hotel = hotel };
                    _context.Add(hotelvardataplan);

                    HotelVardataInvoice hotelvardatainvoice = new HotelVardataInvoice { Hotel = hotel };
                    _context.Add(hotelvardatainvoice);

                    Branch branch = new Branch { Hotel = hotel, Name = hotel.Name };
                    _context.Add(branch);

                    BranchVardata branchvardata = new BranchVardata { Branch = branch };
                    _context.Add(branchvardata);

                    Nationality usualnationality = await _context.Nationalities.FirstOrDefaultAsync();
                    BranchVardataReservation branchvardatareservation = new BranchVardataReservation { Branch = branch, UsualNationality = usualnationality };
                    _context.Add(branchvardatareservation);

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Edit", new { id = hotel.id });
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
            "Try again, and if the problem persists " +
            "see your system administrator.");
            }
            ViewData["HotelGroupID"] = new SelectList(_context.HotelGroups, "id", "Name", hotel.HotelGroupID);
            return View(hotel);
        }

        // GET: Hotels/Edit/5
        public async Task<IActionResult>
            Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels.Include(r => r.VardataInvoice).AsNoTracking().Include(r => r.VardataPlan).AsNoTracking().SingleOrDefaultAsync(m => m.id == id);
            if (hotel == null)
            {
                return NotFound();
            }
            ViewData["HotelGroupID"] = new SelectList(_context.HotelGroups, "id", "Name", hotel.HotelGroupID);
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var modelstate = ModelState;
            var hoteltoupdate = await _context.Hotels.Include(r => r.VardataInvoice).Include(r => r.VardataPlan).SingleOrDefaultAsync(m => m.id == id);
            if (await TryUpdateModelAsync(hoteltoupdate, "",
                s => s.Name,
                s => s.HotelDate,
                s => s.Dealer,
                s => s.ExpirationDate,
                s => s.VardataInvoice,
                s => s.VardataPlan))
            {
                var hotelvardata = await _context.HotelVardataInvoices.SingleOrDefaultAsync(r => r.HotelVardataInvoiceId == id);
                if (await TryUpdateModelAsync(hotelvardata, "",
                    s => s.ArrangementDescription,
                    s => s.DefaultPaymentMethod,
                    s => s.DefaultView,
                    s => s.IncludePaxInInvoice,
                    s => s.InvoiceCopies,
                    s => s.InvoiceEmailBody,
                    s => s.ObligatoryAddressAtInvoices,
                    s => s.ResetInvoiceNumbers,
                    s => s.SendInvoiceToHotelMailAsCC))
                {
                    var hotelvardataplan = await _context.HotelVardataPlans.SingleOrDefaultAsync(r => r.HotelVardataPlanID == id);
                    if (await TryUpdateModelAsync(hotelvardataplan, "",
                        s => s.DisplayAgentCode,
                        s => s.HistoryColor,
                        s => s.InHouseColor,
                        s => s.LabelAddins,
                        s => s.OptionReservationColor,
                        s => s.PendingDepositColor,
                        s => s.ReservationColor))
                    {
                        try
                        {
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index", new { id = hoteltoupdate.HotelGroupID });
                        }
                        catch (DbUpdateException)
                        {
                            ModelState.AddModelError("", "Unable to save changes. " +
                                "Try again, and if the problem persists, " +
                                "see your system administrator.");
                        }
                    }

                }

            }
            ViewData["HotelGroupID"] = new SelectList(_context.HotelGroups, "id", "Name", hoteltoupdate.HotelGroupID);
            return View(hoteltoupdate);
        }

        // GET: Hotels/Delete/5
        public async Task<IActionResult>
            Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels.SingleOrDefaultAsync(m => m.id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            DeleteConfirmed(Guid id)
        {
            var hotel = await _context.Hotels.SingleOrDefaultAsync(m => m.id == id);
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = hotel.HotelGroupID });
        }

        private bool HotelExists(Guid id)
        {
            return _context.Hotels.Any(e => e.id == id);
        }
    }
}
