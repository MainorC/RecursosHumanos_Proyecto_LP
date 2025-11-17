using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Presentation.Interfaces
{
    /// <summary>
    /// Interfaz para la vista de gestión de nómina (MVP Pattern)
    /// </summary>
    public interface INominaView
    {
        // Propiedades para datos del formulario
        string Periodo { get; set; }
        
        // Métodos para actualizar la vista
        void CargarNomina(List<Nomina> nominas);
        void MostrarMensaje(string mensaje, string titulo, MessageBoxIcon icono);
        void MostrarError(string mensaje, string campo);
        void LimpiarErrores();
        
        // Eventos
        event EventHandler CargarDatos;
        event EventHandler<string> PrepararNomina;
        event EventHandler<int> MarcarComoPagada;
        event EventHandler<int> SeleccionarNomina;
    }
}

