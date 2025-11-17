using RecursosHumanos.Common.Helpers;

namespace RecursosHumanos.Presentation.Views
{
    partial class frmIncorporacion
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private FlowLayoutPanel flpIncorporaciones;
        private Button btnNuevoProceso;

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
            flpIncorporaciones = new FlowLayoutPanel();
            btnNuevoProceso = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = UIHelper.FuenteTituloFormulario;
            lblTitulo.ForeColor = UIHelper.ColorTextoOscuro;
            lblTitulo.Location = new Point(10, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(462, 50);
            lblTitulo.TabIndex = 3;
            lblTitulo.Text = "Gestión de Incorporación";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSubtitulo.ForeColor = Color.FromArgb(127, 140, 141);
            lblSubtitulo.Location = new Point(20, 66);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(473, 20);
            lblSubtitulo.TabIndex = 2;
            lblSubtitulo.Text = "Supervisa los procesos de Incorporación y Desincorporación de tu equipo.";
            // 
            // flpIncorporaciones
            // 
            flpIncorporaciones.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flpIncorporaciones.AutoScroll = true;
            flpIncorporaciones.BackColor = Color.Transparent;
            flpIncorporaciones.FlowDirection = FlowDirection.TopDown;
            flpIncorporaciones.Location = new Point(20, 105);
            flpIncorporaciones.Name = "flpIncorporaciones";
            flpIncorporaciones.Size = new Size(1510, 799);
            flpIncorporaciones.TabIndex = 0;
            flpIncorporaciones.WrapContents = false;
            flpIncorporaciones.HorizontalScroll.Enabled = false;
            flpIncorporaciones.HorizontalScroll.Visible = false;
            flpIncorporaciones.HorizontalScroll.Maximum = 0;
            flpIncorporaciones.AutoScrollMargin = new Size(0, 0);
            // 
            // btnNuevoProceso
            // 
            btnNuevoProceso.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNuevoProceso.BackColor = Color.FromArgb(52, 152, 219);
            btnNuevoProceso.Cursor = Cursors.Hand;
            btnNuevoProceso.FlatAppearance.BorderSize = 0;
            btnNuevoProceso.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
            btnNuevoProceso.FlatStyle = FlatStyle.Flat;
            btnNuevoProceso.Font = new Font("Segoe UI", 9F);
            btnNuevoProceso.ForeColor = Color.White;
            btnNuevoProceso.Location = new Point(1350, 23);
            btnNuevoProceso.Name = "btnNuevoProceso";
            btnNuevoProceso.Size = new Size(180, 40);
            btnNuevoProceso.TabIndex = 1;
            btnNuevoProceso.Text = "+ Crear Nuevo Proceso";
            btnNuevoProceso.UseVisualStyleBackColor = false;
            btnNuevoProceso.Click += btnNuevoProceso_Click;
            // 
            // frmIncorporacion
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1550, 940);
            Controls.Add(flpIncorporaciones);
            Controls.Add(btnNuevoProceso);
            Controls.Add(lblSubtitulo);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmIncorporacion";
            Load += frmIncorporacion_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

