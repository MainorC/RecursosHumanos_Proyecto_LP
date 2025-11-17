using Microsoft.EntityFrameworkCore;
using RecursosHumanos.Data.Context;

namespace RecursosHumanos.Data.Context
{
    /// <summary>
    /// Factory para crear instancias de RecursosHumanosDbContext
    /// </summary>
    public static class DbContextFactory
    {
        private static string? _connectionString;

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    // Usar la misma cadena de conexión que DatabaseConnection para mantener compatibilidad
                    _connectionString = Access.DatabaseConnection.ConnectionString;
                }
                return _connectionString;
            }
            set
            {
                _connectionString = value;
                // Sincronizar también con DatabaseConnection
                Access.DatabaseConnection.ConnectionString = value;
            }
        }

        /// <summary>
        /// Crea una nueva instancia de RecursosHumanosDbContext
        /// </summary>
        public static RecursosHumanosDbContext Create()
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
                using (var context = Create())
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

