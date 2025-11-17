using RecursosHumanos.Domain.Models;
using RecursosHumanos.Domain.Interfaces;
using RecursosHumanos.Business.Interfaces;
using RecursosHumanos.Common.Helpers;
using System.Linq;

namespace RecursosHumanos.Business.Services
{
    // servicio de negocio para usuarios
    public class UsuarioBLL : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioBLL(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        public UsuarioBLL() : this(new Data.Access.UsuarioDAL())
        {
        }

        public Usuario? ValidarCredenciales(string nombreUsuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                throw new ArgumentException("El nombre de usuario y la contraseña son obligatorios.");
            }

            if (_usuarioRepository.ValidarCredenciales(nombreUsuario, contrasena))
            {
                return _usuarioRepository.ObtenerPorNombreUsuario(nombreUsuario);
            }
            return null;
        }

        public List<Usuario> ObtenerTodos(bool soloActivos = false)
        {
            return _usuarioRepository.ObtenerTodos(soloActivos);
        }

        public Usuario? ObtenerPorId(int id)
        {
            return _usuarioRepository.ObtenerPorId(id);
        }

        public Usuario? ObtenerPorNombreUsuario(string nombreUsuario)
        {
            return _usuarioRepository.ObtenerPorNombreUsuario(nombreUsuario);
        }

        public bool Guardar(Usuario usuario)
        {
            ValidarUsuario(usuario);

            // hashear contraseña si no esta hasheada ya
            if (!PasswordHelper.IsHashed(usuario.Contrasena))
            {
                usuario.Contrasena = PasswordHelper.HashPassword(usuario.Contrasena);
            }

            if (usuario.Id == 0)
            {
                // verificar si el usuario ya existe
                if (_usuarioRepository.ExisteNombreUsuario(usuario.NombreUsuario))
                {
                    throw new InvalidOperationException($"El nombre de usuario '{usuario.NombreUsuario}' ya está registrado en un usuario activo. Si el usuario anterior fue eliminado, puede reutilizar el nombre sin problemas.");
                }
                return _usuarioRepository.Insertar(usuario);
            }
            else
            {
                // al editar verificar que no haya otro con el mismo nombre
                if (_usuarioRepository.ExisteNombreUsuario(usuario.NombreUsuario, usuario.Id))
                {
                    throw new InvalidOperationException($"El nombre de usuario '{usuario.NombreUsuario}' ya está registrado en otro usuario activo.");
                }
                
                // si se actualiza verificar si cambio la contraseña
                var usuarioExistente = _usuarioRepository.ObtenerPorId(usuario.Id);
                if (usuarioExistente != null)
                {
                    // si esta vacia o es la misma mantener la original
                    if (string.IsNullOrWhiteSpace(usuario.Contrasena) || 
                        usuario.Contrasena == usuarioExistente.Contrasena ||
                        PasswordHelper.IsHashed(usuario.Contrasena))
                    {
                        usuario.Contrasena = usuarioExistente.Contrasena;
                    }
                    else
                    {
                        // cambio la contraseña, hashearla
                        usuario.Contrasena = PasswordHelper.HashPassword(usuario.Contrasena);
                    }
                }
                
                return _usuarioRepository.Actualizar(usuario);
            }
        }

        public bool Eliminar(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID de usuario inválido.");
            }
            
            // verificar que no sea el ultimo usuario activo
            var usuarios = _usuarioRepository.ObtenerTodos(soloActivos: true);
            var usuariosActivos = usuarios.Where(u => u.Activo && u.Id != id).ToList();
            if (usuariosActivos.Count == 0)
            {
                throw new InvalidOperationException("No se puede eliminar el usuario. Debe haber al menos un usuario activo en el sistema.");
            }
            
            return _usuarioRepository.Eliminar(id);
        }

        private void ValidarUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario))
            {
                throw new ArgumentException("El nombre de usuario es obligatorio.");
            }

            // contraseña obligatoria solo al crear, al editar si esta vacia se mantiene la original
            if (usuario.Id == 0 && string.IsNullOrWhiteSpace(usuario.Contrasena))
            {
                throw new ArgumentException("La contraseña es obligatoria para nuevos usuarios.");
            }

            if (string.IsNullOrWhiteSpace(usuario.NombreCompleto))
            {
                throw new ArgumentException("El nombre completo es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Rol))
            {
                throw new ArgumentException("El rol es obligatorio.");
            }
        }
    }
}

