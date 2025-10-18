using BlazorCTH.Shared.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.DAL;
using server.Modelo;
using Shared.DTOS;
using System.Runtime.CompilerServices;

namespace BlazorCTH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly Contexto _db;
        public AutorController(Contexto db)
        {
            _db = db;
        }

        [HttpGet("GetAutores")]
        public async Task<ActionResult> GetAutores()
        {
            List<Autor_DTO> autors = await _db.Autors.Select(l =>
               new Autor_DTO
               {
                   Id = l.Id,
                   Nombre = l.Nombre,
               }
               ).ToListAsync();
            if (!autors.Any())
            {
                return BadRequest("No hay Autores");
            }
            return Ok(autors);
        }

        [HttpGet("GetAutorLibros/{idAutor}")]
        public async Task<ActionResult> GetAutorLibros(int idAutor)
        {
            var autor = await _db.Autors.FirstOrDefaultAsync(a => a.Id == idAutor);
            if (autor == null)
            {
                return NotFound("Autor no encontrado");
            }


            return Ok(autor);
        }

        [HttpPost("Crear")]
        public async Task<ActionResult> CreateAutor(Autor_DTO autor)
        {
            var NuevoAutor = new Autor
            {
                Id = autor.Id,
                Nombre = autor.Nombre,
            };     
            _db.Autors.Add(NuevoAutor);
            await _db.SaveChangesAsync();
            return Ok("Autor creado correctamente");
        }
        [HttpDelete("Borrar/{Id}")]
        public async Task<ActionResult<List<Autor>>>DeleteAutor( int Id)
        {
            var autor = await _db.Autors.FirstOrDefaultAsync(l => l.Id == Id);
            if(autor == null)
            {
                return NotFound("NO Existe");
            }

            _db.Autors.Remove(autor);
            await _db.SaveChangesAsync();
            return Ok(await GetAutores());
        }

        [HttpPut("Actualizar/{Id}")]
        public async Task<ActionResult<Autor_DTO>>UpdateAutor(Autor_DTO dto, int Id )
        {
            var autor = await _db.Autors.FirstOrDefaultAsync(l => l.Id == Id);
            autor.Nombre = dto.Nombre;
            
            await _db.SaveChangesAsync();
            return Ok(autor);
        }

               
    }
}
