namespace RecursosHumanos.Presentation.Views
{
    partial class frmReportes
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnEmpleadosActivos;
        private Button btnAsistenciaMensual;
        private Button btnVacaciones;
        private Button btnEvaluaciones;
        private Button btnNomina;

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
            lblDescripcion = new Label();
            btnEmpleadosActivos = new Button();
            btnAsistenciaMensual = new Button();
            btnVacaciones = new Button();
            btnEvaluaciones = new Button();
            btnNomina = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitulo.Location = new Point(20, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(144, 41);
            lblTitulo.TabIndex = 6;
            lblTitulo.Text = "Reportes";
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDescripcion.ForeColor = Color.FromArgb(127, 140, 141);
            lblDescripcion.Location = new Point(20, 76);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(1036, 23);
            lblDescripcion.TabIndex = 5;
            lblDescripcion.Text = "Selecciona un reporte para generar y descargar como archivo Excel. Los filtros por rangos de fecha estarán disponibles donde aplique.";
            // 
            // btnEmpleadosActivos
            // 
            btnEmpleadosActivos.BackColor = Color.White;
            btnEmpleadosActivos.Cursor = Cursors.Hand;
            btnEmpleadosActivos.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199);
            btnEmpleadosActivos.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 245, 245);
            btnEmpleadosActivos.FlatStyle = FlatStyle.Flat;
            btnEmpleadosActivos.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnEmpleadosActivos.ForeColor = Color.FromArgb(44, 62, 80);
            btnEmpleadosActivos.Location = new Point(20, 141);
            btnEmpleadosActivos.Name = "btnEmpleadosActivos";
            btnEmpleadosActivos.Padding = new Padding(15);
            btnEmpleadosActivos.Size = new Size(400, 120);
            btnEmpleadosActivos.TabIndex = 4;
            btnEmpleadosActivos.Text = "Lista de Empleados Activos\r\n\r\nDatos de todos los empleados actualmente activos.\r\n\r\nDescargar Reporte →";
            btnEmpleadosActivos.TextAlign = ContentAlignment.MiddleLeft;
            btnEmpleadosActivos.UseVisualStyleBackColor = false;
            btnEmpleadosActivos.Click += btnEmpleadosActivos_Click;
            // 
            // btnAsistenciaMensual
            // 
            btnAsistenciaMensual.BackColor = Color.White;
            btnAsistenciaMensual.Cursor = Cursors.Hand;
            btnAsistenciaMensual.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199);
            btnAsistenciaMensual.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 245, 245);
            btnAsistenciaMensual.FlatStyle = FlatStyle.Flat;
            btnAsistenciaMensual.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnAsistenciaMensual.ForeColor = Color.FromArgb(44, 62, 80);
            btnAsistenciaMensual.Location = new Point(456, 141);
            btnAsistenciaMensual.Name = "btnAsistenciaMensual";
            btnAsistenciaMensual.Padding = new Padding(15);
            btnAsistenciaMensual.Size = new Size(400, 120);
            btnAsistenciaMensual.TabIndex = 3;
            btnAsistenciaMensual.Text = "Asistencia por Rango de Fechas\r\n\r\nReporte de asistencia personalizable por rango de fechas.\r\n\r\nDescargar Reporte →";
            btnAsistenciaMensual.TextAlign = ContentAlignment.MiddleLeft;
            btnAsistenciaMensual.UseVisualStyleBackColor = false;
            btnAsistenciaMensual.Click += btnAsistenciaMensual_Click;
            // 
            // btnVacaciones
            // 
            btnVacaciones.BackColor = Color.White;
            btnVacaciones.Cursor = Cursors.Hand;
            btnVacaciones.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199);
            btnVacaciones.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 245, 245);
            btnVacaciones.FlatStyle = FlatStyle.Flat;
            btnVacaciones.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnVacaciones.ForeColor = Color.FromArgb(44, 62, 80);
            btnVacaciones.Location = new Point(892, 141);
            btnVacaciones.Name = "btnVacaciones";
            btnVacaciones.Padding = new Padding(15);
            btnVacaciones.Size = new Size(400, 120);
            btnVacaciones.TabIndex = 2;
            btnVacaciones.Text = "Vacaciones por Rango de Fechas\r\n\r\nReporte de solicitudes de vacaciones personalizable por rango de fechas.\r\n\r\nDescargar Reporte →";
            btnVacaciones.TextAlign = ContentAlignment.MiddleLeft;
            btnVacaciones.UseVisualStyleBackColor = false;
            btnVacaciones.Click += btnVacaciones_Click;
            // 
            // btnEvaluaciones
            // 
            btnEvaluaciones.BackColor = Color.White;
            btnEvaluaciones.Cursor = Cursors.Hand;
            btnEvaluaciones.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199);
            btnEvaluaciones.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 245, 245);
            btnEvaluaciones.FlatStyle = FlatStyle.Flat;
            btnEvaluaciones.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEvaluaciones.ForeColor = Color.FromArgb(44, 62, 80);
            btnEvaluaciones.Location = new Point(20, 344);
            btnEvaluaciones.Name = "btnEvaluaciones";
            btnEvaluaciones.Padding = new Padding(15);
            btnEvaluaciones.Size = new Size(400, 120);
            btnEvaluaciones.TabIndex = 1;
            btnEvaluaciones.Text = "Evaluaciones por Rango de Fechas\r\n\r\nReporte de evaluaciones personalizable por rango de fechas.\r\n\r\nDescargar Reporte →";
            btnEvaluaciones.TextAlign = ContentAlignment.MiddleLeft;
            btnEvaluaciones.UseVisualStyleBackColor = false;
            btnEvaluaciones.Click += btnEvaluaciones_Click;
            // 
            // btnNomina
            // 
            btnNomina.BackColor = Color.White;
            btnNomina.Cursor = Cursors.Hand;
            btnNomina.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199);
            btnNomina.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 245, 245);
            btnNomina.FlatStyle = FlatStyle.Flat;
            btnNomina.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnNomina.ForeColor = Color.FromArgb(44, 62, 80);
            btnNomina.Location = new Point(456, 344);
            btnNomina.Name = "btnNomina";
            btnNomina.Padding = new Padding(15);
            btnNomina.Size = new Size(400, 120);
            btnNomina.TabIndex = 0;
            btnNomina.Text = "Nómina por Período\r\n\r\nReporte de nómina personalizable por período (mes y año).\r\n\r\nDescargar Reporte →";
            btnNomina.TextAlign = ContentAlignment.MiddleLeft;
            btnNomina.UseVisualStyleBackColor = false;
            btnNomina.Click += btnNomina_Click;
            // 
            // frmReportes
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1550, 940);
            Controls.Add(btnNomina);
            Controls.Add(btnEvaluaciones);
            Controls.Add(btnVacaciones);
            Controls.Add(btnAsistenciaMensual);
            Controls.Add(btnEmpleadosActivos);
            Controls.Add(lblDescripcion);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmReportes";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

