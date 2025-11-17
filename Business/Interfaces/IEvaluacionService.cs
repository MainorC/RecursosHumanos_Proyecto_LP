using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Business.Interfaces
{
    /// <summary>
    /// Interfaz del servicio de negocio de evaluaciones
    /// </summary>
    public interface IEvaluacionService
    {
        List<Evaluacion> ObtenerTodas();
        Evaluacion? ObtenerPorId(int id);
        List<Evaluacion> ObtenerPorEmpleado(int empleadoId);
        bool Guardar(Evaluacion evaluacion);
        bool Eliminar(int id);
    }
}

