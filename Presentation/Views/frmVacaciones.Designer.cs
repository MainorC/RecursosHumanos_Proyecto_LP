namespace RecursosHumanos.Presentation.Views
{
    partial class frmVacaciones
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private GroupBox gbNuevaSolicitud;
        private TextBox txtEmpleado;
        private DateTimePicker dtpInicio;
        private DateTimePicker dtpFin;
        private Label lblDiasTotales;
        private Button btnEnviar;
        private GroupBox gbTodasSolicitudes;
        private DataGridView dgvVacaciones;
        private Button btnAprobar;
        private Button btnRechazar;

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
            lblTitulo = new Label();
            gbNuevaSolicitud = new GroupBox();
            lblEmpleado = new Label();
            txtEmpleado = new TextBox();
            lblInicio = new Label();
            dtpInicio = new DateTimePicker();
            lblFin = new Label();
            dtpFin = new DateTimePicker();
            lblDias = new Label();
            lblDiasTotales = new Label();
            lblInfoDias = new Label();
            btnEnviar = new Button();
            gbTodasSolicitudes = new GroupBox();
            txtBuscar = new TextBox();
            cmbFiltroEstado = new ComboBox();
            lblBuscar = new Label();
            lblFiltroEstado = new Label();
            lblEstadisticas = new Label();
            dgvVacaciones = new DataGridView();
            btnAprobar = new Button();
            btnRechazar = new Button();
            gbNuevaSolicitud.SuspendLayout();
            gbTodasSolicitudes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVacaciones).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitulo.Location = new Point(20, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(328, 41);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "Gestión de Vacaciones";
            // 
            // gbNuevaSolicitud
            // 
            gbNuevaSolicitud.Controls.Add(lblInfoDias);
            gbNuevaSolicitud.Controls.Add(lblEmpleado);
            gbNuevaSolicitud.Controls.Add(txtEmpleado);
            gbNuevaSolicitud.Controls.Add(lblInicio);
            gbNuevaSolicitud.Controls.Add(dtpInicio);
            gbNuevaSolicitud.Controls.Add(lblFin);
            gbNuevaSolicitud.Controls.Add(dtpFin);
            gbNuevaSolicitud.Controls.Add(lblDias);
            gbNuevaSolicitud.Controls.Add(lblDiasTotales);
            gbNuevaSolicitud.Controls.Add(btnEnviar);
            gbNuevaSolicitud.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbNuevaSolicitud.Location = new Point(20, 72);
            gbNuevaSolicitud.Name = "gbNuevaSolicitud";
            gbNuevaSolicitud.Size = new Size(500, 420);
            gbNuevaSolicitud.TabIndex = 1;
            gbNuevaSolicitud.TabStop = false;
            gbNuevaSolicitud.Text = "Nueva Solicitud";
            // 
            // lblEmpleado
            // 
            lblEmpleado.Location = new Point(20, 29);
            lblEmpleado.Name = "lblEmpleado";
            lblEmpleado.Size = new Size(100, 23);
            lblEmpleado.TabIndex = 0;
            lblEmpleado.Text = "Empleado";
            // 
            // txtEmpleado
            // 
            txtEmpleado.Font = new Font("Segoe UI", 9F);
            txtEmpleado.Location = new Point(20, 55);
            txtEmpleado.Name = "txtEmpleado";
            txtEmpleado.Size = new Size(450, 27);
            txtEmpleado.TabIndex = 1;
            txtEmpleado.PlaceholderText = "Buscar empleado...";
            // 
            // lblInicio
            // 
            lblInicio.Location = new Point(20, 102);
            lblInicio.Name = "lblInicio";
            lblInicio.Size = new Size(100, 23);
            lblInicio.TabIndex = 2;
            lblInicio.Text = "Inicio";
            // 
            // dtpInicio
            // 
            dtpInicio.Font = new Font("Segoe UI", 9F);
            dtpInicio.Format = DateTimePickerFormat.Short;
            dtpInicio.Location = new Point(20, 128);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.Size = new Size(450, 27);
            dtpInicio.TabIndex = 3;
            dtpInicio.ValueChanged += dtpInicio_ValueChanged;
            // 
            // lblFin
            // 
            lblFin.Location = new Point(20, 167);
            lblFin.Name = "lblFin";
            lblFin.Size = new Size(100, 23);
            lblFin.TabIndex = 4;
            lblFin.Text = "Fin";
            // 
            // dtpFin
            // 
            dtpFin.Font = new Font("Segoe UI", 9F);
            dtpFin.Format = DateTimePickerFormat.Short;
            dtpFin.Location = new Point(20, 206);
            dtpFin.Name = "dtpFin";
            dtpFin.Size = new Size(450, 27);
            dtpFin.TabIndex = 5;
            dtpFin.ValueChanged += dtpFin_ValueChanged;
            // 
            // lblDias
            // 
            lblDias.Location = new Point(20, 246);
            lblDias.Name = "lblDias";
            lblDias.Size = new Size(100, 23);
            lblDias.TabIndex = 6;
            lblDias.Text = "Días";
            // 
            // lblDiasTotales
            // 
            lblDiasTotales.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDiasTotales.ForeColor = Color.FromArgb(52, 152, 219);
            lblDiasTotales.Location = new Point(130, 249);
            lblDiasTotales.Name = "lblDiasTotales";
            lblDiasTotales.Size = new Size(340, 20);
            lblDiasTotales.TabIndex = 7;
            lblDiasTotales.Text = "1";
            // 
            // lblInfoDias
            // 
            lblInfoDias.Font = new Font("Segoe UI", 8.5F);
            lblInfoDias.ForeColor = Color.FromArgb(127, 140, 141);
            lblInfoDias.Location = new Point(20, 275);
            lblInfoDias.Name = "lblInfoDias";
            lblInfoDias.Size = new Size(450, 60);
            lblInfoDias.TabIndex = 9;
            lblInfoDias.Text = "Seleccione un empleado para ver información detallada de días disponibles y su historial de vacaciones.";
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(20, 340);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(450, 35);
            btnEnviar.TabIndex = 8;
            btnEnviar.Text = "Enviar Solicitud";
            btnEnviar.Click += btnEnviar_Click;
            // 
            // gbTodasSolicitudes
            // 
            gbTodasSolicitudes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbTodasSolicitudes.Controls.Add(lblEstadisticas);
            gbTodasSolicitudes.Controls.Add(lblFiltroEstado);
            gbTodasSolicitudes.Controls.Add(cmbFiltroEstado);
            gbTodasSolicitudes.Controls.Add(lblBuscar);
            gbTodasSolicitudes.Controls.Add(txtBuscar);
            gbTodasSolicitudes.Controls.Add(dgvVacaciones);
            gbTodasSolicitudes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbTodasSolicitudes.Location = new Point(537, 72);
            gbTodasSolicitudes.Name = "gbTodasSolicitudes";
            gbTodasSolicitudes.Size = new Size(990, 850);
            gbTodasSolicitudes.TabIndex = 0;
            gbTodasSolicitudes.TabStop = false;
            gbTodasSolicitudes.Text = "Todas las Solicitudes (Total: 0 | Pendientes: 0 | Aprobadas: 0 | Días aprobados: 0)";
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Font = new Font("Segoe UI", 9F);
            lblBuscar.ForeColor = Color.FromArgb(44, 62, 80);
            lblBuscar.Location = new Point(20, 30);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(55, 20);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            txtBuscar.Font = new Font("Segoe UI", 9F);
            txtBuscar.Location = new Point(80, 28);
            txtBuscar.Margin = new Padding(3, 5, 3, 5);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por nombre de empleado...";
            txtBuscar.Size = new Size(400, 27);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += TxtBuscar_TextChanged;
            // 
            // lblFiltroEstado
            // 
            lblFiltroEstado.AutoSize = true;
            lblFiltroEstado.Font = new Font("Segoe UI", 9F);
            lblFiltroEstado.ForeColor = Color.FromArgb(44, 62, 80);
            lblFiltroEstado.Location = new Point(500, 30);
            lblFiltroEstado.Name = "lblFiltroEstado";
            lblFiltroEstado.Size = new Size(58, 20);
            lblFiltroEstado.TabIndex = 3;
            lblFiltroEstado.Text = "Estado:";
            // 
            // cmbFiltroEstado
            // 
            cmbFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroEstado.Font = new Font("Segoe UI", 9F);
            cmbFiltroEstado.Location = new Point(565, 28);
            cmbFiltroEstado.Margin = new Padding(3, 5, 3, 5);
            cmbFiltroEstado.Name = "cmbFiltroEstado";
            cmbFiltroEstado.Size = new Size(200, 28);
            cmbFiltroEstado.TabIndex = 4;
            cmbFiltroEstado.SelectedIndexChanged += CmbFiltroEstado_SelectedIndexChanged;
            // 
            // lblEstadisticas
            // 
            lblEstadisticas.AutoSize = true;
            lblEstadisticas.Font = new Font("Segoe UI", 9F);
            lblEstadisticas.ForeColor = Color.FromArgb(44, 62, 80);
            lblEstadisticas.Location = new Point(20, 65);
            lblEstadisticas.Name = "lblEstadisticas";
            lblEstadisticas.Size = new Size(0, 20);
            lblEstadisticas.TabIndex = 5;
            // 
            // dgvVacaciones
            // 
            dgvVacaciones.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvVacaciones.ColumnHeadersHeight = 29;
            dgvVacaciones.Location = new Point(20, 95);
            dgvVacaciones.Name = "dgvVacaciones";
            dgvVacaciones.RowHeadersWidth = 4;
            dgvVacaciones.Size = new Size(950, 735);
            dgvVacaciones.TabIndex = 0;
            // 
            // btnAprobar
            // 
            btnAprobar.Location = new Point(0, 0);
            btnAprobar.Name = "btnAprobar";
            btnAprobar.Size = new Size(75, 23);
            btnAprobar.TabIndex = 0;
            // 
            // btnRechazar
            // 
            btnRechazar.Location = new Point(0, 0);
            btnRechazar.Name = "btnRechazar";
            btnRechazar.Size = new Size(75, 23);
            btnRechazar.TabIndex = 0;
            // 
            // frmVacaciones
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1550, 940);
            Controls.Add(gbTodasSolicitudes);
            Controls.Add(gbNuevaSolicitud);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmVacaciones";
            Load += frmVacaciones_Load;
            gbNuevaSolicitud.ResumeLayout(false);
            gbTodasSolicitudes.ResumeLayout(false);
            gbTodasSolicitudes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVacaciones).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Label lblEmpleado;
        private Label lblInicio;
        private Label lblFin;
        private Label lblDias;
        public TextBox txtBuscar;
        public ComboBox cmbFiltroEstado;
        public Label lblBuscar;
        public Label lblFiltroEstado;
        public Label lblEstadisticas;
        public Label lblInfoDias;
    }
}

