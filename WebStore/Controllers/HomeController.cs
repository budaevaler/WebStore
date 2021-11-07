﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NotFound404() => View();
        public IActionResult Cart() => View();
        public IActionResult CheckOut() => View();
        public IActionResult Login() => View();
        public IActionResult ProductDetails() => View();
        public IActionResult Shop() => View();
        public IActionResult ContactUs() => View();

        public IActionResult Status(string id)
        {
            switch (id)
            {
                case "404": return View("NotFound404");
                default: return Content($"Status code ---{id} ");
            }
        }
    }
}
