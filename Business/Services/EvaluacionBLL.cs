using RecursosHumanos.Domain.Models;
using RecursosHumanos.Domain.Interfaces;
using RecursosHumanos.Business.Interfaces;

namespace RecursosHumanos.Business.Services
{
    /// <summary>
    /// Servicio de negocio para la gestión de evaaluaciones (Bsiness Logic Layer)
    /// </summary>
    public class EvaluacionBLL : IEvaluacionService
    {
        private readonly IEvaluacionRepository _evaluacionRepository;

        /// <summary>
        /// Constructor con inyección de dependencias
        /// </summary>
        public EvaluacionBLL(IEvaluacionRepository evaluacionRepository)
        {
            _evaluacionRepository = evaluacionRepository ?? throw new ArgumentNullException(nameof(evaluacionRepository));
        }

        /// <summary>
        /// Constructor por defecto para compatibilidad (crea instancias concretas)
        /// </summary>
        public EvaluacionBLL() : this(new Data.Access.EvaluacionDAL())
        {
        }

        public List<Evaluacion> ObtenerTodas()
        {
            return _evaluacionRepository.ObtenerTodas();
        }

        public Evaluacion? ObtenerPorId(int id)
        {
            return _evaluacionRepository.ObtenerPorId(id);
        }

        public List<Evaluacion> ObtenerPorEmpleado(int empleadoId)
        {
            return _evaluacionRepository.ObtenerPorEmpleado(empleadoId);
        }

        public bool Guardar(Evaluacion evaluacion)
        {
            ValidarEvaluacion(evaluacion);
            
            if (evaluacion.Id == 0)
            {
                return _evaluacionRepository.Insertar(evaluacion);
            }
            else
            {
                return _evaluacionRepository.Actualizar(evaluacion);
            }
        }

        public bool Eliminar(int id)
        {
            return _evaluacionRepository.Eliminar(id);
        }

        private void ValidarEvaluacion(Evaluacion evaluacion)
        {
            if (evaluacion.EmpleadoId <= 0)
            {
                throw new ArgumentException("El empleado es obligatorio.");
            }

            if (evaluacion.Puntaje < 0 || evaluacion.Puntaje > 100)
            {
                throw new ArgumentException("El puntaje debe estar entre 0 y 100.");
            }
        }
    }
}

