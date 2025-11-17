namespace RecursosHumanos.Presentation.Views
{
    partial class frmEditarNomina
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblEmpleado;
        private Label lblSalarioBruto;
        private TextBox txtSalarioBruto;
        private Label lblBonificaciones;
        private TextBox txtBonificaciones;
        private Label lblDeducciones;
        private TextBox txtDeducciones;
        private Label lblSalarioNeto;
        private TextBox txtSalarioNeto;
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
            lblSalarioBruto = new Label();
            txtSalarioBruto = new TextBox();
            lblBonificaciones = new Label();
            txtBonificaciones = new TextBox();
            lblDeducciones = new Label();
            txtDeducciones = new TextBox();
            lblSalarioNeto = new Label();
            txtSalarioNeto = new TextBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitulo.Location = new Point(20, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(202, 37);
            lblTitulo.TabIndex = 11;
            lblTitulo.Text = "Editar Nómina";
            // 
            // lblEmpleado
            // 
            lblEmpleado.AutoSize = true;
            lblEmpleado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEmpleado.ForeColor = Color.FromArgb(52, 152, 219);
            lblEmpleado.Location = new Point(20, 60);
            lblEmpleado.Name = "lblEmpleado";
            lblEmpleado.Size = new Size(95, 23);
            lblEmpleado.TabIndex = 10;
            lblEmpleado.Text = "Empleado:";
            // 
            // lblSalarioBruto
            // 
            lblSalarioBruto.AutoSize = true;
            lblSalarioBruto.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSalarioBruto.ForeColor = Color.FromArgb(44, 62, 80);
            lblSalarioBruto.Location = new Point(20, 100);
            lblSalarioBruto.Name = "lblSalarioBruto";
            lblSalarioBruto.Size = new Size(135, 20);
            lblSalarioBruto.TabIndex = 9;
            lblSalarioBruto.Text = "Salario Bruto (S/):";
            // 
            // txtSalarioBruto
            // 
            txtSalarioBruto.Font = new Font("Segoe UI", 9F);
            txtSalarioBruto.Location = new Point(20, 125);
            txtSalarioBruto.Name = "txtSalarioBruto";
            txtSalarioBruto.Size = new Size(400, 27);
            txtSalarioBruto.TabIndex = 8;
            txtSalarioBruto.TextChanged += txtSalarioBruto_TextChanged;
            // 
            // lblBonificaciones
            // 
            lblBonificaciones.AutoSize = true;
            lblBonificaciones.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBonificaciones.ForeColor = Color.FromArgb(44, 62, 80);
            lblBonificaciones.Location = new Point(20, 160);
            lblBonificaciones.Name = "lblBonificaciones";
            lblBonificaciones.Size = new Size(145, 20);
            lblBonificaciones.TabIndex = 7;
            lblBonificaciones.Text = "Bonificaciones (S/):";
            // 
            // txtBonificaciones
            // 
            txtBonificaciones.Font = new Font("Segoe UI", 9F);
            txtBonificaciones.Location = new Point(20, 185);
            txtBonificaciones.Name = "txtBonificaciones";
            txtBonificaciones.Size = new Size(400, 27);
            txtBonificaciones.TabIndex = 6;
            txtBonificaciones.TextChanged += txtBonificaciones_TextChanged;
            // 
            // lblDeducciones
            // 
            lblDeducciones.AutoSize = true;
            lblDeducciones.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDeducciones.ForeColor = Color.FromArgb(44, 62, 80);
            lblDeducciones.Location = new Point(20, 220);
            lblDeducciones.Name = "lblDeducciones";
            lblDeducciones.Size = new Size(132, 20);
            lblDeducciones.TabIndex = 5;
            lblDeducciones.Text = "Deducciones (S/):";
            // 
            // txtDeducciones
            // 
            txtDeducciones.Font = new Font("Segoe UI", 9F);
            txtDeducciones.Location = new Point(20, 245);
            txtDeducciones.Name = "txtDeducciones";
            txtDeducciones.Size = new Size(400, 27);
            txtDeducciones.TabIndex = 4;
            txtDeducciones.TextChanged += txtDeducciones_TextChanged;
            // 
            // lblSalarioNeto
            // 
            lblSalarioNeto.AutoSize = true;
            lblSalarioNeto.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSalarioNeto.ForeColor = Color.FromArgb(44, 62, 80);
            lblSalarioNeto.Location = new Point(20, 280);
            lblSalarioNeto.Name = "lblSalarioNeto";
            lblSalarioNeto.Size = new Size(149, 23);
            lblSalarioNeto.TabIndex = 3;
            lblSalarioNeto.Text = "Salario Neto (S/):";
            // 
            // txtSalarioNeto
            // 
            txtSalarioNeto.BackColor = Color.FromArgb(240, 240, 240);
            txtSalarioNeto.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            txtSalarioNeto.ForeColor = Color.FromArgb(52, 152, 219);
            txtSalarioNeto.Location = new Point(20, 305);
            txtSalarioNeto.Name = "txtSalarioNeto";
            txtSalarioNeto.ReadOnly = true;
            txtSalarioNeto.Size = new Size(400, 30);
            txtSalarioNeto.TabIndex = 2;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(240, 350);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(85, 35);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(335, 350);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(85, 35);
            btnCancelar.TabIndex = 0;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // frmEditarNomina
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(460, 410);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(txtSalarioNeto);
            Controls.Add(lblSalarioNeto);
            Controls.Add(txtDeducciones);
            Controls.Add(lblDeducciones);
            Controls.Add(txtBonificaciones);
            Controls.Add(lblBonificaciones);
            Controls.Add(txtSalarioBruto);
            Controls.Add(lblSalarioBruto);
            Controls.Add(lblEmpleado);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmEditarNomina";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Editar Nómina";
            Load += frmEditarNomina_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

