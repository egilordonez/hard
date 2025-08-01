using System;
using System.ComponentModel.DataAnnotations;
using BooksService.WebAPI.CustomAttributes; // Asegúrate de importar los atributos personalizados

namespace BooksService.WebAPI.Models
{
    public class Book
    {
        [Required]
        [StringLength(255, MinimumLength = 5)]
        [CapitalLetter]
        public string Title { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [CapitalLetter]
        [MinimumWords(2)]
        public string Author { get; set; }

        [Required]
        [Range(typeof(DateTime), "1/1/1900", "1/1/9999",
            ErrorMessage = "PublicationDate is invalid. PublicationDate must be after 01/01/1900 and before the current date.")]
        public DateTime PublicationDate { get; set; }

        // Validador a nivel de objeto para la fecha de publicación, ya que el atributo Range no puede usar la fecha actual dinámicamente.
        [ValidationResult("PublicationDate is invalid. PublicationDate must be after 01/01/1900 and before the current date.", "PublicationDate")]
        public static ValidationResult ValidatePublicationDate(Book book, ValidationContext context)
        {
            if (book.PublicationDate >= DateTime.Now)
            {
                return new ValidationResult("PublicationDate is invalid. PublicationDate must be after 01/01/1900 and before the current date.");
            }
            return ValidationResult.Success;
        }
    }
}