using Microsoft.EntityFrameworkCore;
using server.Modelo;

namespace server.DAL
{
    public class Contexto:DbContext
    {
        public Contexto (DbContextOptions<Contexto> options):base(options) { }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Autor> Autors { get; set; }

    }
}
