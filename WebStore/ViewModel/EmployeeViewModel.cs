using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.ViewModel
{
    public class EmployeeViewModel : IValidatableObject // Для реализации своих правил валидации
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя не указано")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Длина от 2 до 200 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Первая буква должна быть заглавная")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия не указана")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Длина от 2 до 200 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Первая буква должна быть заглавная")]
        public string LastName { get; set; }
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
        [Display(Name = "Возраст")]
        [Range(18,80,ErrorMessage = "Возраст должен быть от 18 до 89 лет")]
        public int Age { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            switch (validationContext.MemberName)
            {
                //default: return Enumerable.Empty<ValidationResult>();
                default: return new[] {ValidationResult.Success};

                case nameof(Age):
                    if (Age < 15 && Age > 90) return new[] {new ValidationResult("Странный возраст", new [] {nameof(Age)})};
                    return new[] { ValidationResult.Success };

            }
        }
    }
}
