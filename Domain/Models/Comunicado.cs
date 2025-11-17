namespace RecursosHumanos.Domain.Models
{
    public class Comunicado
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Contenido { get; set; } = string.Empty;
        public DateTime FechaPublicacion { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;
    }
}

