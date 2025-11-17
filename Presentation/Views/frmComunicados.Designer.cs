using RecursosHumanos.Common.Helpers;

namespace RecursosHumanos.Presentation.Views
{
    partial class frmComunicados
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private GroupBox gbNuevoComunicado;
        private TextBox txtTitulo;
        private TextBox txtContenido;
        private Button btnGuardar;
        private Button btnLimpiar;
        private GroupBox gbLista;
        private FlowLayoutPanel flpComunicados;
        private TextBox txtBuscar;
        private ComboBox cmbFiltroFecha;
        private Label lblBuscar;
        private Label lblFiltroFecha;

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
            gbNuevoComunicado = new GroupBox();
            lblTituloCom = new Label();
            txtTitulo = new TextBox();
            lblContenido = new Label();
            txtContenido = new TextBox();
            btnGuardar = new Button();
            btnLimpiar = new Button();
            gbLista = new GroupBox();
            flpComunicados = new FlowLayoutPanel();
            lblFiltroFecha = new Label();
            cmbFiltroFecha = new ComboBox();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            gbNuevoComunicado.SuspendLayout();
            gbLista.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = UIHelper.FuenteTituloFormulario;
            lblTitulo.ForeColor = UIHelper.ColorTextoOscuro;
            lblTitulo.Location = new Point(20, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(245, 28);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gestión de Comunicados";
            // 
            // gbNuevoComunicado
            // 
            gbNuevoComunicado.Controls.Add(lblTituloCom);
            gbNuevoComunicado.Controls.Add(txtTitulo);
            gbNuevoComunicado.Controls.Add(lblContenido);
            gbNuevoComunicado.Controls.Add(txtContenido);
            gbNuevoComunicado.Controls.Add(btnGuardar);
            gbNuevoComunicado.Controls.Add(btnLimpiar);
            gbNuevoComunicado.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbNuevoComunicado.Location = new Point(20, 60);
            gbNuevoComunicado.Margin = new Padding(3, 5, 3, 5);
            gbNuevoComunicado.Name = "gbNuevoComunicado";
            gbNuevoComunicado.Padding = new Padding(3, 5, 3, 5);
            gbNuevoComunicado.Size = new Size(500, 400);
            gbNuevoComunicado.TabIndex = 1;
            gbNuevoComunicado.TabStop = false;
            gbNuevoComunicado.Text = "Nuevo Comunicado";
            // 
            // lblTituloCom
            // 
            lblTituloCom.AutoSize = true;
            lblTituloCom.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloCom.ForeColor = Color.FromArgb(44, 62, 80);
            lblTituloCom.Location = new Point(20, 30);
            lblTituloCom.Name = "lblTituloCom";
            lblTituloCom.Size = new Size(54, 20);
            lblTituloCom.TabIndex = 0;
            lblTituloCom.Text = "Título:";
            // 
            // txtTitulo
            // 
            txtTitulo.Font = new Font("Segoe UI", 9F);
            txtTitulo.Location = new Point(20, 55);
            txtTitulo.Margin = new Padding(3, 5, 3, 5);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(450, 27);
            txtTitulo.TabIndex = 1;
            // 
            // lblContenido
            // 
            lblContenido.AutoSize = true;
            lblContenido.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblContenido.ForeColor = Color.FromArgb(44, 62, 80);
            lblContenido.Location = new Point(20, 95);
            lblContenido.Name = "lblContenido";
            lblContenido.Size = new Size(85, 20);
            lblContenido.TabIndex = 2;
            lblContenido.Text = "Contenido:";
            // 
            // txtContenido
            // 
            txtContenido.Font = new Font("Segoe UI", 9F);
            txtContenido.Location = new Point(20, 120);
            txtContenido.Margin = new Padding(3, 5, 3, 5);
            txtContenido.Multiline = true;
            txtContenido.Name = "txtContenido";
            txtContenido.Size = new Size(450, 180);
            txtContenido.TabIndex = 3;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(20, 320);
            btnGuardar.Margin = new Padding(3, 5, 3, 5);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(220, 35);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "Guardar Comunicado";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.Location = new Point(250, 320);
            btnLimpiar.Margin = new Padding(3, 5, 3, 5);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(220, 35);
            btnLimpiar.TabIndex = 5;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // gbLista
            // 
            gbLista.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbLista.Controls.Add(flpComunicados);
            gbLista.Controls.Add(lblFiltroFecha);
            gbLista.Controls.Add(cmbFiltroFecha);
            gbLista.Controls.Add(lblBuscar);
            gbLista.Controls.Add(txtBuscar);
            gbLista.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbLista.Location = new Point(540, 60);
            gbLista.Margin = new Padding(3, 5, 3, 5);
            gbLista.Name = "gbLista";
            gbLista.Padding = new Padding(3, 5, 3, 5);
            gbLista.Size = new Size(990, 850);
            gbLista.TabIndex = 0;
            gbLista.TabStop = false;
            gbLista.Text = "Lista de Comunicados (Total: 0 | Activos: 0 | Este mes: 0)";
            // 
            // flpComunicados
            // 
            flpComunicados.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flpComunicados.AutoScroll = true;
            flpComunicados.FlowDirection = FlowDirection.TopDown;
            flpComunicados.WrapContents = false; // No envolver contenido para que el scrollbar sea vertical
            flpComunicados.Location = new Point(20, 65);
            flpComunicados.Margin = new Padding(3, 5, 3, 5);
            flpComunicados.Name = "flpComunicados";
            flpComunicados.Size = new Size(950, 765);
            flpComunicados.TabIndex = 4;
            // 
            // lblFiltroFecha
            // 
            lblFiltroFecha.AutoSize = true;
            lblFiltroFecha.Font = new Font("Segoe UI", 9F);
            lblFiltroFecha.ForeColor = Color.FromArgb(44, 62, 80);
            lblFiltroFecha.Location = new Point(500, 30);
            lblFiltroFecha.Name = "lblFiltroFecha";
            lblFiltroFecha.Size = new Size(50, 20);
            lblFiltroFecha.TabIndex = 3;
            lblFiltroFecha.Text = "Fecha:";
            // 
            // cmbFiltroFecha
            // 
            cmbFiltroFecha.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroFecha.Font = new Font("Segoe UI", 9F);
            cmbFiltroFecha.Items.AddRange(new object[] { "Todos", "Hoy", "Esta semana", "Este mes", "Últimos 3 meses" });
            cmbFiltroFecha.Location = new Point(560, 28);
            cmbFiltroFecha.Margin = new Padding(3, 5, 3, 5);
            cmbFiltroFecha.Name = "cmbFiltroFecha";
            cmbFiltroFecha.Size = new Size(200, 28);
            cmbFiltroFecha.TabIndex = 2;
            cmbFiltroFecha.SelectedIndexChanged += cmbFiltroFecha_SelectedIndexChanged;
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
            txtBuscar.PlaceholderText = "Buscar por título o contenido...";
            txtBuscar.Size = new Size(400, 27);
            txtBuscar.TabIndex = 0;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // frmComunicados
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1550, 940);
            Controls.Add(gbLista);
            Controls.Add(gbNuevoComunicado);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 5, 3, 5);
            Name = "frmComunicados";
            Load += frmComunicados_Load;
            gbNuevoComunicado.ResumeLayout(false);
            gbNuevoComunicado.PerformLayout();
            gbLista.ResumeLayout(false);
            gbLista.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        private Label lblContenido;
        private Label lblTituloCom;
    }
}

