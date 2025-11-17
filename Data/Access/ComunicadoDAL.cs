using Microsoft.EntityFrameworkCore;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Domain.Interfaces;
using System.Linq;

namespace RecursosHumanos.Data.Access
{
    /// <summary>
    /// Implementación del repositorio de comunicados usando Entity Framework Core (Data Access Layer)
    /// </summary>
    public class ComunicadoDAL : IComunicadoRepository
    {
        public List<Comunicado> ObtenerTodos(bool soloActivos = false)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var query = context.Comunicados.AsQueryable();
                
                if (soloActivos)
                {
                    query = query.Where(c => c.Activo);
                }
                
                return query.OrderByDescending(c => c.FechaPublicacion).ToList();
            }
        }

        public Comunicado? ObtenerPorId(int id)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                return context.Comunicados.Find(id);
            }
        }

        public bool Insertar(Comunicado comunicado)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                context.Comunicados.Add(comunicado);
                return context.SaveChanges() > 0;
            }
        }

        public bool Actualizar(Comunicado comunicado)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var comunicadoExistente = context.Comunicados.Find(comunicado.Id);
                if (comunicadoExistente == null)
                    return false;

                comunicadoExistente.Titulo = comunicado.Titulo;
                comunicadoExistente.Contenido = comunicado.Contenido;
                comunicadoExistente.Activo = comunicado.Activo;

                return context.SaveChanges() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var comunicado = context.Comunicados.Find(id);
                if (comunicado == null)
                    return false;

                context.Comunicados.Remove(comunicado);
                return context.SaveChanges() > 0;
            }
        }

        public bool CambiarEstado(int id, bool activo)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var comunicado = context.Comunicados.Find(id);
                if (comunicado == null)
                    return false;

                comunicado.Activo = activo;
                return context.SaveChanges() > 0;
            }
        }
    }
}
