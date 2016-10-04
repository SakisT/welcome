using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using welcome.Data;
using welcome.Models;

namespace welcome.Controllers
{
    public class BranchesController : Controller
    {
        private readonly WelcomeContext _context;

        public BranchesController(WelcomeContext context)
        {
            _context = context;    
        }

        // GET: Branches
        public async Task<IActionResult> Index()
        {
            var welcomeContext = _context.Branches.Include(b => b.Hotel);
            return View(await welcomeContext.ToListAsync());
        }

        // GET: Branches/Details/5
        public async Task<IActionResult> Details(Guid? id)
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
        public IActionResult Create()
        {
            ViewData["HotelID"] = new SelectList(_context.Hotels, "id", "Name");
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,HotelID,Name")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                branch.id = Guid.NewGuid();
                _context.Add(branch);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
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

            var branch = await _context.Branches.SingleOrDefaultAsync(m => m.id == id);
            if (branch == null)
            {
                return NotFound();
            }
            ViewData["HotelID"] = new SelectList(_context.Hotels, "id", "Name", branch.HotelID);
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,HotelID,Name")] Branch branch)
        {
            if (id != branch.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(branch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchExists(branch.id))
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
            ViewData["HotelID"] = new SelectList(_context.Hotels, "id", "Name", branch.HotelID);
            return View(branch);
        }

        // GET: Branches/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var branch = await _context.Branches.SingleOrDefaultAsync(m => m.id == id);
            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BranchExists(Guid id)
        {
            return _context.Branches.Any(e => e.id == id);
        }
    }
}
