using System;
using System.ComponentModel.DataAnnotations;

namespace BooksService.WebAPI.Models
{
    public class Book
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}