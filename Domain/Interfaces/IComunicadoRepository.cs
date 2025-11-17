using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Domain.Interfaces
{
    /// <summary>
    /// Interfaz del repositorio de comunicados
    /// </summary>
    public interface IComunicadoRepository
    {
        List<Comunicado> ObtenerTodos(bool soloActivos = false);
        Comunicado? ObtenerPorId(int id);
        bool Insertar(Comunicado comunicado);
        bool Actualizar(Comunicado comunicado);
        bool Eliminar(int id);
        bool CambiarEstado(int id, bool activo);
    }
}

