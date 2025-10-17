using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.DAL;
using Shared.DTOS;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly Contexto _db;
        public LibroController(Contexto db)
        {
            _db = db;
        }

        [HttpGet("GetLibros")]
        public async Task<ActionResult> GetLibros()
        {
            List<Libro_DTO> libros = await _db.Libros.Select(l =>
               new Libro_DTO
               {
                   Nombre = l.Nombre,
               }
               ).ToListAsync();
            if (!libros.Any())
            {
                return BadRequest("No hay Libros");
            }
            return Ok(libros);
        }
    }
}
