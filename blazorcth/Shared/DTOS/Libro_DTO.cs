using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCTH.Shared.DTOS
{
    public class Libro_DTO
    {
        public int Id { get; set; } 
        public string Nombre { get; set; }

        public int IdAutor { get; set; } = 0;
        public string NombreAutor { get; set; } = string.Empty;

        public string UrlPortada { get; set; } = string.Empty;
        public bool Seleccionado { get; set; } = false;

        public string Descripcion { get; set; } = string.Empty;
    }
}
