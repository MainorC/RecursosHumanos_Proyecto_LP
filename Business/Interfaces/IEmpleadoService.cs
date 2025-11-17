using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Business.Interfaces
{
    /// <summary>
    /// Interfaz del servicio de negocio de empleados
    /// </summary>
    public interface IEmpleadoService
    {
        List<Empleado> ObtenerTodos(bool soloActivos = false);
        Empleado? ObtenerPorId(int id);
        List<Empleado> Buscar(string criterio);
        bool Guardar(Empleado empleado);
        bool Eliminar(int id);
    }
}

