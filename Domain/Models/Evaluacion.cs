namespace RecursosHumanos.Domain.Models
{
    public class Evaluacion
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public DateTime Fecha { get; set; }
        public int Puntaje { get; set; }
        public string? Fortalezas { get; set; }
        public string? OportunidadesMejora { get; set; }
        public string? Comentarios { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        
        // Propiedad de navegación EF
        public virtual Empleado Empleado { get; set; } = null!;
        
        // Propiedad calculada (no mapeada)
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string? NombreEmpleado { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Clasificacion
        {
            get
            {
                if (Puntaje >= 90) return "Excelente";
                if (Puntaje >= 75) return "Bueno";
                if (Puntaje >= 60) return "Regular";
                return "Deficiente";
            }
        }
    }
}

