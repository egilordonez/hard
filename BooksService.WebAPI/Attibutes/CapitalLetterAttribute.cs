using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BooksService.WebAPI.CustomAttributes
{
    public class CapitalLetterAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string str && !string.IsNullOrEmpty(str))
            {
                if (!char.IsUpper(str.First()))
                {
                    return new ValidationResult($"The first letter of the {validationContext.DisplayName} must be uppercase.");
                }
            }
            return ValidationResult.Success;
        }
    }
}