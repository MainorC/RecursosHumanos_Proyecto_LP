using Microsoft.EntityFrameworkCore;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Domain.Interfaces;
using System.Linq;

namespace RecursosHumanos.Data.Access
{
    /// <summary>
    /// Implementación del repositorio de nóminas usando Entity Framework Core (Data Access Layer)
    /// </summary>
    public class NominaDAL : INominaRepository
    {
        public List<Nomina> ObtenerTodas()
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var nominas = context.Nomina
                    .Include(n => n.Empleado)
                    .OrderByDescending(n => n.Periodo)
                    .ThenBy(n => n.Empleado.Nombre)
                    .ToList();

                // Asignar NombreEmpleado desde la relación
                foreach (var nomina in nominas)
                {
                    nomina.NombreEmpleado = nomina.Empleado?.NombreCompleto;
                }

                return nominas;
            }
        }

        public Nomina? ObtenerPorId(int id)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                return context.Nomina
                    .Include(n => n.Empleado)
                    .FirstOrDefault(n => n.Id == id);
            }
        }

        public List<Nomina> ObtenerPorEmpleado(int empleadoId)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var nominas = context.Nomina
                    .Include(n => n.Empleado)
                    .Where(n => n.EmpleadoId == empleadoId)
                    .OrderByDescending(n => n.Periodo)
                    .ToList();

                // Asignar NombreEmpleado desde la relación
                foreach (var nomina in nominas)
                {
                    nomina.NombreEmpleado = nomina.Empleado?.NombreCompleto;
                }

                return nominas;
            }
        }

        public List<Nomina> ObtenerPorPeriodo(string periodo)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var nominas = context.Nomina
                    .Include(n => n.Empleado)
                    .Where(n => n.Periodo == periodo)
                    .OrderBy(n => n.Empleado.Nombre)
                    .ToList();

                // Asignar NombreEmpleado desde la relación
                foreach (var nomina in nominas)
                {
                    nomina.NombreEmpleado = nomina.Empleado?.NombreCompleto;
                }

                return nominas;
            }
        }

        public bool Insertar(Nomina nomina)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                context.Nomina.Add(nomina);
                return context.SaveChanges() > 0;
            }
        }

        public bool Actualizar(Nomina nomina)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var nominaExistente = context.Nomina.Find(nomina.Id);
                if (nominaExistente == null)
                    return false;

                nominaExistente.SalarioBruto = nomina.SalarioBruto;
                nominaExistente.Bonificaciones = nomina.Bonificaciones;
                nominaExistente.Deducciones = nomina.Deducciones;
                nominaExistente.SalarioNeto = nomina.SalarioNeto;
                nominaExistente.Estado = nomina.Estado;
                nominaExistente.FechaPago = nomina.FechaPago;

                return context.SaveChanges() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                var nomina = context.Nomina.Find(id);
                // Permitir eliminar nóminas en estado "Pendiente" o "Borrador" (no las "Pagadas")
                if (nomina == null || nomina.Estado == "Pagada")
                    return false;

                context.Nomina.Remove(nomina);
                return context.SaveChanges() > 0;
            }
        }

        public bool EliminarPorPeriodo(string periodo)
        {
            using (var context = DatabaseConnection.CreateDbContext())
            {
                // Eliminar nóminas pendientes o en borrador del período (no las pagadas)
                var nominas = context.Nomina
                    .Where(n => n.Periodo == periodo && n.Estado != "Pagada")
                    .ToList();

                if (nominas.Count == 0)
                    return false;

                context.Nomina.RemoveRange(nominas);
                return context.SaveChanges() > 0;
            }
        }
    }
}
