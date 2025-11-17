namespace RecursosHumanos.Presentation.Views
{
    partial class frmAreas
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private GroupBox gbNuevaArea;
        private TextBox txtNombre;
        private TextBox txtDescripcion;
        private CheckBox chkActivo;
        private Button btnGuardar;
        private Button btnLimpiar;
        private GroupBox gbLista;
        private TextBox txtBuscar;
        private DataGridView dgvAreas;
        private Button btnEliminar;

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
            gbNuevaArea = new GroupBox();
            txtNombre = new TextBox();
            txtDescripcion = new TextBox();
            chkActivo = new CheckBox();
            btnGuardar = new Button();
            btnLimpiar = new Button();
            lblNombre = new Label();
            lblDescripcion = new Label();
            btnEliminar = new Button();
            gbLista = new GroupBox();
            txtBuscar = new TextBox();
            dgvAreas = new DataGridView();
            gbNuevaArea.SuspendLayout();
            gbLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAreas).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitulo.Location = new Point(20, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(254, 41);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "Gestión de Áreas";
            // 
            // gbNuevaArea
            // 
            gbNuevaArea.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            gbNuevaArea.Controls.Add(txtNombre);
            gbNuevaArea.Controls.Add(txtDescripcion);
            gbNuevaArea.Controls.Add(chkActivo);
            gbNuevaArea.Controls.Add(btnGuardar);
            gbNuevaArea.Controls.Add(btnLimpiar);
            gbNuevaArea.Controls.Add(lblNombre);
            gbNuevaArea.Controls.Add(lblDescripcion);
            gbNuevaArea.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbNuevaArea.Location = new Point(20, 60);
            gbNuevaArea.Name = "gbNuevaArea";
            gbNuevaArea.Size = new Size(400, 401);
            gbNuevaArea.TabIndex = 1;
            gbNuevaArea.TabStop = false;
            gbNuevaArea.Text = "Nueva Área";
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNombre.Location = new Point(20, 65);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(350, 27);
            txtNombre.TabIndex = 0;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDescripcion.Location = new Point(20, 143);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(350, 124);
            txtDescripcion.TabIndex = 1;
            // 
            // chkActivo
            // 
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.Location = new Point(20, 283);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(100, 20);
            chkActivo.TabIndex = 2;
            chkActivo.Text = "Activo";
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(20, 324);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(170, 35);
            btnGuardar.TabIndex = 3;
            btnGuardar.Text = "Guardar Área";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(196, 324);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(170, 35);
            btnLimpiar.TabIndex = 4;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNombre.ForeColor = Color.FromArgb(44, 62, 80);
            lblNombre.Location = new Point(20, 30);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(133, 20);
            lblNombre.TabIndex = 6;
            lblNombre.Text = "Nombre del Área:";
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDescripcion.ForeColor = Color.FromArgb(44, 62, 80);
            lblDescripcion.Location = new Point(20, 103);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(94, 20);
            lblDescripcion.TabIndex = 7;
            lblDescripcion.Text = "Descripción:";
            // 
            // btnEliminar
            // 
            btnEliminar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnEliminar.Location = new Point(20, 285);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(350, 35);
            btnEliminar.TabIndex = 5;
            btnEliminar.Text = "Eliminar Área";
            btnEliminar.Click += btnEliminar_Click;
            // 
            // gbLista
            // 
            gbLista.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbLista.Controls.Add(txtBuscar);
            gbLista.Controls.Add(dgvAreas);
            gbLista.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gbLista.Location = new Point(440, 60);
            gbLista.Margin = new Padding(3, 5, 3, 5);
            gbLista.Name = "gbLista";
            gbLista.Padding = new Padding(3, 5, 3, 5);
            gbLista.Size = new Size(1090, 850);
            gbLista.TabIndex = 0;
            gbLista.TabStop = false;
            gbLista.Text = "Lista de Áreas";
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.Font = new Font("Segoe UI", 9F);
            txtBuscar.Location = new Point(20, 30);
            txtBuscar.Margin = new Padding(3, 5, 3, 5);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por nombre...";
            txtBuscar.Size = new Size(1050, 27);
            txtBuscar.TabIndex = 0;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // dgvAreas
            // 
            dgvAreas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAreas.ColumnHeadersHeight = 29;
            dgvAreas.Location = new Point(20, 65);
            dgvAreas.Margin = new Padding(3, 5, 3, 5);
            dgvAreas.Name = "dgvAreas";
            dgvAreas.RowHeadersWidth = 4;
            dgvAreas.Size = new Size(1050, 760);
            dgvAreas.TabIndex = 1;
            // 
            // frmAreas
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1550, 940);
            Controls.Add(gbLista);
            Controls.Add(gbNuevaArea);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmAreas";
            Load += frmAreas_Load;
            gbNuevaArea.ResumeLayout(false);
            gbNuevaArea.PerformLayout();
            gbLista.ResumeLayout(false);
            gbLista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAreas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Label lblNombre;
        private Label lblDescripcion;
    }
}

