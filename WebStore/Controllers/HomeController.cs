using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employee> _employees = new List<Employee>()
        {
            new Employee {Id=1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 27},
            new Employee {Id=1, LastName = "Петров", FirstName = "Петр", Patronymic = "Петрович", Age = 31},
            new Employee {Id=1, LastName = "Иванова", FirstName = "Мария", Patronymic = "Ивановна", Age = 23},
        };

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Employees()
        {
            return View(_employees);
        }
    }
}
