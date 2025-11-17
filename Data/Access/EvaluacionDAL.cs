using Microsoft.EntityFrameworkCore;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Domain.Interfaces;
using System.Linq;

namespace RecursosHumanos.Data.Access
{
    /// <summary>
    /// Implementación del repositorio de evaluaciones usando Entity Framework Core (Data Access Layer)
    /// </summary>
    public class EvaluacionDAL : IEvaluacionRepository
    {
        public List<Evaluacion> ObtenerTodas()
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var evaluaciones = context.Evaluaciones
                    .Include(e => e.Empleado)
                    .OrderByDescending(e => e.Fecha)
                    .ToList();

                // Asignar NombreEmpleado desde la relación
                foreach (var evaluacion in evaluaciones)
                {
                    evaluacion.NombreEmpleado = evaluacion.Empleado?.NombreCompleto;
                }

                return evaluaciones;
            }
        }

        // Método adicional para compatibilidad con BLL
        public List<Evaluacion> ObtenerTodas(int? empleadoId)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var query = context.Evaluaciones
                    .Include(e => e.Empleado)
                    .AsQueryable();

                if (empleadoId.HasValue)
                {
                    query = query.Where(e => e.EmpleadoId == empleadoId.Value);
                }

                var evaluaciones = query
                    .OrderByDescending(e => e.Fecha)
                    .ToList();

                // Asignar NombreEmpleado desde la relación
                foreach (var evaluacion in evaluaciones)
                {
                    evaluacion.NombreEmpleado = evaluacion.Empleado?.NombreCompleto;
                }

                return evaluaciones;
            }
        }

        public Evaluacion? ObtenerPorId(int id)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var evaluacion = context.Evaluaciones
                    .Include(e => e.Empleado)
                    .FirstOrDefault(e => e.Id == id);

                if (evaluacion != null)
                {
                    evaluacion.NombreEmpleado = evaluacion.Empleado?.NombreCompleto;
                }

                return evaluacion;
            }
        }

        public List<Evaluacion> ObtenerPorEmpleado(int empleadoId)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var evaluaciones = context.Evaluaciones
                    .Include(e => e.Empleado)
                    .Where(e => e.EmpleadoId == empleadoId)
                    .OrderByDescending(e => e.Fecha)
                    .ToList();

                // Asignar NombreEmpleado desde la relación
                foreach (var evaluacion in evaluaciones)
                {
                    evaluacion.NombreEmpleado = evaluacion.Empleado?.NombreCompleto;
                }

                return evaluaciones;
            }
        }

        public bool Insertar(Evaluacion evaluacion)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                context.Evaluaciones.Add(evaluacion);
                return context.SaveChanges() > 0;
            }
        }

        public bool Actualizar(Evaluacion evaluacion)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var evaluacionExistente = context.Evaluaciones.Find(evaluacion.Id);
                if (evaluacionExistente == null)
                    return false;

                evaluacionExistente.EmpleadoId = evaluacion.EmpleadoId;
                evaluacionExistente.Fecha = evaluacion.Fecha;
                evaluacionExistente.Puntaje = evaluacion.Puntaje;
                evaluacionExistente.Fortalezas = evaluacion.Fortalezas;
                evaluacionExistente.OportunidadesMejora = evaluacion.OportunidadesMejora;
                evaluacionExistente.Comentarios = evaluacion.Comentarios;

                return context.SaveChanges() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var evaluacion = context.Evaluaciones.Find(id);
                if (evaluacion == null)
                    return false;

                context.Evaluaciones.Remove(evaluacion);
                return context.SaveChanges() > 0;
            }
        }
    }
}
