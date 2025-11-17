namespace RecursosHumanos.Presentation.Views
{
    partial class frmUsuarios
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblAdvertencia;
        private GroupBox gbNuevoUsuario;
        private TextBox txtUsuario;
        private ComboBox cmbRol;
        private TextBox txtNombreCompleto;
        private TextBox txtContrasena;
        private CheckBox chkMostrarContrasena;
        private Button btnGuardar;
        private Button btnLimpiar;
        private GroupBox gbLista;
        private DataGridView dgvUsuarios;
        private Button btnEliminar;

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
            lblAdvertencia = new Label();
            gbNuevoUsuario = new GroupBox();
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            lblRol = new Label();
            cmbRol = new ComboBox();
            lblNombreCompleto = new Label();
            txtNombreCompleto = new TextBox();
            lblContrasena = new Label();
            txtContrasena = new TextBox();
            chkMostrarContrasena = new CheckBox();
            btnGuardar = new Button();
            btnLimpiar = new Button();
            btnEliminar = new Button();
            gbLista = new GroupBox();
            dgvUsuarios = new DataGridView();
            gbNuevoUsuario.SuspendLayout();
            gbLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitulo.Location = new Point(20, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(405, 41);
            lblTitulo.TabIndex = 3;
            lblTitulo.Text = "Administración de Usuarios";
            // 
            // lblAdvertencia
            // 
            lblAdvertencia.AutoSize = true;
            lblAdvertencia.BackColor = Color.LightGoldenrodYellow;
            lblAdvertencia.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAdvertencia.Location = new Point(20, 73);
            lblAdvertencia.Name = "lblAdvertencia";
            lblAdvertencia.Size = new Size(815, 20);
            lblAdvertencia.TabIndex = 2;
            lblAdvertencia.Text = "Advertencia: Estás en la sección de gestión de usuarios. Los cambios aquí afectan directamente el acceso al sistema.";
            // 
            // gbNuevoUsuario
            // 
            gbNuevoUsuario.Controls.Add(lblUsuario);
            gbNuevoUsuario.Controls.Add(txtUsuario);
            gbNuevoUsuario.Controls.Add(lblRol);
            gbNuevoUsuario.Controls.Add(cmbRol);
            gbNuevoUsuario.Controls.Add(lblNombreCompleto);
            gbNuevoUsuario.Controls.Add(txtNombreCompleto);
            gbNuevoUsuario.Controls.Add(lblContrasena);
            gbNuevoUsuario.Controls.Add(txtContrasena);
            gbNuevoUsuario.Controls.Add(chkMostrarContrasena);
            gbNuevoUsuario.Controls.Add(btnGuardar);
            gbNuevoUsuario.Controls.Add(btnLimpiar);
            gbNuevoUsuario.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbNuevoUsuario.Location = new Point(20, 123);
            gbNuevoUsuario.Name = "gbNuevoUsuario";
            gbNuevoUsuario.Size = new Size(400, 412);
            gbNuevoUsuario.TabIndex = 1;
            gbNuevoUsuario.TabStop = false;
            gbNuevoUsuario.Text = "Nuevo Usuario";
            // 
            // lblUsuario
            // 
            lblUsuario.Location = new Point(20, 30);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(100, 23);
            lblUsuario.TabIndex = 0;
            lblUsuario.Text = "Usuario";
            // 
            // txtUsuario
            // 
            txtUsuario.Font = new Font("Segoe UI", 9F);
            txtUsuario.Location = new Point(20, 55);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(350, 27);
            txtUsuario.TabIndex = 1;
            // 
            // lblRol
            // 
            lblRol.Location = new Point(20, 89);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(100, 23);
            lblRol.TabIndex = 2;
            lblRol.Text = "Rol";
            // 
            // cmbRol
            // 
            cmbRol.Font = new Font("Segoe UI", 9F);
            cmbRol.Items.AddRange(new object[] { "Empleado", "Administrador" });
            cmbRol.Location = new Point(20, 115);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(350, 28);
            cmbRol.TabIndex = 3;
            // 
            // lblNombreCompleto
            // 
            lblNombreCompleto.Location = new Point(20, 159);
            lblNombreCompleto.Name = "lblNombreCompleto";
            lblNombreCompleto.Size = new Size(150, 23);
            lblNombreCompleto.TabIndex = 4;
            lblNombreCompleto.Text = "Nombre Completo";
            // 
            // txtNombreCompleto
            // 
            txtNombreCompleto.Font = new Font("Segoe UI", 9F);
            txtNombreCompleto.Location = new Point(20, 185);
            txtNombreCompleto.Name = "txtNombreCompleto";
            txtNombreCompleto.Size = new Size(350, 27);
            txtNombreCompleto.TabIndex = 5;
            // 
            // lblContrasena
            // 
            lblContrasena.Location = new Point(20, 229);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new Size(100, 23);
            lblContrasena.TabIndex = 6;
            lblContrasena.Text = "Contraseña";
            // 
            // txtContrasena
            // 
            txtContrasena.Font = new Font("Segoe UI", 9F);
            txtContrasena.Location = new Point(20, 265);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '*';
            txtContrasena.Size = new Size(350, 27);
            txtContrasena.TabIndex = 7;
            // 
            // chkMostrarContrasena
            // 
            chkMostrarContrasena.Location = new Point(20, 310);
            chkMostrarContrasena.Name = "chkMostrarContrasena";
            chkMostrarContrasena.Size = new Size(150, 20);
            chkMostrarContrasena.TabIndex = 8;
            chkMostrarContrasena.Text = "Mostrar contraseña";
            chkMostrarContrasena.CheckedChanged += chkMostrarContrasena_CheckedChanged;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(20, 349);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(170, 35);
            btnGuardar.TabIndex = 9;
            btnGuardar.Text = "Guardar Usuario";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(200, 349);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(170, 35);
            btnLimpiar.TabIndex = 10;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(0, 0);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 0;
            // 
            // gbLista
            // 
            gbLista.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbLista.Controls.Add(dgvUsuarios);
            gbLista.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbLista.Location = new Point(438, 123);
            gbLista.Name = "gbLista";
            gbLista.Size = new Size(1090, 807);
            gbLista.TabIndex = 0;
            gbLista.TabStop = false;
            gbLista.Text = "Lista de Usuarios";
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvUsuarios.ColumnHeadersHeight = 29;
            dgvUsuarios.Location = new Point(20, 30);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.RowHeadersWidth = 4;
            dgvUsuarios.Size = new Size(1050, 757);
            dgvUsuarios.TabIndex = 0;
            // 
            // frmUsuarios
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1550, 940);
            Controls.Add(gbLista);
            Controls.Add(gbNuevoUsuario);
            Controls.Add(lblAdvertencia);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmUsuarios";
            Load += frmUsuarios_Load;
            gbNuevoUsuario.ResumeLayout(false);
            gbNuevoUsuario.PerformLayout();
            gbLista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Label lblUsuario;
        private Label lblRol;
        private Label lblNombreCompleto;
        private Label lblContrasena;
    }
}

