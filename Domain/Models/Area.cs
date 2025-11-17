namespace RecursosHumanos.Domain.Models
{
    public class Area
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        
        // Propiedad de navegación
        public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
    }
}

