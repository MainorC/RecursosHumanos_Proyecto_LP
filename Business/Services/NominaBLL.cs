using RecursosHumanos.Domain.Models;
using RecursosHumanos.Domain.Interfaces;
using RecursosHumanos.Business.Interfaces;
using System.Linq;

namespace RecursosHumanos.Business.Services
{
    /// <summary>
    /// Servicio de negocio para la gestión de nominas (Business Logic Layer)
    /// </summary>
    public class NominaBLL : INominaService
    {
        private readonly INominaRepository _nominaRepository;
        private readonly IEmpleadoRepository _empleadoRepository;

        /// <summary>
        /// Constructor con inyección de dependencias
        /// </summary>
        public NominaBLL(INominaRepository nominaRepository, IEmpleadoRepository empleadoRepository)
        {
            _nominaRepository = nominaRepository ?? throw new ArgumentNullException(nameof(nominaRepository));
            _empleadoRepository = empleadoRepository ?? throw new ArgumentNullException(nameof(empleadoRepository));
        }

        /// <summary>
        /// Constructor por defecto para compatibilidad (crea instancias concretas)
        /// </summary>
        public NominaBLL() : this(new Data.Access.NominaDAL(), new Data.Access.EmpleadoDAL())
        {
        }

        public List<Nomina> ObtenerTodas()
        {
            return _nominaRepository.ObtenerTodas();
        }

        public Nomina? ObtenerPorId(int id)
        {
            return _nominaRepository.ObtenerPorId(id);
        }

        public List<Nomina> ObtenerPorEmpleado(int empleadoId)
        {
            return _nominaRepository.ObtenerPorEmpleado(empleadoId);
        }

        public List<Nomina> ObtenerPorPeriodo(string periodo)
        {
            return _nominaRepository.ObtenerPorPeriodo(periodo);
        }

        public bool Guardar(Nomina nomina)
        {
            ValidarNomina(nomina);

            // Recalcular salario neto
            nomina.SalarioNeto = nomina.SalarioBruto + nomina.Bonificaciones - nomina.Deducciones;

            if (nomina.Id == 0)
            {
                return _nominaRepository.Insertar(nomina);
            }
            else
            {
                return _nominaRepository.Actualizar(nomina);
            }
        }

        public bool Eliminar(int id)
        {
            return _nominaRepository.Eliminar(id);
        }

        public bool PrepararNomina(string periodo)
        {
            // Validar formato de período (debe ser "Mes Año", ej: "Enero 2024")
            if (string.IsNullOrWhiteSpace(periodo))
            {
                throw new ArgumentException("El período es obligatorio.");
            }

            // Verificar si ya existe una nómina pagada para este período
            var nominasExistentes = _nominaRepository.ObtenerPorPeriodo(periodo);
            if (nominasExistentes.Any(n => n.Estado == "Pagada"))
            {
                throw new InvalidOperationException($"Ya existe una nómina pagada para el período {periodo}. No se puede preparar una nueva nómina.");
            }

            // Eliminar nóminas en borrador del período
            var nominasBorrador = nominasExistentes.Where(n => n.Estado == "Pendiente").ToList();
            foreach (var nomina in nominasBorrador)
            {
                _nominaRepository.Eliminar(nomina.Id);
            }

            // Obtener todos los empleados activos
            var empleados = _empleadoRepository.ObtenerTodos(soloActivos: true);

            if (empleados.Count == 0)
            {
                throw new InvalidOperationException("No hay empleados activos para generar la nómina.");
            }

            foreach (var empleado in empleados)
            {
                var nomina = new Nomina
                {
                    EmpleadoId = empleado.Id,
                    Periodo = periodo,
                    SalarioBruto = empleado.SalarioBase,
                    Bonificaciones = 0,
                    Deducciones = 0,
                    SalarioNeto = empleado.SalarioBase,
                    Estado = "Pendiente"
                };

                _nominaRepository.Insertar(nomina);
            }

            return true;
        }

        public bool MarcarComoPagada(int id)
        {
            var nomina = _nominaRepository.ObtenerPorId(id);
            if (nomina == null)
            {
                throw new InvalidOperationException("La nómina no existe.");
            }
            
            if (nomina.Estado == "Pagada")
            {
                throw new InvalidOperationException("Esta nómina ya está marcada como pagada.");
            }
            
            nomina.Estado = "Pagada";
            nomina.FechaPago = DateTime.Now;
            return _nominaRepository.Actualizar(nomina);
        }

        private void ValidarNomina(Nomina nomina)
        {
            if (nomina.EmpleadoId <= 0)
            {
                throw new ArgumentException("El empleado es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(nomina.Periodo))
            {
                throw new ArgumentException("El período es obligatorio.");
            }

            if (nomina.SalarioBruto < 0)
            {
                throw new ArgumentException("El salario bruto no puede ser negativo.");
            }
        }
    }
}

