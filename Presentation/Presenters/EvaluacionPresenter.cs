using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Presentation.Interfaces;
using RecursosHumanos.Common.Helpers;
using System.Linq;

namespace RecursosHumanos.Presentation.Presenters
{
    // presenter para evaluaciones
    public class EvaluacionPresenter
    {
        private readonly IEvaluacionView _view;
        private readonly EvaluacionBLL _evaluacionBLL;
        private readonly EmpleadoBLL _empleadoBLL;
        private Evaluacion? _evaluacionActual;

        public EvaluacionPresenter(IEvaluacionView view)
        {
            _view = view;
            _evaluacionBLL = new EvaluacionBLL();
            _empleadoBLL = new EmpleadoBLL();
            
            // suscribir eventos
            _view.CargarDatos += View_CargarDatos;
            _view.GuardarEvaluacion += View_GuardarEvaluacion;
            _view.EliminarEvaluacion += View_EliminarEvaluacion;
            _view.SeleccionarEvaluacion += View_SeleccionarEvaluacion;
        }

        private void View_CargarDatos(object? sender, EventArgs e)
        {
            try
            {
                CargarEmpleados();
                CargarEvaluaciones();
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar datos: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void View_GuardarEvaluacion(object? sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario())
                    return;

                var evaluacion = new Evaluacion
                {
                    Id = _evaluacionActual?.Id ?? 0,
                    EmpleadoId = _view.EmpleadoId ?? 0,
                    Fecha = _view.Fecha,
                    Puntaje = _view.Puntaje,
                    Fortalezas = string.IsNullOrWhiteSpace(_view.Fortalezas) ? null : _view.Fortalezas,
                    OportunidadesMejora = string.IsNullOrWhiteSpace(_view.OportunidadesMejora) ? null : _view.OportunidadesMejora,
                    Comentarios = string.IsNullOrWhiteSpace(_view.Comentarios) ? null : _view.Comentarios
                };

                bool resultado = _evaluacionBLL.Guardar(evaluacion);
                
                if (resultado)
                {
                    string mensaje = _evaluacionActual == null 
                        ? "Evaluación guardada exitosamente." 
                        : "Evaluación actualizada exitosamente.";
                    _view.MostrarMensaje(mensaje, "Éxito", MessageBoxIcon.Information);
                    _view.LimpiarFormulario();
                    _evaluacionActual = null;
                    CargarEvaluaciones();
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
                if (mensajeError.Contains("puntaje debe estar entre"))
                {
                    mensajeError = "El puntaje debe estar entre 0 y 100.";
                }
                _view.MostrarMensaje(mensajeError, "Error", MessageBoxIcon.Error);
            }
        }

        private void View_EliminarEvaluacion(object? sender, EventArgs e)
        {
            if (_evaluacionActual == null)
            {
                _view.MostrarMensaje("Seleccione una evaluación para eliminar.", "Advertencia", MessageBoxIcon.Warning);
                return;
            }

            EliminarEvaluacion(_evaluacionActual.Id);
        }

        private void View_SeleccionarEvaluacion(object? sender, int evaluacionId)
        {
            try
            {
                var evaluacion = _evaluacionBLL.ObtenerPorId(evaluacionId);
                if (evaluacion != null)
                {
                    _evaluacionActual = evaluacion;
                    _view.MostrarEvaluacion(evaluacion);
                }
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar evaluación: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        public void CargarEvaluaciones()
        {
            try
            {
                var evaluaciones = _evaluacionBLL.ObtenerTodas();
                _view.CargarEvaluaciones(evaluaciones);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar evaluaciones: {ex.Message}", "Error", MessageBoxIcon.Error);
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

        public void EliminarEvaluacion(int evaluacionId)
        {
            try
            {
                var evaluacion = _evaluacionBLL.ObtenerPorId(evaluacionId);
                if (evaluacion == null) return;

                var mensaje = $"¿Está seguro que desea eliminar la evaluación del {evaluacion.Fecha:dd/MM/yyyy}?\n\n" +
                             "Esta acción no se puede deshacer.";

                if (UIHelper.MostrarConfirmacion(mensaje, "Confirmar Eliminación") == DialogResult.Yes)
                {
                    _evaluacionBLL.Eliminar(evaluacionId);
                    _view.MostrarMensaje("Evaluación eliminada exitosamente.", "Éxito", MessageBoxIcon.Information);
                    _view.LimpiarFormulario();
                    _evaluacionActual = null;
                    CargarEvaluaciones();
                }
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al eliminar evaluación: {ex.Message}", "Error", MessageBoxIcon.Error);
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

            if (_view.Puntaje < 0 || _view.Puntaje > 100)
            {
                _view.MostrarError("El puntaje debe estar entre 0 y 100", "Puntaje");
                esValido = false;
            }

            return esValido;
        }

        public void CancelarEdicion()
        {
            _evaluacionActual = null;
            _view.LimpiarFormulario();
        }
    }
}

