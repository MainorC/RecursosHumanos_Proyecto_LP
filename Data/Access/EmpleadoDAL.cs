using Microsoft.EntityFrameworkCore;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Domain.Interfaces;
using System.Linq;

namespace RecursosHumanos.Data.Access
{
    // repositorio de empleados
    public class EmpleadoDAL : IEmpleadoRepository
    {
        public List<Empleado> ObtenerTodos(bool soloActivos = false)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var query = context.Empleados
                    .Include(e => e.Area)
                    .AsQueryable();

                if (soloActivos)
                {
                    query = query.Where(e => e.Activo);
                }

                var empleados = query.OrderBy(e => e.Nombre).ThenBy(e => e.Apellido).ToList();
                
                // Asignar NombreArea desde la relación
                foreach (var empleado in empleados)
                {
                    empleado.NombreArea = empleado.Area?.Nombre;
                }

                return empleados;
            }
        }

        public Empleado? ObtenerPorId(int id)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var empleado = context.Empleados
                    .Include(e => e.Area)
                    .FirstOrDefault(e => e.Id == id);

                if (empleado != null)
                {
                    empleado.NombreArea = empleado.Area?.Nombre;
                }

                return empleado;
            }
        }

        public List<Empleado> Buscar(string criterio)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var query = context.Empleados
                    .Include(e => e.Area)
                    .Where(e => e.Activo && (
                        e.DNI.Contains(criterio) ||
                        e.Nombre.Contains(criterio) ||
                        e.Apellido.Contains(criterio) ||
                        e.Email.Contains(criterio) ||
                        (e.Area != null && e.Area.Nombre.Contains(criterio))
                    ));

                var empleados = query.OrderBy(e => e.Nombre).ThenBy(e => e.Apellido).ToList();
                
                // Asignar NombreArea desde la relación
                foreach (var empleado in empleados)
                {
                    empleado.NombreArea = empleado.Area?.Nombre;
                }

                return empleados;
            }
        }

        public bool Insertar(Empleado empleado)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                context.Empleados.Add(empleado);
                return context.SaveChanges() > 0;
            }
        }

        public bool Actualizar(Empleado empleado)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var empleadoExistente = context.Empleados.Find(empleado.Id);
                if (empleadoExistente == null)
                    return false;

                // Actualizar propiedades
                empleadoExistente.DNI = empleado.DNI;
                empleadoExistente.Nombre = empleado.Nombre;
                empleadoExistente.Apellido = empleado.Apellido;
                empleadoExistente.Email = empleado.Email;
                empleadoExistente.Telefono = empleado.Telefono;
                empleadoExistente.FechaNacimiento = empleado.FechaNacimiento;
                empleadoExistente.Direccion = empleado.Direccion;
                empleadoExistente.AreaId = empleado.AreaId;
                empleadoExistente.Puesto = empleado.Puesto;
                empleadoExistente.TipoContrato = empleado.TipoContrato;
                empleadoExistente.FechaContrato = empleado.FechaContrato;
                empleadoExistente.SalarioBase = empleado.SalarioBase;
                empleadoExistente.SistemaPension = empleado.SistemaPension;
                empleadoExistente.Estado = empleado.Estado;
                empleadoExistente.Activo = empleado.Activo;

                return context.SaveChanges() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            // Soft delete - marcar como inactivo
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var empleado = context.Empleados.Find(id);
                if (empleado == null)
                    return false;

                empleado.Activo = false;
                return context.SaveChanges() > 0;
            }
        }

        public bool ExisteDNI(string dni, int? idExcluir = null)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var query = context.Empleados.Where(e => e.DNI == dni && e.Activo);
                
                if (idExcluir.HasValue)
                {
                    query = query.Where(e => e.Id != idExcluir.Value);
                }
                
                return query.Any();
            }
        }

        public bool ExistenEmpleadosActivosPorArea(int areaId)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                return context.Empleados
                    .Any(e => e.AreaId == areaId && e.Activo);
            }
        }
    }
}
