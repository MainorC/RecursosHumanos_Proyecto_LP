using Microsoft.EntityFrameworkCore;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Domain.Interfaces;
using RecursosHumanos.Common.Helpers;
using System.Linq;

namespace RecursosHumanos.Data.Access
{
    // repositorio de usuarios con EF
    public class UsuarioDAL : IUsuarioRepository
    {
        public Usuario? ObtenerPorId(int id)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                return context.Usuarios.Find(id);
            }
        }

        public Usuario? ObtenerPorNombreUsuario(string nombreUsuario)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                return context.Usuarios
                    .FirstOrDefault(u => u.NombreUsuario == nombreUsuario);
            }
        }

        public bool ValidarCredenciales(string nombreUsuario, string contrasena)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var usuario = context.Usuarios
                    .FirstOrDefault(u => u.NombreUsuario == nombreUsuario && u.Activo);
                
                if (usuario == null)
                {
                    return false;
                }

                // verificar si esta hasheada o en texto plano
                if (PasswordHelper.IsHashed(usuario.Contrasena))
                {
                    return PasswordHelper.VerifyPassword(contrasena, usuario.Contrasena);
                }
                else
                {
                    // texto plano, comparar directamente (migracion)
                    bool esValida = usuario.Contrasena == contrasena;
                    
                    // si es valida hashearla automaticamente
                    if (esValida)
                    {
                        usuario.Contrasena = PasswordHelper.HashPassword(contrasena);
                        context.SaveChanges();
                    }
                    
                    return esValida;
                }
            }
        }

        public List<Usuario> ObtenerTodos(bool soloActivos = false)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var query = context.Usuarios.AsQueryable();
                
                if (soloActivos)
                {
                    query = query.Where(u => u.Activo);
                }
                
                return query.OrderBy(u => u.NombreCompleto).ToList();
            }
        }

        public bool Insertar(Usuario usuario)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                context.Usuarios.Add(usuario);
                return context.SaveChanges() > 0;
            }
        }

        public bool Actualizar(Usuario usuario)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var usuarioExistente = context.Usuarios.Find(usuario.Id);
                if (usuarioExistente == null)
                    return false;

                usuarioExistente.NombreUsuario = usuario.NombreUsuario;
                usuarioExistente.Contrasena = usuario.Contrasena;
                usuarioExistente.NombreCompleto = usuario.NombreCompleto;
                usuarioExistente.Rol = usuario.Rol;
                usuarioExistente.Activo = usuario.Activo;

                return context.SaveChanges() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            // Soft delete - marcar como inactivo
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var usuario = context.Usuarios.Find(id);
                if (usuario == null)
                    return false;

                usuario.Activo = false;
                return context.SaveChanges() > 0;
            }
        }

        public bool ExisteNombreUsuario(string nombreUsuario, int? idExcluir = null)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var query = context.Usuarios.Where(u => u.NombreUsuario == nombreUsuario && u.Activo);
                
                if (idExcluir.HasValue)
                {
                    query = query.Where(u => u.Id != idExcluir.Value);
                }
                
                return query.Any();
            }
        }
    }
}
