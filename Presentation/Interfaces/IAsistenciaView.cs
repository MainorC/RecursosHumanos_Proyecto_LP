using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Presentation.Interfaces
{
    /// <summary>
    /// Interfaz para la vista de gestión de asistencias (MVP Pattern)
    /// </summary>
    public interface IAsistenciaView
    {
        // Propiedades para datos del formulario
        int? EmpleadoId { get; set; }
        DateTime Fecha { get; set; }
        TimeSpan? HoraEntrada { get; set; }
        TimeSpan? HoraSalida { get; set; }
        string Estado { get; set; }
        
        // Métodos para actualizar la vista
        void CargarAsistencias(List<Asistencia> asistencias);
        void CargarEmpleados(List<Empleado> empleados);
        void MostrarAsistencia(Asistencia asistencia);
        void LimpiarFormulario();
        void MostrarMensaje(string mensaje, string titulo, MessageBoxIcon icono);
        void MostrarError(string mensaje, string campo);
        void LimpiarErrores();
        
        // Eventos
        event EventHandler CargarDatos;
        event EventHandler GuardarAsistencia;
        event EventHandler EliminarAsistencia;
        event EventHandler<int> SeleccionarAsistencia;
    }
}

