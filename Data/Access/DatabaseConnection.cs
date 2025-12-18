using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RecursosHumanos.Data.Context;

namespace RecursosHumanos.Data.Access
{
    /// <summary>
    /// Clase estática para gestión de conexión a base de datos
    /// Soporta tanto ADO.NET como Entity Framework Core
    /// </summary>
    public static class DatabaseConnection
    {
        private static string? _connectionString;

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    // Cadena de conexión por defecto - puede ser configurada
                    _connectionString = "Server=DESKTOP-G1T7S2S\\SQLEXPRESS;Database=SGRH;Integrated Security=true;TrustServerCertificate=true;";

                    // Si existe un archivo de configuración, se puede leer desde ahí
                    // _connectionString = ConfigurationManager.ConnectionStrings["SGRHConnection"].ConnectionString;
                }
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        /// <summary>
        /// Obtiene una conexión SQL directa (ADO.NET)
        /// </summary>
        public static Microsoft.Data.SqlClient.SqlConnection GetConnection()
        {
            return new Microsoft.Data.SqlClient.SqlConnection(ConnectionString);
        }

        /// <summary>
        /// Crea un DbContext configurado para Entity Framework Core
        /// </summary>
        public static RecursosHumanosDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RecursosHumanosDbContext>();
            optionsBuilder.UseSqlServer(ConnectionString);
            return new RecursosHumanosDbContext(optionsBuilder.Options);
        }

        /// <summary>
        /// Prueba la conexión a la base de datos
        /// </summary>
        public static bool TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Prueba la conexión usando Entity Framework Core
        /// </summary>
        public static bool TestConnectionEF()
        {
            try
            {
                using (var context = CreateDbContext())
                {
                    return context.Database.CanConnect();
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

