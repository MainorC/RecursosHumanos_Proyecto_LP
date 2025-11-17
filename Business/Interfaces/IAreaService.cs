using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Business.Interfaces
{
    /// <summary>
    /// Interfaz del servicio de negocio de áreas
    /// </summary>
    public interface IAreaService
    {
        List<Area> ObtenerTodas(bool soloActivas = false);
        Area? ObtenerPorId(int id);
        bool Guardar(Area area);
        bool Eliminar(int id);
    }
}

