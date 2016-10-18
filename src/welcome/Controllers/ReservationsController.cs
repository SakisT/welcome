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

        // GET: Reservations
        public async Task<IActionResult> Index(string shortby = "Date", bool asc = true)
        {
            ViewBag.Asceding = asc;
            IQueryable<Reservation> reservations = _context.Reservations.Include(r => r.Hotel).Include(r => r.StayRooms);

            switch (shortby)
            {
                case "AA":
                    if (asc)
                    {
                        reservations = reservations.OrderBy(r => r.AA);
                    }
                    else
                    {
                        reservations = reservations.OrderByDescending(r => r.AA);
                    }
                    break;
                case "Reservation":
                    if (asc)
                    {
                        reservations = reservations.OrderBy(r => r.GuestOrGroup);
                    }
                    else
                    {
                        reservations = reservations.OrderByDescending(r => r.GuestOrGroup);
                    }
                    break;
                case "Rooms":
                    if (asc)
                    {
                        reservations = reservations.OrderBy(r => r.StayRooms.Count());
                    }
                    else
                    {
                        reservations = reservations.OrderByDescending(r => r.StayRooms.Count());
                    }
                    break;
                default:
                    if (asc)
                    {
                        reservations = reservations.OrderBy(r => r.StayRooms.OrderBy(r1 => r1.Arrival).FirstOrDefault().Arrival);
                    }
                    else
                    {
                        reservations = reservations.OrderByDescending(r => r.StayRooms.OrderByDescending(r1 => r1.Arrival).FirstOrDefault().Arrival);
                    }
                    break;
            }
            return View(await reservations.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.SingleOrDefaultAsync(m => m.id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["HotelID"] = new SelectList(_context.Hotels, "id", "Name");
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
                reservation.id = Guid.NewGuid();
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["HotelID"] = new SelectList(_context.Hotels, "id", "Name", reservation.HotelID);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.SingleOrDefaultAsync(m => m.id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["HotelID"] = new SelectList(_context.Hotels, "id", "Name", reservation.HotelID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,AA,AskPrePay,AskPrePayDate,AskPrePayRemarks,GuestOrGroup,HotelID,Remarks")] Reservation reservation)
        {
            if (id != reservation.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["HotelID"] = new SelectList(_context.Hotels, "id", "Name", reservation.HotelID);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult>
            Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.SingleOrDefaultAsync(m => m.id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            DeleteConfirmed(Guid id)
        {
            var reservation = await _context.Reservations.SingleOrDefaultAsync(m => m.id == id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ReservationExists(Guid id)
        {
            return _context.Reservations.Any(e => e.id == id);
        }
    }
}
