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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace welcome.Controllers
{
    [Authorize(Policy = "HotelUser")]
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

       
          //var claim=User.Claims.SingleOrDefault(s => s.Type == "BranchID") ;

            HttpContext.Session.SetString("Message", "My test Message");

            ViewBag.Message = HttpContext.Session.GetString("Message");
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
        public async Task<IActionResult> Create([Bind("Dealer,ExpirationDate,HotelDate,HotelGroupID,IsPayingSupport,Name")] Hotel hotel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    hotel.id = Guid.NewGuid();
                    _context.Add(hotel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
            "Try again, and if the problem persists " +
            "see your system administrator.");
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
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHotel(Guid id)
        {
            if (id ==null|id==Guid.Empty)
            {
                return NotFound();
            }
            Hotel hoteltoupdate = await _context.Hotels.SingleOrDefaultAsync(r => r.id == id);
            if(await TryUpdateModelAsync(hoteltoupdate,"",
                s=>s.Dealer, s=>s.ExpirationDate, s=>s.HotelDate, s=>s.HotelGroupID, s=>s.IsPayingSupport, s=>s.Name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            ViewData["HotelGroupID"] = new SelectList(_context.HotelGroups, "id", "Name", hoteltoupdate.HotelGroupID);
            return View(hoteltoupdate);
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

        //private bool HotelExists(Guid id)
        //{
        //    return _context.Hotels.Any(e => e.id == id);
        //}
    }
}
