using RecursosHumanos.Domain.Models;
using RecursosHumanos.Domain.Interfaces;
using RecursosHumanos.Business.Interfaces;
using System.Linq;

namespace RecursosHumanos.Business.Services
{
    /// <summary>
    /// Servicio de negocio para la gestión de vacaciones (Business Logic Layer)
    /// </summary>
    public class VacacionBLL : IVacacionService
    {
        private readonly IVacacionRepository _vacacionRepository;
        private readonly IEmpleadoRepository _empleadoRepository;

        /// <summary>
        /// Constructor con inyección de dependencias
        /// </summary>
        public VacacionBLL(IVacacionRepository vacacionRepository, IEmpleadoRepository empleadoRepository)
        {
            _vacacionRepository = vacacionRepository ?? throw new ArgumentNullException(nameof(vacacionRepository));
            _empleadoRepository = empleadoRepository ?? throw new ArgumentNullException(nameof(empleadoRepository));
        }

        /// <summary>
        /// Constructor por defecto para compatibilidad (crea instancias concretas)
        /// </summary>
        public VacacionBLL() : this(new Data.Access.VacacionDAL(), new Data.Access.EmpleadoDAL())
        {
        }

        public List<Vacacion> ObtenerTodas()
        {
            return _vacacionRepository.ObtenerTodas();
        }

        public Vacacion? ObtenerPorId(int id)
        {
            return _vacacionRepository.ObtenerPorId(id);
        }

        public List<Vacacion> ObtenerPorEmpleado(int empleadoId)
        {
            return _vacacionRepository.ObtenerPorEmpleado(empleadoId);
        }

        public int CalcularDiasDisponibles(int empleadoId)
        {
            int anio = DateTime.Now.Year;
            
            // Días estándar de vacaciones por año (30 días en Perú)
            const int diasAnuales = 30;
            
            // Calcular días usados
            int diasUsados = CalcularDiasUsados(empleadoId);
            
            return diasAnuales - diasUsados;
        }

        public int CalcularDiasUsados(int empleadoId)
        {
            int anio = DateTime.Now.Year;
            
            var vacaciones = _vacacionRepository.ObtenerPorEmpleado(empleadoId)
                .Where(v => v.Estado == "Aprobado" && v.FechaInicio.Year == anio)
                .ToList();
            
            return vacaciones.Sum(v => v.DiasTotales);
        }

        public bool Guardar(Vacacion vacacion)
        {
            ValidarVacacion(vacacion);

            // Verificar que el empleado esté activo
            var empleado = _empleadoRepository.ObtenerPorId(vacacion.EmpleadoId);
            if (empleado == null || !empleado.Activo)
            {
                throw new InvalidOperationException("No se pueden crear solicitudes de vacaciones para empleados inactivos.");
            }

            // Calcular días totales
            var dias = (vacacion.FechaFin - vacacion.FechaInicio).Days + 1;
            vacacion.DiasTotales = dias;

            // Validar días disponibles
            int diasDisponibles = CalcularDiasDisponibles(vacacion.EmpleadoId);
            if (dias > diasDisponibles)
            {
                throw new InvalidOperationException($"El empleado solo tiene {diasDisponibles} días de vacaciones disponibles. Está solicitando {dias} días.");
            }

            if (vacacion.Id == 0)
            {
                // Validar solapamiento de fechas
                var vacacionesExistentes = _vacacionRepository.ObtenerPorEmpleado(vacacion.EmpleadoId)
                    .Where(v => v.Estado == "Aprobado" || v.Estado == "Pendiente")
                    .Where(v => (v.FechaInicio <= vacacion.FechaFin && v.FechaFin >= vacacion.FechaInicio))
                    .ToList();
                
                if (vacacionesExistentes.Any())
                {
                    throw new InvalidOperationException("Ya existe una solicitud de vacaciones aprobada o pendiente que se solapa con las fechas seleccionadas.");
                }
                
                return _vacacionRepository.Insertar(vacacion);
            }
            else
            {
                // Solo permitir editar si está en estado "Pendiente"
                var vacacionExistente = _vacacionRepository.ObtenerPorId(vacacion.Id);
                if (vacacionExistente == null)
                {
                    throw new InvalidOperationException("La solicitud de vacaciones no existe.");
                }
                
                if (vacacionExistente.Estado != "Pendiente")
                {
                    throw new InvalidOperationException("Solo se pueden modificar solicitudes de vacaciones en estado 'Pendiente'.");
                }
                
                // Validar solapamiento excluyendo la solicitud actual
                var vacacionesExistentes = _vacacionRepository.ObtenerPorEmpleado(vacacion.EmpleadoId)
                    .Where(v => v.Id != vacacion.Id && (v.Estado == "Aprobado" || v.Estado == "Pendiente"))
                    .Where(v => (v.FechaInicio <= vacacion.FechaFin && v.FechaFin >= vacacion.FechaInicio))
                    .ToList();
                
                if (vacacionesExistentes.Any())
                {
                    throw new InvalidOperationException("Ya existe otra solicitud de vacaciones aprobada o pendiente que se solapa con las fechas seleccionadas.");
                }
                
                return _vacacionRepository.Actualizar(vacacion);
            }
        }

        public bool Eliminar(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID de vacación inválido.");
            }
            
            var vacacion = _vacacionRepository.ObtenerPorId(id);
            if (vacacion == null)
            {
                throw new InvalidOperationException("La solicitud de vacaciones no existe.");
            }
            
            return _vacacionRepository.Eliminar(id);
        }

        public bool Aprobar(int id)
        {
            var vacacion = _vacacionRepository.ObtenerPorId(id);
            if (vacacion == null)
            {
                throw new InvalidOperationException("La solicitud de vacaciones no existe.");
            }
            
            if (vacacion.Estado != "Pendiente")
            {
                throw new InvalidOperationException("Solo se pueden aprobar solicitudes en estado 'Pendiente'.");
            }
            
            vacacion.Estado = "Aprobado";
            vacacion.FechaAprobacion = DateTime.Now;
            return _vacacionRepository.Actualizar(vacacion);
        }

        public bool Rechazar(int id)
        {
            var vacacion = _vacacionRepository.ObtenerPorId(id);
            if (vacacion == null)
            {
                throw new InvalidOperationException("La solicitud de vacaciones no existe.");
            }
            
            if (vacacion.Estado != "Pendiente")
            {
                throw new InvalidOperationException("Solo se pueden rechazar solicitudes en estado 'Pendiente'.");
            }
            
            vacacion.Estado = "Rechazado";
            return _vacacionRepository.Actualizar(vacacion);
        }

        private void ValidarVacacion(Vacacion vacacion)
        {
            if (vacacion.EmpleadoId <= 0)
            {
                throw new ArgumentException("El empleado es obligatorio.");
            }

            if (vacacion.FechaFin < vacacion.FechaInicio)
            {
                throw new ArgumentException("La fecha de fin debe ser posterior a la fecha de inicio.");
            }
        }
    }
}

