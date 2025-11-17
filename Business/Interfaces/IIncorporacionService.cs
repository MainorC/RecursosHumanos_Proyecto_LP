using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Business.Interfaces
{
    /// <summary>
    /// Interfaz del servicio de negocio de incorporaciones
    /// </summary>
    public interface IIncorporacionService
    {
        List<Incorporacion> ObtenerTodas();
        Incorporacion? ObtenerPorId(int id);
        List<Incorporacion> ObtenerPorEmpleado(int empleadoId);
        bool Guardar(Incorporacion incorporacion);
        bool Eliminar(int id);
    }
}

