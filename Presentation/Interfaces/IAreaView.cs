using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Presentation.Interfaces
{
    /// <summary>
    /// Interfaz para la vista de gestión de áreas (MVP Pattern)
    /// </summary>
    public interface IAreaView
    {
        // Propiedades para datos del formulario
        string Nombre { get; set; }
        string Descripcion { get; set; }
        bool Activo { get; set; }
        
        // Métodos para actualizar la vista
        void CargarAreas(List<Area> areas);
        void MostrarArea(Area area);
        void LimpiarFormulario();
        void MostrarMensaje(string mensaje, string titulo, MessageBoxIcon icono);
        void MostrarError(string mensaje, string campo);
        void LimpiarErrores();
        void ActualizarInterfazModoEdicion(bool esEdicion, Area? area = null);
        
        // Eventos
        event EventHandler CargarDatos;
        event EventHandler GuardarArea;
        event EventHandler EliminarArea;
        event EventHandler<int> SeleccionarArea;
    }
}

