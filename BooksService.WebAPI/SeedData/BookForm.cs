using System;
using Newtonsoft.Json;

namespace BooksService.WebAPI.SeedData
{
    public class BookForm
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("publicationDate")]
        public DateTime PublicationDate { get; set; }
    }
}
