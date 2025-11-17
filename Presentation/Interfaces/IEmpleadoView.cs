using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Presentation.Interfaces
{
    /// <summary>
    /// Interfaz para la vista de gestión de empleados (MVP Pattern)
    /// </summary>
    public interface IEmpleadoView
    {
        // Propiedades para datos del formulario
        string DNI { get; set; }
        string Nombre { get; set; }
        string Apellido { get; set; }
        string Email { get; set; }
        string Telefono { get; set; }
        DateTime? FechaNacimiento { get; set; }
        string Direccion { get; set; }
        int? AreaId { get; set; }
        string Puesto { get; set; }
        string TipoContrato { get; set; }
        DateTime? FechaContrato { get; set; }
        decimal SalarioBase { get; set; }
        string SistemaPension { get; set; }
        string Estado { get; set; }
        
        // Métodos para actualizar la vista
        void CargarEmpleados(List<Empleado> empleados);
        void CargarAreas(List<Area> areas);
        void MostrarEmpleado(Empleado empleado);
        void LimpiarFormulario();
        void MostrarMensaje(string mensaje, string titulo, MessageBoxIcon icono);
        void MostrarError(string mensaje, string campo);
        void LimpiarErrores();
        void ActualizarInterfazModoEdicion(bool esEdicion, Empleado? empleado = null);
        
        // Eventos
        event EventHandler CargarDatos;
        event EventHandler GuardarEmpleado;
        event EventHandler EliminarEmpleado;
        event EventHandler BuscarEmpleado;
        event EventHandler<int> SeleccionarEmpleado;
    }
}

