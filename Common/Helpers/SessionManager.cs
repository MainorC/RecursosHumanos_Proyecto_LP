using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Common.Helpers
{
    public static class SessionManager
    {
        public static Usuario? UsuarioActual { get; set; }

        public static bool EsAdministrador => UsuarioActual?.Rol == "Administrador";

        public static void CerrarSesion()
        {
            UsuarioActual = null;
        }
    }
}

