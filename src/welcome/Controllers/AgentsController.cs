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
    public class AgentsController : Controller
    {
        private readonly WelcomeContext _context;

        private readonly IStringLocalizer<AgentsController> _localizer;

        private UserAccessInfo _userinfo;

        public AgentsController(IStringLocalizer<AgentsController> localizer, WelcomeContext context, UserAccessInfo UserInfo)
        {
            _localizer = localizer;
            _context = context;
            _userinfo = UserInfo;
        }

        // GET: Agents
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
            var welcomeContext = _context.Agents.Include(a => a.Hotel).Where(r => r.HotelID == ID);
            ViewBag.HotelID = ID;
            return View(await welcomeContext.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> AgentsJsonByType(string type)
        {
            var AgentType = (Agent.AgentType)Enum.Parse(typeof(Agent.AgentType), type);
            Guid defaulthotelid = _userinfo.HotelIDs.FirstOrDefault();
                if(User.IsInRole("Admin")){ defaulthotelid = _context.Hotels.FirstOrDefault().HotelID; }
            var hotelid = _userinfo.GetActiveHotels().DefaultIfEmpty().FirstOrDefault();
            var hotelagents =await _context.Agents.Where(r => r.HotelID == hotelid && 
                        r.Type== AgentType).ToListAsync();
            //if type==Agent.AgentType.CreditCard

            var returnvalue =Json( from p in hotelagents select new { agentid=p.AgentID, name=p.Name});
            return returnvalue;
        }

        // GET: Agents/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents.SingleOrDefaultAsync(m => m.AgentID == id);
            if (agent == null)
            {
                return NotFound();
            }
            return View(agent);
        }

        // GET: Agents/Create
        public IActionResult Create(Guid id)
        {
            Agent agent = new Agent { HotelID = id, Type = Agent.AgentType.Agent };
            return View(agent);
        }

        // POST: Agents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChannelID,Code,DisplayOrder,HotelID,IsActive,Name,Type")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                agent.AgentID = Guid.NewGuid();
                _context.Add(agent);

                AgentVardata agentvardata = new AgentVardata { Agent = agent, CreateOn = DateTime.Now };

                _context.Add(agentvardata);

                InvoiceDetail invoicedetail = new InvoiceDetail { Hotel=agent.Hotel, InvoiceStructure_InvoiceName=agent.Name };
                _context.Add(invoicedetail);

                agentvardata.InvoiceDetail = invoicedetail;

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Edit", new { id = agent.AgentID });
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
            "Try again, and if the problem persists " +
            "see your system administrator.");
                }
            }
            return View(agent);
        }

        // GET: Agents/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = _context.Agents.Include(r => r.Vardata).AsNoTracking().Include(r=>r.Vardata.InvoiceDetail).AsNoTracking().SingleOrDefault(m => m.AgentID == id);
            if (agent == null)
            {
                return NotFound();
            }
            ViewBag.HotelID = agent.HotelID;
            return View(agent);
        }

        // POST: Agents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(Guid id)
        {

            var agenttoupdate = await _context.Agents.Include(r => r.Vardata).Include(r=>r.Vardata.InvoiceDetail).SingleOrDefaultAsync(m => m.AgentID == id);
            if (await TryUpdateModelAsync(agenttoupdate, "",
                s => s.Code,
                s => s.ChannelID,
                s => s.DisplayOrder,
                s => s.IsActive,
                s => s.Name,
                s => s.Type,
                s => s.Vardata))
            {
                try
                {
                    var agentvardata = await _context.AgentsVardatas.SingleOrDefaultAsync(r => r.AgentVardataID == id);
                    if (await TryUpdateModelAsync(agentvardata, "",
                        s => s.emailForInvoices,
                        s => s.AccountCode,
                        s => s.Color,
                        s => s.Commission,
                        s => s.Contact_email,
                        s => s.Contact_Fax,
                        s => s.Contact_Mobile,
                        s => s.Contact_Phone1,
                        s => s.Contact_Phone2,
                        s => s.Contact_WebSite,
                        s => s.emailForInvoices,
                        s => s.Person,
                        s => s.PreDefinedInvoiceRemarks,
                        s => s.Remarks))
                    {
                        var invoicedetail = await _context.InvoiceDetails.SingleOrDefaultAsync(m => m.InvoiceDetailID == agentvardata.InvoiceDetailID.GetValueOrDefault(Guid.Empty));
                        if (invoicedetail != null)
                        {
                            if(await TryUpdateModelAsync(invoicedetail, "", 
                                s => s.InvoiceStructure_InvoiceName,
                                s => s.InvoiceStructure_Address_AddressLine1,
                                s => s.InvoiceStructure_Address_AddressLine2,
                                s => s.InvoiceStructure_Address_City,
                                s => s.InvoiceStructure_Address_Country, 
                                s => s.InvoiceStructure_Address_PostCode,
                                s => s.InvoiceStructure_Job,
                                s => s.InvoiceStructure_TaxDepartment,
                                s => s.InvoiceStructure_VATNumber))
                            {
                                await _context.SaveChangesAsync();
                                return RedirectToAction("Index", new { id = agenttoupdate.HotelID });
                            }
                        }
                        try
                        {
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index", new { id = agenttoupdate.HotelID });
                        }
                        catch (DbUpdateException ex2)
                        {
                            ModelState.AddModelError("", "Unable to save changes. " +
                   "Try again, and if the problem persists " +
                   "see your system administrator.");
                        }
                    }
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
           "Try again, and if the problem persists " +
           "see your system administrator.");
                }
            }
            return View(agenttoupdate);
        }

        // GET: Agents/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents.SingleOrDefaultAsync(m => m.AgentID == id);
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
            var agent = await _context.Agents.SingleOrDefaultAsync(m => m.AgentID == id);
            _context.Agents.Remove(agent);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AgentExists(Guid id)
        {
            return _context.Agents.Any(e => e.AgentID == id);
        }
    }
}
