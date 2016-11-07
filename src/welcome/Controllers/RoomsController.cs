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
    public class RoomsController : Controller
    {
        private readonly WelcomeContext _context;

        private readonly IStringLocalizer<RoomsController> _localizer;

        private UserAccessInfo _userinfo;

        public RoomsController(IStringLocalizer<RoomsController> localizer, WelcomeContext context, UserAccessInfo UserInfo)
        {
            _localizer = localizer;
            _context = context;
            _userinfo = UserInfo;
        }

        // GET: Rooms
        public async Task<IActionResult> Index(Guid id)
        {
            if (id == Guid.Empty)
            {
                id = _userinfo.GetActiveBranches().FirstOrDefault();
            }
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var welcomeContext = _context.Rooms.OrderBy(r=>r.DisplayOrder).Include(r => r.Branch).Include(r => r.RoomType).Where(r => r.BranchID == id);
            ViewBag.BranchID = id;
            return View(await welcomeContext.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult>
            Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.SingleOrDefaultAsync(m => m.RoomID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public async Task<IActionResult> Create(Guid id)
        {
            Branch branch = await _context.Branches.SingleOrDefaultAsync(m => m.BranchID == id);
            Hotel hotel = await _context.Hotels.SingleOrDefaultAsync(m => m.HotelID == branch.HotelID);
            var roomtypes = _context.RoomTypes.Where(r => r.HotelID == hotel.HotelID);
            ViewData["RoomTypeID"] = new SelectList(roomtypes, "RoomTypeID", "Abbreviation");
            var maxdisplayorder = _context.Rooms.Where(r => r.BranchID == id).Max(r => r.DisplayOrder);
            Room room = new Room { BranchID = id, DisplayOrder=maxdisplayorder+1 };
            return View(room);
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BranchID,Number,RoomTypeID,DisplayOrder")] Room room)
        {
            if (ModelState.IsValid)
            {
                room.RoomID = Guid.NewGuid();
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index",new { id=room.BranchID});
            }
            Branch branch = await _context.Branches.SingleOrDefaultAsync(m => m.BranchID == room.BranchID);
            Hotel hotel = await _context.Hotels.SingleOrDefaultAsync(m => m.HotelID == branch.HotelID);
            var roomtypes = _context.RoomTypes.OrderBy(r=>r.DisplayOrder).ThenBy(r=>r.Abbreviation).Where(r => r.HotelID == hotel.HotelID);
            ViewData["RoomTypeID"] = new SelectList(roomtypes, "RoomTypeID", "Abbreviation");
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.SingleOrDefaultAsync(m => m.RoomID == id);
            if (room == null)
            {
                return NotFound();
            }
            Branch branch = await _context.Branches.SingleOrDefaultAsync(m => m.BranchID == room.BranchID);
            Hotel hotel = await _context.Hotels.SingleOrDefaultAsync(m => m.HotelID == branch.HotelID);
            var roomtypes = _context.RoomTypes.OrderBy(r => r.DisplayOrder).ThenBy(r => r.Abbreviation).Where(r => r.HotelID == hotel.HotelID);
            ViewData["RoomTypeID"] = new SelectList(roomtypes, "RoomTypeID", "Abbreviation");
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(Guid id)
        {
            var roomtoupdate = await _context.Rooms.SingleOrDefaultAsync(r => r.RoomID == id);
            if (await TryUpdateModelAsync(roomtoupdate, "", s => s.Number, s => s.RoomTypeID, s=>s.DisplayOrder))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { id = roomtoupdate.BranchID });
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists " +
                            "see your system administrator.");
                }
            }
            ViewData["RoomTypeID"] = new SelectList(_context.RoomTypes, "RoomTypeID", "Abbreviation", roomtoupdate.RoomTypeID);
            return View(roomtoupdate);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult>
            Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.SingleOrDefaultAsync(m => m.RoomID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            DeleteConfirmed(Guid id)
        {
            var room = await _context.Rooms.SingleOrDefaultAsync(m => m.RoomID == id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RoomExists(Guid id)
        {
            return _context.Rooms.Any(e => e.RoomID == id);
        }
    }
}
