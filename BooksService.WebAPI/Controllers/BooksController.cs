using System.Threading.Tasks;
using BooksService.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BooksController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Validate(Book book)
        {
            return Ok();
        }
    }
}
