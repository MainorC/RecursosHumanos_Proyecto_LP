using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Presentation.Interfaces;
using RecursosHumanos.Common.Helpers;

namespace RecursosHumanos.Presentation.Presenters
{
    // presenter para empleados
    public class EmpleadoPresenter
    {
        private readonly IEmpleadoView _view;
        private readonly EmpleadoBLL _empleadoBLL;
        private readonly AreaBLL _areaBLL;
        private Empleado? _empleadoActual;

        public EmpleadoPresenter(IEmpleadoView view)
        {
            _view = view;
            _empleadoBLL = new EmpleadoBLL();
            _areaBLL = new AreaBLL();
            
            // suscribir eventos
            _view.CargarDatos += View_CargarDatos;
            _view.GuardarEmpleado += View_GuardarEmpleado;
            _view.EliminarEmpleado += View_EliminarEmpleado;
            _view.BuscarEmpleado += View_BuscarEmpleado;
            _view.SeleccionarEmpleado += View_SeleccionarEmpleado;
        }

        private void View_CargarDatos(object? sender, EventArgs e)
        {
            try
            {
                CargarEmpleados();
                CargarAreas();
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar datos: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void View_GuardarEmpleado(object? sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario())
                    return;

                var empleado = new Empleado
                {
                    Id = _empleadoActual?.Id ?? 0,
                    DNI = _view.DNI,
                    Nombre = _view.Nombre,
                    Apellido = _view.Apellido,
                    Email = _view.Email,
                    Telefono = string.IsNullOrWhiteSpace(_view.Telefono) ? null : _view.Telefono,
                    FechaNacimiento = _view.FechaNacimiento,
                    Direccion = string.IsNullOrWhiteSpace(_view.Direccion) ? null : _view.Direccion,
                    AreaId = _view.AreaId > 0 ? _view.AreaId : null,
                    Puesto = string.IsNullOrWhiteSpace(_view.Puesto) ? null : _view.Puesto,
                    TipoContrato = string.IsNullOrWhiteSpace(_view.TipoContrato) ? null : _view.TipoContrato,
                    FechaContrato = _view.FechaContrato,
                    SalarioBase = _view.SalarioBase,
                    SistemaPension = string.IsNullOrWhiteSpace(_view.SistemaPension) ? null : _view.SistemaPension,
                    Estado = _view.Estado
                };

                bool resultado = _empleadoBLL.Guardar(empleado);
                
                if (resultado)
                {
                    string mensaje = _empleadoActual == null 
                        ? "Empleado guardado exitosamente." 
                        : "Empleado actualizado exitosamente.";
                    _view.MostrarMensaje(mensaje, "Éxito", MessageBoxIcon.Information);
                    _view.LimpiarFormulario();
                    _empleadoActual = null;
                    CargarEmpleados();
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
                if (mensajeError.Contains("UNIQUE constraint") || mensajeError.Contains("duplicate"))
                {
                    mensajeError = "Ya existe un empleado con este DNI. Por favor, verifique los datos.";
                }
                else if (mensajeError.Contains("FOREIGN KEY") || mensajeError.Contains("constraint"))
                {
                    mensajeError = "No se puede realizar esta operación porque hay información relacionada. Contacte al administrador.";
                }
                _view.MostrarMensaje(mensajeError, "Error", MessageBoxIcon.Error);
            }
        }

        private void View_EliminarEmpleado(object? sender, EventArgs e)
        {
            // La eliminación se maneja desde la vista directamente
        }

        private void View_BuscarEmpleado(object? sender, EventArgs e)
        {
            // La búsqueda se maneja desde la vista directamente
        }

        private void View_SeleccionarEmpleado(object? sender, int empleadoId)
        {
            try
            {
                var empleado = _empleadoBLL.ObtenerPorId(empleadoId);
                if (empleado != null)
                {
                    _empleadoActual = empleado;
                    _view.MostrarEmpleado(empleado);
                    _view.ActualizarInterfazModoEdicion(true, empleado);
                }
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar empleado: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        public void CargarEmpleados()
        {
            try
            {
                var empleados = _empleadoBLL.ObtenerTodos(soloActivos: true);
                _view.CargarEmpleados(empleados);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar empleados: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        public void CargarAreas()
        {
            try
            {
                var areas = _areaBLL.ObtenerTodas(soloActivas: true);
                _view.CargarAreas(areas);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar áreas: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        public void EliminarEmpleado(int empleadoId)
        {
            try
            {
                var empleado = _empleadoBLL.ObtenerPorId(empleadoId);
                if (empleado == null) return;

                var mensaje = $"¿Está seguro que desea eliminar al empleado {empleado.NombreCompleto}?\n\n" +
                             "Esta acción no se puede deshacer.";

                if (UIHelper.MostrarConfirmacion(mensaje, "Confirmar Eliminación") == DialogResult.Yes)
                {
                    _empleadoBLL.Eliminar(empleadoId);
                    _view.MostrarMensaje("Empleado eliminado exitosamente.", "Éxito", MessageBoxIcon.Information);
                    _view.LimpiarFormulario();
                    _empleadoActual = null;
                    CargarEmpleados();
                }
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al eliminar empleado: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        public List<Empleado> BuscarEmpleados(string criterio)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(criterio))
                {
                    return _empleadoBLL.ObtenerTodos(soloActivos: true);
                }
                return _empleadoBLL.Buscar(criterio);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al buscar empleados: {ex.Message}", "Error", MessageBoxIcon.Error);
                return new List<Empleado>();
            }
        }

        private bool ValidarFormulario()
        {
            _view.LimpiarErrores();
            bool esValido = true;

            if (string.IsNullOrWhiteSpace(_view.DNI))
            {
                _view.MostrarError("El DNI es obligatorio", "DNI");
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(_view.Nombre))
            {
                _view.MostrarError("El nombre es obligatorio", "Nombre");
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(_view.Apellido))
            {
                _view.MostrarError("El apellido es obligatorio", "Apellido");
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(_view.Email))
            {
                _view.MostrarError("El email es obligatorio", "Email");
                esValido = false;
            }

            if (_view.SalarioBase < 0)
            {
                _view.MostrarError("El salario base debe ser mayor o igual a cero", "SalarioBase");
                esValido = false;
            }

            return esValido;
        }

        public void CancelarEdicion()
        {
            _empleadoActual = null;
            _view.LimpiarFormulario();
            _view.ActualizarInterfazModoEdicion(false);
        }
    }
}

