using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Business.Interfaces
{
    /// <summary>
    /// Interfaz del servicio de negocio de usuarios
    /// </summary>
    public interface IUsuarioService
    {
        List<Usuario> ObtenerTodos(bool soloActivos = false);
        Usuario? ObtenerPorId(int id);
        Usuario? ObtenerPorNombreUsuario(string nombreUsuario);
        bool Guardar(Usuario usuario);
        bool Eliminar(int id);
        Usuario? ValidarCredenciales(string nombreUsuario, string contrasena);
    }
}

