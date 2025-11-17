using Microsoft.EntityFrameworkCore;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Domain.Interfaces;
using System.Linq;

namespace RecursosHumanos.Data.Access
{
    /// <summary>
    /// Implementación del repositorio de asistencias usando Entity Framework Core (Data Access Layer)
    /// </summary>
    public class AsistenciaDAL : IAsistenciaRepository
    {
        public List<Asistencia> ObtenerTodas()
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var asistencias = context.Asistencia
                    .Include(a => a.Empleado)
                    .OrderByDescending(a => a.Fecha)
                    .ThenBy(a => a.Empleado.Nombre)
                    .ToList();

                // Asignar NombreEmpleado desde la relación
                foreach (var asistencia in asistencias)
                {
                    asistencia.NombreEmpleado = asistencia.Empleado?.NombreCompleto;
                }

                return asistencias;
            }
        }

        // Método adicional para compatibilidad con BLL
        public List<Asistencia> ObtenerTodas(DateTime? mes)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var query = context.Asistencia
                    .Include(a => a.Empleado)
                    .AsQueryable();

                if (mes.HasValue)
                {
                    query = query.Where(a => a.Fecha.Year == mes.Value.Year && a.Fecha.Month == mes.Value.Month);
                }

                var asistencias = query
                    .OrderByDescending(a => a.Fecha)
                    .ThenBy(a => a.Empleado.Nombre)
                    .ToList();

                // Asignar NombreEmpleado desde la relación
                foreach (var asistencia in asistencias)
                {
                    asistencia.NombreEmpleado = asistencia.Empleado?.NombreCompleto;
                }

                return asistencias;
            }
        }

        public Asistencia? ObtenerPorEmpleadoYFecha(int empleadoId, DateTime fecha)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var fechaDate = fecha.Date;
                return context.Asistencia
                    .Include(a => a.Empleado)
                    .FirstOrDefault(a => a.EmpleadoId == empleadoId && a.Fecha.Date == fechaDate);
            }
        }

        public Asistencia? ObtenerPorId(int id)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var asistencia = context.Asistencia
                    .Include(a => a.Empleado)
                    .FirstOrDefault(a => a.Id == id);

                if (asistencia != null)
                {
                    asistencia.NombreEmpleado = asistencia.Empleado?.NombreCompleto;
                }

                return asistencia;
            }
        }

        public List<Asistencia> ObtenerPorEmpleado(int empleadoId)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var asistencias = context.Asistencia
                    .Include(a => a.Empleado)
                    .Where(a => a.EmpleadoId == empleadoId)
                    .OrderByDescending(a => a.Fecha)
                    .ToList();

                // Asignar NombreEmpleado desde la relación
                foreach (var asistencia in asistencias)
                {
                    asistencia.NombreEmpleado = asistencia.Empleado?.NombreCompleto;
                }

                return asistencias;
            }
        }

        public List<Asistencia> ObtenerPorFecha(DateTime fecha)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var fechaDate = fecha.Date;
                var asistencias = context.Asistencia
                    .Include(a => a.Empleado)
                    .Where(a => a.Fecha.Date == fechaDate)
                    .OrderBy(a => a.Empleado.Nombre)
                    .ToList();

                // Asignar NombreEmpleado desde la relación
                foreach (var asistencia in asistencias)
                {
                    asistencia.NombreEmpleado = asistencia.Empleado?.NombreCompleto;
                }

                return asistencias;
            }
        }

        public bool Insertar(Asistencia asistencia)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                asistencia.Fecha = asistencia.Fecha.Date;
                context.Asistencia.Add(asistencia);
                return context.SaveChanges() > 0;
            }
        }

        public bool Actualizar(Asistencia asistencia)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var asistenciaExistente = context.Asistencia.Find(asistencia.Id);
                if (asistenciaExistente == null)
                    return false;

                asistenciaExistente.HoraEntrada = asistencia.HoraEntrada;
                asistenciaExistente.HoraSalida = asistencia.HoraSalida;
                asistenciaExistente.HorasTrabajadas = asistencia.HorasTrabajadas;
                asistenciaExistente.Estado = asistencia.Estado;

                return context.SaveChanges() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var asistencia = context.Asistencia.Find(id);
                if (asistencia == null)
                    return false;

                context.Asistencia.Remove(asistencia);
                return context.SaveChanges() > 0;
            }
        }
    }
}
