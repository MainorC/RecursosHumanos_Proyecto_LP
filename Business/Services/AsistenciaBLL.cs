using RecursosHumanos.Domain.Models;
using RecursosHumanos.Domain.Interfaces;
using RecursosHumanos.Business.Interfaces;
using System.Linq;

namespace RecursosHumanos.Business.Services
{
    /// <summary>
    /// Servicio de negocio para la gestión de asistencias (Business Logic Layer)
    /// </summary>
    public class AsistenciaBLL : IAsistenciaService
    {
        private readonly IAsistenciaRepository _asistenciaRepository;

        /// <summary>
        /// Constructor con inyección de dependencias
        /// </summary>
        public AsistenciaBLL(IAsistenciaRepository asistenciaRepository)
        {
            _asistenciaRepository = asistenciaRepository ?? throw new ArgumentNullException(nameof(asistenciaRepository));
        }

        /// <summary>
        /// Constructor por defecto para compatibilidad (crea instancias concretas)
        /// </summary>
        public AsistenciaBLL() : this(new Data.Access.AsistenciaDAL())
        {
        }

        public List<Asistencia> ObtenerTodas()
        {
            return _asistenciaRepository.ObtenerTodas();
        }

        public List<Asistencia> ObtenerTodas(DateTime? mes)
        {
            if (mes.HasValue)
            {
                var inicioMes = new DateTime(mes.Value.Year, mes.Value.Month, 1);
                var finMes = inicioMes.AddMonths(1).AddDays(-1);
                return _asistenciaRepository.ObtenerTodas()
                    .Where(a => a.Fecha.Date >= inicioMes.Date && a.Fecha.Date <= finMes.Date)
                    .ToList();
            }
            return _asistenciaRepository.ObtenerTodas();
        }

        public Asistencia? ObtenerPorEmpleadoYFecha(int empleadoId, DateTime fecha)
        {
            var asistenciasFecha = _asistenciaRepository.ObtenerPorFecha(fecha);
            return asistenciasFecha.FirstOrDefault(a => a.EmpleadoId == empleadoId);
        }

        public Asistencia? ObtenerPorId(int id)
        {
            return _asistenciaRepository.ObtenerPorId(id);
        }

        public List<Asistencia> ObtenerPorEmpleado(int empleadoId)
        {
            return _asistenciaRepository.ObtenerPorEmpleado(empleadoId);
        }

        public List<Asistencia> ObtenerPorFecha(DateTime fecha)
        {
            return _asistenciaRepository.ObtenerPorFecha(fecha);
        }

        public bool Guardar(Asistencia asistencia)
        {
            ValidarAsistencia(asistencia);

            // Verificar si ya existe un registro para este empleado en esta fecha
            var asistenciasFecha = _asistenciaRepository.ObtenerPorFecha(asistencia.Fecha);
            var existente = asistenciasFecha.FirstOrDefault(a => a.EmpleadoId == asistencia.EmpleadoId);
            
            if (existente != null && asistencia.Id == 0)
            {
                throw new InvalidOperationException("Ya existe un registro de asistencia para este empleado en esta fecha.");
            }

            // Validar y calcular horas trabajadas
            if (asistencia.HoraEntrada.HasValue && asistencia.HoraSalida.HasValue)
            {
                var entrada = asistencia.HoraEntrada.Value;
                var salida = asistencia.HoraSalida.Value;
                
                // Validar que la hora de salida sea posterior a la de entrada (o del día siguiente)
                if (salida < entrada)
                {
                    // Si la salida es menor que la entrada asumimos que es del día siguiente
                    salida = salida.Add(TimeSpan.FromDays(1));
                }
                
                // Validar que no exceda 24 horas (trabajo máximo razonable)
                var horas = (decimal)(salida - entrada).TotalHours;
                if (horas > 24)
                {
                    throw new ArgumentException("Las horas trabajadas no pueden exceder 24 horas. Verifique las horas de entrada y salida.");
                }
                
                asistencia.HorasTrabajadas = Math.Round(horas, 2);
            }
            else if (asistencia.HoraSalida.HasValue && !asistencia.HoraEntrada.HasValue)
            {
                throw new ArgumentException("No se puede registrar una hora de salida sin una hora de entrada.");
            }

            // Determinar estado automáticamente solo si no se especificó o está vacío
            if (string.IsNullOrWhiteSpace(asistencia.Estado))
            {
                if (asistencia.HoraEntrada.HasValue)
                {
                    var horaReferencia = new TimeSpan(8, 30, 0); // 8:30 AM
                    if (asistencia.HoraEntrada.Value > horaReferencia)
                    {
                        asistencia.Estado = "Tarde";
                    }
                    else
                    {
                        asistencia.Estado = "Presente";
                    }
                }
                else
                {
                    asistencia.Estado = "Ausente";
                }
            }
            
            // Validar que el estado sea válido
            var estadosValidos = new[] { "Presente", "Tarde", "Ausente" };
            if (!estadosValidos.Contains(asistencia.Estado))
            {
                throw new ArgumentException($"El estado '{asistencia.Estado}' no es válido. Debe ser: Presente, Tarde o Ausente.");
            }

            if (asistencia.Id == 0)
            {
                return _asistenciaRepository.Insertar(asistencia);
            }
            else
            {
                return _asistenciaRepository.Actualizar(asistencia);
            }
        }

        public bool Eliminar(int id)
        {
            return _asistenciaRepository.Eliminar(id);
        }

        private void ValidarAsistencia(Asistencia asistencia)
        {
            if (asistencia.EmpleadoId <= 0)
            {
                throw new ArgumentException("El empleado es obligatorio.");
            }
        }
    }
}

