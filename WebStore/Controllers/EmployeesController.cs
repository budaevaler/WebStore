using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Route("Employees/[action]/{id?}")]
    [Route("Staff/[action]/{id?}")]
    public class EmployeesController : Controller
    {
        private readonly IEnumerable<Employee> _employees;
        public EmployeesController()
        {
            _employees = TestData.Employees;
        }

        [Route("~/employees/all")]
        public IActionResult Index()
        {
            return View(_employees);
        }

        [Route("~/employees/info-{id}")]
        public IActionResult Details(int id)
        {
            var employee = _employees.SingleOrDefault(e => e.Id == id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }
    }
}
