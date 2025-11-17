namespace RecursosHumanos.Domain.Models
{
    public class TareaIncorporacion
    {
        public int Id { get; set; }
        public int IncorporacionId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public bool Completada { get; set; } = false;
        public DateTime? FechaCompletada { get; set; }
        
        // Propiedad de navegación EF
        public virtual Incorporacion Incorporacion { get; set; } = null!;
    }
}

