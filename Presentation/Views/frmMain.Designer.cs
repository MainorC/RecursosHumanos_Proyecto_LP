namespace RecursosHumanos.Presentation.Views
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlSidebar;
        private Panel pnlHeader;
        private Panel pnlContenido;
        private Label lblTitulo;
        private Button btnDashboard;
        private Button btnEmpleados;
        private Button btnIncorporacion;
        private Button btnComunicados;
        private Button btnAreas;
        private Button btnAsistencia;
        private Button btnVacaciones;
        private Button btnEvaluaciones;
        private Button btnNomina;
        private Button btnReportes;
        private Button btnUsuarios;
        private Label lblUsuario;
        private Label lblRol;
        private Button btnCerrarSesion;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlSidebar = new Panel();
            lblTitulo = new Label();
            btnDashboard = new Button();
            btnEmpleados = new Button();
            btnIncorporacion = new Button();
            btnComunicados = new Button();
            btnAreas = new Button();
            btnAsistencia = new Button();
            btnVacaciones = new Button();
            btnEvaluaciones = new Button();
            btnNomina = new Button();
            btnReportes = new Button();
            btnUsuarios = new Button();
            pnlHeader = new Panel();
            btnCerrarSesion = new Button();
            lblRol = new Label();
            lblUsuario = new Label();
            pnlContenido = new Panel();
            pnlSidebar.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlSidebar.BackColor = Color.FromArgb(44, 62, 80);
            pnlSidebar.Controls.Add(lblTitulo);
            pnlSidebar.Controls.Add(btnDashboard);
            pnlSidebar.Controls.Add(btnEmpleados);
            pnlSidebar.Controls.Add(btnIncorporacion);
            pnlSidebar.Controls.Add(btnComunicados);
            pnlSidebar.Controls.Add(btnAreas);
            pnlSidebar.Controls.Add(btnAsistencia);
            pnlSidebar.Controls.Add(btnVacaciones);
            pnlSidebar.Controls.Add(btnEvaluaciones);
            pnlSidebar.Controls.Add(btnNomina);
            pnlSidebar.Controls.Add(btnReportes);
            pnlSidebar.Controls.Add(btnUsuarios);
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Margin = new Padding(0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(250, 1000);
            pnlSidebar.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(24, 13);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(99, 41);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "SGRH";
            // 
            // btnDashboard
            // 
            btnDashboard.BackColor = Color.FromArgb(44, 62, 80);
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnDashboard.ForeColor = Color.White;
            btnDashboard.Location = new Point(0, 60);
            btnDashboard.Margin = new Padding(3, 4, 3, 4);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Padding = new Padding(20, 0, 0, 0);
            btnDashboard.Size = new Size(250, 40);
            btnDashboard.TabIndex = 1;
            btnDashboard.Text = "Panel Principal";
            btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnDashboard.UseVisualStyleBackColor = false;
            btnDashboard.Click += btnDashboard_Click;
            // 
            // btnEmpleados
            // 
            btnEmpleados.BackColor = Color.FromArgb(44, 62, 80);
            btnEmpleados.FlatAppearance.BorderSize = 0;
            btnEmpleados.FlatStyle = FlatStyle.Flat;
            btnEmpleados.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnEmpleados.ForeColor = Color.White;
            btnEmpleados.Location = new Point(0, 102);
            btnEmpleados.Margin = new Padding(3, 4, 3, 4);
            btnEmpleados.Name = "btnEmpleados";
            btnEmpleados.Padding = new Padding(20, 0, 0, 0);
            btnEmpleados.Size = new Size(250, 40);
            btnEmpleados.TabIndex = 2;
            btnEmpleados.Text = "Empleados";
            btnEmpleados.TextAlign = ContentAlignment.MiddleLeft;
            btnEmpleados.UseVisualStyleBackColor = false;
            btnEmpleados.Click += btnEmpleados_Click;
            // 
            // btnIncorporacion
            // 
            btnIncorporacion.BackColor = Color.FromArgb(44, 62, 80);
            btnIncorporacion.FlatAppearance.BorderSize = 0;
            btnIncorporacion.FlatStyle = FlatStyle.Flat;
            btnIncorporacion.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnIncorporacion.ForeColor = Color.White;
            btnIncorporacion.Location = new Point(0, 144);
            btnIncorporacion.Margin = new Padding(3, 4, 3, 4);
            btnIncorporacion.Name = "btnIncorporacion";
            btnIncorporacion.Padding = new Padding(20, 0, 0, 0);
            btnIncorporacion.Size = new Size(250, 40);
            btnIncorporacion.TabIndex = 3;
            btnIncorporacion.Text = "Incorporación";
            btnIncorporacion.TextAlign = ContentAlignment.MiddleLeft;
            btnIncorporacion.UseVisualStyleBackColor = false;
            btnIncorporacion.Click += btnIncorporacion_Click;
            // 
            // btnComunicados
            // 
            btnComunicados.BackColor = Color.FromArgb(44, 62, 80);
            btnComunicados.FlatAppearance.BorderSize = 0;
            btnComunicados.FlatStyle = FlatStyle.Flat;
            btnComunicados.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnComunicados.ForeColor = Color.White;
            btnComunicados.Location = new Point(0, 186);
            btnComunicados.Margin = new Padding(3, 4, 3, 4);
            btnComunicados.Name = "btnComunicados";
            btnComunicados.Padding = new Padding(20, 0, 0, 0);
            btnComunicados.Size = new Size(250, 40);
            btnComunicados.TabIndex = 4;
            btnComunicados.Text = "Comunicados";
            btnComunicados.TextAlign = ContentAlignment.MiddleLeft;
            btnComunicados.UseVisualStyleBackColor = false;
            btnComunicados.Click += btnComunicados_Click;
            // 
            // btnAreas
            // 
            btnAreas.BackColor = Color.FromArgb(44, 62, 80);
            btnAreas.FlatAppearance.BorderSize = 0;
            btnAreas.FlatStyle = FlatStyle.Flat;
            btnAreas.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnAreas.ForeColor = Color.White;
            btnAreas.Location = new Point(0, 228);
            btnAreas.Margin = new Padding(3, 4, 3, 4);
            btnAreas.Name = "btnAreas";
            btnAreas.Padding = new Padding(20, 0, 0, 0);
            btnAreas.Size = new Size(250, 40);
            btnAreas.TabIndex = 5;
            btnAreas.Text = "Áreas";
            btnAreas.TextAlign = ContentAlignment.MiddleLeft;
            btnAreas.UseVisualStyleBackColor = false;
            btnAreas.Click += btnAreas_Click;
            // 
            // btnAsistencia
            // 
            btnAsistencia.BackColor = Color.FromArgb(44, 62, 80);
            btnAsistencia.FlatAppearance.BorderSize = 0;
            btnAsistencia.FlatStyle = FlatStyle.Flat;
            btnAsistencia.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnAsistencia.ForeColor = Color.White;
            btnAsistencia.Location = new Point(0, 270);
            btnAsistencia.Margin = new Padding(3, 4, 3, 4);
            btnAsistencia.Name = "btnAsistencia";
            btnAsistencia.Padding = new Padding(20, 0, 0, 0);
            btnAsistencia.Size = new Size(250, 40);
            btnAsistencia.TabIndex = 6;
            btnAsistencia.Text = "Asistencia";
            btnAsistencia.TextAlign = ContentAlignment.MiddleLeft;
            btnAsistencia.UseVisualStyleBackColor = false;
            btnAsistencia.Click += btnAsistencia_Click;
            // 
            // btnVacaciones
            // 
            btnVacaciones.BackColor = Color.FromArgb(44, 62, 80);
            btnVacaciones.FlatAppearance.BorderSize = 0;
            btnVacaciones.FlatStyle = FlatStyle.Flat;
            btnVacaciones.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnVacaciones.ForeColor = Color.White;
            btnVacaciones.Location = new Point(0, 312);
            btnVacaciones.Margin = new Padding(3, 4, 3, 4);
            btnVacaciones.Name = "btnVacaciones";
            btnVacaciones.Padding = new Padding(20, 0, 0, 0);
            btnVacaciones.Size = new Size(250, 40);
            btnVacaciones.TabIndex = 7;
            btnVacaciones.Text = "Vacaciones";
            btnVacaciones.TextAlign = ContentAlignment.MiddleLeft;
            btnVacaciones.UseVisualStyleBackColor = false;
            btnVacaciones.Click += btnVacaciones_Click;
            // 
            // btnEvaluaciones
            // 
            btnEvaluaciones.BackColor = Color.FromArgb(44, 62, 80);
            btnEvaluaciones.FlatAppearance.BorderSize = 0;
            btnEvaluaciones.FlatStyle = FlatStyle.Flat;
            btnEvaluaciones.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnEvaluaciones.ForeColor = Color.White;
            btnEvaluaciones.Location = new Point(0, 354);
            btnEvaluaciones.Margin = new Padding(3, 4, 3, 4);
            btnEvaluaciones.Name = "btnEvaluaciones";
            btnEvaluaciones.Padding = new Padding(20, 0, 0, 0);
            btnEvaluaciones.Size = new Size(250, 40);
            btnEvaluaciones.TabIndex = 8;
            btnEvaluaciones.Text = "Evaluaciones";
            btnEvaluaciones.TextAlign = ContentAlignment.MiddleLeft;
            btnEvaluaciones.UseVisualStyleBackColor = false;
            btnEvaluaciones.Click += btnEvaluaciones_Click;
            // 
            // btnNomina
            // 
            btnNomina.BackColor = Color.FromArgb(44, 62, 80);
            btnNomina.FlatAppearance.BorderSize = 0;
            btnNomina.FlatStyle = FlatStyle.Flat;
            btnNomina.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnNomina.ForeColor = Color.White;
            btnNomina.Location = new Point(0, 396);
            btnNomina.Margin = new Padding(3, 4, 3, 4);
            btnNomina.Name = "btnNomina";
            btnNomina.Padding = new Padding(20, 0, 0, 0);
            btnNomina.Size = new Size(250, 40);
            btnNomina.TabIndex = 9;
            btnNomina.Text = "Nómina";
            btnNomina.TextAlign = ContentAlignment.MiddleLeft;
            btnNomina.UseVisualStyleBackColor = false;
            btnNomina.Click += btnNomina_Click;
            // 
            // btnReportes
            // 
            btnReportes.BackColor = Color.FromArgb(44, 62, 80);
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnReportes.ForeColor = Color.White;
            btnReportes.Location = new Point(0, 438);
            btnReportes.Margin = new Padding(3, 4, 3, 4);
            btnReportes.Name = "btnReportes";
            btnReportes.Padding = new Padding(20, 0, 0, 0);
            btnReportes.Size = new Size(250, 40);
            btnReportes.TabIndex = 10;
            btnReportes.Text = "Reportes";
            btnReportes.TextAlign = ContentAlignment.MiddleLeft;
            btnReportes.UseVisualStyleBackColor = false;
            btnReportes.Click += btnReportes_Click;
            // 
            // btnUsuarios
            // 
            btnUsuarios.BackColor = Color.FromArgb(44, 62, 80);
            btnUsuarios.FlatAppearance.BorderSize = 0;
            btnUsuarios.FlatStyle = FlatStyle.Flat;
            btnUsuarios.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold);
            btnUsuarios.ForeColor = Color.White;
            btnUsuarios.Location = new Point(0, 480);
            btnUsuarios.Margin = new Padding(3, 4, 3, 4);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.Padding = new Padding(20, 0, 0, 0);
            btnUsuarios.Size = new Size(250, 40);
            btnUsuarios.TabIndex = 11;
            btnUsuarios.Text = "Usuarios";
            btnUsuarios.TextAlign = ContentAlignment.MiddleLeft;
            btnUsuarios.UseVisualStyleBackColor = false;
            btnUsuarios.Click += btnUsuarios_Click;
            // 
            // pnlHeader
            // 
            pnlHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlHeader.BackColor = Color.FromArgb(236, 240, 241);
            pnlHeader.Controls.Add(btnCerrarSesion);
            pnlHeader.Controls.Add(lblRol);
            pnlHeader.Controls.Add(lblUsuario);
            pnlHeader.Location = new Point(250, 0);
            pnlHeader.Margin = new Padding(0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1550, 60);
            pnlHeader.TabIndex = 1;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrarSesion.BackColor = Color.FromArgb(52, 152, 219);
            btnCerrarSesion.Cursor = Cursors.Hand;
            btnCerrarSesion.FlatAppearance.BorderSize = 0;
            btnCerrarSesion.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
            btnCerrarSesion.FlatStyle = FlatStyle.Flat;
            btnCerrarSesion.Font = new Font("Segoe UI", 9F);
            btnCerrarSesion.ForeColor = Color.White;
            btnCerrarSesion.Location = new Point(1410, 13);
            btnCerrarSesion.Margin = new Padding(3, 4, 3, 4);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(120, 35);
            btnCerrarSesion.TabIndex = 0;
            btnCerrarSesion.Text = "Cerrar Sesión";
            btnCerrarSesion.UseVisualStyleBackColor = false;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // lblRol
            // 
            lblRol.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblRol.AutoSize = true;
            lblRol.Font = new Font("Segoe UI", 9F);
            lblRol.ForeColor = Color.FromArgb(127, 140, 141);
            lblRol.Location = new Point(1147, 32);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(104, 20);
            lblRol.TabIndex = 1;
            lblRol.Text = "Administrador";
            lblRol.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblUsuario
            // 
            lblUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsuario.ForeColor = Color.FromArgb(44, 62, 80);
            lblUsuario.Location = new Point(1147, 9);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(225, 23);
            lblUsuario.TabIndex = 2;
            lblUsuario.Text = "Administrador del Sistema";
            lblUsuario.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlContenido
            // 
            pnlContenido.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlContenido.BackColor = Color.WhiteSmoke;
            pnlContenido.Location = new Point(250, 60);
            pnlContenido.Margin = new Padding(0);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(1550, 940);
            pnlContenido.TabIndex = 2;
            // 
            // frmMain
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1800, 1000);
            Controls.Add(pnlContenido);
            Controls.Add(pnlHeader);
            Controls.Add(pnlSidebar);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1400, 800);
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Gestión de RRHH";
            WindowState = FormWindowState.Maximized;
            Load += frmMain_Load;
            pnlSidebar.ResumeLayout(false);
            pnlSidebar.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);
        }
    }
}

