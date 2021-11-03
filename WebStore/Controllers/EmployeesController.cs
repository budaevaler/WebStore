using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebStore.Data;
using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.ViewModel;

namespace WebStore.Controllers
{
    [Route("Employees/[action]/{id?}")]
    [Route("Staff/[action]/{id?}")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _employeesData;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IEmployeesData employeesData, ILogger<EmployeesController> logger)
        {
            _employeesData = employeesData;
            _logger = logger;
        }

        [Route("~/employees/all")]
        public IActionResult Index()
        {
            return View(_employeesData.GetAll());
        }

        [Route("~/employees/info-{id}")]
        public IActionResult Details(int id)
        {
            var employee = _employeesData.GetById(id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }

        public IActionResult Create() => View("Edit", new EmployeeViewModel());

        #region Edit

        public IActionResult Edit(int? id)
        {
            if (id is null)
                return View(new EmployeeViewModel());

            var employee = _employeesData.GetById(id.Value);
            if (employee is null)
                return NotFound();
            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                Age = employee.Age,
                Patronymic = employee.Patronymic,
            };
            return View(model);
        }

        [HttpPost] //фильтр действия, перехват вызова формы с html-страницы
        public IActionResult Edit(EmployeeViewModel model)
        {
            //Здесь тоже можно проводить валидацию
            if (!Regex.IsMatch(model.Name, @"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)"))
                ModelState.AddModelError("","Имя должно начинаться с большой буквы");

            //Валидация происходит в момент сопостовления входных данных из запроса с классом EmloyeeViewModel
            if (!ModelState.IsValid) return View(model);

            var employee = new Employee
            {
                Id = model.Id,
                FirstName = model.Name,
                LastName = model.LastName,
                Age = model.Age,
                Patronymic = model.Patronymic,
            };
            if (employee.Id == 0)
                _employeesData.Add(employee);
            else
            {
                _employeesData.Update(employee);
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete

        public IActionResult Delete(int id)
        {
            if (id < 0) BadRequest();
            var employee = _employeesData.GetById(id);
            if (employee is null)
                return NotFound();
            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                Age = employee.Age,
                Patronymic = employee.Patronymic,
            });
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _employeesData.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}