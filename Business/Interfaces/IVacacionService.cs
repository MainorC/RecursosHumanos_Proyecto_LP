using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Business.Interfaces
{
    /// <summary>
    /// Interfaz del servicio de negocio de vacaciones
    /// </summary>
    public interface IVacacionService
    {
        List<Vacacion> ObtenerTodas();
        Vacacion? ObtenerPorId(int id);
        List<Vacacion> ObtenerPorEmpleado(int empleadoId);
        bool Guardar(Vacacion vacacion);
        bool Eliminar(int id);
        int CalcularDiasDisponibles(int empleadoId);
        int CalcularDiasUsados(int empleadoId);
    }
}

