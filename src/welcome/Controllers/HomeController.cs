using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using welcome.Data;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using welcome.Services;

namespace welcome.Controllers
{
    //[RequireHttps]

    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;

        private WelcomeContext _context;

        private UserAccessInfo _userinfo;
        public HomeController(IStringLocalizer<HomeController> localizer, WelcomeContext context, UserAccessInfo UserInfo)
        {
            _context = context;
            _userinfo = UserInfo;
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            //User.Claims.Append(new System.Security.Claims.Claim("BranchID", "3db65ecf-b2f4-4329-9f8d-79286eb93b02"));
            //ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            //Console.WriteLine();
            //HotelGroup hotelgroup = new HotelGroup { id = Guid.NewGuid(), Name = "Test Hotel Group" };
            //db.HotelGroups.Add(hotelgroup);

            //Hotel hotel = new Hotel { HotelGroup = hotelgroup, Name = "Test Hotel Name", HotelDate = DateTime.Today, ExpirationDate = DateTime.Today.AddYears(1), id = Guid.NewGuid(), IsPayingSupport = true };
            //db.Hotels.Add(hotel);

            //Branch branch = new Branch { Hotel = hotel, id = Guid.NewGuid(), Name = "Test Branch Name" };
            //db.Branches.Add(branch);

            //BranchVardata branchvardata = new BranchVardata { Branch = branch };
            //db.BranchVardatas.Add(branchvardata);

            //db.SaveChanges();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
