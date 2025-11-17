namespace RecursosHumanos.Presentation.Views
{
    partial class frmFiltrosReporte
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblFechaInicio;
        private DateTimePicker dtpFechaInicio;
        private Label lblFechaFin;
        private DateTimePicker dtpFechaFin;
        private Button btnGenerar;
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
            lblFechaInicio = new Label();
            dtpFechaInicio = new DateTimePicker();
            lblFechaFin = new Label();
            dtpFechaFin = new DateTimePicker();
            btnGenerar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitulo.Location = new Point(20, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(420, 41);
            lblTitulo.TabIndex = 6;
            lblTitulo.Text = "Seleccionar Rango de Fechas";
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFechaInicio.ForeColor = Color.FromArgb(44, 62, 80);
            lblFechaInicio.Location = new Point(76, 70);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(95, 20);
            lblFechaInicio.TabIndex = 5;
            lblFechaInicio.Text = "Fecha Inicio:";
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Format = DateTimePickerFormat.Short;
            dtpFechaInicio.Location = new Point(76, 110);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(300, 27);
            dtpFechaInicio.TabIndex = 4;
            // 
            // lblFechaFin
            // 
            lblFechaFin.AutoSize = true;
            lblFechaFin.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFechaFin.ForeColor = Color.FromArgb(44, 62, 80);
            lblFechaFin.Location = new Point(76, 151);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(78, 20);
            lblFechaFin.TabIndex = 3;
            lblFechaFin.Text = "Fecha Fin:";
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Format = DateTimePickerFormat.Short;
            dtpFechaFin.Location = new Point(76, 174);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(300, 27);
            dtpFechaFin.TabIndex = 2;
            // 
            // btnGenerar
            // 
            btnGenerar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGenerar.Location = new Point(126, 219);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(85, 35);
            btnGenerar.TabIndex = 1;
            btnGenerar.Text = "Generar";
            btnGenerar.Click += btnGenerar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.Location = new Point(230, 219);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(85, 35);
            btnCancelar.TabIndex = 0;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // frmFiltrosReporte
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(456, 280);
            Controls.Add(btnCancelar);
            Controls.Add(btnGenerar);
            Controls.Add(dtpFechaFin);
            Controls.Add(lblFechaFin);
            Controls.Add(dtpFechaInicio);
            Controls.Add(lblFechaInicio);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmFiltrosReporte";
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

