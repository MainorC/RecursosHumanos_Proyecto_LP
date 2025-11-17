using Microsoft.EntityFrameworkCore;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Domain.Interfaces;
using System.Linq;

namespace RecursosHumanos.Data.Access
{
    // repositorio de vacaciones
    public class VacacionDAL : IVacacionRepository
    {
        public List<Vacacion> ObtenerTodas()
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var vacaciones = context.Vacaciones
                    .Include(v => v.Empleado)
                    .OrderByDescending(v => v.FechaCreacion)
                    .ToList();

                // Asignar NombreEmpleado desde la relación
                foreach (var vacacion in vacaciones)
                {
                    vacacion.NombreEmpleado = vacacion.Empleado?.NombreCompleto;
                }

                return vacaciones;
            }
        }

        public Vacacion? ObtenerPorId(int id)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var vacacion = context.Vacaciones
                    .Include(v => v.Empleado)
                    .FirstOrDefault(v => v.Id == id);

                if (vacacion != null)
                {
                    vacacion.NombreEmpleado = vacacion.Empleado?.NombreCompleto;
                }

                return vacacion;
            }
        }

        public List<Vacacion> ObtenerPorEmpleado(int empleadoId)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var vacaciones = context.Vacaciones
                    .Include(v => v.Empleado)
                    .Where(v => v.EmpleadoId == empleadoId)
                    .OrderByDescending(v => v.FechaCreacion)
                    .ToList();

                // Asignar NombreEmpleado desde la relación
                foreach (var vacacion in vacaciones)
                {
                    vacacion.NombreEmpleado = vacacion.Empleado?.NombreCompleto;
                }

                return vacaciones;
            }
        }

        public bool Insertar(Vacacion vacacion)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                context.Vacaciones.Add(vacacion);
                return context.SaveChanges() > 0;
            }
        }

        public bool Actualizar(Vacacion vacacion)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var vacacionExistente = context.Vacaciones.Find(vacacion.Id);
                if (vacacionExistente == null)
                    return false;

                vacacionExistente.EmpleadoId = vacacion.EmpleadoId;
                vacacionExistente.FechaInicio = vacacion.FechaInicio;
                vacacionExistente.FechaFin = vacacion.FechaFin;
                vacacionExistente.DiasTotales = vacacion.DiasTotales;
                vacacionExistente.Estado = vacacion.Estado;
                if (vacacion.FechaAprobacion.HasValue)
                {
                    vacacionExistente.FechaAprobacion = vacacion.FechaAprobacion;
                }

                return context.SaveChanges() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var vacacion = context.Vacaciones.Find(id);
                if (vacacion == null)
                    return false;

                context.Vacaciones.Remove(vacacion);
                return context.SaveChanges() > 0;
            }
        }

        public bool ActualizarEstado(int id, string estado)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var vacacion = context.Vacaciones.Find(id);
                if (vacacion == null)
                    return false;

                vacacion.Estado = estado;
                if (estado == "Aprobado")
                {
                    vacacion.FechaAprobacion = DateTime.Now;
                }

                return context.SaveChanges() > 0;
            }
        }

        public bool ExistenVacacionesSolapadas(int empleadoId, DateTime fechaInicio, DateTime fechaFin, int? idExcluir = null)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var query = context.Vacaciones
                    .Where(v => v.EmpleadoId == empleadoId
                        && v.Estado != "Rechazado"
                        && ((fechaInicio.Date >= v.FechaInicio.Date && fechaInicio.Date <= v.FechaFin.Date)
                            || (fechaFin.Date >= v.FechaInicio.Date && fechaFin.Date <= v.FechaFin.Date)
                            || (v.FechaInicio.Date >= fechaInicio.Date && v.FechaInicio.Date <= fechaFin.Date)
                            || (v.FechaFin.Date >= fechaInicio.Date && v.FechaFin.Date <= fechaFin.Date)));

                if (idExcluir.HasValue)
                {
                    query = query.Where(v => v.Id != idExcluir.Value);
                }

                return query.Any();
            }
        }

        public List<Vacacion> ObtenerPorEmpleadoYAnio(int empleadoId, int anio)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                return context.Vacaciones
                    .Where(v => v.EmpleadoId == empleadoId
                        && v.FechaInicio.Year == anio
                        && v.Estado == "Aprobado")
                    .OrderBy(v => v.FechaInicio)
                    .ToList();
            }
        }

        public int CalcularDiasUsados(int empleadoId, int anio)
        {
            var vacaciones = ObtenerPorEmpleadoYAnio(empleadoId, anio);
            return vacaciones.Sum(v => v.DiasTotales);
        }
    }
}
