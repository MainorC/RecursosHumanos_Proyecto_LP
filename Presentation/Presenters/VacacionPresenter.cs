using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Presentation.Interfaces;
using RecursosHumanos.Common.Helpers;
using System.Linq;

namespace RecursosHumanos.Presentation.Presenters
{
    // presenter para gestion de vacaciones
    public class VacacionPresenter
    {
        private readonly IVacacionView _view;
        private readonly VacacionBLL _vacacionBLL;
        private readonly EmpleadoBLL _empleadoBLL;
        private Vacacion? _vacacionActual;

        public VacacionPresenter(IVacacionView view)
        {
            _view = view;
            _vacacionBLL = new VacacionBLL();
            _empleadoBLL = new EmpleadoBLL();
            
            // suscribir eventos
            _view.CargarDatos += View_CargarDatos;
            _view.GuardarVacacion += View_GuardarVacacion;
            _view.EliminarVacacion += View_EliminarVacacion;
            _view.AprobarVacacion += View_AprobarVacacion;
            _view.RechazarVacacion += View_RechazarVacacion;
            _view.SeleccionarVacacion += View_SeleccionarVacacion;
        }

        private void View_CargarDatos(object? sender, EventArgs e)
        {
            try
            {
                CargarEmpleados();
                CargarVacaciones();
                ActualizarEstadisticas();
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar datos: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void View_GuardarVacacion(object? sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario())
                    return;

                var vacacion = new Vacacion
                {
                    Id = _vacacionActual?.Id ?? 0,
                    EmpleadoId = _view.EmpleadoId ?? 0,
                    FechaInicio = _view.FechaInicio,
                    FechaFin = _view.FechaFin,
                    Estado = _view.Estado ?? "Pendiente"
                };

                bool resultado = _vacacionBLL.Guardar(vacacion);
                
                if (resultado)
                {
                    string mensaje = _vacacionActual == null 
                        ? "Solicitud de vacaciones creada exitosamente." 
                        : "Solicitud de vacaciones actualizada exitosamente.";
                    _view.MostrarMensaje(mensaje, "Éxito", MessageBoxIcon.Information);
                    _view.LimpiarFormulario();
                    _vacacionActual = null;
                    CargarVacaciones();
                    ActualizarEstadisticas();
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
                if (mensajeError.Contains("días de vacaciones disponibles"))
                {
                    // Mantener el mensaje original que es claro
                }
                else if (mensajeError.Contains("solapa"))
                {
                    mensajeError = "Ya existe una solicitud de vacaciones aprobada o pendiente que se solapa con las fechas seleccionadas.";
                }
                else if (mensajeError.Contains("Solo se pueden modificar"))
                {
                    mensajeError = "Solo se pueden modificar solicitudes de vacaciones en estado 'Pendiente'.";
                }
                _view.MostrarMensaje(mensajeError, "Error", MessageBoxIcon.Error);
            }
        }

        private void View_EliminarVacacion(object? sender, EventArgs e)
        {
            if (_vacacionActual == null)
            {
                _view.MostrarMensaje("Seleccione una solicitud de vacaciones para eliminar.", "Advertencia", MessageBoxIcon.Warning);
                return;
            }

            EliminarVacacion(_vacacionActual.Id);
        }

        private void View_AprobarVacacion(object? sender, int vacacionId)
        {
            try
            {
                var vacacion = _vacacionBLL.ObtenerPorId(vacacionId);
                if (vacacion == null)
                {
                    _view.MostrarMensaje("No se encontró la solicitud de vacaciones.", "Error", MessageBoxIcon.Error);
                    return;
                }

                var mensaje = $"¿Está seguro que desea aprobar la solicitud de vacaciones de {vacacion.NombreEmpleado}?\n\n" +
                             $"Período: {vacacion.FechaInicio:dd/MM/yyyy} - {vacacion.FechaFin:dd/MM/yyyy}\n" +
                             $"Días: {vacacion.DiasTotales}";
                
                if (UIHelper.MostrarConfirmacion(mensaje, "Confirmar Aprobación") == DialogResult.Yes)
                {
                    bool resultado = _vacacionBLL.Aprobar(vacacionId);
                    if (resultado)
                    {
                        _view.MostrarMensaje("Solicitud de vacaciones aprobada exitosamente.", "Éxito", MessageBoxIcon.Information);
                        CargarVacaciones();
                        ActualizarEstadisticas();
                    }
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
                if (mensajeError.Contains("Solo se pueden aprobar"))
                {
                    mensajeError = "Solo se pueden aprobar solicitudes en estado 'Pendiente'.";
                }
                _view.MostrarMensaje(mensajeError, "Error", MessageBoxIcon.Error);
            }
        }

        private void View_RechazarVacacion(object? sender, int vacacionId)
        {
            try
            {
                var vacacion = _vacacionBLL.ObtenerPorId(vacacionId);
                if (vacacion == null)
                {
                    _view.MostrarMensaje("No se encontró la solicitud de vacaciones.", "Error", MessageBoxIcon.Error);
                    return;
                }

                var mensaje = $"¿Está seguro que desea rechazar la solicitud de vacaciones de {vacacion.NombreEmpleado}?\n\n" +
                             $"Período: {vacacion.FechaInicio:dd/MM/yyyy} - {vacacion.FechaFin:dd/MM/yyyy}\n" +
                             $"Días: {vacacion.DiasTotales}\n\n" +
                             "Esta acción no se puede deshacer.";
                
                if (UIHelper.MostrarConfirmacion(mensaje, "Confirmar Rechazo") == DialogResult.Yes)
                {
                    bool resultado = _vacacionBLL.Rechazar(vacacionId);
                    if (resultado)
                    {
                        _view.MostrarMensaje("Solicitud de vacaciones rechazada exitosamente.", "Éxito", MessageBoxIcon.Information);
                        CargarVacaciones();
                        ActualizarEstadisticas();
                    }
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
                if (mensajeError.Contains("Solo se pueden rechazar"))
                {
                    mensajeError = "Solo se pueden rechazar solicitudes en estado 'Pendiente'.";
                }
                _view.MostrarMensaje(mensajeError, "Error", MessageBoxIcon.Error);
            }
        }

        private void View_SeleccionarVacacion(object? sender, int vacacionId)
        {
            try
            {
                var vacacion = _vacacionBLL.ObtenerPorId(vacacionId);
                if (vacacion != null)
                {
                    _vacacionActual = vacacion;
                    _view.MostrarVacacion(vacacion);
                }
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar vacación: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        public void CargarVacaciones()
        {
            try
            {
                var vacaciones = _vacacionBLL.ObtenerTodas();
                _view.CargarVacaciones(vacaciones);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar vacaciones: {ex.Message}", "Error", MessageBoxIcon.Error);
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

        public void EliminarVacacion(int vacacionId)
        {
            try
            {
                var vacacion = _vacacionBLL.ObtenerPorId(vacacionId);
                if (vacacion == null) return;

                var mensaje = $"¿Está seguro que desea eliminar la solicitud de vacaciones del {vacacion.FechaInicio:dd/MM/yyyy} al {vacacion.FechaFin:dd/MM/yyyy}?\n\n" +
                             "Esta acción no se puede deshacer.";

                if (UIHelper.MostrarConfirmacion(mensaje, "Confirmar Eliminación") == DialogResult.Yes)
                {
                    _vacacionBLL.Eliminar(vacacionId);
                    _view.MostrarMensaje("Solicitud de vacaciones eliminada exitosamente.", "Éxito", MessageBoxIcon.Information);
                    _view.LimpiarFormulario();
                    _vacacionActual = null;
                    CargarVacaciones();
                    ActualizarEstadisticas();
                }
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al eliminar vacación: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        public void AprobarVacacion(int vacacionId)
        {
            View_AprobarVacacion(this, vacacionId);
        }

        public void RechazarVacacion(int vacacionId)
        {
            View_RechazarVacacion(this, vacacionId);
        }

        public int ObtenerDiasDisponibles(int empleadoId)
        {
            try
            {
                return _vacacionBLL.CalcularDiasDisponibles(empleadoId);
            }
            catch
            {
                return 0;
            }
        }

        public int ObtenerDiasUsados(int empleadoId)
        {
            try
            {
                return _vacacionBLL.CalcularDiasUsados(empleadoId);
            }
            catch
            {
                return 0;
            }
        }

        public List<Vacacion> ObtenerPorEmpleado(int empleadoId)
        {
            try
            {
                return _vacacionBLL.ObtenerPorEmpleado(empleadoId);
            }
            catch
            {
                return new List<Vacacion>();
            }
        }

        private void ActualizarEstadisticas()
        {
            try
            {
                var vacaciones = _vacacionBLL.ObtenerTodas();
                int pendientes = vacaciones.Count(v => v.Estado == "Pendiente");
                int aprobadas = vacaciones.Count(v => v.Estado == "Aprobado");
                int rechazadas = vacaciones.Count(v => v.Estado == "Rechazado");
                
                _view.ActualizarEstadisticas(pendientes, aprobadas, rechazadas);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al actualizar estadísticas: {ex.Message}", "Error", MessageBoxIcon.Error);
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

            if (_view.FechaFin < _view.FechaInicio)
            {
                _view.MostrarError("La fecha de fin debe ser posterior a la fecha de inicio", "FechaFin");
                esValido = false;
            }

            return esValido;
        }

        public void CancelarEdicion()
        {
            _vacacionActual = null;
            _view.LimpiarFormulario();
        }
    }
}

