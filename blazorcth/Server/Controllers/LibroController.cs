using BlazorCTH.Shared.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.DAL;
using server.Modelo;


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
                   Id = l.Id,
                   Nombre = l.Nombre,
                   IdAutor = l.IdAutor,
                   NombreAutor = _db.Autors.Where(a => a.Id == l.IdAutor).Select(a => a.Nombre).FirstOrDefault(),
                   UrlPortada = l.UrlPortada,
                   Descripcion = l.Descripcion
               }
               ).ToListAsync();
            if (!libros.Any())
            {
                return BadRequest("No hay Libros");
            }
            return Ok(libros);
        }
        [HttpGet("GetLibro/{idLibro}")]
        public async Task<ActionResult> GetLibro(int idLibro)
        {
            Libro_DTO libros = await _db.Libros.Where(l => !string.IsNullOrWhiteSpace(l.UrlPortada)&& l.Id == idLibro).Select(l =>
               new Libro_DTO
               {
                   Id = l.Id,
                   Nombre = l.Nombre,
                   IdAutor = l.IdAutor,
                   NombreAutor = _db.Autors.Where(a => a.Id == l.IdAutor).Select(a => a.Nombre).FirstOrDefault(),
                   UrlPortada = l.UrlPortada,
                   Descripcion = l.Descripcion
               }
               ).FirstOrDefaultAsync();
            if (libros.Id == 0)
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
                UrlPortada = libro.UrlPortada,
                Descripcion = libro.Descripcion,
                

            };
            _db.Libros.Add(NuevoLibro);
            await _db.SaveChangesAsync();
            return Ok("Libro Agregado Correctamente");

        }
        [HttpDelete("Borrar/{Id}")]
        public async Task<ActionResult<List<Libro>>> BorrarLibro(int id)
        { 
            var libro =await _db.Libros.FirstOrDefaultAsync(l => l.Id == id);
            if (libro == null)
            {
                return NotFound("Libro no encontrado");
            }
            _db.Libros.Remove(libro);
            await _db.SaveChangesAsync();
            return Ok(await GetLibros());
        }

        [HttpPut("Actualizar/{Id}")]
        public async Task<ActionResult<Libro_DTO>> UpdateLibro(Libro_DTO dto, int Id)
        {
            var libro = await _db.Libros.FirstOrDefaultAsync(l => l.Id == Id);
            libro.Nombre = dto.Nombre;
            libro.IdAutor = dto.IdAutor;
            libro.Descripcion = dto.Descripcion;
            await _db.SaveChangesAsync();
            return Ok(libro);
        }

        [HttpGet ("BuscarLibro/{busqueda}")]
        public async Task<List<Libro_DTO>> GetLibro(string busqueda)
        {
            var libros = (from l in _db.Libros
                          join a in _db.Autors on l.IdAutor equals a.Id
                          where l.Nombre.Contains(busqueda)|| a.Nombre.Contains(busqueda)
                          select new Libro_DTO
                          {
                              Id = l.Id,
                              Nombre = l.Nombre,
                              IdAutor = l.IdAutor,
                              NombreAutor = _db.Autors.Where(a => a.Id == l.IdAutor).Select(a => a.Nombre).FirstOrDefault(),
                              UrlPortada = l.UrlPortada,
                              Descripcion = l.Descripcion
                          }
                ).ToList();     
            return libros;
        }

        


        
    }
}
