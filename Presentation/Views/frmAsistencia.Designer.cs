namespace RecursosHumanos.Presentation.Views
{
    partial class frmAsistencia
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private GroupBox gbRegistrar;
        private TextBox txtEmpleado;
        private DateTimePicker dtpFecha;
        private ComboBox cmbEstado;
        private DateTimePicker dtpEntrada;
        private DateTimePicker dtpSalida;
        private Button btnGuardar;
        private GroupBox gbHistorial;
        private DateTimePicker dtpMes;
        private DataGridView dgvAsistencia;

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
            gbRegistrar = new GroupBox();
            lblEmpleado = new Label();
            txtEmpleado = new TextBox();
            lblFecha = new Label();
            dtpFecha = new DateTimePicker();
            lblEstado = new Label();
            cmbEstado = new ComboBox();
            lblEntrada = new Label();
            dtpEntrada = new DateTimePicker();
            lblSalida = new Label();
            dtpSalida = new DateTimePicker();
            btnGuardar = new Button();
            gbHistorial = new GroupBox();
            dtpMes = new DateTimePicker();
            dgvAsistencia = new DataGridView();
            gbRegistrar.SuspendLayout();
            gbHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAsistencia).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitulo.Location = new Point(20, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(316, 41);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "Control de Asistencia";
            // 
            // gbRegistrar
            // 
            gbRegistrar.Controls.Add(lblEmpleado);
            gbRegistrar.Controls.Add(txtEmpleado);
            gbRegistrar.Controls.Add(lblFecha);
            gbRegistrar.Controls.Add(dtpFecha);
            gbRegistrar.Controls.Add(lblEstado);
            gbRegistrar.Controls.Add(cmbEstado);
            gbRegistrar.Controls.Add(lblEntrada);
            gbRegistrar.Controls.Add(dtpEntrada);
            gbRegistrar.Controls.Add(lblSalida);
            gbRegistrar.Controls.Add(dtpSalida);
            gbRegistrar.Controls.Add(btnGuardar);
            gbRegistrar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbRegistrar.Location = new Point(20, 60);
            gbRegistrar.Name = "gbRegistrar";
            gbRegistrar.Size = new Size(500, 444);
            gbRegistrar.TabIndex = 1;
            gbRegistrar.TabStop = false;
            gbRegistrar.Text = "Registrar Asistencia";
            // 
            // lblEmpleado
            // 
            lblEmpleado.Location = new Point(20, 30);
            lblEmpleado.Name = "lblEmpleado";
            lblEmpleado.Size = new Size(100, 23);
            lblEmpleado.TabIndex = 0;
            lblEmpleado.Text = "Empleado";
            // 
            // txtEmpleado
            // 
            txtEmpleado.Font = new Font("Segoe UI", 9F);
            txtEmpleado.Location = new Point(20, 65);
            txtEmpleado.Name = "txtEmpleado";
            txtEmpleado.Size = new Size(450, 27);
            txtEmpleado.TabIndex = 1;
            txtEmpleado.PlaceholderText = "Buscar empleado...";
            // 
            // lblFecha
            // 
            lblFecha.Location = new Point(20, 114);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(100, 23);
            lblFecha.TabIndex = 2;
            lblFecha.Text = "Fecha";
            // 
            // dtpFecha
            // 
            dtpFecha.Font = new Font("Segoe UI", 9F);
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(20, 150);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(450, 27);
            dtpFecha.TabIndex = 3;
            // 
            // lblEstado
            // 
            lblEstado.Location = new Point(20, 192);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(100, 23);
            lblEstado.TabIndex = 4;
            lblEstado.Text = "Estado";
            // 
            // cmbEstado
            // 
            cmbEstado.Font = new Font("Segoe UI", 9F);
            cmbEstado.Items.AddRange(new object[] { "Presente", "Tarde", "Ausente" });
            cmbEstado.Location = new Point(20, 228);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(450, 28);
            cmbEstado.TabIndex = 5;
            // 
            // lblEntrada
            // 
            lblEntrada.Location = new Point(20, 270);
            lblEntrada.Name = "lblEntrada";
            lblEntrada.Size = new Size(100, 23);
            lblEntrada.TabIndex = 6;
            lblEntrada.Text = "Entrada";
            // 
            // dtpEntrada
            // 
            dtpEntrada.Font = new Font("Segoe UI", 9F);
            dtpEntrada.Format = DateTimePickerFormat.Time;
            dtpEntrada.Location = new Point(20, 309);
            dtpEntrada.Name = "dtpEntrada";
            dtpEntrada.ShowUpDown = true;
            dtpEntrada.Size = new Size(220, 27);
            dtpEntrada.TabIndex = 7;
            dtpEntrada.Value = new DateTime(2025, 11, 14, 8, 30, 0, 0);
            // 
            // lblSalida
            // 
            lblSalida.Location = new Point(250, 270);
            lblSalida.Name = "lblSalida";
            lblSalida.Size = new Size(100, 23);
            lblSalida.TabIndex = 8;
            lblSalida.Text = "Salida";
            // 
            // dtpSalida
            // 
            dtpSalida.Font = new Font("Segoe UI", 9F);
            dtpSalida.Format = DateTimePickerFormat.Time;
            dtpSalida.Location = new Point(250, 309);
            dtpSalida.Name = "dtpSalida";
            dtpSalida.ShowUpDown = true;
            dtpSalida.Size = new Size(220, 27);
            dtpSalida.TabIndex = 9;
            dtpSalida.Value = new DateTime(2025, 11, 14, 17, 30, 0, 0);
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(20, 369);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(450, 35);
            btnGuardar.TabIndex = 10;
            btnGuardar.Text = "Guardar Registro";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // gbHistorial
            // 
            gbHistorial.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbHistorial.Controls.Add(dtpMes);
            gbHistorial.Controls.Add(dgvAsistencia);
            gbHistorial.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbHistorial.Location = new Point(540, 60);
            gbHistorial.Name = "gbHistorial";
            gbHistorial.Size = new Size(990, 850);
            gbHistorial.TabIndex = 0;
            gbHistorial.TabStop = false;
            gbHistorial.Text = "Historial de Asistencia";
            // 
            // dtpMes
            // 
            dtpMes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dtpMes.CustomFormat = "MMMM yyyy";
            dtpMes.Font = new Font("Segoe UI", 9F);
            dtpMes.Format = DateTimePickerFormat.Custom;
            dtpMes.Location = new Point(20, 30);
            dtpMes.Name = "dtpMes";
            dtpMes.ShowUpDown = true;
            dtpMes.Size = new Size(200, 27);
            dtpMes.TabIndex = 0;
            dtpMes.ValueChanged += dtpMes_ValueChanged;
            // 
            // dgvAsistencia
            // 
            dgvAsistencia.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAsistencia.ColumnHeadersHeight = 29;
            dgvAsistencia.Location = new Point(20, 65);
            dgvAsistencia.Name = "dgvAsistencia";
            dgvAsistencia.RowHeadersWidth = 4;
            dgvAsistencia.Size = new Size(950, 760);
            dgvAsistencia.TabIndex = 1;
            // 
            // frmAsistencia
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1550, 940);
            Controls.Add(gbHistorial);
            Controls.Add(gbRegistrar);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmAsistencia";
            Load += frmAsistencia_Load;
            gbRegistrar.ResumeLayout(false);
            gbHistorial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAsistencia).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Label lblEmpleado;
        private Label lblFecha;
        private Label lblEstado;
        private Label lblEntrada;
        private Label lblSalida;
    }
}

