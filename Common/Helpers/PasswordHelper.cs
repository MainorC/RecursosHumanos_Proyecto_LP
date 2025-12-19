using BCrypt.Net;

namespace RecursosHumanos.Common.Helpers
{
    // Helper para manejar contraseñas con BCrypt
    public static class PasswordHelper
    {
        // genera hash de la contraseña
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("La contraseña no puede estar vacía.", nameof(password));
            }

            // bcrypt genera el salt automaticamente, work factor 10
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 10);
        }

        // verifica si la contraseña coincide con el hash
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hashedPassword))
            {
                return false;
            }

            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
            catch (BCrypt.Net.SaltParseException)
            {
                // Si el hash tiene formato inválido
                return false;
            }
            catch (Exception)
            {
                // si hay error retornar false
                return false;
            }
        }

        // verifica si es un hash bcrypt valido
        public static bool IsHashed(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
            {
                return false;
            }

            // los hashes bcrypt empiezan con $2a$, $2b$, etc
            return hash.StartsWith("$2a$") || 
                   hash.StartsWith("$2b$") || 
                   hash.StartsWith("$2x$") || 
                   hash.StartsWith("$2y$");
        }
    }
}

