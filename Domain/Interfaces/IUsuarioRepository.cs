using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Domain.Interfaces
{
    /// <summary>
    /// Interfaz del repositorio de usuarios
    /// </summary>
    public interface IUsuarioRepository
    {
        List<Usuario> ObtenerTodos(bool soloActivos = false);
        Usuario? ObtenerPorId(int id);
        Usuario? ObtenerPorNombreUsuario(string nombreUsuario);
        bool Insertar(Usuario usuario);
        bool Actualizar(Usuario usuario);
        bool Eliminar(int id);
        bool ExisteNombreUsuario(string nombreUsuario, int? idExcluir = null);
        bool ValidarCredenciales(string nombreUsuario, string contrasena);
    }
}

