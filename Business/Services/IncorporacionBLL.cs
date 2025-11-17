using RecursosHumanos.Domain.Models;
using RecursosHumanos.Domain.Interfaces;
using RecursosHumanos.Business.Interfaces;

namespace RecursosHumanos.Business.Services
{
    /// <summary>
    /// Servicio de negocio para la gestión de incorporaciones (Business Logic Layer)
    /// </summary>
    public class IncorporacionBLL : IIncorporacionService
    {
        private readonly IIncorporacionRepository _incorporacionRepository;

        /// <summary>
        /// Constructor con inyección de dependencias
        /// </summary>
        public IncorporacionBLL(IIncorporacionRepository incorporacionRepository)
        {
            _incorporacionRepository = incorporacionRepository ?? throw new ArgumentNullException(nameof(incorporacionRepository));
        }

        /// <summary>
        /// Constructor por defecto para compatibilidad (crea instancias concretas)
        /// </summary>
        public IncorporacionBLL() : this(new Data.Access.IncorporacionDAL())
        {
        }

        public List<Incorporacion> ObtenerTodas()
        {
            return _incorporacionRepository.ObtenerTodas();
        }

        public Incorporacion? ObtenerPorId(int id)
        {
            return _incorporacionRepository.ObtenerPorId(id);
        }

        public List<Incorporacion> ObtenerPorEmpleado(int empleadoId)
        {
            return _incorporacionRepository.ObtenerPorEmpleado(empleadoId);
        }

        public bool Guardar(Incorporacion incorporacion)
        {
            ValidarIncorporacion(incorporacion);
            
            if (incorporacion.Id == 0)
            {
                return _incorporacionRepository.Insertar(incorporacion);
            }
            else
            {
                return _incorporacionRepository.Actualizar(incorporacion);
            }
        }

        public bool Eliminar(int id)
        {
            return _incorporacionRepository.Eliminar(id);
        }

        // Métodos adicionales para tareas (compatibilidad con código existente)
        public List<TareaIncorporacion> ObtenerTareas(int incorporacionId)
        {
            var incorporacion = _incorporacionRepository.ObtenerPorId(incorporacionId);
            return incorporacion?.Tareas.ToList() ?? new List<TareaIncorporacion>();
        }

        public bool GuardarTarea(TareaIncorporacion tarea)
        {
            // Verificar que la incorporación existe
            var incorporacion = _incorporacionRepository.ObtenerPorId(tarea.IncorporacionId);
            if (incorporacion == null)
            {
                throw new InvalidOperationException("La incorporación no existe.");
            }

            // Usar el DAL directamente para insertar/actualizar tareas
            var dal = new Data.Access.IncorporacionDAL();
            if (tarea.Id == 0)
            {
                return dal.InsertarTarea(tarea);
            }
            else
            {
                return dal.ActualizarTarea(tarea);
            }
        }

        public bool Actualizar(Incorporacion incorporacion)
        {
            return _incorporacionRepository.Actualizar(incorporacion);
        }

        public int GuardarConId(Incorporacion incorporacion)
        {
            ValidarIncorporacion(incorporacion);
            
            if (incorporacion.Id == 0)
            {
                if (_incorporacionRepository.Insertar(incorporacion))
                {
                    return incorporacion.Id;
                }
                return 0;
            }
            else
            {
                if (_incorporacionRepository.Actualizar(incorporacion))
                {
                    return incorporacion.Id;
                }
                return 0;
            }
        }

        private void ValidarIncorporacion(Incorporacion incorporacion)
        {
            if (string.IsNullOrWhiteSpace(incorporacion.NombreEmpleado))
            {
                throw new ArgumentException("El nombre del empleado es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(incorporacion.TipoProceso))
            {
                throw new ArgumentException("El tipo de proceso es obligatorio.");
            }
        }
    }
}

