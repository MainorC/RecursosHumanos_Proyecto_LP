using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Presentation.Interfaces
{
    /// <summary>
    /// Interfaz para la vista de gestión de vacaciones (MVP Pattern)
    /// </summary>
    public interface IVacacionView
    {
        // Propiedades para datos del formulario
        int? EmpleadoId { get; set; }
        DateTime FechaInicio { get; set; }
        DateTime FechaFin { get; set; }
        string Estado { get; set; }
        string Motivo { get; set; }
        
        // Métodos para actualizar la vista
        void CargarVacaciones(List<Vacacion> vacaciones);
        void CargarEmpleados(List<Empleado> empleados);
        void MostrarVacacion(Vacacion vacacion);
        void LimpiarFormulario();
        void MostrarMensaje(string mensaje, string titulo, MessageBoxIcon icono);
        void MostrarError(string mensaje, string campo);
        void LimpiarErrores();
        void ActualizarEstadisticas(int pendientes, int aprobadas, int rechazadas);
        
        // Eventos
        event EventHandler CargarDatos;
        event EventHandler GuardarVacacion;
        event EventHandler EliminarVacacion;
        event EventHandler<int> AprobarVacacion;
        event EventHandler<int> RechazarVacacion;
        event EventHandler<int> SeleccionarVacacion;
    }
}

