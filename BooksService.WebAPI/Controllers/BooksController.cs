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
        public async Task<IActionResult> Validate([FromBody] Book book)
        {
            // La validación se activa automáticamente gracias a los atributos en el modelo Book.
            // Si el modelo es inválido, ModelState.IsValid será falso.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Simulación de una operación exitosa, ya que no se requiere base de datos.
            // En una aplicación real, aquí se guardaría el libro en la base de datos.
            return Ok(book);
        }
    }
}
