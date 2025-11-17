using RecursosHumanos.Common.Helpers;

namespace RecursosHumanos.Presentation.Views
{
    partial class frmDashboard
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblBienvenida;
        private Panel pnlTotalEmpleados;
        private Panel pnlEmpleadosActivos;
        private Panel pnlVacacionesPendientes;
        private Panel pnlSalarioPromedio;
        private Label lblTotalEmpleados;
        private Label lblEmpleadosActivos;
        private Label lblVacacionesPendientes;
        private Label lblSalarioPromedio;
        private Label lblTituloAcciones;
        private FlowLayoutPanel flpAccionesPendientes;
        private FlowLayoutPanel flpComunicados;
        private Label lblTituloComunicados;
        private FlowLayoutPanel flpProximosEventos;
        private Label lblTituloProximosEventos;

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
            lblBienvenida = new Label();
            pnlTotalEmpleados = new Panel();
            lblIconoTotal = new Label();
            lblTotalEmpleados = new Label();
            lblTextoTotal = new Label();
            pnlEmpleadosActivos = new Panel();
            lblIconoActivos = new Label();
            lblEmpleadosActivos = new Label();
            lblTextoActivos = new Label();
            pnlVacacionesPendientes = new Panel();
            lblIconoVacaciones = new Label();
            lblVacacionesPendientes = new Label();
            lblTextoVacaciones = new Label();
            pnlSalarioPromedio = new Panel();
            lblIconoSalario = new Label();
            lblSalarioPromedio = new Label();
            lblTextoSalario = new Label();
            lblTituloAcciones = new Label();
            flpAccionesPendientes = new FlowLayoutPanel();
            flpComunicados = new FlowLayoutPanel();
            lblTituloComunicados = new Label();
            flpProximosEventos = new FlowLayoutPanel();
            lblTituloProximosEventos = new Label();
            lblTitulo = new Label();
            pnlTotalEmpleados.SuspendLayout();
            pnlEmpleadosActivos.SuspendLayout();
            pnlVacacionesPendientes.SuspendLayout();
            pnlSalarioPromedio.SuspendLayout();
            SuspendLayout();
            // 
            // lblBienvenida
            // 
            lblBienvenida.AutoSize = true;
            lblBienvenida.Font = new Font("Segoe UI", 9F);
            lblBienvenida.Location = new Point(44, 68);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(452, 20);
            lblBienvenida.TabIndex = 10;
            lblBienvenida.Text = "Bienvenido, aquí tienes un resumen del estado actual de tu equipo.";
            // 
            // pnlTotalEmpleados
            // 
            pnlTotalEmpleados.BackColor = Color.White;
            pnlTotalEmpleados.BorderStyle = BorderStyle.FixedSingle;
            pnlTotalEmpleados.Controls.Add(lblIconoTotal);
            pnlTotalEmpleados.Controls.Add(lblTotalEmpleados);
            pnlTotalEmpleados.Controls.Add(lblTextoTotal);
            pnlTotalEmpleados.Location = new Point(45, 106);
            pnlTotalEmpleados.Margin = new Padding(3, 5, 3, 5);
            pnlTotalEmpleados.Name = "pnlTotalEmpleados";
            pnlTotalEmpleados.Padding = new Padding(22, 36, 22, 36);
            pnlTotalEmpleados.Size = new Size(280, 228);
            pnlTotalEmpleados.TabIndex = 9;
            // 
            // lblIconoTotal
            // 
            lblIconoTotal.Font = new Font("Segoe UI", 24F);
            lblIconoTotal.ForeColor = Color.FromArgb(52, 152, 219);
            lblIconoTotal.Location = new Point(8, 2);
            lblIconoTotal.Name = "lblIconoTotal";
            lblIconoTotal.Size = new Size(46, 53);
            lblIconoTotal.TabIndex = 0;
            lblIconoTotal.TextAlign = ContentAlignment.MiddleCenter;
            lblIconoTotal.Visible = false;
            // 
            // lblTotalEmpleados
            // 
            lblTotalEmpleados.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            lblTotalEmpleados.ForeColor = Color.FromArgb(44, 62, 80);
            lblTotalEmpleados.Location = new Point(22, 55);
            lblTotalEmpleados.Name = "lblTotalEmpleados";
            lblTotalEmpleados.Size = new Size(222, 101);
            lblTotalEmpleados.TabIndex = 1;
            lblTotalEmpleados.Text = "0";
            lblTotalEmpleados.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTextoTotal
            // 
            lblTextoTotal.Font = new Font("Segoe UI", 9F);
            lblTextoTotal.ForeColor = Color.FromArgb(127, 140, 141);
            lblTextoTotal.Location = new Point(22, 173);
            lblTextoTotal.Name = "lblTextoTotal";
            lblTextoTotal.Size = new Size(222, 31);
            lblTextoTotal.TabIndex = 2;
            lblTextoTotal.Text = "Total Empleados";
            // 
            // pnlEmpleadosActivos
            // 
            pnlEmpleadosActivos.BackColor = Color.White;
            pnlEmpleadosActivos.BorderStyle = BorderStyle.FixedSingle;
            pnlEmpleadosActivos.Controls.Add(lblIconoActivos);
            pnlEmpleadosActivos.Controls.Add(lblEmpleadosActivos);
            pnlEmpleadosActivos.Controls.Add(lblTextoActivos);
            pnlEmpleadosActivos.Location = new Point(432, 106);
            pnlEmpleadosActivos.Margin = new Padding(3, 5, 3, 5);
            pnlEmpleadosActivos.Name = "pnlEmpleadosActivos";
            pnlEmpleadosActivos.Padding = new Padding(22, 36, 22, 36);
            pnlEmpleadosActivos.Size = new Size(280, 228);
            pnlEmpleadosActivos.TabIndex = 8;
            // 
            // lblIconoActivos
            // 
            lblIconoActivos.Font = new Font("Segoe UI", 24F);
            lblIconoActivos.ForeColor = Color.FromArgb(46, 204, 113);
            lblIconoActivos.Location = new Point(18, 12);
            lblIconoActivos.Name = "lblIconoActivos";
            lblIconoActivos.Size = new Size(46, 53);
            lblIconoActivos.TabIndex = 0;
            lblIconoActivos.TextAlign = ContentAlignment.MiddleCenter;
            lblIconoActivos.Visible = false;
            // 
            // lblEmpleadosActivos
            // 
            lblEmpleadosActivos.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            lblEmpleadosActivos.ForeColor = Color.FromArgb(44, 62, 80);
            lblEmpleadosActivos.Location = new Point(22, 55);
            lblEmpleadosActivos.Name = "lblEmpleadosActivos";
            lblEmpleadosActivos.Size = new Size(222, 101);
            lblEmpleadosActivos.TabIndex = 1;
            lblEmpleadosActivos.Text = "0";
            lblEmpleadosActivos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTextoActivos
            // 
            lblTextoActivos.Font = new Font("Segoe UI", 9F);
            lblTextoActivos.ForeColor = Color.FromArgb(127, 140, 141);
            lblTextoActivos.Location = new Point(22, 173);
            lblTextoActivos.Name = "lblTextoActivos";
            lblTextoActivos.Size = new Size(222, 31);
            lblTextoActivos.TabIndex = 2;
            lblTextoActivos.Text = "Empleados Activos";
            // 
            // pnlVacacionesPendientes
            // 
            pnlVacacionesPendientes.BackColor = Color.White;
            pnlVacacionesPendientes.BorderStyle = BorderStyle.FixedSingle;
            pnlVacacionesPendientes.Controls.Add(lblIconoVacaciones);
            pnlVacacionesPendientes.Controls.Add(lblVacacionesPendientes);
            pnlVacacionesPendientes.Controls.Add(lblTextoVacaciones);
            pnlVacacionesPendientes.Location = new Point(819, 106);
            pnlVacacionesPendientes.Margin = new Padding(3, 5, 3, 5);
            pnlVacacionesPendientes.Name = "pnlVacacionesPendientes";
            pnlVacacionesPendientes.Padding = new Padding(22, 36, 22, 36);
            pnlVacacionesPendientes.Size = new Size(280, 228);
            pnlVacacionesPendientes.TabIndex = 7;
            // 
            // lblIconoVacaciones
            // 
            lblIconoVacaciones.Font = new Font("Segoe UI", 24F);
            lblIconoVacaciones.ForeColor = Color.FromArgb(241, 196, 15);
            lblIconoVacaciones.Location = new Point(64, 12);
            lblIconoVacaciones.Name = "lblIconoVacaciones";
            lblIconoVacaciones.Size = new Size(46, 53);
            lblIconoVacaciones.TabIndex = 0;
            lblIconoVacaciones.TextAlign = ContentAlignment.MiddleCenter;
            lblIconoVacaciones.Visible = false;
            // 
            // lblVacacionesPendientes
            // 
            lblVacacionesPendientes.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            lblVacacionesPendientes.ForeColor = Color.FromArgb(44, 62, 80);
            lblVacacionesPendientes.Location = new Point(22, 55);
            lblVacacionesPendientes.Name = "lblVacacionesPendientes";
            lblVacacionesPendientes.Size = new Size(222, 101);
            lblVacacionesPendientes.TabIndex = 1;
            lblVacacionesPendientes.Text = "0";
            lblVacacionesPendientes.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTextoVacaciones
            // 
            lblTextoVacaciones.Font = new Font("Segoe UI", 9F);
            lblTextoVacaciones.ForeColor = Color.FromArgb(127, 140, 141);
            lblTextoVacaciones.Location = new Point(22, 173);
            lblTextoVacaciones.Name = "lblTextoVacaciones";
            lblTextoVacaciones.Size = new Size(222, 31);
            lblTextoVacaciones.TabIndex = 2;
            lblTextoVacaciones.Text = "Vacaciones Pendientes";
            // 
            // pnlSalarioPromedio
            // 
            pnlSalarioPromedio.BackColor = Color.White;
            pnlSalarioPromedio.BorderStyle = BorderStyle.FixedSingle;
            pnlSalarioPromedio.Controls.Add(lblIconoSalario);
            pnlSalarioPromedio.Controls.Add(lblSalarioPromedio);
            pnlSalarioPromedio.Controls.Add(lblTextoSalario);
            pnlSalarioPromedio.Location = new Point(1206, 106);
            pnlSalarioPromedio.Margin = new Padding(3, 5, 3, 5);
            pnlSalarioPromedio.Name = "pnlSalarioPromedio";
            pnlSalarioPromedio.Padding = new Padding(22, 36, 22, 36);
            pnlSalarioPromedio.Size = new Size(280, 228);
            pnlSalarioPromedio.TabIndex = 6;
            // 
            // lblIconoSalario
            // 
            lblIconoSalario.Font = new Font("Segoe UI", 24F);
            lblIconoSalario.ForeColor = Color.FromArgb(155, 89, 182);
            lblIconoSalario.Location = new Point(22, 36);
            lblIconoSalario.Name = "lblIconoSalario";
            lblIconoSalario.Size = new Size(46, 53);
            lblIconoSalario.TabIndex = 0;
            lblIconoSalario.TextAlign = ContentAlignment.MiddleCenter;
            lblIconoSalario.Visible = false;
            // 
            // lblSalarioPromedio
            // 
            lblSalarioPromedio.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblSalarioPromedio.ForeColor = Color.FromArgb(44, 62, 80);
            lblSalarioPromedio.Location = new Point(23, 65);
            lblSalarioPromedio.Name = "lblSalarioPromedio";
            lblSalarioPromedio.Size = new Size(222, 78);
            lblSalarioPromedio.TabIndex = 1;
            lblSalarioPromedio.Text = "S/0.00";
            lblSalarioPromedio.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTextoSalario
            // 
            lblTextoSalario.Font = new Font("Segoe UI", 9F);
            lblTextoSalario.ForeColor = Color.FromArgb(127, 140, 141);
            lblTextoSalario.Location = new Point(22, 173);
            lblTextoSalario.Name = "lblTextoSalario";
            lblTextoSalario.Size = new Size(222, 31);
            lblTextoSalario.TabIndex = 2;
            lblTextoSalario.Text = "Salario Promedio";
            // 
            // lblTituloAcciones
            // 
            lblTituloAcciones.AutoSize = true;
            lblTituloAcciones.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTituloAcciones.ForeColor = Color.FromArgb(44, 62, 80);
            lblTituloAcciones.Location = new Point(44, 394);
            lblTituloAcciones.Name = "lblTituloAcciones";
            lblTituloAcciones.Size = new Size(192, 25);
            lblTituloAcciones.TabIndex = 4;
            lblTituloAcciones.Text = "Acciones Pendientes";
            // 
            // flpAccionesPendientes
            // 
            flpAccionesPendientes.AutoScroll = true;
            flpAccionesPendientes.BackColor = Color.Transparent;
            flpAccionesPendientes.FlowDirection = FlowDirection.TopDown;
            flpAccionesPendientes.Location = new Point(45, 444);
            flpAccionesPendientes.Margin = new Padding(3, 5, 3, 5);
            flpAccionesPendientes.Name = "flpAccionesPendientes";
            flpAccionesPendientes.Size = new Size(576, 170);
            flpAccionesPendientes.TabIndex = 5;
            flpAccionesPendientes.WrapContents = false;
            // 
            // flpComunicados
            // 
            flpComunicados.AutoScroll = true;
            flpComunicados.BackColor = Color.Transparent;
            flpComunicados.FlowDirection = FlowDirection.TopDown;
            flpComunicados.Location = new Point(673, 444);
            flpComunicados.Margin = new Padding(3, 5, 3, 5);
            flpComunicados.Name = "flpComunicados";
            flpComunicados.Size = new Size(813, 456);
            flpComunicados.TabIndex = 3;
            flpComunicados.WrapContents = false;
            // 
            // lblTituloComunicados
            // 
            lblTituloComunicados.AutoSize = true;
            lblTituloComunicados.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTituloComunicados.ForeColor = Color.FromArgb(44, 62, 80);
            lblTituloComunicados.Location = new Point(673, 394);
            lblTituloComunicados.Name = "lblTituloComunicados";
            lblTituloComunicados.Size = new Size(237, 25);
            lblTituloComunicados.TabIndex = 2;
            lblTituloComunicados.Text = "Anuncios de la Compañía";
            // 
            // flpProximosEventos
            // 
            flpProximosEventos.AutoScroll = true;
            flpProximosEventos.BackColor = Color.Transparent;
            flpProximosEventos.FlowDirection = FlowDirection.TopDown;
            flpProximosEventos.Location = new Point(45, 668);
            flpProximosEventos.Margin = new Padding(3, 5, 3, 5);
            flpProximosEventos.Name = "flpProximosEventos";
            flpProximosEventos.Size = new Size(576, 191);
            flpProximosEventos.TabIndex = 1;
            flpProximosEventos.WrapContents = false;
            // 
            // lblTituloProximosEventos
            // 
            lblTituloProximosEventos.AutoSize = true;
            lblTituloProximosEventos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTituloProximosEventos.ForeColor = Color.FromArgb(44, 62, 80);
            lblTituloProximosEventos.Location = new Point(45, 638);
            lblTituloProximosEventos.Name = "lblTituloProximosEventos";
            lblTituloProximosEventos.Size = new Size(252, 25);
            lblTituloProximosEventos.TabIndex = 0;
            lblTituloProximosEventos.Text = "Próximos Eventos (30 días)";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = UIHelper.FuenteTituloFormulario;
            lblTitulo.ForeColor = UIHelper.ColorTextoOscuro;
            lblTitulo.Location = new Point(44, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(281, 50);
            lblTitulo.TabIndex = 11;
            lblTitulo.Text = "Panel Principal";
            // 
            // frmDashboard
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1550, 940);
            Controls.Add(lblTituloProximosEventos);
            Controls.Add(flpProximosEventos);
            Controls.Add(lblTituloComunicados);
            Controls.Add(flpComunicados);
            Controls.Add(lblTituloAcciones);
            Controls.Add(flpAccionesPendientes);
            Controls.Add(pnlSalarioPromedio);
            Controls.Add(pnlVacacionesPendientes);
            Controls.Add(pnlEmpleadosActivos);
            Controls.Add(pnlTotalEmpleados);
            Controls.Add(lblBienvenida);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 5, 3, 5);
            Name = "frmDashboard";
            Load += frmDashboard_Load;
            pnlTotalEmpleados.ResumeLayout(false);
            pnlEmpleadosActivos.ResumeLayout(false);
            pnlVacacionesPendientes.ResumeLayout(false);
            pnlSalarioPromedio.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
        private Label lblIconoTotal;
        private Label lblTextoTotal;
        private Label lblIconoActivos;
        private Label lblTextoActivos;
        private Label lblIconoVacaciones;
        private Label lblTextoVacaciones;
        private Label lblIconoSalario;
        private Label lblTextoSalario;
        private Label lblTitulo;
    }
}

