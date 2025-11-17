using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Common.Helpers;
using RecursosHumanos.Presentation.Interfaces;
using RecursosHumanos.Presentation.Presenters;
using System.Linq;

namespace RecursosHumanos.Presentation.Views
{
    // formulario para gestion de usuarios
    public partial class frmUsuarios : Form, IUsuarioView
    {
        private readonly UsuarioPresenter _presenter;
        private ErrorProvider _errorProvider = new ErrorProvider();

        public string NombreUsuario { get => txtUsuario.Text; set => txtUsuario.Text = value; }
        public string Contrasena { get => txtContrasena.Text; set => txtContrasena.Text = value; }
        public string NombreCompleto { get => txtNombreCompleto.Text; set => txtNombreCompleto.Text = value; }
        public string Rol { get => cmbRol.Text; set => cmbRol.Text = value; }
        public bool Activo { get => true; set { } } // Los usuarios siempre están activos al crearse

        // Eventos de IUsuarioView
        public event EventHandler? CargarDatos;
        public event EventHandler? GuardarUsuario;
#pragma warning disable CS0067 // Evento nunca usado - parte del contrato de la interfaz MVP
        public event EventHandler? EliminarUsuario;
#pragma warning restore CS0067
        public event EventHandler<int>? SeleccionarUsuario;

        public frmUsuarios()
        {
            InitializeComponent();
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            _presenter = new UsuarioPresenter(this);
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarDatos?.Invoke(this, EventArgs.Empty);
            
            // Inicializar interfaz en modo nuevo
            ActualizarInterfazModoEdicion(false);
        }

        private void ConfigurarDataGridView()
        {
            try
            {
                // Aplicar estilo estándar
                UIHelper.AplicarEstiloDataGridView(dgvUsuarios);
                
                dgvUsuarios.Columns.Clear();
                dgvUsuarios.AutoGenerateColumns = false;
                dgvUsuarios.ScrollBars = ScrollBars.Vertical;
                dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                
                // Columna Usuario
                var colUsuario = new DataGridViewTextBoxColumn
                {
                    Name = "Usuario",
                    HeaderText = "USUARIO",
                    Width = 150,
                    ReadOnly = true
                };
                colUsuario.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                colUsuario.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                dgvUsuarios.Columns.Add(colUsuario);
                
                // Columna Nombre Completo
                var colNombreCompleto = new DataGridViewTextBoxColumn
                {
                    Name = "NombreCompleto",
                    HeaderText = "NOMBRE COMPLETO",
                    Width = 300,
                    ReadOnly = true
                };
                colNombreCompleto.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                colNombreCompleto.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                dgvUsuarios.Columns.Add(colNombreCompleto);
                
                // Columna Rol
                var colRol = new DataGridViewTextBoxColumn
                {
                    Name = "Rol",
                    HeaderText = "ROL",
                    Width = 200,
                    ReadOnly = true
                };
                colRol.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                colRol.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                dgvUsuarios.Columns.Add(colRol);
                
                // Columna Acciones (usando columna de texto con botones reales)
                var colAcciones = new DataGridViewTextBoxColumn
                {
                    Name = "Acciones",
                    HeaderText = "ACCIONES",
                    Width = 180,
                    ReadOnly = true,
                    Resizable = DataGridViewTriState.False
                };
                colAcciones.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colAcciones.DefaultCellStyle.BackColor = Color.White;
                colAcciones.DefaultCellStyle.Padding = new Padding(0);
                colAcciones.DefaultCellStyle.SelectionBackColor = Color.White;
                colAcciones.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvUsuarios.Columns.Add(colAcciones);
                
                // Calcular ancho disponible
                int anchoDisponible = dgvUsuarios.Width - SystemInformation.VerticalScrollBarWidth - 4;
                int anchoTotalColumnas = dgvUsuarios.Columns.Cast<DataGridViewColumn>().Sum(c => c.Width);
                
                // Ajustar columnas para que quepan sin scrollbar horizontal
                if (anchoTotalColumnas > anchoDisponible)
                {
                    double factor = (double)anchoDisponible / anchoTotalColumnas;
                    foreach (DataGridViewColumn col in dgvUsuarios.Columns)
                    {
                        if (col.Name != "Acciones")
                        {
                            col.Width = (int)(col.Width * factor);
                        }
                    }
                }
                else if (anchoTotalColumnas < anchoDisponible)
                {
                    int espacioExtra = anchoDisponible - anchoTotalColumnas;
                    colNombreCompleto.Width += espacioExtra;
                }
                
                // Eventos
                dgvUsuarios.CellFormatting += DgvUsuarios_CellFormatting;
                dgvUsuarios.CellPainting += DgvUsuarios_CellPainting;
                dgvUsuarios.RowsAdded += DgvUsuarios_RowsAdded;
                dgvUsuarios.Scroll += DgvUsuarios_Scroll;
                dgvUsuarios.Resize += DgvUsuarios_Resize;
                dgvUsuarios.CellDoubleClick += DgvUsuarios_CellDoubleClick;
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al configurar el DataGridView: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void DgvUsuarios_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.CellStyle != null && e.Value != null)
            {
                string columnName = dgvUsuarios.Columns[e.ColumnIndex].Name;
                
                if (columnName == "Rol")
                {
                    string rol = e.Value.ToString() ?? "";
                    if (rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60);
                        e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    }
                }
            }
        }

        private void DgvUsuarios_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            // Ocultar el contenido de texto de la columna Acciones
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && 
                dgvUsuarios.Columns[e.ColumnIndex].Name == "Acciones")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                e.Handled = true;
            }
        }

        private void DgvUsuarios_RowsAdded(object? sender, DataGridViewRowsAddedEventArgs e)
        {
            // Agregar botones a las nuevas filas
            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                if (i < dgvUsuarios.Rows.Count)
                {
                    AgregarBotonesAcciones(i);
                }
            }
        }

        private void DgvUsuarios_Scroll(object? sender, ScrollEventArgs e)
        {
            AjustarPosicionBotones();
        }

        private void DgvUsuarios_Resize(object? sender, EventArgs e)
        {
            AjustarPosicionBotones();
        }

        private void LimpiarBotonesAcciones()
        {
            var controlesAEliminar = new List<Control>();
            foreach (Control ctrl in dgvUsuarios.Controls)
            {
                if (ctrl.Tag != null && ctrl.Tag.ToString()?.StartsWith("btn_") == true)
                {
                    controlesAEliminar.Add(ctrl);
                }
            }
            
            foreach (var ctrl in controlesAEliminar)
            {
                dgvUsuarios.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
        }

        private void AgregarBotonesAcciones(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvUsuarios.Rows.Count) return;
            
            var colAcciones = dgvUsuarios.Columns["Acciones"];
            if (colAcciones == null) return;

            var cellBounds = dgvUsuarios.GetCellDisplayRectangle(colAcciones.Index, rowIndex, false);
            if (cellBounds.Width == 0 || cellBounds.Height == 0) return;

            // Verificar si es el usuario admin
            var row = dgvUsuarios.Rows[rowIndex];
            string nombreUsuario = row.Cells["Usuario"]?.Value?.ToString() ?? "";
            bool esAdmin = nombreUsuario.Equals("admin", StringComparison.OrdinalIgnoreCase);

            // Limpiar botones existentes de esta fila
            var controlesAEliminar = new List<Control>();
            foreach (Control ctrl in dgvUsuarios.Controls)
            {
                if (ctrl.Tag != null && ctrl.Tag.ToString()?.StartsWith($"btn_{rowIndex}_") == true)
                {
                    controlesAEliminar.Add(ctrl);
                }
            }
            
            foreach (var ctrl in controlesAEliminar)
            {
                dgvUsuarios.Controls.Remove(ctrl);
                ctrl.Dispose();
            }

            // Crear botón Editar
            var btnEditar = new Button
            {
                Text = "Editar",
                Size = new Size(80, 28),
                Location = new Point(cellBounds.Left + 5, cellBounds.Top + (cellBounds.Height - 28) / 2),
                Tag = $"btn_{rowIndex}_editar",
                Cursor = Cursors.Hand
            };
            UIHelper.AplicarEstiloBotonAccion(btnEditar, TipoBotonAccion.Advertencia);
            btnEditar.Click += (s, e) =>
            {
                if (dgvUsuarios.Rows[rowIndex].Tag != null)
                {
                    int usuarioId = (int)(dgvUsuarios.Rows[rowIndex].Tag ?? 0);
                    if (usuarioId > 0)
                    {
                        CargarUsuarioEnFormulario(usuarioId);
                    }
                }
            };

            // Crear botón Eliminar (solo si no es admin)
            if (!esAdmin)
            {
                var btnEliminar = new Button
                {
                    Text = "Eliminar",
                    Size = new Size(80, 28),
                    Location = new Point(cellBounds.Left + 90, cellBounds.Top + (cellBounds.Height - 28) / 2),
                    Tag = $"btn_{rowIndex}_eliminar",
                    Cursor = Cursors.Hand
                };
                UIHelper.AplicarEstiloBotonAccion(btnEliminar, TipoBotonAccion.Error);
                btnEliminar.Click += (s, e) =>
                {
                    if (dgvUsuarios.Rows[rowIndex].Tag != null)
                    {
                        int usuarioId = (int)(dgvUsuarios.Rows[rowIndex].Tag ?? 0);
                        if (usuarioId > 0)
                        {
                            _presenter.EliminarUsuario(usuarioId);
                        }
                    }
                };

                // Agregar botones al DataGridView
                dgvUsuarios.Controls.Add(btnEditar);
                dgvUsuarios.Controls.Add(btnEliminar);
            }
            else
            {
                // Solo agregar botón Editar para admin
                dgvUsuarios.Controls.Add(btnEditar);
            }
        }

        private void AjustarPosicionBotones()
        {
            var colAcciones = dgvUsuarios.Columns["Acciones"];
            if (colAcciones == null) return;

            for (int i = 0; i < dgvUsuarios.Rows.Count; i++)
            {
                var cellBounds = dgvUsuarios.GetCellDisplayRectangle(colAcciones.Index, i, false);
                if (cellBounds.Width == 0 || cellBounds.Height == 0) continue;

                var row = dgvUsuarios.Rows[i];
                string nombreUsuario = row.Cells["Usuario"]?.Value?.ToString() ?? "";
                bool esAdmin = nombreUsuario.Equals("admin", StringComparison.OrdinalIgnoreCase);

                foreach (Control ctrl in dgvUsuarios.Controls)
                {
                    if (ctrl.Tag != null && ctrl.Tag.ToString()?.StartsWith($"btn_{i}_") == true)
                    {
                        if (ctrl.Tag.ToString()?.EndsWith("_editar") == true)
                        {
                            ctrl.Location = new Point(cellBounds.Left + 5, cellBounds.Top + (cellBounds.Height - ctrl.Height) / 2);
                        }
                        else if (ctrl.Tag.ToString()?.EndsWith("_eliminar") == true)
                        {
                            ctrl.Location = new Point(cellBounds.Left + 90, cellBounds.Top + (cellBounds.Height - ctrl.Height) / 2);
                        }
                        ctrl.Visible = cellBounds.IntersectsWith(dgvUsuarios.ClientRectangle);
                    }
                }
            }
        }

        private void CargarUsuarioEnFormulario(int usuarioId)
        {
            SeleccionarUsuario?.Invoke(this, usuarioId);
        }

        public void ActualizarInterfazModoEdicion(bool esEdicion, Usuario? usuario = null)
        {
            if (esEdicion && usuario != null)
            {
                gbNuevoUsuario.Text = $"EDITAR USUARIO - {usuario.NombreUsuario}";
                btnGuardar.Text = "Actualizar Usuario";
                btnLimpiar.Text = "Cancelar Edición";
                txtUsuario.ReadOnly = true; // Usuario no se puede cambiar al editar
                txtUsuario.BackColor = Color.FromArgb(240, 240, 240); // Indicador visual
            }
            else
            {
                gbNuevoUsuario.Text = "NUEVO USUARIO";
                btnGuardar.Text = "Guardar Usuario";
                btnLimpiar.Text = "Crear Nuevo Usuario";
                txtUsuario.ReadOnly = false;
                txtUsuario.BackColor = Color.White;
            }
        }

        private void DgvUsuarios_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                
                // No hacer nada si se hace doble clic en la columna de acciones
                if (e.ColumnIndex == dgvUsuarios.Columns["Acciones"].Index)
                    return;
                
                var row = dgvUsuarios.Rows[e.RowIndex];
                if (row.Tag == null) return;
                
                int usuarioId = (int)row.Tag;
                CargarUsuarioEnFormulario(usuarioId);
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al procesar el doble clic: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }


        public void CargarUsuarios(List<Usuario> usuarios)
        {
            try
            {
                if (dgvUsuarios.Columns.Count == 0)
                {
                    ConfigurarDataGridView();
                }
                
                // Limpiar botones existentes antes de limpiar las filas
                LimpiarBotonesAcciones();
                
                dgvUsuarios.Rows.Clear();
                
                if (usuarios.Count == 0)
                {
                    // Mostrar mensaje si no hay usuarios
                    dgvUsuarios.Rows.Add("", "", "", "");
                    dgvUsuarios.Rows[0].Height = 200;
                    dgvUsuarios.Rows[0].ReadOnly = true;
                    dgvUsuarios.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
                    dgvUsuarios.Rows[0].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 250, 250);
                    dgvUsuarios.Rows[0].DefaultCellStyle.ForeColor = UIHelper.ColorTextoGris;
                    dgvUsuarios.Rows[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvUsuarios.Rows[0].Cells[0].Value = "No hay usuarios registrados.\nHaga clic en 'Nuevo' para agregar el primer usuario.";
                    dgvUsuarios.Rows[0].Cells[0].Style.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
                    // Combinar celdas visualmente
                    for (int i = 1; i < dgvUsuarios.Columns.Count; i++)
                    {
                        dgvUsuarios.Rows[0].Cells[i].Value = "";
                    }
                }
                else
                {
                    foreach (var usuario in usuarios)
                    {
                        int rowIndex = dgvUsuarios.Rows.Add(
                            usuario.NombreUsuario,
                            usuario.NombreCompleto,
                            usuario.Rol,
                            "" // Acciones - se llenará con botones
                        );
                        
                        dgvUsuarios.Rows[rowIndex].Tag = usuario.Id;
                    }
                    
                    // Agregar botones a todas las filas después de cargar los datos
                    for (int i = 0; i < dgvUsuarios.Rows.Count; i++)
                    {
                        AgregarBotonesAcciones(i);
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al cargar usuarios: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarUsuario?.Invoke(this, EventArgs.Empty);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            _presenter.CancelarEdicion();
        }

        public void LimpiarFormulario()
        {
            txtUsuario.Clear();
            txtContrasena.Clear();
            txtNombreCompleto.Clear();
            cmbRol.SelectedIndex = 0;
            chkMostrarContrasena.Checked = false;
            ActualizarInterfazModoEdicion(false);
        }

        public void MostrarUsuario(Usuario usuario)
        {
            NombreUsuario = usuario.NombreUsuario;
            // No mostrar la contraseña hasheada al editar (por seguridad y para permitir cambiarla)
            Contrasena = "";
            NombreCompleto = usuario.NombreCompleto;
            Rol = usuario.Rol;
            Activo = usuario.Activo;
        }

        public void MostrarMensaje(string mensaje, string titulo, MessageBoxIcon icono)
        {
            UIHelper.MostrarMensaje(mensaje, titulo, icono);
        }

        public void MostrarError(string mensaje, string campo)
        {
            var control = GetControlByField(campo);
            if (control != null)
            {
                _errorProvider.SetError(control, mensaje);
            }
        }

        public void LimpiarErrores()
        {
            _errorProvider.Clear();
        }

        private Control? GetControlByField(string campo)
        {
            return campo switch
            {
                "NombreUsuario" => txtUsuario,
                "Contrasena" => txtContrasena,
                "NombreCompleto" => txtNombreCompleto,
                "Rol" => cmbRol,
                _ => null
            };
        }

        private void chkMostrarContrasena_CheckedChanged(object sender, EventArgs e)
        {
            txtContrasena.PasswordChar = chkMostrarContrasena.Checked ? '\0' : '*';
        }

        // Los eventos CellClick y btnEliminar_Click ya no se usan, ahora se manejan en CellContentClick
    }
}

