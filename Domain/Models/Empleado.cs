namespace RecursosHumanos.Domain.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public string DNI { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Direccion { get; set; }
        public int? AreaId { get; set; }
        public string? Puesto { get; set; }
        public string? TipoContrato { get; set; }
        public DateTime? FechaContrato { get; set; }
        public decimal SalarioBase { get; set; } = 0;
        public string? SistemaPension { get; set; }
        public string Estado { get; set; } = "Activo";
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        
        // Propiedades de navegación EF
        public virtual Area? Area { get; set; }
        public virtual ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
        public virtual ICollection<Vacacion> Vacaciones { get; set; } = new List<Vacacion>();
        public virtual ICollection<Evaluacion> Evaluaciones { get; set; } = new List<Evaluacion>();
        public virtual ICollection<Nomina> Nominas { get; set; } = new List<Nomina>();
        
        // Propiedad calculada (no mapeada)
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string? NombreArea { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}

