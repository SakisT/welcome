using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using welcome.Data;
using welcome.Models;
using Microsoft.Extensions.Localization;

namespace welcome.Controllers
{
    public class HotelsController : Controller
    {
        private readonly WelcomeContext _context;
        private readonly IStringLocalizer<HomeController> _localizer;

        private WelcomeContext db;
        public HotelsController(IStringLocalizer<HomeController> localizer, WelcomeContext context)
        {
            _context = context;
            _localizer = localizer;
        }

        // GET: Hotels
        public async Task<IActionResult> Index()
        {
            var welcomeContext = _context.Hotels.Include(h => h.HotelGroup);
            return View(await welcomeContext.ToListAsync());
        }

        // GET: Hotels/Details/5
        public async Task<IActionResult> Details(Guid? id)
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
        public IActionResult Create()
        {
            ViewData["HotelGroupID"] = new SelectList(_context.HotelGroups, "id", "Name");
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Dealer,ExpirationDate,HotelDate,HotelGroupID,IsPayingSupport,Name")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                hotel.id = Guid.NewGuid();
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["HotelGroupID"] = new SelectList(_context.HotelGroups, "id", "Name", hotel.HotelGroupID);
            return View(hotel);
        }

        // GET: Hotels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
            ViewData["HotelGroupID"] = new SelectList(_context.HotelGroups, "id", "Name", hotel.HotelGroupID);
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,Dealer,ExpirationDate,HotelDate,HotelGroupID,IsPayingSupport,Name")] Hotel hotel)
        {
            if (id != hotel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.id))
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
            ViewData["HotelGroupID"] = new SelectList(_context.HotelGroups, "id", "Name", hotel.HotelGroupID);
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var hotel = await _context.Hotels.SingleOrDefaultAsync(m => m.id == id);
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool HotelExists(Guid id)
        {
            return _context.Hotels.Any(e => e.id == id);
        }
    }
}
