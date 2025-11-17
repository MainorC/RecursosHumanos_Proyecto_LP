namespace RecursosHumanos.Presentation.Views
{
    partial class frmLogin
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblUsuario;
        private Label lblContrasena;
        private TextBox txtUsuario;
        private TextBox txtContrasena;
        private Button btnLogin;
        private Label lblError;

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
            lblSubtitulo = new Label();
            lblUsuario = new Label();
            lblContrasena = new Label();
            txtUsuario = new TextBox();
            txtContrasena = new TextBox();
            btnLogin = new Button();
            lblError = new Label();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitulo.Location = new Point(50, 48);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(423, 41);
            lblTitulo.TabIndex = 7;
            lblTitulo.Text = "Sistema de Gestión de RRHH";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSubtitulo.ForeColor = Color.FromArgb(127, 140, 141);
            lblSubtitulo.Location = new Point(50, 104);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(219, 23);
            lblSubtitulo.TabIndex = 6;
            lblSubtitulo.Text = "Inicia sesión para continuar";
            lblSubtitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUsuario.ForeColor = Color.FromArgb(52, 73, 94);
            lblUsuario.Location = new Point(50, 146);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(150, 20);
            lblUsuario.TabIndex = 5;
            lblUsuario.Text = "Nombre de Usuario:";
            // 
            // lblContrasena
            // 
            lblContrasena.AutoSize = true;
            lblContrasena.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblContrasena.ForeColor = Color.FromArgb(52, 73, 94);
            lblContrasena.Location = new Point(50, 224);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new Size(92, 20);
            lblContrasena.TabIndex = 3;
            lblContrasena.Text = "Contraseña:";
            // 
            // txtUsuario
            // 
            txtUsuario.BorderStyle = BorderStyle.FixedSingle;
            txtUsuario.Font = new Font("Segoe UI", 10F);
            txtUsuario.Location = new Point(57, 180);
            txtUsuario.Margin = new Padding(3, 4, 3, 4);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.PlaceholderText = "Usuario";
            txtUsuario.Size = new Size(399, 30);
            txtUsuario.TabIndex = 1;
            txtUsuario.KeyDown += txtUsuario_KeyDown;
            // 
            // txtContrasena
            // 
            txtContrasena.BorderStyle = BorderStyle.FixedSingle;
            txtContrasena.Font = new Font("Segoe UI", 10F);
            txtContrasena.Location = new Point(57, 257);
            txtContrasena.Margin = new Padding(3, 4, 3, 4);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '*';
            txtContrasena.PlaceholderText = "Contraseña";
            txtContrasena.Size = new Size(399, 30);
            txtContrasena.TabIndex = 2;
            txtContrasena.KeyDown += txtContrasena_KeyDown;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(44, 62, 80);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(57, 353);
            btnLogin.Margin = new Padding(3, 4, 3, 4);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(399, 45);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Iniciar Sesión";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblError
            // 
            lblError.Font = new Font("Segoe UI", 9F);
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(57, 302);
            lblError.Name = "lblError";
            lblError.Padding = new Padding(10, 5, 10, 5);
            lblError.Size = new Size(399, 35);
            lblError.TabIndex = 0;
            lblError.TextAlign = ContentAlignment.MiddleLeft;
            lblError.Visible = false;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 240, 241);
            ClientSize = new Size(520, 450);
            Controls.Add(lblError);
            Controls.Add(btnLogin);
            Controls.Add(txtContrasena);
            Controls.Add(lblContrasena);
            Controls.Add(txtUsuario);
            Controls.Add(lblUsuario);
            Controls.Add(lblSubtitulo);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmLogin";
            Padding = new Padding(20);
            StartPosition = FormStartPosition.CenterScreen;
            Load += frmLogin_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

