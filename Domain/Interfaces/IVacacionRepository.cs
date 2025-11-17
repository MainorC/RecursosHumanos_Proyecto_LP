using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Domain.Interfaces
{
    /// <summary>
    /// Interfaz del repositorio de vacaciones
    /// </summary>
    public interface IVacacionRepository
    {
        List<Vacacion> ObtenerTodas();
        Vacacion? ObtenerPorId(int id);
        List<Vacacion> ObtenerPorEmpleado(int empleadoId);
        bool Insertar(Vacacion vacacion);
        bool Actualizar(Vacacion vacacion);
        bool Eliminar(int id);
    }
}

