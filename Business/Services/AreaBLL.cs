using RecursosHumanos.Domain.Models;
using RecursosHumanos.Domain.Interfaces;
using RecursosHumanos.Business.Interfaces;
using System.Linq;

namespace RecursosHumanos.Business.Services
{
    /// <summary>
    /// Servicio de negocio para la gestión de áreas (Business Logic Layer)
    /// </summary>
    public class AreaBLL : IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IEmpleadoRepository _empleadoRepository;

        /// <summary>
        /// Constructor con inyección de dependencias
        /// </summary>
        public AreaBLL(IAreaRepository areaRepository, IEmpleadoRepository empleadoRepository)
        {
            _areaRepository = areaRepository ?? throw new ArgumentNullException(nameof(areaRepository));
            _empleadoRepository = empleadoRepository ?? throw new ArgumentNullException(nameof(empleadoRepository));
        }

        /// <summary>
        /// Constructor por defecto para compatibilidad (crea instancias concretas)
        /// </summary>
        public AreaBLL() : this(new Data.Access.AreaDAL(), new Data.Access.EmpleadoDAL())
        {
        }

        public List<Area> ObtenerTodas(bool soloActivas = false)
        {
            return _areaRepository.ObtenerTodas(soloActivas);
        }

        public Area? ObtenerPorId(int id)
        {
            return _areaRepository.ObtenerPorId(id);
        }

        public bool Guardar(Area area)
        {
            ValidarArea(area);

            if (area.Id == 0)
            {
                // Verificar si el nombre del área ya está registrado en un área activa
                if (_areaRepository.ExisteNombre(area.Nombre))
                {
                    throw new InvalidOperationException($"El nombre del área '{area.Nombre}' ya está registrado en un área activa. Si el área anterior fue eliminada, puede reutilizar el nombre sin problemas.");
                }
                return _areaRepository.Insertar(area);
            }
            else
            {
                // Al editar, verificar que no haya otra área activa con el mismo nombre
                if (_areaRepository.ExisteNombre(area.Nombre, area.Id))
                {
                    throw new InvalidOperationException($"El nombre del área '{area.Nombre}' ya está registrado en otra área activa.");
                }
                return _areaRepository.Actualizar(area);
            }
        }

        public bool Eliminar(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID de área inválido.");
            }
            
            // Verificar si hay empleados activos asignados a esta área
            if (_empleadoRepository.ExistenEmpleadosActivosPorArea(id))
            {
                throw new InvalidOperationException("No se puede eliminar el área porque tiene empleados activos asignados. Primero reasigne los empleados a otra área.");
            }
            
            return _areaRepository.Eliminar(id);
        }

        private void ValidarArea(Area area)
        {
            if (string.IsNullOrWhiteSpace(area.Nombre))
            {
                throw new ArgumentException("El nombre del área es obligatorio.");
            }
        }
    }
}

