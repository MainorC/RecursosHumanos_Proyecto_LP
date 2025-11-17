using RecursosHumanos.Domain.Models;
using RecursosHumanos.Domain.Interfaces;
using RecursosHumanos.Business.Interfaces;

namespace RecursosHumanos.Business.Services
{
    /// <summary>
    /// Servicio de negocio para la gestión de comunicados (Business Logic Layer)
    /// </summary>
    public class ComunicadoBLL : IComunicadoService
    {
        private readonly IComunicadoRepository _comunicadoRepository;

        /// <summary>
        /// Constructor con inyección de dependencias
        /// </summary>
        public ComunicadoBLL(IComunicadoRepository comunicadoRepository)
        {
            _comunicadoRepository = comunicadoRepository ?? throw new ArgumentNullException(nameof(comunicadoRepository));
        }

        /// <summary>
        /// Constructor por defecto para compatibilidad (crea instancias concretas)
        /// </summary>
        public ComunicadoBLL() : this(new Data.Access.ComunicadoDAL())
        {
        }

        public List<Comunicado> ObtenerTodos(bool soloActivos = false)
        {
            return _comunicadoRepository.ObtenerTodos(soloActivos);
        }

        public Comunicado? ObtenerPorId(int id)
        {
            return _comunicadoRepository.ObtenerPorId(id);
        }

        public bool Guardar(Comunicado comunicado)
        {
            ValidarComunicado(comunicado);
            
            if (comunicado.Id == 0)
            {
                return _comunicadoRepository.Insertar(comunicado);
            }
            else
            {
                return _comunicadoRepository.Actualizar(comunicado);
            }
        }

        public bool Eliminar(int id)
        {
            return _comunicadoRepository.Eliminar(id);
        }

        public bool CambiarEstado(int id, bool activo)
        {
            var comunicado = _comunicadoRepository.ObtenerPorId(id);
            if (comunicado == null)
            {
                throw new InvalidOperationException("El comunicado no existe.");
            }
            
            comunicado.Activo = activo;
            return _comunicadoRepository.Actualizar(comunicado);
        }

        private void ValidarComunicado(Comunicado comunicado)
        {
            if (string.IsNullOrWhiteSpace(comunicado.Titulo))
            {
                throw new ArgumentException("El título es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(comunicado.Contenido))
            {
                throw new ArgumentException("El contenido es obligatorio.");
            }
        }
    }
}

