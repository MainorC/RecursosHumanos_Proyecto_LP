namespace RecursosHumanos.Presentation.Views
{
    partial class frmNomina
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private DateTimePicker dtpPeriodo;
        private Button btnPreparar;
        private DataGridView dgvNomina;

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
            dtpPeriodo = new DateTimePicker();
            btnPreparar = new Button();
            dgvNomina = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvNomina).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitulo.Location = new Point(12, 8);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(339, 41);
            lblTitulo.TabIndex = 3;
            lblTitulo.Text = "Generación de Nómina";
            // 
            // dtpPeriodo
            // 
            dtpPeriodo.CustomFormat = "MMMM yyyy";
            dtpPeriodo.Format = DateTimePickerFormat.Custom;
            dtpPeriodo.Location = new Point(20, 60);
            dtpPeriodo.Name = "dtpPeriodo";
            dtpPeriodo.ShowUpDown = true;
            dtpPeriodo.Size = new Size(200, 27);
            dtpPeriodo.TabIndex = 2;
            dtpPeriodo.ValueChanged += dtpPeriodo_ValueChanged;
            // 
            // btnPreparar
            // 
            btnPreparar.Location = new Point(250, 52);
            btnPreparar.Name = "btnPreparar";
            btnPreparar.Size = new Size(180, 35);
            btnPreparar.TabIndex = 1;
            btnPreparar.Text = "Preparar Nómina";
            btnPreparar.Click += btnPreparar_Click;
            // 
            // dgvNomina
            // 
            dgvNomina.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvNomina.ColumnHeadersHeight = 29;
            dgvNomina.Location = new Point(20, 110);
            dgvNomina.Name = "dgvNomina";
            dgvNomina.RowHeadersWidth = 4;
            dgvNomina.Size = new Size(1510, 800);
            dgvNomina.TabIndex = 0;
            // 
            // frmNomina
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1550, 940);
            Controls.Add(dgvNomina);
            Controls.Add(btnPreparar);
            Controls.Add(dtpPeriodo);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmNomina";
            Load += frmNomina_Load;
            ((System.ComponentModel.ISupportInitialize)dgvNomina).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

