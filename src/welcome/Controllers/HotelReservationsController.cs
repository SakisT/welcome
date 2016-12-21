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
using welcome.ViewModels;

namespace welcome.Controllers
{
    public class HotelReservationsController : Controller
    {
        private readonly WelcomeContext _context;

        private readonly IStringLocalizer<HotelReservationsController> _localizer;

        private UserAccessInfo _userinfo;

        public HotelReservationsController(IStringLocalizer<HotelReservationsController> localizer, WelcomeContext context, UserAccessInfo UserInfo)
        {
            _localizer = localizer;
            _context = context;
            _userinfo = UserInfo;
        }

        // GET: HotelReservations
        public IActionResult Index()
        {
            var result = (from Reservation p in _context.Reservations
                          .Include(r => r.StayRooms).ThenInclude(stayroom => stayroom.Agent)
                          .Include(r => r.StayRooms).ThenInclude(stayroom => stayroom.Pricelist)
                          .Include(r => r.StayRooms).ThenInclude(stayroom => stayroom.StayPersons).ThenInclude(stayperson => stayperson.Customer)
                          .Include(r => r.Deposits).AsNoTracking().ToList()
                          let reservation = p
                          let agent = p.StayRooms.FirstOrDefault().Agent
                          let deposits = p.Deposits.ToArray()
                          let reservationrooms = p.StayRooms.ToArray()
                          let pricelist = p.StayRooms.FirstOrDefault().Pricelist
                          select new ViewModels.ReservationView { Reservation = reservation, Agent = agent, Deposits = deposits, ReservationRooms = reservationrooms, Pricelist = pricelist });
            return View(result);
        }

        // GET: HotelReservations/Details/5
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

        // GET: HotelReservations/Create
        public IActionResult Create()
        {
            ViewData["HotelID"] = new SelectList(_context.Hotels, "HotelID", "Name");
            return View();
        }

        // POST: HotelReservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationID,AA,AskPrePay,AskPrePayDate,AskPrePayRemarks,GuestOrGroup,HotelID,Remarks")] Reservation reservation)
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

        // GET: HotelReservations/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var reservation = _context.Reservations.Include(r => r.StayRooms).ThenInclude(stayroom => stayroom.Agent)
                         .Include(r => r.StayRooms).ThenInclude(stayroom => stayroom.Pricelist)
                         .Include(r => r.StayRooms).ThenInclude(stayroom => stayroom.StayPersons).ThenInclude(stayperson => stayperson.Customer)
                         .Include(r => r.Deposits).SingleOrDefault(r => r.ReservationID == id.Value);
            var reservationRooms = reservation.StayRooms.ToArray();
            var pricelist = reservationRooms.FirstOrDefault().Pricelist;
            var agent = reservationRooms.FirstOrDefault().Agent;
            var deposits = reservation.Deposits.ToArray();
            DateTime Arrival = reservationRooms.FirstOrDefault().Arrival;
            DateTime Departure = reservationRooms.FirstOrDefault().Departure;
            return PartialView(new ReservationView
            {
                Reservation = reservation,
                Agent = agent,
                Deposits = deposits,
                Pricelist = pricelist,
                ReservationRooms = reservationRooms,
                Arrival = Arrival,
                Departure = Departure
            });
        }

        // POST: HotelReservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReservationView reservationview)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservationview.Reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservationview.Reservation.ReservationID))
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
            ViewData["HotelID"] = new SelectList(_context.Hotels, "HotelID", "Name", reservationview.Reservation.HotelID);
            return View(reservationview);
        }

        // GET: HotelReservations/Delete/5
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

        // POST: HotelReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reservation = await _context.Reservations.SingleOrDefaultAsync(m => m.ReservationID == id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ReservationExists(Guid id)
        {
            return _context.Reservations.Any(e => e.ReservationID == id);
        }
    }
}
