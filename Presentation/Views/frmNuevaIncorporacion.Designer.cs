namespace RecursosHumanos.Presentation.Views
{
    partial class frmNuevaIncorporacion
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblEmpleado;
        private TextBox txtEmpleado;
        private Label lblTipoProceso;
        private ComboBox cmbTipoProceso;
        private Label lblFechaInicio;
        private DateTimePicker dtpFechaInicio;
        private Label lblTareas;
        private CheckedListBox clbTareas;
        private Label lblTareaPersonalizada;
        private TextBox txtTareaPersonalizada;
        private Button btnAgregarTarea;
        private Button btnGuardar;
        private Button btnCancelar;

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
            lblEmpleado = new Label();
            txtEmpleado = new TextBox();
            lblTipoProceso = new Label();
            cmbTipoProceso = new ComboBox();
            lblFechaInicio = new Label();
            dtpFechaInicio = new DateTimePicker();
            lblTareas = new Label();
            clbTareas = new CheckedListBox();
            lblTareaPersonalizada = new Label();
            txtTareaPersonalizada = new TextBox();
            btnAgregarTarea = new Button();
            btnGuardar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitulo.Location = new Point(23, 27);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(436, 37);
            lblTitulo.TabIndex = 13;
            lblTitulo.Text = "Nuevo Proceso de Incorporación";
            // 
            // lblEmpleado
            // 
            lblEmpleado.AutoSize = true;
            lblEmpleado.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEmpleado.ForeColor = Color.FromArgb(44, 62, 80);
            lblEmpleado.Location = new Point(23, 93);
            lblEmpleado.Name = "lblEmpleado";
            lblEmpleado.Size = new Size(82, 20);
            lblEmpleado.TabIndex = 12;
            lblEmpleado.Text = "Empleado:";
            // 
            // txtEmpleado
            // 
            txtEmpleado.Location = new Point(23, 127);
            txtEmpleado.Margin = new Padding(3, 4, 3, 4);
            txtEmpleado.Name = "txtEmpleado";
            txtEmpleado.Size = new Size(457, 27);
            txtEmpleado.TabIndex = 11;
            txtEmpleado.PlaceholderText = "Buscar empleado...";
            // 
            // lblTipoProceso
            // 
            lblTipoProceso.AutoSize = true;
            lblTipoProceso.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTipoProceso.ForeColor = Color.FromArgb(44, 62, 80);
            lblTipoProceso.Location = new Point(23, 187);
            lblTipoProceso.Name = "lblTipoProceso";
            lblTipoProceso.Size = new Size(124, 20);
            lblTipoProceso.TabIndex = 10;
            lblTipoProceso.Text = "Tipo de Proceso:";
            // 
            // cmbTipoProceso
            // 
            cmbTipoProceso.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoProceso.Items.AddRange(new object[] { "Incorporación", "Desincorporación" });
            cmbTipoProceso.Location = new Point(23, 220);
            cmbTipoProceso.Margin = new Padding(3, 4, 3, 4);
            cmbTipoProceso.Name = "cmbTipoProceso";
            cmbTipoProceso.Size = new Size(457, 28);
            cmbTipoProceso.TabIndex = 9;
            cmbTipoProceso.SelectedIndexChanged += cmbTipoProceso_SelectedIndexChanged;
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFechaInicio.ForeColor = Color.FromArgb(44, 62, 80);
            lblFechaInicio.Location = new Point(23, 280);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(116, 20);
            lblFechaInicio.TabIndex = 8;
            lblFechaInicio.Text = "Fecha de Inicio:";
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Format = DateTimePickerFormat.Short;
            dtpFechaInicio.Location = new Point(23, 313);
            dtpFechaInicio.Margin = new Padding(3, 4, 3, 4);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(457, 27);
            dtpFechaInicio.TabIndex = 7;
            dtpFechaInicio.Value = new DateTime(2025, 11, 14, 19, 16, 20, 462);
            // 
            // lblTareas
            // 
            lblTareas.AutoSize = true;
            lblTareas.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTareas.ForeColor = Color.FromArgb(44, 62, 80);
            lblTareas.Location = new Point(23, 373);
            lblTareas.Name = "lblTareas";
            lblTareas.Size = new Size(58, 20);
            lblTareas.TabIndex = 6;
            lblTareas.Text = "Tareas:";
            // 
            // clbTareas
            // 
            clbTareas.BackColor = Color.White;
            clbTareas.BorderStyle = BorderStyle.FixedSingle;
            clbTareas.CheckOnClick = true;
            clbTareas.Font = new Font("Segoe UI", 9F);
            clbTareas.Location = new Point(23, 407);
            clbTareas.Margin = new Padding(3, 4, 3, 4);
            clbTareas.Name = "clbTareas";
            clbTareas.Size = new Size(457, 266);
            clbTareas.TabIndex = 5;
            // 
            // lblTareaPersonalizada
            // 
            lblTareaPersonalizada.AutoSize = true;
            lblTareaPersonalizada.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTareaPersonalizada.ForeColor = Color.FromArgb(44, 62, 80);
            lblTareaPersonalizada.Location = new Point(23, 693);
            lblTareaPersonalizada.Name = "lblTareaPersonalizada";
            lblTareaPersonalizada.Size = new Size(208, 20);
            lblTareaPersonalizada.TabIndex = 4;
            lblTareaPersonalizada.Text = "Agregar Tarea Personalizada:";
            // 
            // txtTareaPersonalizada
            // 
            txtTareaPersonalizada.Font = new Font("Segoe UI", 9F);
            txtTareaPersonalizada.Location = new Point(23, 727);
            txtTareaPersonalizada.Margin = new Padding(3, 4, 3, 4);
            txtTareaPersonalizada.Name = "txtTareaPersonalizada";
            txtTareaPersonalizada.PlaceholderText = "Escriba una nueva tarea...";
            txtTareaPersonalizada.Size = new Size(342, 27);
            txtTareaPersonalizada.TabIndex = 3;
            txtTareaPersonalizada.KeyDown += txtTareaPersonalizada_KeyDown;
            // 
            // btnAgregarTarea
            // 
            btnAgregarTarea.BackColor = Color.FromArgb(52, 152, 219);
            btnAgregarTarea.Cursor = Cursors.Hand;
            btnAgregarTarea.FlatAppearance.BorderSize = 0;
            btnAgregarTarea.FlatStyle = FlatStyle.Flat;
            btnAgregarTarea.Font = new Font("Segoe UI Semibold", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAgregarTarea.ForeColor = Color.White;
            btnAgregarTarea.Location = new Point(377, 727);
            btnAgregarTarea.Margin = new Padding(3, 4, 3, 4);
            btnAgregarTarea.Name = "btnAgregarTarea";
            btnAgregarTarea.Size = new Size(103, 31);
            btnAgregarTarea.TabIndex = 2;
            btnAgregarTarea.Text = "Agregar";
            btnAgregarTarea.UseVisualStyleBackColor = false;
            btnAgregarTarea.Click += btnAgregarTarea_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.Location = new Point(274, 773);
            btnGuardar.Margin = new Padding(3, 4, 3, 4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(97, 47);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.Location = new Point(383, 773);
            btnCancelar.Margin = new Padding(3, 4, 3, 4);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(97, 47);
            btnCancelar.TabIndex = 0;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // frmNuevaIncorporacion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(526, 853);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(btnAgregarTarea);
            Controls.Add(txtTareaPersonalizada);
            Controls.Add(lblTareaPersonalizada);
            Controls.Add(clbTareas);
            Controls.Add(lblTareas);
            Controls.Add(dtpFechaInicio);
            Controls.Add(lblFechaInicio);
            Controls.Add(cmbTipoProceso);
            Controls.Add(lblTipoProceso);
            Controls.Add(txtEmpleado);
            Controls.Add(lblEmpleado);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmNuevaIncorporacion";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Nuevo Proceso de Incorporación";
            Load += frmNuevaIncorporacion_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

