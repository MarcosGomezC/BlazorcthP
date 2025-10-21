using BlazorCTH.Shared.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.DAL;
using server.Modelo;
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
            List<Libro_DTO> libros = await _db.Libros.Where(l=> !string.IsNullOrWhiteSpace(l.UrlPortada)).Select(l =>
               new Libro_DTO
               {
                   Nombre = l.Nombre,
                   IdAutor = l.IdAutor,
                   NombreAutor = _db.Autors.Where(a => a.Id == l.IdAutor).Select(a => a.Nombre).FirstOrDefault(),
                   UrlPortada = l.UrlPortada,
               }
               ).ToListAsync();
            if (!libros.Any())
            {
                return BadRequest("No hay Libros");
            }
            return Ok(libros);
        }

        [HttpPost("CrearLibros")]
        public async Task<ActionResult<Libro_DTO>> CrearLibros(Libro_DTO libro)
        {
            var NuevoLibro = new Libro
            {
                Id = libro.Id,
                Nombre = libro.Nombre,
                IdAutor = libro.IdAutor,
                UrlPortada = libro.UrlPortada

            };
            _db.Libros.Add(NuevoLibro);
            await _db.SaveChangesAsync();
            return Ok("Libro Agregado Correctamente");

        }

    }
}
