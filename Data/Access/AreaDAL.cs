using Microsoft.EntityFrameworkCore;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Domain.Interfaces;
using System.Linq;

namespace RecursosHumanos.Data.Access
{
    // repositorio de areas
    public class AreaDAL : IAreaRepository
    {
        public List<Area> ObtenerTodas(bool soloActivas = false)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var query = context.Areas.AsQueryable();
                
                if (soloActivas)
                {
                    query = query.Where(a => a.Activo);
                }
                
                return query.OrderBy(a => a.Nombre).ToList();
            }
        }

        public Area? ObtenerPorId(int id)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                return context.Areas.Find(id);
            }
        }

        public bool Insertar(Area area)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                // Verificar si existe un área inactiva con el mismo nombre
                var areaExistente = context.Areas
                       .FirstOrDefault(a => a.Nombre == area.Nombre && !a.Activo);
                
                if (areaExistente != null)
                {
                    // Reactivar el área existente en lugar de crear una nueva
                    areaExistente.Descripcion = area.Descripcion;
                    areaExistente.Activo = true;
                    return context.SaveChanges() > 0;
                }
                        
                context.Areas.Add(area);
                return context.SaveChanges() > 0;
            }
        }

        public bool Actualizar(Area area)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var areaExistente = context.Areas.Find(area.Id);
                if (areaExistente == null)
                    return false;

                areaExistente.Nombre = area.Nombre;
                areaExistente.Descripcion = area.Descripcion;
                areaExistente.Activo = area.Activo;

                return context.SaveChanges() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            // Soft delete - marcar como inactivo
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var area = context.Areas.Find(id);
                if (area == null)
                    return false;

                area.Activo = false;
                return context.SaveChanges() > 0;
            }
        }

        public bool ExisteNombre(string nombre, int? idExcluir = null)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                // Solo verificar entre las áreas ACTIVAS
                var query = context.Areas.Where(a => a.Nombre == nombre && a.Activo);
                
                if (idExcluir.HasValue)
                {
                    query = query.Where(a => a.Id != idExcluir.Value);
                }
                    
                return query.Any();
            }
        }
    }
}

