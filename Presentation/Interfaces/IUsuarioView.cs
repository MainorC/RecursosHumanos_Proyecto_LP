using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Presentation.Interfaces
{
    /// <summary>
    /// Interfaz para la vista de gestión de usuarios (MVP Pattern)
    /// </summary>
    public interface IUsuarioView
    {
        // Propiedades para datos del formulario
        string NombreUsuario { get; set; }
        string Contrasena { get; set; }
        string NombreCompleto { get; set; }
        string Rol { get; set; }
        bool Activo { get; set; }
        
        // Métodos para actualizar la vista
        void CargarUsuarios(List<Usuario> usuarios);
        void MostrarUsuario(Usuario usuario);
        void LimpiarFormulario();
        void MostrarMensaje(string mensaje, string titulo, MessageBoxIcon icono);
        void MostrarError(string mensaje, string campo);
        void LimpiarErrores();
        void ActualizarInterfazModoEdicion(bool esEdicion, Usuario? usuario = null);
        
        // Eventos
        event EventHandler CargarDatos;
        event EventHandler GuardarUsuario;
        event EventHandler EliminarUsuario;
        event EventHandler<int> SeleccionarUsuario;
    }
}

