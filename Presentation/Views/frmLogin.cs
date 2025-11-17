using RecursosHumanos.Business.Services;
using RecursosHumanos.Common.Helpers;

namespace RecursosHumanos.Presentation.Views
{
    public partial class frmLogin : Form
    {
        private readonly UsuarioBLL _usuarioBLL = new();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;
                lblError.Text = "";

                if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text))
                {
                    MostrarError("Por favor, ingrese usuario y contraseña.");
                    return;
                }

                var usuario = _usuarioBLL.ValidarCredenciales(txtUsuario.Text, txtContrasena.Text);

                if (usuario != null)
                {
                    SessionManager.UsuarioActual = usuario;
                    this.Hide();
                    var frmMain = new frmMain();
                    frmMain.ShowDialog();
                    // Limpiar campos al volver al login
                    txtUsuario.Clear();
                    txtContrasena.Clear();
                    lblError.Visible = false;
                    this.Show();
                    txtUsuario.Focus();
                }
                else
                {
                    MostrarError("Credenciales inválidas. Por favor, inténtalo de nuevo.");
                    txtContrasena.Clear();
                    txtContrasena.Focus();
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error al iniciar sesión: {ex.Message}");
            }
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
            lblError.ForeColor = Color.Red;
            lblError.BackColor = Color.FromArgb(255, 245, 245);
            lblError.BorderStyle = BorderStyle.FixedSingle;
            lblError.Padding = new Padding(10, 5, 10, 5);
            lblError.AutoSize = false;
            lblError.Size = new Size(399, 35);
            lblError.TextAlign = ContentAlignment.MiddleLeft;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.Text = "Sistema de Gestión de RRHH - Login";
            txtUsuario.Focus();
        }

        private void txtContrasena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtContrasena.Focus();
            }
        }
    }
}

