using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BooksService.WebAPI;
using BooksService.WebAPI.SeedData;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using Xunit;
using Program = BooksService.WebAPI.Program;

namespace BooksService.Tests
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public HttpClient Client { get; private set; }

        public IntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            SetUpClient();
        }

        private async Task CheckOnBadRequest(BookForm book, string errorMessage)
        {
            var response0 = await Client.PostAsync($"/api/books",
                new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json"));
            response0.StatusCode.Should().BeEquivalentTo(StatusCodes.Status400BadRequest);
            var responseMessage = await response0.Content.ReadAsStringAsync();
            responseMessage.Contains(errorMessage).Should().BeTrue();
        }

        [Fact]
        // Checking Title
        public async Task TestValidateBooks_BadRequests()
        {
            string titleError = "Title is invalid: Title must contain a minimum of 5 characters and a maximum of 255, and the first letter should be in upper case";
            await CheckOnBadRequest(new BookForm
            {
                Title = "initial Professional Development for Civil Engineers, Second edition",
                Author = "Patrick Waterhouse",
                PublicationDate = DateTime.Parse("05/03/2018", new CultureInfo("en"))
            }, titleError);
            await CheckOnBadRequest(new BookForm
            {
                Title = "",
                Author = "Patrick Waterhouse",
                PublicationDate = DateTime.Parse("05/03/2018", new CultureInfo("en"))
            }, titleError);
            await CheckOnBadRequest(new BookForm
            {
                Title = "Init",
                Author = "Patrick Waterhouse",
                PublicationDate = DateTime.Parse("05/03/2018", new CultureInfo("en"))
            }, titleError);
            await CheckOnBadRequest(new BookForm
            {
                Title = "Initial Professional Development for Civil Engineers, Second editionInitial Professional Development for Civil Engineers, Second editionInitial Professional Development for Civil Engineers, Second editionInitial Professional Development for Civil Engineers, Second editionInitial Professional Development for Civil Engineers, Second editionInitial Professional Development for Civil Engineers, Second editionInitial Professional Development for Civil Engineers, Second edition",
                Author = "Patrick Waterhouse",
                PublicationDate = DateTime.Parse("05/03/2018", new CultureInfo("en"))
            }, titleError);

            var book = new BookForm
            {
                Title = "Initial Professional Development for Civil Engineers, Second edition",
                Author = "Patrick Waterhouse",
                PublicationDate = DateTime.Parse("05/03/2018", new CultureInfo("en"))
            };
            var response3 = await Client.PostAsync($"/api/books",
                new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json"));
            response3.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);
        }


        [Fact]
        // Checking Author
        public async Task TestValidateBooks_BadRequests_Oks()
        {
            string errorMessage = "Author is invalid: Author must contain a minimum of 3 characters and a maximum of 30, and the first letter should be in upper case";
            await CheckOnBadRequest(new BookForm
            {
                Title = "Initial Professional Development for Civil Engineers, Second edition",
                Author = "Pa",
                PublicationDate = DateTime.Parse("05/03/2018", new CultureInfo("en"))
            }, errorMessage);
            await CheckOnBadRequest(new BookForm
            {
                Title = "Initial Professional Development for Civil Engineers, Second edition",
                Author = "",
                PublicationDate = DateTime.Parse("05/03/2018", new CultureInfo("en"))
            }, errorMessage);
            await CheckOnBadRequest(new BookForm
            {
                Title = "Initial Professional Development for Civil Engineers, Second edition",
                Author = "Patrick Waterhouse with H. Macdonald SteelsPatrick Waterhouse with H. Macdonald SteelsPatrick Waterhouse with H. Macdonald SteelsPatrick Waterhouse with H. Macdonald SteelsPatrick Waterhouse",
                PublicationDate = DateTime.Parse("05/03/2018", new CultureInfo("en"))
            }, errorMessage);
            await CheckOnBadRequest(new BookForm
            {
                Title = "Initial Professional Development for Civil Engineers, Second edition",
                Author = "patrick Waterhouse with H. Macdonald Steels",
                PublicationDate = DateTime.Parse("05/03/2018", new CultureInfo("en"))
            }, errorMessage);

            var book = new BookForm
            {
                Title = "Initial Professional Development for Civil Engineers, Second edition",
                Author = "Patrick Waterhouse",
                PublicationDate = DateTime.Parse("05/03/2018", new CultureInfo("en"))
            };
            var response3 = await Client.PostAsync($"/api/books",
                new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json"));
            response3.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);
        }


        [Fact]
        // Checking PublicationDate
        public async Task TestValidateBooks_BadRequests_PublicationDate()
        {
            string errorMessage = "PublicationDate is invalid: PublicationDate must be after 01/01/1900 and before the current date";
            await CheckOnBadRequest(new BookForm
            {
                Title = "Initial Professional Development for Civil Engineers, Second edition",
                Author = "Patrick Waterhouse",
                PublicationDate = DateTime.Parse("05/03/1888", new CultureInfo("en"))
            }, errorMessage);
            await CheckOnBadRequest(new BookForm
            {
                Title = "Initial Professional Development for Civil Engineers, Second edition",
                Author = "Patrick Waterhouse",
                PublicationDate = DateTime.Now.AddDays(2)
            }, errorMessage);

            var book = new BookForm
            {
                Title = "Initial Professional Development for Civil Engineers, Second edition",
                Author = "Patrick Waterhouse",
                PublicationDate = DateTime.Parse("05/03/2018", new CultureInfo("en"))
            };
            var response3 = await Client.PostAsync($"/api/books",
                new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json"));
            response3.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);
        }

        private void SetUpClient()
        {
            Client = _factory.WithWebHostBuilder(builder =>
                builder.UseStartup<Startup>()
                .ConfigureServices(services =>
                {

                })).CreateClient();
        }
    }
}
