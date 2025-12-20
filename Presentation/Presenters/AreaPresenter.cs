using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Presentation.Interfaces;
using RecursosHumanos.Common.Helpers;
using System.Linq;

namespace RecursosHumanos.Presentation.Presenters
{
    // presenter para areas
    public class AreaPresenter
    {
        private readonly IAreaView _view;
        private readonly AreaBLL _areaBLL;
        private Area? _areaActual;

        public AreaPresenter(IAreaView view)
        {
            _view = view;
            _areaBLL = new AreaBLL();
            
            // suscribir eventos
            _view.CargarDatos += View_CargarDatos;
            _view.GuardarArea += View_GuardarArea;
            _view.EliminarArea += View_EliminarArea;
            _view.SeleccionarArea += View_SeleccionarArea;
        }

        private void View_CargarDatos(object? sender, EventArgs e)
        {
            try
            {
                CargarAreas();
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar datos: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void View_GuardarArea(object? sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario())
                    return;

                var area = new Area
                {
                    Id = _areaActual?.Id ?? 0,
                    Nombre = _view.Nombre,
                    Descripcion = string.IsNullOrWhiteSpace(_view.Descripcion) ? null : _view.Descripcion,
                    Activo = _view.Activo
                };

                bool resultado = _areaBLL.Guardar(area);
                
                if (resultado)
                {
                    string mensaje = _areaActual == null 
                        ? "Área guardada exitosamente." 
                        : "Área actualizada exitosamente.";
                    _view.MostrarMensaje(mensaje, "Éxito", MessageBoxIcon.Information);
                    _view.LimpiarFormulario();
                    _areaActual = null;
                    CargarAreas();
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
                if (mensajeError.Contains("UNIQUE constraint") || mensajeError.Contains("duplicate") || 
                    mensajeError.Contains("IX_Areas_Nombre") || mensajeError.Contains("unique index"))
                {
                    mensajeError = "Ya existe un área con este nombre (puede estar eliminada). Por favor, elija otro nombre.";
                }
                else if (mensajeError.Contains("FOREIGN KEY") || mensajeError.Contains("constraint"))
                {
                    mensajeError = "No se puede realizar esta operación porque hay datos relacionados.";
                }
                _view.MostrarMensaje(mensajeError, "Error", MessageBoxIcon.Error);
            }
        }

        private void View_EliminarArea(object? sender, EventArgs e)
        {
            if (_areaActual == null)
            {
                _view.MostrarMensaje("Seleccione un área para eliminar.", "Advertencia", MessageBoxIcon.Warning);
                return;
            }

            EliminarArea(_areaActual.Id);
        }

        private void View_SeleccionarArea(object? sender, int areaId)
        {
            try
            {
                var area = _areaBLL.ObtenerPorId(areaId);
                if (area != null)
                {
                    _areaActual = area;
                    _view.MostrarArea(area);
                    _view.ActualizarInterfazModoEdicion(true, area);
                }
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar área: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        public void CargarAreas()
        {
            try
            {
                // Cargar solo áreas activas para mostrar en la lista
                var areas = _areaBLL.ObtenerTodas(soloActivas: true);
                _view.CargarAreas(areas);
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al cargar áreas: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        public void EliminarArea(int areaId)
        {
            try
            {
                var area = _areaBLL.ObtenerPorId(areaId);
                if (area == null) return;

                var mensaje = $"¿Está seguro que desea eliminar el área '{area.Nombre}'?\n\n" +
                             "Esta acción no se puede deshacer.";

                if (UIHelper.MostrarConfirmacion(mensaje, "Confirmar Eliminación") == DialogResult.Yes)
                {
                    bool resultado = _areaBLL.Eliminar(areaId);
                    if (resultado)
                    {
                        _view.MostrarMensaje("Área eliminada exitosamente.", "Éxito", MessageBoxIcon.Information);
                        _view.LimpiarFormulario();
                        _areaActual = null;
                        CargarAreas();
                    }
                    else
                    {
                        _view.MostrarMensaje("No se pudo eliminar el área.", "Error", MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
                if (mensajeError.Contains("empleados activos"))
                {
                    mensajeError = "No se puede eliminar el área porque tiene empleados activos asignados. Primero reasigne los empleados a otra área.";
                }
                _view.MostrarMensaje(mensajeError, "Error", MessageBoxIcon.Error);
            }
        }

        private bool ValidarFormulario()
        {
            _view.LimpiarErrores();
            bool esValido = true;

            if (string.IsNullOrWhiteSpace(_view.Nombre))
            {
                _view.MostrarError("El nombre es obligatorio", "Nombre");
                esValido = false;
            }

            return esValido;
        }

        public void CancelarEdicion()
        {
            _areaActual = null;
            _view.LimpiarFormulario();
            _view.ActualizarInterfazModoEdicion(false);
        }

        public List<Area> BuscarAreas(string criterio)
        {
            try
            {
                // Buscar solo en áreas activas
                var areas = _areaBLL.ObtenerTodas(soloActivas: true);
                if (!string.IsNullOrWhiteSpace(criterio))
                {
                    string criterioLower = criterio.Trim();
                    areas = areas.Where(a => 
      a.Nombre.Contains(criterioLower, StringComparison.OrdinalIgnoreCase) ||
   (a.Descripcion != null && a.Descripcion.Contains(criterioLower, StringComparison.OrdinalIgnoreCase))
         ).ToList();
                }
                return areas;
            }
            catch (Exception ex)
            {
                _view.MostrarMensaje($"Error al buscar áreas: {ex.Message}", "Error", MessageBoxIcon.Error);
                return new List<Area>();
            }
        }
    }
}
