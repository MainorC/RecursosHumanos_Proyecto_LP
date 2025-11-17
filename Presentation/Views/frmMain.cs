using RecursosHumanos.Common.Helpers;

namespace RecursosHumanos.Presentation.Views
{
    public partial class frmMain : Form
    {
        private Form? formularioActivo = null;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Maximizar la ventana
            this.WindowState = FormWindowState.Maximized;
            
            // Asegurar que el panel de contenido tenga el tamaño correcto después de maximizar
            this.Resize += FrmMain_Resize;
            AjustarPanelContenido();
            
            lblUsuario.Text = SessionManager.UsuarioActual?.NombreCompleto ?? "Administrador del Sistema";
            lblRol.Text = SessionManager.UsuarioActual?.Rol ?? "Administrador";

            // Cargar dashboard por defecto
            var dashboard = new frmDashboard();
            dashboard.SolicitarAbrirFormulario += Dashboard_SolicitarAbrirFormulario;
            AbrirFormulario(dashboard);
        }

        private void FrmMain_Resize(object? sender, EventArgs e)
        {
            AjustarPanelContenido();
            if (formularioActivo != null)
            {
                CentrarFormularioHijo();
            }
        }

        private void AjustarPanelContenido()
        {
            // Calcular el tamaño disponible para el panel de contenido
            int anchoDisponible = this.ClientSize.Width - 250; // Restar el ancho del sidebar
            int altoDisponible = this.ClientSize.Height - 60; // Restar la altura del header
            
            // Ajustar el header
            pnlHeader.Size = new Size(anchoDisponible, 60);
            pnlHeader.Location = new Point(250, 0);
            
            // Ajustar el sidebar
            pnlSidebar.Size = new Size(250, this.ClientSize.Height);
            
            // Mantener el tamaño estándar de 1550x940 para el panel de contenido
            // Si hay más espacio disponible, el panel se expande pero los formularios hijos se centran
            int ancho = Math.Max(1550, anchoDisponible);
            int alto = Math.Max(940, altoDisponible);
            
            pnlContenido.Size = new Size(ancho, alto);
            pnlContenido.Location = new Point(250, 60);
        }

        private void CentrarFormularioHijo()
        {
            if (formularioActivo != null)
            {
                int x = (pnlContenido.Width - 1550) / 2;
                int y = (pnlContenido.Height - 940) / 2;
                formularioActivo.Location = new Point(Math.Max(0, x), Math.Max(0, y));
            }
        }

        private void AbrirFormulario(Form formulario)
        {
            if (formularioActivo != null)
            {
                formularioActivo.Close();
                formularioActivo.Dispose();
            }

            formularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.AutoScaleMode = AutoScaleMode.None;
            formulario.Dock = DockStyle.None;
            
            // Establecer el tamaño exacto del formulario hijo
            formulario.Size = new Size(1550, 940);
            
            pnlContenido.Controls.Add(formulario);
            CentrarFormularioHijo();
            formulario.Show();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            var dashboard = new frmDashboard();
            dashboard.SolicitarAbrirFormulario += Dashboard_SolicitarAbrirFormulario;
            AbrirFormulario(dashboard);
            ResaltarBoton(btnDashboard);
        }

        private void Dashboard_SolicitarAbrirFormulario(Type tipoFormulario, object? parametro)
        {
            Form? nuevoFormulario = null;

            if (tipoFormulario == typeof(frmVacaciones))
            {
                nuevoFormulario = new frmVacaciones();
                // Si hay un ID de vacación, podríamos seleccionarlo en el grid
                // Por ahora solo abrimos el formulario
            }
            else if (tipoFormulario == typeof(frmIncorporacion))
            {
                nuevoFormulario = new frmIncorporacion();
                // Si hay un ID de incorporación, podríamos seleccionarlo
            }
            else if (tipoFormulario == typeof(frmComunicados))
            {
                nuevoFormulario = new frmComunicados();
                // Si hay un ID de comunicado, podríamos seleccionarlo
            }
            else if (tipoFormulario == typeof(frmEmpleados))
            {
                nuevoFormulario = new frmEmpleados();
                // Si hay un ID de empleado, podríamos seleccionarlo
            }

            if (nuevoFormulario != null)
            {
                AbrirFormulario(nuevoFormulario);
                
                // Resaltar el botón correspondiente en el menú
                if (tipoFormulario == typeof(frmVacaciones))
                    ResaltarBoton(btnVacaciones);
                else if (tipoFormulario == typeof(frmIncorporacion))
                    ResaltarBoton(btnIncorporacion);
                else if (tipoFormulario == typeof(frmComunicados))
                    ResaltarBoton(btnComunicados);
                else if (tipoFormulario == typeof(frmEmpleados))
                    ResaltarBoton(btnEmpleados);
            }
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmEmpleados());
            ResaltarBoton(btnEmpleados);
        }

        private void btnAreas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmAreas());
            ResaltarBoton(btnAreas);
        }

        private void btnAsistencia_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmAsistencia());
            ResaltarBoton(btnAsistencia);
        }

        private void btnVacaciones_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmVacaciones());
            ResaltarBoton(btnVacaciones);
        }

        private void btnEvaluaciones_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmEvaluaciones());
            ResaltarBoton(btnEvaluaciones);
        }

        private void btnNomina_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmNomina());
            ResaltarBoton(btnNomina);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmReportes());
            ResaltarBoton(btnReportes);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            if (!SessionManager.EsAdministrador)
            {
                UIHelper.MostrarMensaje("Solo los administradores pueden acceder a esta sección.", "Acceso Denegado", MessageBoxIcon.Warning);
                return;
            }
            AbrirFormulario(new frmUsuarios());
            ResaltarBoton(btnUsuarios);
        }

        private void btnComunicados_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmComunicados());
            ResaltarBoton(btnComunicados);
        }

        private void btnIncorporacion_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new frmIncorporacion());
            ResaltarBoton(btnIncorporacion);
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            if (UIHelper.MostrarConfirmacion("¿Está seguro que desea cerrar sesión?", "Cerrar Sesión") == DialogResult.Yes)
            {
                SessionManager.CerrarSesion();
                this.Hide();
                var frmLogin = new frmLogin();
                frmLogin.ShowDialog();
                this.Close();
            }
        }

        private void ResaltarBoton(Button boton)
        {
            // Resetear todos los botones
            foreach (Control control in pnlSidebar.Controls)
            {
                if (control is Button btn)
                {
                    btn.BackColor = UIHelper.ColorSidebar;
                }
            }
            // Resaltar el botón seleccionado
            boton.BackColor = UIHelper.ColorSidebarHover;
        }
    }
}

