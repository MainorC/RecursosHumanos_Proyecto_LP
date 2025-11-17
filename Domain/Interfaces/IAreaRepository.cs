using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Domain.Interfaces
{
    /// <summary>
    /// Interfaz del repositorio de áreas
    /// </summary>
    public interface IAreaRepository
    {
        List<Area> ObtenerTodas(bool soloActivas = false);
        Area? ObtenerPorId(int id);
        bool Insertar(Area area);
        bool Actualizar(Area area);
        bool Eliminar(int id);
        bool ExisteNombre(string nombre, int? idExcluir = null);
    }
}

