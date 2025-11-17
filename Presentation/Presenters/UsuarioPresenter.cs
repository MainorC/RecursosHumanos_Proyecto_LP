using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Presentation.Interfaces;
using RecursosHumanos.Common.Helpers;

namespace RecursosHumanos.Presentation.Presenters
{
    // presenter para usuarios
    public class UsuarioPresenter
    {
        private readonly IUsuarioView _view;
        private readonly UsuarioBLL _usuarioBLL;
        private Usuario? _usuarioActual;

        public UsuarioPresenter(IUsuarioView view)
        {
            _view = view;
            _usuarioBLL = new UsuarioBLL();
            
            // suscribir eventos
            _view.CargarDatos += View_CargarDatos;
            _view.GuardarUsuario += View_GuardarUsuario;
            _view.EliminarUsuario += View_EliminarUsuario;
            _view.SeleccionarUsuario += View_SeleccionarUsuario;
        }

        private void View_CargarDatos(object? sender, EventArgs e)
        {
            try
            {
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar datos: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void View_GuardarUsuario(object? sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario())
                    return;

                var usuario = new Usuario
                {
                    Id = _usuarioActual?.Id ?? 0,
                    NombreUsuario = _view.NombreUsuario,
                    Contrasena = _view.Contrasena,
                    NombreCompleto = _view.NombreCompleto,
                    Rol = _view.Rol,
                    Activo = _view.Activo
                };

                bool resultado = _usuarioBLL.Guardar(usuario);
                
                if (resultado)
                {
                    string mensaje = _usuarioActual == null 
                        ? "Usuario guardado exitosamente." 
                        : "Usuario actualizado exitosamente.";
                    _view.MostrarMensaje(mensaje, "Éxito", MessageBoxIcon.Information);
                    _view.LimpiarFormulario();
                    _usuarioActual = null;
                    CargarUsuarios();
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
                if (mensajeError.Contains("UNIQUE constraint") || mensajeError.Contains("duplicate"))
                {
                    mensajeError = "Ya existe un usuario con este nombre de usuario. Por favor, elija otro nombre.";
                }
                else if (mensajeError.Contains("último usuario activo"))
                {
                    mensajeError = "No se puede eliminar el usuario. Debe haber al menos un usuario activo en el sistema.";
                }
                _view.MostrarMensaje(mensajeError, "Error", MessageBoxIcon.Error);
            }
        }

        private void View_EliminarUsuario(object? sender, EventArgs e)
        {
            if (_usuarioActual == null)
            {
                _view.MostrarMensaje("Seleccione un usuario para eliminar.", "Advertencia", MessageBoxIcon.Warning);
                return;
            }

            EliminarUsuario(_usuarioActual.Id);
        }

        private void View_SeleccionarUsuario(object? sender, int usuarioId)
        {
            try
            {
                var usuario = _usuarioBLL.ObtenerPorId(usuarioId);
                if (usuario != null)
                {
                    _usuarioActual = usuario;
                    _view.MostrarUsuario(usuario);
                    _view.ActualizarInterfazModoEdicion(true, usuario);
                }
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar usuario: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        public void CargarUsuarios()
        {
            try
            {
                var usuarios = _usuarioBLL.ObtenerTodos(soloActivos: true);
                _view.CargarUsuarios(usuarios);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar usuarios: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        public void EliminarUsuario(int usuarioId)
        {
            try
            {
                var usuario = _usuarioBLL.ObtenerPorId(usuarioId);
                if (usuario == null) return;

                // Validar que no sea el usuario admin
                if (usuario.NombreUsuario.Equals("admin", StringComparison.OrdinalIgnoreCase))
                {
                    _view.MostrarMensaje("No se puede eliminar el usuario administrador.", "Advertencia", MessageBoxIcon.Warning);
                    return;
                }

                var mensaje = $"¿Está seguro que desea eliminar al usuario '{usuario.NombreUsuario}'?\n\n" +
                             "Esta acción no se puede deshacer.";

                if (UIHelper.MostrarConfirmacion(mensaje, "Confirmar Eliminación") == DialogResult.Yes)
                {
                    _usuarioBLL.Eliminar(usuarioId);
                    _view.MostrarMensaje("Usuario eliminado exitosamente.", "Éxito", MessageBoxIcon.Information);
                    _view.LimpiarFormulario();
                    _usuarioActual = null;
                    CargarUsuarios();
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
                if (mensajeError.Contains("último usuario activo"))
                {
                    mensajeError = "No se puede eliminar el usuario. Debe haber al menos un usuario activo en el sistema.";
                }
                else if (mensajeError.Contains("FOREIGN KEY") || mensajeError.Contains("constraint"))
                {
                    mensajeError = "No se puede eliminar este usuario porque tiene información relacionada.";
                }
                _view.MostrarMensaje(mensajeError, "Error", MessageBoxIcon.Error);
            }
        }

        private bool ValidarFormulario()
        {
            _view.LimpiarErrores();
            bool esValido = true;

            if (string.IsNullOrWhiteSpace(_view.NombreUsuario))
            {
                _view.MostrarError("El nombre de usuario es obligatorio", "NombreUsuario");
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(_view.Contrasena))
            {
                _view.MostrarError("La contraseña es obligatoria", "Contrasena");
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(_view.NombreCompleto))
            {
                _view.MostrarError("El nombre completo es obligatorio", "NombreCompleto");
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(_view.Rol))
            {
                _view.MostrarError("El rol es obligatorio", "Rol");
                esValido = false;
            }

            return esValido;
        }

        public void CancelarEdicion()
        {
            _usuarioActual = null;
            _view.LimpiarFormulario();
            _view.ActualizarInterfazModoEdicion(false);
        }
    }
}

