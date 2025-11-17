using RecursosHumanos.Common.Helpers;

namespace RecursosHumanos.Presentation.Views
{
    partial class frmFiltroPeriodo
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblPeriodo;
        private DateTimePicker dtpPeriodo;
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
            lblPeriodo = new Label();
            dtpPeriodo = new DateTimePicker();
            btnGenerar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = UIHelper.FuenteTituloFormulario;
            lblTitulo.ForeColor = UIHelper.ColorTextoOscuro;
            lblTitulo.Location = new Point(20, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(198, 28);
            lblTitulo.TabIndex = 4;
            lblTitulo.Text = "Seleccionar Período";
            // 
            // lblPeriodo
            // 
            lblPeriodo.AutoSize = true;
            lblPeriodo.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPeriodo.ForeColor = Color.FromArgb(44, 62, 80);
            lblPeriodo.Location = new Point(20, 70);
            lblPeriodo.Name = "lblPeriodo";
            lblPeriodo.Size = new Size(67, 20);
            lblPeriodo.TabIndex = 3;
            lblPeriodo.Text = "Período:";
            // 
            // dtpPeriodo
            // 
            dtpPeriodo.CustomFormat = "MMMM yyyy";
            dtpPeriodo.Format = DateTimePickerFormat.Custom;
            dtpPeriodo.Location = new Point(20, 95);
            dtpPeriodo.Name = "dtpPeriodo";
            dtpPeriodo.ShowUpDown = true;
            dtpPeriodo.Size = new Size(300, 27);
            dtpPeriodo.TabIndex = 2;
            // 
            // btnGenerar
            // 
            btnGenerar.Location = new Point(150, 150);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(85, 35);
            btnGenerar.TabIndex = 1;
            btnGenerar.Text = "Generar";
            btnGenerar.Click += btnGenerar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(245, 150);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(85, 35);
            btnCancelar.TabIndex = 0;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // frmFiltroPeriodo
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(360, 210);
            Controls.Add(btnCancelar);
            Controls.Add(btnGenerar);
            Controls.Add(dtpPeriodo);
            Controls.Add(lblPeriodo);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmFiltroPeriodo";
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

