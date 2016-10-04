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
    public class AgentsController : Controller
    {
        private readonly WelcomeContext _context;

        public AgentsController(WelcomeContext context)
        {
            _context = context;    
        }

        // GET: Agents
        public async Task<IActionResult> Index()
        {
            var welcomeContext = _context.Agents.Include(a => a.Hotel);
            return View(await welcomeContext.ToListAsync());
        }

        // GET: Agents/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents.SingleOrDefaultAsync(m => m.id == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // GET: Agents/Create
        public IActionResult Create()
        {
            ViewData["HotelID"] = new SelectList(_context.Hotels, "id", "Name");
            return View();
        }

        // POST: Agents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ChannelID,Code,DisplayOrder,HotelID,IsActive,Name,Type")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                agent.id = Guid.NewGuid();
                _context.Add(agent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["HotelID"] = new SelectList(_context.Hotels, "id", "Name", agent.HotelID);
            return View(agent);
        }

        // GET: Agents/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents.SingleOrDefaultAsync(m => m.id == id);
            if (agent == null)
            {
                return NotFound();
            }
            ViewData["HotelID"] = new SelectList(_context.Hotels, "id", "Name", agent.HotelID);
            return View(agent);
        }

        // POST: Agents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,ChannelID,Code,DisplayOrder,HotelID,IsActive,Name,Type")] Agent agent)
        {
            if (id != agent.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentExists(agent.id))
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
            ViewData["HotelID"] = new SelectList(_context.Hotels, "id", "Name", agent.HotelID);
            return View(agent);
        }

        // GET: Agents/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents.SingleOrDefaultAsync(m => m.id == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var agent = await _context.Agents.SingleOrDefaultAsync(m => m.id == id);
            _context.Agents.Remove(agent);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AgentExists(Guid id)
        {
            return _context.Agents.Any(e => e.id == id);
        }
    }
}
