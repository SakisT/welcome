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
    public class HotelGroupsController : Controller
    {
        private readonly WelcomeContext _context;
        private readonly IStringLocalizer<HomeController> _localizer;

        private WelcomeContext db;
        public HotelGroupsController(IStringLocalizer<HomeController> localizer, WelcomeContext context)
        {
            _context = context;
            _localizer = localizer;
        }

        // GET: HotelGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.HotelGroups.ToListAsync());
        }

        // GET: HotelGroups/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelGroup = await _context.HotelGroups.SingleOrDefaultAsync(m => m.id == id);
            if (hotelGroup == null)
            {
                return NotFound();
            }

            return View(hotelGroup);
        }

        // GET: HotelGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HotelGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name")] HotelGroup hotelGroup)
        {
            if (ModelState.IsValid)
            {
                hotelGroup.id = Guid.NewGuid();
                _context.Add(hotelGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hotelGroup);
        }

        // GET: HotelGroups/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelGroup = await _context.HotelGroups.SingleOrDefaultAsync(m => m.id == id);
            if (hotelGroup == null)
            {
                return NotFound();
            }
            return View(hotelGroup);
        }

        // POST: HotelGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,Name")] HotelGroup hotelGroup)
        {
            if (id != hotelGroup.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotelGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelGroupExists(hotelGroup.id))
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
            return View(hotelGroup);
        }

        // GET: HotelGroups/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelGroup = await _context.HotelGroups.SingleOrDefaultAsync(m => m.id == id);
            if (hotelGroup == null)
            {
                return NotFound();
            }

            return View(hotelGroup);
        }

        // POST: HotelGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var hotelGroup = await _context.HotelGroups.SingleOrDefaultAsync(m => m.id == id);
            _context.HotelGroups.Remove(hotelGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool HotelGroupExists(Guid id)
        {
            return _context.HotelGroups.Any(e => e.id == id);
        }
    }
}