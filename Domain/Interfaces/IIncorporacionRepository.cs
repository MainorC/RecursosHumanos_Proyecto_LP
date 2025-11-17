using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Domain.Interfaces
{
    /// <summary>
    /// Interfaz del repositorio de incorporaciones
    /// </summary>
    public interface IIncorporacionRepository
    {
        List<Incorporacion> ObtenerTodas();
        Incorporacion? ObtenerPorId(int id);
        List<Incorporacion> ObtenerPorEmpleado(int empleadoId);
        bool Insertar(Incorporacion incorporacion);
        bool Actualizar(Incorporacion incorporacion);
        bool Eliminar(int id);
    }
}

