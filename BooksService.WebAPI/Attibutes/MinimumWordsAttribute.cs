using System;
using System.ComponentModel.DataAnnotations;

namespace BooksService.WebAPI.CustomAttributes
{
    public class MinimumWordsAttribute : ValidationAttribute
    {
        private readonly int _minWords;

        public MinimumWordsAttribute(int minWords)
        {
            _minWords = minWords;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string str && !string.IsNullOrEmpty(str))
            {
                string[] words = str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length < _minWords)
                {
                    return new ValidationResult($"The {validationContext.DisplayName} must contain at least {_minWords} words.");
                }
            }
            return ValidationResult.Success;
        }
    }
}