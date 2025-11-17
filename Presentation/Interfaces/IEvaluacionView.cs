using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Presentation.Interfaces
{
    /// <summary>
    /// Interfaz para la vista de gestión de evaluaciones (MVP Pattern)
    /// </summary>
    public interface IEvaluacionView
    {
        // Propiedades para datos del formulario
        int? EmpleadoId { get; set; }
        DateTime Fecha { get; set; }
        int Puntaje { get; set; }
        string Fortalezas { get; set; }
        string OportunidadesMejora { get; set; }
        string Comentarios { get; set; }
        
        // Métodos para actualizar la vista
        void CargarEvaluaciones(List<Evaluacion> evaluaciones);
        void CargarEmpleados(List<Empleado> empleados);
        void MostrarEvaluacion(Evaluacion evaluacion);
        void LimpiarFormulario();
        void MostrarMensaje(string mensaje, string titulo, MessageBoxIcon icono);
        void MostrarError(string mensaje, string campo);
        void LimpiarErrores();
        
        // Eventos
        event EventHandler CargarDatos;
        event EventHandler GuardarEvaluacion;
        event EventHandler EliminarEvaluacion;
        event EventHandler<int> SeleccionarEvaluacion;
    }
}

