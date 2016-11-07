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
                Hotel activehotel = _context.Hotels.SingleOrDefault(s => s.HotelID == _userinfo.GetActiveHotels().FirstOrDefault());
                IQueryable<Hotel> userhotels = _context.Hotels.Include(h => h.HotelGroup).Where(s => s.HotelGroupID == activehotel.HotelGroupID);
                if (!User.IsInRole("Administrator"))
                {
                    userhotels = userhotels.Where(r => _userinfo.HotelIDs.Any(s => s == r.HotelID));
                }
                else
                {
                        _userinfo.HotelIDs=new Guid[] { id.Value }.ToList();
                        _userinfo.SetActiveHotels(new Guid[] { id.Value});
                }
                return View(await userhotels.ToListAsync());
            }
            else
            {
                ViewBag.ActiveHotelGroupID = id;
                if (User.IsInRole("Administrator"))
                {
                    _userinfo.HotelIDs = new Guid[] { id.Value }.ToList();
                    _userinfo.SetActiveHotels(new Guid[] { id.Value });
                }
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

            var hotel = await _context.Hotels.SingleOrDefaultAsync(m => m.HotelID == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // GET: Hotels/Create
        public async Task<IActionResult> Create(Guid id)
        {
            HotelGroup hotelgroup = await _context.HotelGroups.SingleOrDefaultAsync(s => s.HotelGroupID == id);
            if (hotelgroup == null)
            {
                return NotFound();
            }
            Hotel newhotel = new Hotel { HotelGroupID = id, HotelDate = DateTime.Today, ExpirationDate = DateTime.Today.AddMonths(6), Dealer = "Netera Software", Name = hotelgroup.Name };
            return View(newhotel);
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Create([Bind("Dealer,ExpirationDate,HotelDate,HotelGroupID,IsPayingSupport,Name")] Hotel hotel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    hotel.HotelID = Guid.NewGuid();
                    _context.Add(hotel);

                    Department Room = new Department { DepartmentID = Guid.NewGuid(), Hotel = hotel, Name = "Δωμάτια", IsActive = true, TaxPrcentage = 0.005M, VatPercentage = 0.13M ,DisplayOrder=1};
                    Department Breakfast = new Department { DepartmentID = Guid.NewGuid(), Hotel = hotel, Name = "Πρωϊνό", IsActive = true, TaxPrcentage = 0.005M, VatPercentage = 0.24M, DisplayOrder =2 };
                    Department Lunch = new Department { DepartmentID = Guid.NewGuid(), Hotel = hotel, Name = "Γεύμα", IsActive = true, TaxPrcentage = 0.005M, VatPercentage = 0.24M, DisplayOrder =3 };
                    Department Dinner = new Department { DepartmentID = Guid.NewGuid(), Hotel = hotel, Name = "Δείπνο", IsActive = true, TaxPrcentage = 0.005M, VatPercentage = 0.24M, DisplayOrder =4 };
                    Department AllInclusive = new Department { DepartmentID = Guid.NewGuid(), Hotel = hotel, Name = "All Inclusive", IsActive = true, TaxPrcentage = 0.005M, VatPercentage = 0.24M, DisplayOrder =5 };

                    _context.AddRange(new[] { Room, Breakfast, Lunch, Dinner, AllInclusive });

                    Board RR = new Board { BoardID = Guid.NewGuid(), Hotel=hotel, Abbrevation = "RR", Name="Room Rate", DisplayOrder=1 };
                    Board BB = new Board { BoardID = Guid.NewGuid(), Hotel = hotel, Abbrevation = "BB", Name="Bed n' breakfast", DisplayOrder =2};
                    Board HL = new Board { BoardID = Guid.NewGuid(), Hotel = hotel, Abbrevation = "HL" , Name="Half Lunch", DisplayOrder =3};
                    Board HB = new Board { BoardID = Guid.NewGuid(), Hotel = hotel, Abbrevation = "HB" , Name="Half Board", DisplayOrder =4};
                    Board FB = new Board { BoardID = Guid.NewGuid(), Hotel = hotel, Abbrevation = "FB", Name="Full Board", DisplayOrder =5};
                    Board AI = new Board { BoardID = Guid.NewGuid(), Hotel = hotel, Abbrevation = "AL" , Name="All Inclusive", DisplayOrder =6};

                    _context.AddRange(new[] { RR, BB, HL, HB, FB, AI });

                    BoardPart RR_Room = new BoardPart { BoardPartID = Guid.NewGuid(), Board = RR, Department = Room, ParticipationRate = 1 };

                    BoardPart BB_Room = new BoardPart { BoardPartID = Guid.NewGuid(), Board = BB, Department = Room, ParticipationRate = 0.95d };
                    BoardPart BB_Breakfast = new BoardPart { BoardPartID = Guid.NewGuid(), Board = BB, Department = Breakfast, ParticipationRate = 0.05d };

                    BoardPart HL_Room = new BoardPart { BoardPartID = Guid.NewGuid(), Board = HL, Department = Room, ParticipationRate = 0.85d };
                    BoardPart HL_Breakfast = new BoardPart { BoardPartID = Guid.NewGuid(), Board = HL, Department = Breakfast, ParticipationRate = 0.05d };
                    BoardPart HL_Lunch = new BoardPart { BoardPartID = Guid.NewGuid(), Board = HL, Department = Lunch, ParticipationRate = 0.1d };

                    BoardPart HB_Room = new BoardPart { BoardPartID = Guid.NewGuid(), Board = HB, Department = Room, ParticipationRate = 0.85d };
                    BoardPart HB_Breakfast = new BoardPart { BoardPartID = Guid.NewGuid(), Board = HB, Department = Breakfast, ParticipationRate = 0.05d };
                    BoardPart HB_Dinner = new BoardPart { BoardPartID = Guid.NewGuid(), Board = HB, Department = Dinner, ParticipationRate = 0.1d };

                    BoardPart FB_Room = new BoardPart { BoardPartID = Guid.NewGuid(), Board = FB, Department = Room, ParticipationRate = 0.75d };
                    BoardPart FB_Breakfast = new BoardPart { BoardPartID = Guid.NewGuid(), Board = FB, Department = Breakfast, ParticipationRate = 0.05d };
                    BoardPart FB_Lunch = new BoardPart { BoardPartID = Guid.NewGuid(), Board = FB, Department = Lunch, ParticipationRate = 0.1d };
                    BoardPart FB_Dinner = new BoardPart { BoardPartID = Guid.NewGuid(), Board = FB, Department = Dinner, ParticipationRate = 0.1d };

                    BoardPart AI_Room = new BoardPart { BoardPartID = Guid.NewGuid(), Board = AI, Department = Room, ParticipationRate = 0.7d };
                    BoardPart AI_Breakfast = new BoardPart { BoardPartID = Guid.NewGuid(), Board = AI, Department = Breakfast, ParticipationRate = 0.05d };
                    BoardPart AI_Lunch = new BoardPart { BoardPartID = Guid.NewGuid(), Board = AI, Department = Lunch, ParticipationRate = 0.1d };
                    BoardPart AI_Dinner = new BoardPart { BoardPartID = Guid.NewGuid(), Board = AI, Department = Dinner, ParticipationRate = 0.1d };
                    BoardPart AI_AI = new BoardPart { BoardPartID = Guid.NewGuid(), Board = AI, Department = AllInclusive, ParticipationRate = 0.05d };

                    _context.AddRange(new[] { RR_Room,
                    BB_Room , BB_Breakfast ,
                    HL_Room , HL_Breakfast ,HL_Lunch,
                    HB_Room , HB_Breakfast , HB_Dinner ,
                    FB_Room , FB_Breakfast , FB_Lunch , FB_Dinner,
                    AI_Room, AI_Breakfast,AI_Lunch,AI_Dinner ,AI_AI});

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
                    return RedirectToAction("Edit", new { id = hotel.HotelID });
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
            "Try again, and if the problem persists " +
            "see your system administrator.");
            }
            ViewData["HotelGroupID"] = new SelectList(_context.HotelGroups, "HotelGroupID", "Name", hotel.HotelGroupID);
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

            var hotel = await _context.Hotels.Include(r => r.VardataInvoice).AsNoTracking().Include(r => r.VardataPlan).AsNoTracking().SingleOrDefaultAsync(m => m.HotelID == id);
            if (hotel == null)
            {
                return NotFound();
            }
            ViewData["HotelGroupID"] = new SelectList(_context.HotelGroups, "HotelGroupID", "Name", hotel.HotelGroupID);
            return View(hotel);
        }

        // POST: Hotels/Edit/5
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
            var modelstate = ModelState;
            var hoteltoupdate = await _context.Hotels.Include(r => r.VardataInvoice).Include(r => r.VardataPlan).SingleOrDefaultAsync(m => m.HotelID == id);
            if (await TryUpdateModelAsync(hoteltoupdate, "",
                s => s.Name,
                s => s.HotelDate,
                s => s.Dealer,
                s => s.ExpirationDate,
                s => s.VardataInvoice,
                s => s.VardataPlan))
            {
                var hotelvardata = await _context.HotelVardataInvoices.SingleOrDefaultAsync(r => r.HotelVardataInvoiceID == id);
                if (await TryUpdateModelAsync(hotelvardata, "",
                    s => s.ArrangementDescription,
                    s => s.DefaultPaymentMethod,
                    s => s.DefaultView,
                    s => s.IncludePaxInInvoice,
                    s => s.InvoiceCopies,
                    s => s.InvoiceEmailBody,
                    s => s.ObligatoryAddressAtInvoices,
                    s => s.ResetInvoiceNumbers,
                    s => s.SendInvoiceToHotelMailAsCC))
                {
                    var hotelvardataplan = await _context.HotelVardataPlans.SingleOrDefaultAsync(r => r.HotelVardataPlanID == id);
                    if (await TryUpdateModelAsync(hotelvardataplan, "",
                        s => s.DisplayAgentCode,
                        s => s.HistoryColor,
                        s => s.InHouseColor,
                        s => s.LabelAddins,
                        s => s.OptionReservationColor,
                        s => s.PendingDepositColor,
                        s => s.ReservationColor))
                    {
                        try
                        {
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index", new { id = hoteltoupdate.HotelGroupID });
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
            ViewData["HotelGroupID"] = new SelectList(_context.HotelGroups, "HotelGroupID", "Name", hoteltoupdate.HotelGroupID);
            return View(hoteltoupdate);
        }

        // GET: Hotels/Delete/5
        public async Task<IActionResult>
            Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels.SingleOrDefaultAsync(m => m.HotelID == id);
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
            var hotel = await _context.Hotels.SingleOrDefaultAsync(m => m.HotelID == id);
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = hotel.HotelGroupID });
        }

        private bool HotelExists(Guid id)
        {
            return _context.Hotels.Any(e => e.HotelID == id);
        }
    }
}
