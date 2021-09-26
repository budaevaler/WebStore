using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Employee> Employees { get; } = new List<Employee>()
        {
            new Employee {Id=1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 27},
            new Employee {Id=2, LastName = "Петров", FirstName = "Петр", Patronymic = "Петрович", Age = 31},
            new Employee {Id=3, LastName = "Иванова", FirstName = "Мария", Patronymic = "Ивановна", Age = 23},
        };
    }
}
