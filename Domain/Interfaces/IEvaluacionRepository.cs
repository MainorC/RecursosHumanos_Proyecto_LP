using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Domain.Interfaces
{
    /// <summary>
    /// Interfaz del repositorio de evaluaciones
    /// </summary>
    public interface IEvaluacionRepository
    {
        List<Evaluacion> ObtenerTodas();
        Evaluacion? ObtenerPorId(int id);
        List<Evaluacion> ObtenerPorEmpleado(int empleadoId);
        bool Insertar(Evaluacion evaluacion);
        bool Actualizar(Evaluacion evaluacion);
        bool Eliminar(int id);
    }
}

