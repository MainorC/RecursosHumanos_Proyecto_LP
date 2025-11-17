using Microsoft.EntityFrameworkCore;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Domain.Interfaces;
using System.Linq;

namespace RecursosHumanos.Data.Access
{
    /// <summary>
    /// Implementación del repositorio de incorporaciones usando Entity Framework Core (Data Access Layer)
    /// </summary>
    public class IncorporacionDAL : IIncorporacionRepository
    {
        public List<Incorporacion> ObtenerTodas()
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var incorporaciones = context.Incorporacion
                    .Include(i => i.Tareas)
                    .OrderByDescending(i => i.FechaCreacion)
                    .ToList();

                // Recalcular contadores desde las tareas
                foreach (var incorporacion in incorporaciones)
                {
                    incorporacion.TareasCompletadas = incorporacion.Tareas.Count(t => t.Completada);
                    incorporacion.TotalTareas = incorporacion.Tareas.Count;
                }

                return incorporaciones;
            }
        }

        public Incorporacion? ObtenerPorId(int id)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var incorporacion = context.Incorporacion
                    .Include(i => i.Tareas)
                    .FirstOrDefault(i => i.Id == id);

                if (incorporacion != null)
                {
                    incorporacion.TareasCompletadas = incorporacion.Tareas.Count(t => t.Completada);
                    incorporacion.TotalTareas = incorporacion.Tareas.Count;
                }

                return incorporacion;
            }
        }

        public List<Incorporacion> ObtenerPorEmpleado(int empleadoId)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var incorporaciones = context.Incorporacion
                    .Include(i => i.Tareas)
                    .Where(i => i.EmpleadoId == empleadoId)
                    .OrderByDescending(i => i.FechaCreacion)
                    .ToList();

                // Recalcular contadores desde las tareas
                foreach (var incorporacion in incorporaciones)
                {
                    incorporacion.TareasCompletadas = incorporacion.Tareas.Count(t => t.Completada);
                    incorporacion.TotalTareas = incorporacion.Tareas.Count;
                }

                return incorporaciones;
            }
        }

        public bool Insertar(Incorporacion incorporacion)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                context.Incorporacion.Add(incorporacion);
                var resultado = context.SaveChanges() > 0;
                // EF Core actualiza automáticamente el ID después de SaveChanges
                return resultado;
            }
        }

        public bool Actualizar(Incorporacion incorporacion)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var incorporacionExistente = context.Incorporacion
                    .Include(i => i.Tareas)
                    .FirstOrDefault(i => i.Id == incorporacion.Id);

                if (incorporacionExistente == null)
                    return false;

                // Actualizar campos básicos
                incorporacionExistente.Estado = incorporacion.Estado;
                incorporacionExistente.FechaFin = incorporacion.FechaFin;
                incorporacionExistente.NombreEmpleado = incorporacion.NombreEmpleado;
                incorporacionExistente.FechaInicio = incorporacion.FechaInicio;
                
                // Recalcular contadores desde las tareas en la base de datos
                incorporacionExistente.TareasCompletadas = incorporacionExistente.Tareas.Count(t => t.Completada);
                incorporacionExistente.TotalTareas = incorporacionExistente.Tareas.Count;

                return context.SaveChanges() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var incorporacion = context.Incorporacion
                    .Include(i => i.Tareas)
                    .FirstOrDefault(i => i.Id == id);

                if (incorporacion == null)
                    return false;

                // Eliminar tareas primero (EF maneja esto automáticamente con cascade delete)
                context.TareasIncorporacion.RemoveRange(incorporacion.Tareas);
                context.Incorporacion.Remove(incorporacion);

                return context.SaveChanges() > 0;
            }
        }

        // Métodos adicionales para tareas (usados por el código existente)
        public List<TareaIncorporacion> ObtenerTareas(int incorporacionId)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                return context.TareasIncorporacion
                    .Where(t => t.IncorporacionId == incorporacionId)
                    .OrderBy(t => t.Id)
                    .ToList();
            }
        }

        public bool InsertarTarea(TareaIncorporacion tarea)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                context.TareasIncorporacion.Add(tarea);
                return context.SaveChanges() > 0;
            }
        }

        public bool ActualizarTarea(TareaIncorporacion tarea)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var tareaExistente = context.TareasIncorporacion.Find(tarea.Id);
                if (tareaExistente == null)
                    return false;

                tareaExistente.Completada = tarea.Completada;
                tareaExistente.FechaCompletada = tarea.FechaCompletada;

                return context.SaveChanges() > 0;
            }
        }
    }
}
