using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Business.Interfaces
{
    /// <summary>
    /// Interfaz del servicio de negocio de comunicados
    /// </summary>
    public interface IComunicadoService
    {
        List<Comunicado> ObtenerTodos(bool soloActivos = false);
        Comunicado? ObtenerPorId(int id);
        bool Guardar(Comunicado comunicado);
        bool Eliminar(int id);
        bool CambiarEstado(int id, bool activo);
    }
}

