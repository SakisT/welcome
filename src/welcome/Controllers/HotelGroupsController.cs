using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using welcome.Services;
using welcome.Data;
using welcome.Models;

namespace welcome.Controllers
{
    public class HotelGroupsController : Controller
    {
        private readonly WelcomeContext _context;

        private readonly IStringLocalizer<HotelGroupsController> _localizer;

        private UserAccessInfo _userinfo;

        public HotelGroupsController(IStringLocalizer<HotelGroupsController> localizer, WelcomeContext context, UserAccessInfo UserInfo)
        {
            _localizer = localizer;
            _context = context;
            _userinfo = UserInfo;
        }

        // GET: HotelGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.HotelGroups.ToListAsync());
        }

        // GET: HotelGroups/Details/5
        public async Task<IActionResult>
            Details(Guid? id)
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
        public async Task<IActionResult> Create([Bind("Name")] HotelGroup hotelGroup)
        {
            if (ModelState.IsValid)
            {
                hotelGroup.id = Guid.NewGuid();
                _context.Add(hotelGroup);

                Hotel hotel = new Hotel
                {
                    HotelGroup = hotelGroup,
                    id = Guid.NewGuid(),
                    ExpirationDate = DateTime.Today.AddMonths(6),
                    Name = hotelGroup.Name,
                    HotelDate = DateTime.Today
                };

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
                return RedirectToAction("Index");
            }
            return View(hotelGroup);
        }

        // GET: HotelGroups/Edit/5
        public async Task<IActionResult>
            Edit(Guid? id)
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
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var hotelgrouptoupdate = await _context.HotelGroups.SingleOrDefaultAsync(s => s.id == id);
            if (await TryUpdateModelAsync(hotelgrouptoupdate, "", s => s.Name))
            {
                try
                {
                    _context.Update(hotelgrouptoupdate);
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
            return View(hotelgrouptoupdate);
        }

        // GET: HotelGroups/Delete/5
        public async Task<IActionResult>
            Delete(Guid? id)
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
        public async Task<IActionResult>
            DeleteConfirmed(Guid id)
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
