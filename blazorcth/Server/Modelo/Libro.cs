namespace server.Modelo
{
    public class Libro
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public int IdAutor { get; set; }

        public string? UrlPortada { get; set; }

        public string Descripcion { get; set; }
    }
}
