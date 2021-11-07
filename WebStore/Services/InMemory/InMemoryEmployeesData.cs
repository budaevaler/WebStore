using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using WebStore.Data;
using WebStore.Models;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InMemory
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly ILogger<InMemoryEmployeesData> _logger;
        private int _currentMaxId;

        public InMemoryEmployeesData(ILogger<InMemoryEmployeesData> logger)
        {
            _logger = logger;
            _currentMaxId = TestData.Employees.Max(e => e.Id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return TestData.Employees;
        }

        public Employee GetById(int id)
        {
            return TestData.Employees.SingleOrDefault(e => e.Id == id);
        }

        public int Add(Employee employee)
        {
            if (employee is null)
                throw new ArgumentException(nameof(employee));
            if (TestData.Employees.Contains(employee)) return employee.Id;

            employee.Id = ++_currentMaxId;
            TestData.Employees.Add(employee);
            return employee.Id;
        }

        public void Update(Employee employee)
        {
            if (employee is null)
                throw new ArgumentException(nameof(employee));
            if (TestData.Employees.Contains(employee)) return; //Не для базы данных (потеря времени)
            var dbEmployee = GetById(employee.Id);
            if (dbEmployee is null) return;

            dbEmployee.LastName = employee.LastName;
            dbEmployee.FirstName = employee.FirstName;
            dbEmployee.Age = employee.Age;
            dbEmployee.Patronymic = employee.Patronymic;
        }

        public bool Delete(int id)
        {
            var dbEmployee = GetById(id);
            if (dbEmployee is null) return false;
            TestData.Employees.Remove(dbEmployee);
            return true;
        }
    }
}