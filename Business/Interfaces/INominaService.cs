using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Business.Interfaces
{
    /// <summary>
    /// Interfaz del servicio de negocio de nóminas
    /// </summary>
    public interface INominaService
    {
        List<Nomina> ObtenerTodas();
        Nomina? ObtenerPorId(int id);
        List<Nomina> ObtenerPorEmpleado(int empleadoId);
        List<Nomina> ObtenerPorPeriodo(string periodo);
        bool Guardar(Nomina nomina);
        bool Eliminar(int id);
    }
}

