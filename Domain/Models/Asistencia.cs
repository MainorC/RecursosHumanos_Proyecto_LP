namespace RecursosHumanos.Domain.Models
{
    public class Asistencia
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan? HoraEntrada { get; set; }
        public TimeSpan? HoraSalida { get; set; }
        public decimal? HorasTrabajadas { get; set; }
        public string Estado { get; set; } = "Presente";
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        
        // Propiedad de navegación EF
        public virtual Empleado Empleado { get; set; } = null!;
        
        // Propiedad calculada (no mapeada)
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string? NombreEmpleado { get; set; }
    }
}

