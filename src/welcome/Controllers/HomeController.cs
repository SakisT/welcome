﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using welcome.Data;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace welcome.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;

        private WelcomeContext db;

        public HomeController(IStringLocalizer<HomeController> localizer, WelcomeContext context)
        {
            db = context;
            _localizer = localizer;
        }
        public IActionResult Index()
        {

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