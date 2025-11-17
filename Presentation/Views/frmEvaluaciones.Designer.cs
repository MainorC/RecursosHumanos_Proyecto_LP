namespace RecursosHumanos.Presentation.Views
{
    partial class frmEvaluaciones
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private GroupBox gbNuevaEvaluacion;
        private TextBox txtEmpleado;
        private DateTimePicker dtpFecha;
        private NumericUpDown nudPuntaje;
        private TextBox txtFortalezas;
        private TextBox txtOportunidades;
        private TextBox txtComentarios;
        private Button btnGuardar;
        private GroupBox gbHistorial;
        private DataGridView dgvEvaluaciones;
        private TextBox txtBuscar;
        private Label lblBuscar;

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
            gbNuevaEvaluacion = new GroupBox();
            lblEmpleado = new Label();
            txtEmpleado = new TextBox();
            lblFecha = new Label();
            dtpFecha = new DateTimePicker();
            lblPuntaje = new Label();
            nudPuntaje = new NumericUpDown();
            lblFortalezas = new Label();
            txtFortalezas = new TextBox();
            lblOportunidades = new Label();
            txtOportunidades = new TextBox();
            lblComentarios = new Label();
            txtComentarios = new TextBox();
            btnGuardar = new Button();
            gbHistorial = new GroupBox();
            dgvEvaluaciones = new DataGridView();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            gbNuevaEvaluacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPuntaje).BeginInit();
            gbHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEvaluaciones).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitulo.Location = new Point(20, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(413, 41);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "Evaluaciones de Desempeño";
            // 
            // gbNuevaEvaluacion
            // 
            gbNuevaEvaluacion.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            gbNuevaEvaluacion.Controls.Add(lblEmpleado);
            gbNuevaEvaluacion.Controls.Add(txtEmpleado);
            gbNuevaEvaluacion.Controls.Add(lblFecha);
            gbNuevaEvaluacion.Controls.Add(dtpFecha);
            gbNuevaEvaluacion.Controls.Add(lblPuntaje);
            gbNuevaEvaluacion.Controls.Add(nudPuntaje);
            gbNuevaEvaluacion.Controls.Add(lblFortalezas);
            gbNuevaEvaluacion.Controls.Add(txtFortalezas);
            gbNuevaEvaluacion.Controls.Add(lblOportunidades);
            gbNuevaEvaluacion.Controls.Add(txtOportunidades);
            gbNuevaEvaluacion.Controls.Add(lblComentarios);
            gbNuevaEvaluacion.Controls.Add(txtComentarios);
            gbNuevaEvaluacion.Controls.Add(btnGuardar);
            gbNuevaEvaluacion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbNuevaEvaluacion.Location = new Point(20, 60);
            gbNuevaEvaluacion.Margin = new Padding(3, 5, 3, 5);
            gbNuevaEvaluacion.Name = "gbNuevaEvaluacion";
            gbNuevaEvaluacion.Padding = new Padding(3, 5, 3, 5);
            gbNuevaEvaluacion.Size = new Size(500, 850);
            gbNuevaEvaluacion.TabIndex = 1;
            gbNuevaEvaluacion.TabStop = false;
            gbNuevaEvaluacion.Text = "Nueva Evaluación";
            // 
            // lblEmpleado
            // 
            lblEmpleado.AutoSize = true;
            lblEmpleado.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEmpleado.ForeColor = Color.FromArgb(44, 62, 80);
            lblEmpleado.Location = new Point(20, 30);
            lblEmpleado.Name = "lblEmpleado";
            lblEmpleado.Size = new Size(82, 20);
            lblEmpleado.TabIndex = 0;
            lblEmpleado.Text = "Empleado:";
            // 
            // txtEmpleado
            // 
            txtEmpleado.Font = new Font("Segoe UI", 9F);
            txtEmpleado.Location = new Point(20, 55);
            txtEmpleado.Margin = new Padding(3, 5, 3, 5);
            txtEmpleado.Name = "txtEmpleado";
            txtEmpleado.Size = new Size(450, 27);
            txtEmpleado.TabIndex = 1;
            txtEmpleado.PlaceholderText = "Buscar empleado...";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFecha.ForeColor = Color.FromArgb(44, 62, 80);
            lblFecha.Location = new Point(20, 100);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(53, 20);
            lblFecha.TabIndex = 2;
            lblFecha.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            dtpFecha.Font = new Font("Segoe UI", 9F);
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(20, 125);
            dtpFecha.Margin = new Padding(3, 5, 3, 5);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(450, 27);
            dtpFecha.TabIndex = 3;
            // 
            // lblPuntaje
            // 
            lblPuntaje.AutoSize = true;
            lblPuntaje.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPuntaje.ForeColor = Color.FromArgb(44, 62, 80);
            lblPuntaje.Location = new Point(20, 170);
            lblPuntaje.Name = "lblPuntaje";
            lblPuntaje.Size = new Size(124, 20);
            lblPuntaje.TabIndex = 4;
            lblPuntaje.Text = "Puntaje (0-100):";
            // 
            // nudPuntaje
            // 
            nudPuntaje.Font = new Font("Segoe UI", 9F);
            nudPuntaje.Location = new Point(20, 195);
            nudPuntaje.Margin = new Padding(3, 5, 3, 5);
            nudPuntaje.Name = "nudPuntaje";
            nudPuntaje.Size = new Size(450, 27);
            nudPuntaje.TabIndex = 5;
            // 
            // lblFortalezas
            // 
            lblFortalezas.AutoSize = true;
            lblFortalezas.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFortalezas.ForeColor = Color.FromArgb(44, 62, 80);
            lblFortalezas.Location = new Point(20, 240);
            lblFortalezas.Name = "lblFortalezas";
            lblFortalezas.Size = new Size(84, 20);
            lblFortalezas.TabIndex = 6;
            lblFortalezas.Text = "Fortalezas:";
            // 
            // txtFortalezas
            // 
            txtFortalezas.Font = new Font("Segoe UI", 9F);
            txtFortalezas.Location = new Point(20, 265);
            txtFortalezas.Margin = new Padding(3, 5, 3, 5);
            txtFortalezas.Multiline = true;
            txtFortalezas.Name = "txtFortalezas";
            txtFortalezas.ScrollBars = ScrollBars.Vertical;
            txtFortalezas.Size = new Size(450, 120);
            txtFortalezas.TabIndex = 7;
            // 
            // lblOportunidades
            // 
            lblOportunidades.AutoSize = true;
            lblOportunidades.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblOportunidades.ForeColor = Color.FromArgb(44, 62, 80);
            lblOportunidades.Location = new Point(20, 400);
            lblOportunidades.Name = "lblOportunidades";
            lblOportunidades.Size = new Size(191, 20);
            lblOportunidades.TabIndex = 8;
            lblOportunidades.Text = "Oportunidades de Mejora:";
            // 
            // txtOportunidades
            // 
            txtOportunidades.Font = new Font("Segoe UI", 9F);
            txtOportunidades.Location = new Point(20, 425);
            txtOportunidades.Margin = new Padding(3, 5, 3, 5);
            txtOportunidades.Multiline = true;
            txtOportunidades.Name = "txtOportunidades";
            txtOportunidades.ScrollBars = ScrollBars.Vertical;
            txtOportunidades.Size = new Size(450, 120);
            txtOportunidades.TabIndex = 9;
            // 
            // lblComentarios
            // 
            lblComentarios.AutoSize = true;
            lblComentarios.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblComentarios.ForeColor = Color.FromArgb(44, 62, 80);
            lblComentarios.Location = new Point(20, 560);
            lblComentarios.Name = "lblComentarios";
            lblComentarios.Size = new Size(102, 20);
            lblComentarios.TabIndex = 10;
            lblComentarios.Text = "Comentarios:";
            // 
            // txtComentarios
            // 
            txtComentarios.Font = new Font("Segoe UI", 9F);
            txtComentarios.Location = new Point(20, 585);
            txtComentarios.Margin = new Padding(3, 5, 3, 5);
            txtComentarios.Multiline = true;
            txtComentarios.Name = "txtComentarios";
            txtComentarios.ScrollBars = ScrollBars.Vertical;
            txtComentarios.Size = new Size(450, 120);
            txtComentarios.TabIndex = 11;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnGuardar.Location = new Point(20, 755);
            btnGuardar.Margin = new Padding(3, 5, 3, 5);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(450, 47);
            btnGuardar.TabIndex = 12;
            btnGuardar.Text = "Guardar Evaluación";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // gbHistorial
            // 
            gbHistorial.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbHistorial.Controls.Add(dgvEvaluaciones);
            gbHistorial.Controls.Add(lblBuscar);
            gbHistorial.Controls.Add(txtBuscar);
            gbHistorial.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbHistorial.Location = new Point(540, 60);
            gbHistorial.Margin = new Padding(3, 5, 3, 5);
            gbHistorial.Name = "gbHistorial";
            gbHistorial.Padding = new Padding(3, 5, 3, 5);
            gbHistorial.Size = new Size(990, 850);
            gbHistorial.TabIndex = 0;
            gbHistorial.TabStop = false;
            gbHistorial.Text = "Historial de Evaluaciones";
            // 
            // dgvEvaluaciones
            // 
            dgvEvaluaciones.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvEvaluaciones.ColumnHeadersHeight = 29;
            dgvEvaluaciones.Location = new Point(20, 65);
            dgvEvaluaciones.Margin = new Padding(3, 5, 3, 5);
            dgvEvaluaciones.Name = "dgvEvaluaciones";
            dgvEvaluaciones.RowHeadersWidth = 4;
            dgvEvaluaciones.Size = new Size(950, 765);
            dgvEvaluaciones.TabIndex = 2;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Font = new Font("Segoe UI", 9F);
            lblBuscar.ForeColor = Color.FromArgb(44, 62, 80);
            lblBuscar.Location = new Point(20, 30);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(55, 20);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            txtBuscar.Font = new Font("Segoe UI", 9F);
            txtBuscar.Location = new Point(80, 28);
            txtBuscar.Margin = new Padding(3, 5, 3, 5);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por empleado o comentarios...";
            txtBuscar.Size = new Size(850, 27);
            txtBuscar.TabIndex = 0;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // frmEvaluaciones
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1550, 940);
            Controls.Add(gbHistorial);
            Controls.Add(gbNuevaEvaluacion);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 5, 3, 5);
            Name = "frmEvaluaciones";
            Load += frmEvaluaciones_Load;
            gbNuevaEvaluacion.ResumeLayout(false);
            gbNuevaEvaluacion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPuntaje).EndInit();
            gbHistorial.ResumeLayout(false);
            gbHistorial.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEvaluaciones).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Label lblEmpleado;
        private Label lblFecha;
        private Label lblPuntaje;
        private Label lblFortalezas;
        private Label lblOportunidades;
        private Label lblComentarios;
    }
}

