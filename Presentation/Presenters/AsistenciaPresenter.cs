using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Presentation.Interfaces;
using RecursosHumanos.Common.Helpers;
using System.Linq;

namespace RecursosHumanos.Presentation.Presenters
{
    // Presenter pra la gestion de asistencias
    public class AsistenciaPresenter
    {
        private readonly IAsistenciaView _view;
        private readonly AsistenciaBLL _asistenciaBLL;
        private readonly EmpleadoBLL _empleadoBLL;
        private Asistencia? _asistenciaActual;

        public AsistenciaPresenter(IAsistenciaView view)
        {
            _view = view;
            _asistenciaBLL = new AsistenciaBLL();
            _empleadoBLL = new EmpleadoBLL();
            
            // suscribir eventos
            _view.CargarDatos += View_CargarDatos;
            _view.GuardarAsistencia += View_GuardarAsistencia;
            _view.EliminarAsistencia += View_EliminarAsistencia;
            _view.SeleccionarAsistencia += View_SeleccionarAsistencia;
        }

        private void View_CargarDatos(object? sender, EventArgs e)
        {
            try
            {
                CargarEmpleados();
                CargarAsistencias();
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar datos: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void View_GuardarAsistencia(object? sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario())
                    return;

                var asistencia = new Asistencia
                {
                    Id = _asistenciaActual?.Id ?? 0,
                    EmpleadoId = _view.EmpleadoId ?? 0,
                    Fecha = _view.Fecha,
                    HoraEntrada = _view.HoraEntrada,
                    HoraSalida = _view.HoraSalida,
                    Estado = _view.Estado
                };

                bool resultado = _asistenciaBLL.Guardar(asistencia);
                
                if (resultado)
                {
                    string mensaje = _asistenciaActual == null 
                        ? "Asistencia registrada exitosamente." 
                        : "Asistencia actualizada exitosamente.";
                    _view.MostrarMensaje(mensaje, "Éxito", MessageBoxIcon.Information);
                    _view.LimpiarFormulario();
                    _asistenciaActual = null;
                    CargarAsistencias();
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
                if (mensajeError.Contains("Ya existe un registro"))
                {
                    mensajeError = "Ya existe un registro de asistencia para este empleado en esta fecha.";
                }
                else if (mensajeError.Contains("hora de salida sin una hora de entrada"))
                {
                    mensajeError = "No se puede registrar una hora de salida sin una hora de entrada.";
                }
                else if (mensajeError.Contains("exceder 24 horas"))
                {
                    mensajeError = "Las horas trabajadas no pueden exceder 24 horas. Verifique las horas de entrada y salida.";
                }
                _view.MostrarMensaje(mensajeError, "Error", MessageBoxIcon.Error);
            }
        }

        private void View_EliminarAsistencia(object? sender, EventArgs e)
        {
            if (_asistenciaActual == null)
            {
                _view.MostrarMensaje("Seleccione un registro de asistencia para eliminar.", "Advertencia", MessageBoxIcon.Warning);
                return;
            }

            EliminarAsistencia(_asistenciaActual.Id);
        }

        private void View_SeleccionarAsistencia(object? sender, int asistenciaId)
        {
            try
            {
                var asistencia = _asistenciaBLL.ObtenerPorId(asistenciaId);
                if (asistencia != null)
                {
                    _asistenciaActual = asistencia;
                    _view.MostrarAsistencia(asistencia);
                }
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar asistencia: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        public void CargarAsistencias()
        {
            try
            {
                var asistencias = _asistenciaBLL.ObtenerTodas();
                _view.CargarAsistencias(asistencias);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar asistencias: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        public void CargarAsistenciasPorMes(DateTime mes)
        {
            try
            {
                var asistencias = _asistenciaBLL.ObtenerTodas(mes);
                _view.CargarAsistencias(asistencias);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar asistencias: {ex.Message}", "Error", MessageBoxIcon.Error);
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

        public void EliminarAsistencia(int asistenciaId)
        {
            try
            {
                var asistencia = _asistenciaBLL.ObtenerPorId(asistenciaId);
                if (asistencia == null) return;

                var mensaje = $"¿Está seguro que desea eliminar el registro de asistencia del {asistencia.Fecha:dd/MM/yyyy}?\n\n" +
                             "Esta acción no se puede deshacer.";

                if (UIHelper.MostrarConfirmacion(mensaje, "Confirmar Eliminación") == DialogResult.Yes)
                {
                    _asistenciaBLL.Eliminar(asistenciaId);
                    _view.MostrarMensaje("Asistencia eliminada exitosamente.", "Éxito", MessageBoxIcon.Information);
                    _view.LimpiarFormulario();
                    _asistenciaActual = null;
                    CargarAsistencias();
                }
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al eliminar asistencia: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private bool ValidarFormulario()
        {
            _view.LimpiarErrores();
            bool esValido = true;

            if (!_view.EmpleadoId.HasValue || _view.EmpleadoId.Value <= 0)
            {
                _view.MostrarError("Debe seleccionar un empleado", "EmpleadoId");
                esValido = false;
            }

            return esValido;
        }

        public void CancelarEdicion()
        {
            _asistenciaActual = null;
            _view.LimpiarFormulario();
        }
    }
}

