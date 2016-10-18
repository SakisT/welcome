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

                //----------------------------------------------------------------
                Department Room = new Department { id = Guid.NewGuid(), Hotel = hotel, Name = "Δωμάτια", IsActive = true, TaxPrcentage = 0.005M, VatPercentage = 0.13M, DisplayOrder = 1 };
                Department Breakfast = new Department { id = Guid.NewGuid(), Hotel = hotel, Name = "Πρωϊνό", IsActive = true, TaxPrcentage = 0.005M, VatPercentage = 0.24M, DisplayOrder = 2 };
                Department Lunch = new Department { id = Guid.NewGuid(), Hotel = hotel, Name = "Γεύμα", IsActive = true, TaxPrcentage = 0.005M, VatPercentage = 0.24M, DisplayOrder = 3 };
                Department Dinner = new Department { id = Guid.NewGuid(), Hotel = hotel, Name = "Δείπνο", IsActive = true, TaxPrcentage = 0.005M, VatPercentage = 0.24M, DisplayOrder = 4 };
                Department AllInclusive = new Department { id = Guid.NewGuid(), Hotel = hotel, Name = "All Inclusive", IsActive = true, TaxPrcentage = 0.005M, VatPercentage = 0.24M, DisplayOrder = 5 };

                _context.AddRange(new[] { Room, Breakfast, Lunch, Dinner, AllInclusive });

                Board RR = new Board { id = Guid.NewGuid(), Hotel = hotel, Abbrevation = "RR", Name = "Room Rate", DisplayOrder = 1 };
                Board BB = new Board { id = Guid.NewGuid(), Hotel = hotel, Abbrevation = "BB", Name = "Bed n' breakfast", DisplayOrder = 2 };
                Board HL = new Board { id = Guid.NewGuid(), Hotel = hotel, Abbrevation = "HL", Name = "Half Lunch", DisplayOrder = 3 };
                Board HB = new Board { id = Guid.NewGuid(), Hotel = hotel, Abbrevation = "HB", Name = "Half Board", DisplayOrder = 4 };
                Board FB = new Board { id = Guid.NewGuid(), Hotel = hotel, Abbrevation = "FB", Name = "Full Board", DisplayOrder = 5 };
                Board AI = new Board { id = Guid.NewGuid(), Hotel = hotel, Abbrevation = "AL", Name = "All Inclusive", DisplayOrder = 6 };

                _context.AddRange(new[] { RR, BB, HL, HB, FB, AI });

                BoardPart RR_Room = new BoardPart { id = Guid.NewGuid(), Board = RR, Department = Room, ParticipationRate = 1 };

                BoardPart BB_Room = new BoardPart { id = Guid.NewGuid(), Board = BB, Department = Room, ParticipationRate = 0.95d };
                BoardPart BB_Breakfast = new BoardPart { id = Guid.NewGuid(), Board = BB, Department = Breakfast, ParticipationRate = 0.05d };

                BoardPart HL_Room = new BoardPart { id = Guid.NewGuid(), Board = HL, Department = Room, ParticipationRate = 0.85d };
                BoardPart HL_Breakfast = new BoardPart { id = Guid.NewGuid(), Board = HL, Department = Breakfast, ParticipationRate = 0.05d };
                BoardPart HL_Lunch = new BoardPart { id = Guid.NewGuid(), Board = HL, Department = Lunch, ParticipationRate = 0.1d };

                BoardPart HB_Room = new BoardPart { id = Guid.NewGuid(), Board = HB, Department = Room, ParticipationRate = 0.85d };
                BoardPart HB_Breakfast = new BoardPart { id = Guid.NewGuid(), Board = HB, Department = Breakfast, ParticipationRate = 0.05d };
                BoardPart HB_Dinner = new BoardPart { id = Guid.NewGuid(), Board = HB, Department = Dinner, ParticipationRate = 0.1d };

                BoardPart FB_Room = new BoardPart { id = Guid.NewGuid(), Board = FB, Department = Room, ParticipationRate = 0.75d };
                BoardPart FB_Breakfast = new BoardPart { id = Guid.NewGuid(), Board = FB, Department = Breakfast, ParticipationRate = 0.05d };
                BoardPart FB_Lunch = new BoardPart { id = Guid.NewGuid(), Board = FB, Department = Lunch, ParticipationRate = 0.1d };
                BoardPart FB_Dinner = new BoardPart { id = Guid.NewGuid(), Board = FB, Department = Dinner, ParticipationRate = 0.1d };

                BoardPart AI_Room = new BoardPart { id = Guid.NewGuid(), Board = AI, Department = Room, ParticipationRate = 0.7d };
                BoardPart AI_Breakfast = new BoardPart { id = Guid.NewGuid(), Board = AI, Department = Breakfast, ParticipationRate = 0.05d };
                BoardPart AI_Lunch = new BoardPart { id = Guid.NewGuid(), Board = AI, Department = Lunch, ParticipationRate = 0.1d };
                BoardPart AI_Dinner = new BoardPart { id = Guid.NewGuid(), Board = AI, Department = Dinner, ParticipationRate = 0.1d };
                BoardPart AI_AI = new BoardPart { id = Guid.NewGuid(), Board = AI, Department = AllInclusive, ParticipationRate = 0.05d };

                _context.AddRange(new[] { RR_Room,
                    BB_Room , BB_Breakfast ,
                    HL_Room , HL_Breakfast ,HL_Lunch,
                    HB_Room , HB_Breakfast , HB_Dinner ,
                    FB_Room , FB_Breakfast , FB_Lunch , FB_Dinner,
                    AI_Room, AI_Breakfast,AI_Lunch,AI_Dinner ,AI_AI});
                //----------------------------------------------------------------

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
