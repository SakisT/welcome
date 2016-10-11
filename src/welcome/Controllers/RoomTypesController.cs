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
    public class RoomTypesController : Controller
    {
        private readonly WelcomeContext _context;

        private readonly IStringLocalizer<RoomTypesController> _localizer;

        private UserAccessInfo _userinfo;

        public RoomTypesController(IStringLocalizer<RoomTypesController> localizer, WelcomeContext context, UserAccessInfo UserInfo)
        {
            _localizer = localizer;
            _context = context;
            _userinfo = UserInfo;
        }

        // GET: RoomTypes
        public async Task<IActionResult> Index(Guid id)
        {
            var welcomeContext = _context.RoomTypes.Include(r => r.Hotel).Where(s => s.HotelID == id);
            ViewBag.HotelID = id;
            return View(await welcomeContext.ToListAsync());
        }

        // GET: RoomTypes/Details/5
        public async Task<IActionResult>
            Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomType = await _context.RoomTypes.SingleOrDefaultAsync(m => m.id == id);
            if (roomType == null)
            {
                return NotFound();
            }

            return View(roomType);
        }

        // GET: RoomTypes/Create
        public IActionResult Create(Guid id)
        {
            RoomType roomtype = new RoomType { HotelID = id };
            return View(roomtype);
        }

        // POST: RoomTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Create([Bind("Abbreviation,Color,DisplayOrder,Grade,HotelID,IncludeInOnlineAvailabilities,IsActive,Name,SuggestedPax")] RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                roomType.id = Guid.NewGuid();
                _context.Add(roomType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index",new { id=roomType.HotelID});
            }
            return View(roomType);
        }

        // GET: RoomTypes/Edit/5
        public async Task<IActionResult>
            Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomType = await _context.RoomTypes.SingleOrDefaultAsync(m => m.id == id);
            if (roomType == null)
            {
                return NotFound();
            }
            ViewData["HotelID"] = new SelectList(_context.Hotels, "id", "Name", roomType.HotelID);
            return View(roomType);
        }

        // POST: RoomTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            EditPost(Guid id)
        {
            var roomtype =await _context.RoomTypes.SingleOrDefaultAsync(m => m.id == id);
            if( await TryUpdateModelAsync(roomtype,"",
                s=>s.Abbreviation,
                s=>s.Color, 
                s=>s.DisplayOrder, 
                s=>s.Grade,
                s=>s.IncludeInOnlineAvailabilities,
                s=>s.IsActive,
                s=>s.Name,
                s => s.SuggestedPax))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index",new { id=roomtype.HotelID});
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "");
                }
            }
            return View(roomtype);
        }

        // GET: RoomTypes/Delete/5
        public async Task<IActionResult>
            Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomType = await _context.RoomTypes.SingleOrDefaultAsync(m => m.id == id);
            if (roomType == null)
            {
                return NotFound();
            }

            return View(roomType);
        }

        // POST: RoomTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            DeleteConfirmed(Guid id)
        {
            var roomType = await _context.RoomTypes.SingleOrDefaultAsync(m => m.id == id);
            _context.RoomTypes.Remove(roomType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RoomTypeExists(Guid id)
        {
            return _context.RoomTypes.Any(e => e.id == id);
        }
    }
}
