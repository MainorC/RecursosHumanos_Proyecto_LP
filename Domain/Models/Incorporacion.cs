using System;

namespace RecursosHumanos.Domain.Models
{
    public class Incorporacion
    {
        public int Id { get; set; }
        public int? EmpleadoId { get; set; }
        public string NombreEmpleado { get; set; } = string.Empty;
        public string TipoProceso { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Estado { get; set; } = "En Proceso";
        public int TareasCompletadas { get; set; } = 0;
        public int TotalTareas { get; set; } = 0;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        
        // Propiedad de navegación EF
        public virtual ICollection<TareaIncorporacion> Tareas { get; set; } = new List<TareaIncorporacion>();
        
        // Propiedad calculada (no mapeada)
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public decimal PorcentajeCompletado
        {
            get
            {
                if (TotalTareas == 0) return 0;
                var porcentaje = (decimal)TareasCompletadas / TotalTareas * 100;
                // Asegurar que el porcentaje esté entre 0 y 100
                return Math.Max(0, Math.Min(100, porcentaje));
            }
        }
    }
}

