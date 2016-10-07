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
                IQueryable<Hotel> userhotels = _context.Hotels.Include(h => h.HotelGroup).Where(s=>s.HotelGroupID==activehotel.HotelGroupID);
                if (!User.IsInRole("Administrator"))
                {
                    userhotels = userhotels.Where(r => _userinfo.HotelIDs.Any(s=>s==r.id));
                }
                
                return View(await userhotels.ToListAsync());
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
        public async Task< IActionResult> Create()
        {
            var hotelid = _userinfo.HotelIDs.FirstOrDefault();
            Hotel firsthotel =await _context.Hotels.SingleOrDefaultAsync(m => m.id == hotelid);
            HotelGroup hotelgroup = await _context.HotelGroups.SingleOrDefaultAsync(s=>s.id== firsthotel.HotelGroupID);
            if (hotelgroup == null)
            {
                return NotFound();
            }
            Hotel newhotel = new Hotel { HotelGroupID= hotelgroup.id, HotelDate=DateTime.Today, ExpirationDate=DateTime.Today.AddMonths(6), Dealer="Netera Software", Name=hotelgroup.Name };
            return View(newhotel);
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Create([Bind("id,Dealer,ExpirationDate,HotelDate,HotelGroupID,IsPayingSupport,Name")] Hotel hotel)
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
        public async Task<IActionResult>
            Edit(Guid? id)
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
        public async Task<IActionResult>
            Edit(Guid id, [Bind("id,Dealer,ExpirationDate,HotelDate,HotelGroupID,IsPayingSupport,Name")] Hotel hotel)
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
            return RedirectToAction("Index");
        }

        private bool HotelExists(Guid id)
        {
            return _context.Hotels.Any(e => e.id == id);
        }
    }
}
