using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using welcome.Services;
using welcome.Data;
using welcome.Models;
using Microsoft.AspNetCore.Http;

namespace welcome.Controllers
{
    public class BranchesController : Controller
    {
        private readonly WelcomeContext _context;

        private readonly IStringLocalizer<BranchesController> _localizer;

        private UserAccessInfo _userinfo;

        public BranchesController(IStringLocalizer<BranchesController> localizer, WelcomeContext context, UserAccessInfo UserInfo)
        {
            _localizer = localizer;
            _context = context;
            _userinfo = UserInfo;
        }

        // GET: Branches
        public async Task<IActionResult> Index(Guid id)
        {
            IQueryable<Branch> branches = _context.Branches.Include(b => b.Hotel);
            if (id != Guid.Empty)
            {
                branches = branches.Where(r => r.HotelID == id);
                HttpContext.Session.SetString("ActiveHotels", id.ToString());

                ViewBag.ActiveHotelID = id;
            }
            if (!User.IsInRole("Administrator"))
            {
                branches = branches.Where(r => _userinfo.BranchIDs.Any(s => s == r.id));
            }
            else
            {
        
                _userinfo.BranchIDs= new Guid[] { id }.ToList();
                _userinfo.SetActiveBranches(new Guid[] { id });
             
            }
            return View(await branches.ToListAsync());
        }

        // GET: Branches/Details/5
        public async Task<IActionResult>
            Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches.SingleOrDefaultAsync(m => m.id == id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // GET: Branches/Create
        public IActionResult Create(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            Hotel hotel = _context.Hotels.SingleOrDefault(r => r.id == id);
            Branch newbranch = new Branch { HotelID = id, Name = hotel.Name };
            return View(newbranch);
        }

        // POST: Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Create([Bind("HotelID,Name")] Branch branch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    branch.id = Guid.NewGuid();
                    _context.Add(branch);

                    BranchVardata branchvardata = new BranchVardata { Branch = branch };
                    _context.Add(branchvardata);

                    Nationality usualnationality = await _context.Nationalities.FirstOrDefaultAsync();
                    BranchVardataReservation branchvardatareservation = new BranchVardataReservation { Branch = branch, UsualNationality = usualnationality };
                    _context.Add(branchvardatareservation);

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Edit", new { id = branch.id });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
            "Try again, and if the problem persists " +
            "see your system administrator.");
            }
            ViewData["HotelID"] = new SelectList(_context.Hotels, "id", "Name", branch.HotelID);
            return View(branch);
        }

        // GET: Branches/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches.Include(r => r.Vardata).AsNoTracking().Include(r => r.VardataReservations).AsNoTracking().SingleOrDefaultAsync(m => m.id == id);
            if (branch == null)
            {
                return NotFound();
            }
            Hotel hotel = _context.Hotels.Include(r => r.HotelGroup).SingleOrDefault(r => r.id == branch.HotelID);
            HotelGroup hotelgroup = await _context.HotelGroups.Include(r => r.Hotels).SingleOrDefaultAsync(s => s.id == hotel.HotelGroupID);

            ViewData["HotelID"] = new SelectList(hotelgroup.Hotels, "id", "Name", branch.HotelID);
            ViewData["UsualNationalityID"] = new SelectList(_context.Nationalities.OrderBy(m=>m.GreekName).AsEnumerable(), "id", "GreekName", branch.HotelID);
            return View(branch);
        }

        // POST: Branches/Edit/5
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
            var branchtoupdate = await _context.Branches.Include(r => r.Vardata).Include(r => r.VardataReservations).SingleOrDefaultAsync(s => s.id == id);
            if (await TryUpdateModelAsync(branchtoupdate, "",
                s => s.Name, s => s.HotelID, s => s.Vardata, s => s.VardataReservations))
            {
                var branchvardata = await _context.BranchVardatas.SingleOrDefaultAsync(m => m.BranchVardataId == id);
                if (await TryUpdateModelAsync(branchvardata))
                {
                    var branchvardatareservation = await _context.BranchVardataReservations.SingleOrDefaultAsync(m => m.BranchVardataReservationId == id);
                    if (await TryUpdateModelAsync(branchvardatareservation))
                    {
                        try
                        {
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index", new { id = branchtoupdate.HotelID });
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
            return View(branchtoupdate);
        }

        // GET: Branches/Delete/5
        public async Task<IActionResult>
            Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches.SingleOrDefaultAsync(m => m.id == id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            DeleteConfirmed(Guid id)
        {
            var branch = await _context.Branches.SingleOrDefaultAsync(m => m.id == id);
            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = branch.HotelID });
        }

        private bool BranchExists(Guid id)
        {
            return _context.Branches.Any(e => e.id == id);
        }
    }
}
