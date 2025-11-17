using System.Text.RegularExpressions;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Domain.Interfaces;
using RecursosHumanos.Business.Interfaces;

namespace RecursosHumanos.Business.Services
{
    // servicio de negocio para empleados
    public class EmpleadoBLL : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IAreaRepository _areaRepository;

        public EmpleadoBLL(IEmpleadoRepository empleadoRepository, IAreaRepository areaRepository)
        {
            _empleadoRepository = empleadoRepository ?? throw new ArgumentNullException(nameof(empleadoRepository));
            _areaRepository = areaRepository ?? throw new ArgumentNullException(nameof(areaRepository));
        }

        public EmpleadoBLL() : this(new Data.Access.EmpleadoDAL(), new Data.Access.AreaDAL())
        {
        }

        public List<Empleado> ObtenerTodos(bool soloActivos = false)
        {
            return _empleadoRepository.ObtenerTodos(soloActivos);
        }

        public Empleado? ObtenerPorId(int id)
        {
            return _empleadoRepository.ObtenerPorId(id);
        }

        public List<Empleado> Buscar(string criterio)
        {
            return _empleadoRepository.Buscar(criterio);
        }

        public bool Guardar(Empleado empleado)
        {
            ValidarEmpleado(empleado);

            // Validar que el área exista si se especifica
            if (empleado.AreaId.HasValue && empleado.AreaId.Value > 0)
            {
                var area = _areaRepository.ObtenerPorId(empleado.AreaId.Value);
                if (area == null)
                {
                    throw new ArgumentException("El área especificada no existe.");
                }
            }

            if (empleado.Id == 0)
            {
                // Verificar si el DNI ya está registrado en un empleado activo
                if (_empleadoRepository.ExisteDNI(empleado.DNI))
                {
                    throw new InvalidOperationException($"El DNI {empleado.DNI} ya está registrado en un empleado activo. Si el empleado anterior fue eliminado, puede reutilizar el DNI sin problemas.");
                }
                return _empleadoRepository.Insertar(empleado);
            }
            else
            {
                // Al editar, verificar que no haya otro empleado activo con el mismo DNI
                if (_empleadoRepository.ExisteDNI(empleado.DNI, empleado.Id))
                {
                    throw new InvalidOperationException($"El DNI {empleado.DNI} ya está registrado en otro empleado activo.");
                }
                return _empleadoRepository.Actualizar(empleado);
            }
        }

        public bool Eliminar(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID de empleado inválido.");
            }
            // Soft delete - see usa el método del repositorio
            return _empleadoRepository.Eliminar(id);
        }

        private void ValidarEmpleado(Empleado empleado)
        {
            if (string.IsNullOrWhiteSpace(empleado.DNI))
            {
                throw new ArgumentException("El DNI es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(empleado.Nombre))
            {
                throw new ArgumentException("El nombre es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(empleado.Apellido))
            {
                throw new ArgumentException("El apellido es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(empleado.Email))
            {
                throw new ArgumentException("El email es obligatorio.");
            }

            if (!EsEmailValido(empleado.Email))
            {
                throw new ArgumentException("El formato del email no es válido.");
            }

            if (empleado.SalarioBase < 0)
            {
                throw new ArgumentException("El salario base debe ser mayor o igual a cero.");
            }
        }

        private bool EsEmailValido(string email)
        {
            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }
    }
}

