﻿using Microsoft.AspNetCore.Mvc;

namespace IbhayiPharmacy.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult CustomerDashboard()
        {
            return View();
        }

    }
}
