using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Domain.Interfaces
{
    /// <summary>
    /// Interfaz del repositorio de empleados
    /// </summary>
    public interface IEmpleadoRepository
    {
        List<Empleado> ObtenerTodos(bool soloActivos = false);
        Empleado? ObtenerPorId(int id);
        List<Empleado> Buscar(string criterio);
        bool Insertar(Empleado empleado);
        bool Actualizar(Empleado empleado);
        bool Eliminar(int id);
        bool ExisteDNI(string dni, int? idExcluir = null);
        bool ExistenEmpleadosActivosPorArea(int areaId);
    }
}

