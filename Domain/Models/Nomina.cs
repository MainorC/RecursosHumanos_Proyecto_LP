namespace RecursosHumanos.Domain.Models
{
    public class Nomina
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public decimal SalarioBruto { get; set; } = 0;
        public decimal Bonificaciones { get; set; } = 0;
        public decimal Deducciones { get; set; } = 0;
        public decimal SalarioNeto { get; set; } = 0;
        public string Estado { get; set; } = "Pendiente";
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaPago { get; set; }
        
        // Propiedad de navegación EF
        public virtual Empleado Empleado { get; set; } = null!;
        
        // Propiedad calculada (no mapeada)
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string? NombreEmpleado { get; set; }
    }
}

