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
    public class PricelistsController : Controller
    {
        private readonly WelcomeContext _context;

        private readonly IStringLocalizer<PricelistsController> _localizer;

        private UserAccessInfo _userinfo;

        public PricelistsController(IStringLocalizer<PricelistsController> localizer, WelcomeContext context, UserAccessInfo UserInfo)
        {
            _localizer = localizer;
            _context = context;
            _userinfo = UserInfo;
        }

        // GET: Pricelists
        public async Task<IActionResult> Index(Guid id)
        {
            Guid ID = id;
            if (ID == Guid.Empty)
            {
                var temp = _userinfo.GetActiveHotels().FirstOrDefault();
                ID = Guid.Parse(temp.ToString());
            }
            if (ID == Guid.Empty)
            {
                return NotFound();
            }
            var welcomeContext = _context.Pricelists.Include(p => p.Hotel).Where(r => r.HotelID == ID);
            ViewBag.HotelID = ID;
            return View(await welcomeContext.ToListAsync());
        }

        // GET: Pricelists/Details/5
        public async Task<IActionResult>
            Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pricelist = await _context.Pricelists.SingleOrDefaultAsync(m => m.id == id);
            if (pricelist == null)
            {
                return NotFound();
            }

            return View(pricelist);
        }

        // GET: Pricelists/Create
        public IActionResult Create(Guid id)
        {
            Pricelist pricelist = new Pricelist { HotelID = id };
            return View(pricelist);
        }

        // POST: Pricelists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,HotelID,Name")] Pricelist pricelist)
        {
            if (ModelState.IsValid)
            {
                pricelist.id = Guid.NewGuid();
                _context.Add(pricelist);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index",new { id=pricelist.HotelID});
            }
            return View(pricelist);
        }

        // GET: Pricelists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pricelist = await _context.Pricelists.SingleOrDefaultAsync(m => m.id == id);
            if (pricelist == null)
            {
                return NotFound();
            }
            return View(pricelist);
        }

        // POST: Pricelists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(Guid id)
        {

            var pricelisttoupdate = await _context.Pricelists.SingleOrDefaultAsync(m => m.id == id);
            if(await TryUpdateModelAsync(pricelisttoupdate,"", s=>s.Code, s => s.Name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { id = pricelisttoupdate.HotelID });
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", ex.InnerException?.Message);
                }
            }
            return View(pricelisttoupdate);
        }

        // GET: Pricelists/Delete/5
        public async Task<IActionResult>
            Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pricelist = await _context.Pricelists.SingleOrDefaultAsync(m => m.id == id);
            if (pricelist == null)
            {
                return NotFound();
            }

            return View(pricelist);
        }

        // POST: Pricelists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            DeleteConfirmed(Guid id)
        {
            var pricelist = await _context.Pricelists.SingleOrDefaultAsync(m => m.id == id);
            _context.Pricelists.Remove(pricelist);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id=pricelist.HotelID});
        }

        private bool PricelistExists(Guid id)
        {
            return _context.Pricelists.Any(e => e.id == id);
        }
    }
}
