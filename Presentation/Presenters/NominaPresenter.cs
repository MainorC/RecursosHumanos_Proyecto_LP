using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Presentation.Interfaces;
using RecursosHumanos.Common.Helpers;
using System.Linq;

namespace RecursosHumanos.Presentation.Presenters
{
    // presenter para nomina
    public class NominaPresenter
    {
        private readonly INominaView _view;
        private readonly NominaBLL _nominaBLL;

        public NominaPresenter(INominaView view)
        {
            _view = view;
            _nominaBLL = new NominaBLL();
            
            // suscribir eventos
            _view.CargarDatos += View_CargarDatos;
            _view.PrepararNomina += View_PrepararNomina;
            _view.MarcarComoPagada += View_MarcarComoPagada;
            _view.SeleccionarNomina += View_SeleccionarNomina;
        }

        private void View_CargarDatos(object? sender, EventArgs e)
        {
            try
            {
                CargarNomina();
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar datos: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void View_PrepararNomina(object? sender, string periodo)
        {
            try
            {
                bool resultado = _nominaBLL.PrepararNomina(periodo);
                if (resultado)
                {
                    _view.MostrarMensaje($"Nómina preparada exitosamente para el período {periodo}.", "Éxito", MessageBoxIcon.Information);
                    CargarNomina();
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
                if (mensajeError.Contains("Ya existe una nómina pagada"))
                {
                    mensajeError = $"Ya existe una nómina pagada para el período {periodo}. No se puede preparar una nueva nómina.";
                }
                else if (mensajeError.Contains("No hay empleados activos"))
                {
                    mensajeError = "No hay empleados activos para generar la nómina.";
                }
                _view.MostrarMensaje(mensajeError, "Error", MessageBoxIcon.Error);
            }
        }

        private void View_MarcarComoPagada(object? sender, int nominaId)
        {
            try
            {
                var mensaje = "¿Está seguro que desea marcar esta nómina como pagada?";
                if (UIHelper.MostrarConfirmacion(mensaje, "Confirmar Pago") == DialogResult.Yes)
                {
                    bool resultado = _nominaBLL.MarcarComoPagada(nominaId);
                    if (resultado)
                    {
                        _view.MostrarMensaje("Nómina marcada como pagada exitosamente.", "Éxito", MessageBoxIcon.Information);
                        CargarNomina();
                    }
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
                if (mensajeError.Contains("ya está marcada como pagada"))
                {
                    mensajeError = "Esta nómina ya está marcada como pagada.";
                }
                _view.MostrarMensaje(mensajeError, "Error", MessageBoxIcon.Error);
            }
        }

        private void View_SeleccionarNomina(object? sender, int nominaId)
        {
            try
            {
                var nomina = _nominaBLL.ObtenerPorId(nominaId);
                if (nomina != null)
                {
                    // Abrir formulario de edición
                    // Esto se maneja desde la vista
                }
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar nómina: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        public void CargarNomina()
        {
            try
            {
                var periodo = _view.Periodo;
                List<Nomina> nominas;
                
                if (string.IsNullOrWhiteSpace(periodo))
                {
                    nominas = _nominaBLL.ObtenerTodas();
                }
                else
                {
                    nominas = _nominaBLL.ObtenerPorPeriodo(periodo);
                }
                
                _view.CargarNomina(nominas);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar nómina: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        public void CargarNominaPorPeriodo(string periodo)
        {
            try
            {
                var nominas = _nominaBLL.ObtenerPorPeriodo(periodo);
                _view.CargarNomina(nominas);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar nómina: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }
    }
}

