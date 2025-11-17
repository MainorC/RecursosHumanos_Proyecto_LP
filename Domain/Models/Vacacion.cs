namespace RecursosHumanos.Domain.Models
{
    public class Vacacion
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int DiasTotales { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaAprobacion { get; set; }
        
        // Propiedad de navegación EF
        public virtual Empleado Empleado { get; set; } = null!;
        
        // Propiedad calculada (no mapeada)
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string? NombreEmpleado { get; set; }
    }
}

