using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Controllers
{
    public class BlogsController : Controller
    {
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult BlogSingle() => View();
    }
}
