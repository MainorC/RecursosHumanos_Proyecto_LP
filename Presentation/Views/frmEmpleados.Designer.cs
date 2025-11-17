using RecursosHumanos.Common.Helpers;

namespace RecursosHumanos.Presentation.Views
{
    partial class frmEmpleados
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private GroupBox gbNuevoEmpleado;
        private TextBox txtDNI;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtEmail;
        private TextBox txtTelefono;
        private DateTimePicker dtpFechaNacimiento;
        private TextBox txtDireccion;
        private ComboBox cmbArea;
        private TextBox txtPuesto;
        private ComboBox cmbTipoContrato;
        private DateTimePicker dtpFechaContrato;
        private TextBox txtSalarioBase;
        private ComboBox cmbSistemaPension;
        private ComboBox cmbEstado;
        private Button btnGuardar;
        private Button btnLimpiar;
        private GroupBox gbLista;
        private TextBox txtBuscar;
        private DataGridView dgvEmpleados;
        private Button btnEliminar;
        private Label lblDNI;
        private Label lblNombre;
        private Label lblApellido;
        private Label lblEmail;
        private Label lblTelefono;
        private Label lblFechaNacimiento;
        private Label lblDireccion;
        private Label lblArea;
        private Label lblPuesto;
        private Label lblTipoContrato;
        private Label lblFechaContrato;
        private Label lblSalarioBase;
        private Label lblSistemaPension;
        private Label lblEstado;

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
            gbNuevoEmpleado = new GroupBox();
            lblDNI = new Label();
            txtDNI = new TextBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblApellido = new Label();
            txtApellido = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblTelefono = new Label();
            txtTelefono = new TextBox();
            lblFechaNacimiento = new Label();
            dtpFechaNacimiento = new DateTimePicker();
            lblDireccion = new Label();
            txtDireccion = new TextBox();
            lblArea = new Label();
            cmbArea = new ComboBox();
            lblPuesto = new Label();
            txtPuesto = new TextBox();
            lblTipoContrato = new Label();
            cmbTipoContrato = new ComboBox();
            lblFechaContrato = new Label();
            dtpFechaContrato = new DateTimePicker();
            lblSalarioBase = new Label();
            txtSalarioBase = new TextBox();
            lblSistemaPension = new Label();
            cmbSistemaPension = new ComboBox();
            lblEstado = new Label();
            cmbEstado = new ComboBox();
            btnGuardar = new Button();
            btnLimpiar = new Button();
            btnEliminar = new Button();
            gbLista = new GroupBox();
            txtBuscar = new TextBox();
            dgvEmpleados = new DataGridView();
            gbNuevoEmpleado.SuspendLayout();
            gbLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEmpleados).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = UIHelper.FuenteTituloFormulario;
            lblTitulo.ForeColor = UIHelper.ColorTextoOscuro;
            lblTitulo.Location = new Point(12, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(372, 46);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "Gestión de Empleados";
            // 
            // gbNuevoEmpleado
            // 
            gbNuevoEmpleado.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            gbNuevoEmpleado.Controls.Add(lblDNI);
            gbNuevoEmpleado.Controls.Add(txtDNI);
            gbNuevoEmpleado.Controls.Add(lblNombre);
            gbNuevoEmpleado.Controls.Add(txtNombre);
            gbNuevoEmpleado.Controls.Add(lblApellido);
            gbNuevoEmpleado.Controls.Add(txtApellido);
            gbNuevoEmpleado.Controls.Add(lblEmail);
            gbNuevoEmpleado.Controls.Add(txtEmail);
            gbNuevoEmpleado.Controls.Add(lblTelefono);
            gbNuevoEmpleado.Controls.Add(txtTelefono);
            gbNuevoEmpleado.Controls.Add(lblFechaNacimiento);
            gbNuevoEmpleado.Controls.Add(dtpFechaNacimiento);
            gbNuevoEmpleado.Controls.Add(lblDireccion);
            gbNuevoEmpleado.Controls.Add(txtDireccion);
            gbNuevoEmpleado.Controls.Add(lblArea);
            gbNuevoEmpleado.Controls.Add(cmbArea);
            gbNuevoEmpleado.Controls.Add(lblPuesto);
            gbNuevoEmpleado.Controls.Add(txtPuesto);
            gbNuevoEmpleado.Controls.Add(lblTipoContrato);
            gbNuevoEmpleado.Controls.Add(cmbTipoContrato);
            gbNuevoEmpleado.Controls.Add(lblFechaContrato);
            gbNuevoEmpleado.Controls.Add(dtpFechaContrato);
            gbNuevoEmpleado.Controls.Add(lblSalarioBase);
            gbNuevoEmpleado.Controls.Add(txtSalarioBase);
            gbNuevoEmpleado.Controls.Add(lblSistemaPension);
            gbNuevoEmpleado.Controls.Add(cmbSistemaPension);
            gbNuevoEmpleado.Controls.Add(lblEstado);
            gbNuevoEmpleado.Controls.Add(cmbEstado);
            gbNuevoEmpleado.Controls.Add(btnGuardar);
            gbNuevoEmpleado.Controls.Add(btnLimpiar);
            gbNuevoEmpleado.Controls.Add(btnEliminar);
            gbNuevoEmpleado.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbNuevoEmpleado.Location = new Point(20, 76);
            gbNuevoEmpleado.Margin = new Padding(3, 5, 3, 5);
            gbNuevoEmpleado.Name = "gbNuevoEmpleado";
            gbNuevoEmpleado.Padding = new Padding(3, 5, 3, 5);
            gbNuevoEmpleado.Size = new Size(514, 850);
            gbNuevoEmpleado.TabIndex = 1;
            gbNuevoEmpleado.TabStop = false;
            gbNuevoEmpleado.Text = "Nuevo Empleado";
            // 
            // lblDNI
            // 
            lblDNI.AutoSize = true;
            lblDNI.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDNI.ForeColor = Color.FromArgb(44, 62, 80);
            lblDNI.Location = new Point(22, 56);
            lblDNI.Name = "lblDNI";
            lblDNI.Size = new Size(41, 20);
            lblDNI.TabIndex = 0;
            lblDNI.Text = "DNI:";
            // 
            // txtDNI
            // 
            txtDNI.Font = new Font("Segoe UI", 9F);
            txtDNI.Location = new Point(182, 56);
            txtDNI.Margin = new Padding(3, 5, 3, 5);
            txtDNI.Name = "txtDNI";
            txtDNI.Size = new Size(300, 27);
            txtDNI.TabIndex = 1;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNombre.ForeColor = Color.FromArgb(44, 62, 80);
            lblNombre.Location = new Point(22, 101);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(71, 20);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 9F);
            txtNombre.Location = new Point(182, 101);
            txtNombre.Margin = new Padding(3, 5, 3, 5);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(300, 27);
            txtNombre.TabIndex = 3;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblApellido.ForeColor = Color.FromArgb(44, 62, 80);
            lblApellido.Location = new Point(22, 146);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(71, 20);
            lblApellido.TabIndex = 4;
            lblApellido.Text = "Apellido:";
            // 
            // txtApellido
            // 
            txtApellido.Font = new Font("Segoe UI", 9F);
            txtApellido.Location = new Point(182, 146);
            txtApellido.Margin = new Padding(3, 5, 3, 5);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(300, 27);
            txtApellido.TabIndex = 5;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEmail.ForeColor = Color.FromArgb(44, 62, 80);
            lblEmail.Location = new Point(22, 191);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(51, 20);
            lblEmail.TabIndex = 6;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 9F);
            txtEmail.Location = new Point(182, 191);
            txtEmail.Margin = new Padding(3, 5, 3, 5);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(300, 27);
            txtEmail.TabIndex = 7;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTelefono.ForeColor = Color.FromArgb(44, 62, 80);
            lblTelefono.Location = new Point(22, 236);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(74, 20);
            lblTelefono.TabIndex = 8;
            lblTelefono.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            txtTelefono.Font = new Font("Segoe UI", 9F);
            txtTelefono.Location = new Point(182, 236);
            txtTelefono.Margin = new Padding(3, 5, 3, 5);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(300, 27);
            txtTelefono.TabIndex = 9;
            // 
            // lblFechaNacimiento
            // 
            lblFechaNacimiento.AutoSize = true;
            lblFechaNacimiento.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFechaNacimiento.ForeColor = Color.FromArgb(44, 62, 80);
            lblFechaNacimiento.Location = new Point(22, 281);
            lblFechaNacimiento.Name = "lblFechaNacimiento";
            lblFechaNacimiento.Size = new Size(138, 20);
            lblFechaNacimiento.TabIndex = 10;
            lblFechaNacimiento.Text = "Fecha Nacimiento:";
            // 
            // dtpFechaNacimiento
            // 
            dtpFechaNacimiento.Font = new Font("Segoe UI", 9F);
            dtpFechaNacimiento.Format = DateTimePickerFormat.Short;
            dtpFechaNacimiento.Location = new Point(182, 281);
            dtpFechaNacimiento.Margin = new Padding(3, 5, 3, 5);
            dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            dtpFechaNacimiento.Size = new Size(300, 27);
            dtpFechaNacimiento.TabIndex = 11;
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDireccion.ForeColor = Color.FromArgb(44, 62, 80);
            lblDireccion.Location = new Point(22, 326);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(78, 20);
            lblDireccion.TabIndex = 12;
            lblDireccion.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            txtDireccion.Font = new Font("Segoe UI", 9F);
            txtDireccion.Location = new Point(182, 326);
            txtDireccion.Margin = new Padding(3, 5, 3, 5);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(300, 27);
            txtDireccion.TabIndex = 13;
            // 
            // lblArea
            // 
            lblArea.AutoSize = true;
            lblArea.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblArea.ForeColor = Color.FromArgb(44, 62, 80);
            lblArea.Location = new Point(22, 371);
            lblArea.Name = "lblArea";
            lblArea.Size = new Size(46, 20);
            lblArea.TabIndex = 14;
            lblArea.Text = "Área:";
            // 
            // cmbArea
            // 
            cmbArea.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbArea.Font = new Font("Segoe UI", 9F);
            cmbArea.Location = new Point(182, 371);
            cmbArea.Margin = new Padding(3, 5, 3, 5);
            cmbArea.Name = "cmbArea";
            cmbArea.Size = new Size(300, 28);
            cmbArea.TabIndex = 15;
            // 
            // lblPuesto
            // 
            lblPuesto.AutoSize = true;
            lblPuesto.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPuesto.ForeColor = Color.FromArgb(44, 62, 80);
            lblPuesto.Location = new Point(22, 416);
            lblPuesto.Name = "lblPuesto";
            lblPuesto.Size = new Size(61, 20);
            lblPuesto.TabIndex = 16;
            lblPuesto.Text = "Puesto:";
            // 
            // txtPuesto
            // 
            txtPuesto.Font = new Font("Segoe UI", 9F);
            txtPuesto.Location = new Point(182, 417);
            txtPuesto.Margin = new Padding(3, 5, 3, 5);
            txtPuesto.Name = "txtPuesto";
            txtPuesto.Size = new Size(300, 27);
            txtPuesto.TabIndex = 17;
            // 
            // lblTipoContrato
            // 
            lblTipoContrato.AutoSize = true;
            lblTipoContrato.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTipoContrato.ForeColor = Color.FromArgb(44, 62, 80);
            lblTipoContrato.Location = new Point(22, 461);
            lblTipoContrato.Name = "lblTipoContrato";
            lblTipoContrato.Size = new Size(110, 20);
            lblTipoContrato.TabIndex = 18;
            lblTipoContrato.Text = "Tipo Contrato:";
            // 
            // cmbTipoContrato
            // 
            cmbTipoContrato.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoContrato.Font = new Font("Segoe UI", 9F);
            cmbTipoContrato.Items.AddRange(new object[] { "Indefinido", "Temporal", "Por Obra" });
            cmbTipoContrato.Location = new Point(182, 462);
            cmbTipoContrato.Margin = new Padding(3, 5, 3, 5);
            cmbTipoContrato.Name = "cmbTipoContrato";
            cmbTipoContrato.Size = new Size(300, 28);
            cmbTipoContrato.TabIndex = 19;
            // 
            // lblFechaContrato
            // 
            lblFechaContrato.AutoSize = true;
            lblFechaContrato.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFechaContrato.ForeColor = Color.FromArgb(44, 62, 80);
            lblFechaContrato.Location = new Point(22, 506);
            lblFechaContrato.Name = "lblFechaContrato";
            lblFechaContrato.Size = new Size(119, 20);
            lblFechaContrato.TabIndex = 20;
            lblFechaContrato.Text = "Fecha Contrato:";
            // 
            // dtpFechaContrato
            // 
            dtpFechaContrato.Font = new Font("Segoe UI", 9F);
            dtpFechaContrato.Format = DateTimePickerFormat.Short;
            dtpFechaContrato.Location = new Point(182, 508);
            dtpFechaContrato.Margin = new Padding(3, 5, 3, 5);
            dtpFechaContrato.Name = "dtpFechaContrato";
            dtpFechaContrato.Size = new Size(300, 27);
            dtpFechaContrato.TabIndex = 21;
            // 
            // lblSalarioBase
            // 
            lblSalarioBase.AutoSize = true;
            lblSalarioBase.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSalarioBase.ForeColor = Color.FromArgb(44, 62, 80);
            lblSalarioBase.Location = new Point(22, 551);
            lblSalarioBase.Name = "lblSalarioBase";
            lblSalarioBase.Size = new Size(97, 20);
            lblSalarioBase.TabIndex = 22;
            lblSalarioBase.Text = "Salario Base:";
            // 
            // txtSalarioBase
            // 
            txtSalarioBase.Font = new Font("Segoe UI", 9F);
            txtSalarioBase.Location = new Point(182, 553);
            txtSalarioBase.Margin = new Padding(3, 5, 3, 5);
            txtSalarioBase.Name = "txtSalarioBase";
            txtSalarioBase.Size = new Size(300, 27);
            txtSalarioBase.TabIndex = 23;
            txtSalarioBase.Text = "0";
            // 
            // lblSistemaPension
            // 
            lblSistemaPension.AutoSize = true;
            lblSistemaPension.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSistemaPension.ForeColor = Color.FromArgb(44, 62, 80);
            lblSistemaPension.Location = new Point(22, 596);
            lblSistemaPension.Name = "lblSistemaPension";
            lblSistemaPension.Size = new Size(127, 20);
            lblSistemaPension.TabIndex = 24;
            lblSistemaPension.Text = "Sistema Pensión:";
            // 
            // cmbSistemaPension
            // 
            cmbSistemaPension.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSistemaPension.Font = new Font("Segoe UI", 9F);
            cmbSistemaPension.Items.AddRange(new object[] { "AFP", "ONP" });
            cmbSistemaPension.Location = new Point(182, 598);
            cmbSistemaPension.Margin = new Padding(3, 5, 3, 5);
            cmbSistemaPension.Name = "cmbSistemaPension";
            cmbSistemaPension.Size = new Size(300, 28);
            cmbSistemaPension.TabIndex = 25;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEstado.ForeColor = Color.FromArgb(44, 62, 80);
            lblEstado.Location = new Point(22, 641);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(60, 20);
            lblEstado.TabIndex = 26;
            lblEstado.Text = "Estado:";
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.Font = new Font("Segoe UI", 9F);
            cmbEstado.Items.AddRange(new object[] { "Activo", "Inactivo" });
            cmbEstado.Location = new Point(182, 644);
            cmbEstado.Margin = new Padding(3, 5, 3, 5);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(300, 28);
            cmbEstado.TabIndex = 27;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnGuardar.Location = new Point(20, 720);
            btnGuardar.Margin = new Padding(3, 5, 3, 5);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(220, 35);
            btnGuardar.TabIndex = 28;
            btnGuardar.Text = "Guardar Empleado";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLimpiar.Location = new Point(262, 720);
            btnLimpiar.Margin = new Padding(3, 5, 3, 5);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(220, 35);
            btnLimpiar.TabIndex = 29;
            btnLimpiar.Text = "Limpiar / Nuevo";
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnEliminar.Location = new Point(20, 774);
            btnEliminar.Margin = new Padding(3, 5, 3, 5);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(476, 35);
            btnEliminar.TabIndex = 30;
            btnEliminar.Text = "Eliminar Empleado";
            btnEliminar.Click += btnEliminar_Click;
            // 
            // gbLista
            // 
            gbLista.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbLista.Controls.Add(txtBuscar);
            gbLista.Controls.Add(dgvEmpleados);
            gbLista.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbLista.Location = new Point(540, 76);
            gbLista.Margin = new Padding(3, 5, 3, 5);
            gbLista.Name = "gbLista";
            gbLista.Padding = new Padding(3, 5, 3, 5);
            gbLista.Size = new Size(990, 850);
            gbLista.TabIndex = 0;
            gbLista.TabStop = false;
            gbLista.Text = "Lista de Empleados";
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.Font = new Font("Segoe UI", 9F);
            txtBuscar.Location = new Point(20, 30);
            txtBuscar.Margin = new Padding(3, 5, 3, 5);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por nombre o DNI...";
            txtBuscar.Size = new Size(950, 27);
            txtBuscar.TabIndex = 0;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // dgvEmpleados
            // 
            dgvEmpleados.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvEmpleados.ColumnHeadersHeight = 40;
            dgvEmpleados.EnableHeadersVisualStyles = false;
            dgvEmpleados.Location = new Point(20, 65);
            dgvEmpleados.Margin = new Padding(3, 5, 3, 5);
            dgvEmpleados.Name = "dgvEmpleados";
            dgvEmpleados.RowHeadersWidth = 4;
            dgvEmpleados.RowTemplate.Height = 35;
            dgvEmpleados.ScrollBars = ScrollBars.Vertical;
            dgvEmpleados.Size = new Size(950, 760);
            dgvEmpleados.TabIndex = 1;
            // 
            // frmEmpleados
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1550, 940);
            Controls.Add(gbLista);
            Controls.Add(gbNuevoEmpleado);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 5, 3, 5);
            Name = "frmEmpleados";
            Load += frmEmpleados_Load;
            gbNuevoEmpleado.ResumeLayout(false);
            gbNuevoEmpleado.PerformLayout();
            gbLista.ResumeLayout(false);
            gbLista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEmpleados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
