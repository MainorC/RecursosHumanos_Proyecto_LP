using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Domain.Interfaces
{
    /// <summary>
    /// Interfaz del repositorio de nóminas
    /// </summary>
    public interface INominaRepository
    {
        List<Nomina> ObtenerTodas();
        Nomina? ObtenerPorId(int id);
        List<Nomina> ObtenerPorEmpleado(int empleadoId);
        List<Nomina> ObtenerPorPeriodo(string periodo);
        bool Insertar(Nomina nomina);
        bool Actualizar(Nomina nomina);
        bool Eliminar(int id);
    }
}

