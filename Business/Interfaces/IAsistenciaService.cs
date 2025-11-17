using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Business.Interfaces
{
    /// <summary>
    /// Interfaz del servicio de negocio de asistencias
    /// </summary>
    public interface IAsistenciaService
    {
        List<Asistencia> ObtenerTodas();
        List<Asistencia> ObtenerTodas(DateTime? mes);
        Asistencia? ObtenerPorId(int id);
        Asistencia? ObtenerPorEmpleadoYFecha(int empleadoId, DateTime fecha);
        List<Asistencia> ObtenerPorEmpleado(int empleadoId);
        List<Asistencia> ObtenerPorFecha(DateTime fecha);
        bool Guardar(Asistencia asistencia);
        bool Eliminar(int id);
    }
}

