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
    public class DepartmentsController : Controller
    {
        private readonly WelcomeContext _context;

        private readonly IStringLocalizer<DepartmentsController> _localizer;

        private UserAccessInfo _userinfo;

        public DepartmentsController(IStringLocalizer<DepartmentsController> localizer, WelcomeContext context, UserAccessInfo UserInfo)
        {
            _localizer = localizer;
            _context = context;
            _userinfo = UserInfo;
        }

        // GET: Departments
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
            var welcomeContext = _context.Departments.Include(d => d.Hotel).Where(r=>r.HotelID==ID);
            ViewBag.HotelID = ID;
            return View(await welcomeContext.ToListAsync());
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.SingleOrDefaultAsync(m => m.id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public async Task< IActionResult> Create(Guid id)
        {
            Hotel hotel = await _context.Hotels.SingleOrDefaultAsync(r => r.id == id);
            var lastdepartment =_context.Departments.OrderByDescending(r => r.DisplayOrder).Where(r=>r.HotelID==id).FirstOrDefault();
            Department department = new Department { HotelID=id, DisplayOrder=lastdepartment?.DisplayOrder??1 };
            return View(department);
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisplayOrder,HotelID,IsActive,Name,TaxPrcentage,VatPercentage")] Department department)
        {
            if (ModelState.IsValid)
            {
                department.id = Guid.NewGuid();
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index",new { id=department.HotelID});
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.SingleOrDefaultAsync(m => m.id == id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(Guid id)
        {

            var departmenttoupdate = await _context.Departments.SingleOrDefaultAsync(m => m.id == id);
            if (departmenttoupdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync(departmenttoupdate, "", s => s.Name, s => s.DisplayOrder, s => s.IsActive, s => s.TaxPrcentage, s => s.VatPercentage))
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = departmenttoupdate.HotelID });
            }
            return View(departmenttoupdate);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.SingleOrDefaultAsync(m => m.id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var department = await _context.Departments.SingleOrDefaultAsync(m => m.id == id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DepartmentExists(Guid id)
        {
            return _context.Departments.Any(e => e.id == id);
        }
    }
}
