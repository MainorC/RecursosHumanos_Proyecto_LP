using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Domain.Interfaces
{
    /// <summary>
    /// Interfaz del repositorio de asistencias
    /// </summary>
    public interface IAsistenciaRepository
    {
        List<Asistencia> ObtenerTodas();
        Asistencia? ObtenerPorId(int id);
        List<Asistencia> ObtenerPorEmpleado(int empleadoId);
        List<Asistencia> ObtenerPorFecha(DateTime fecha);
        bool Insertar(Asistencia asistencia);
        bool Actualizar(Asistencia asistencia);
        bool Eliminar(int id);
    }
}

