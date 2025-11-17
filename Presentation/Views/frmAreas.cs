using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Common.Helpers;
using RecursosHumanos.Presentation.Interfaces;
using RecursosHumanos.Presentation.Presenters;
using System.Linq;

namespace RecursosHumanos.Presentation.Views
{
    // formulario para gestion de areas
    public partial class frmAreas : Form, IAreaView
    {
        private readonly AreaPresenter _presenter;
        private ErrorProvider _errorProvider = new ErrorProvider();

        public string Nombre { get => txtNombre.Text; set => txtNombre.Text = value; }
        public string Descripcion { get => txtDescripcion.Text; set => txtDescripcion.Text = value; }
        public bool Activo { get => chkActivo.Checked; set => chkActivo.Checked = value; }

        // Eventos de IAreaView
        public event EventHandler? CargarDatos;
        public event EventHandler? GuardarArea;
        public event EventHandler? EliminarArea;
        public event EventHandler<int>? SeleccionarArea;

        public frmAreas()
        {
            InitializeComponent();
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            _presenter = new AreaPresenter(this);
        }

        private void frmAreas_Load(object sender, EventArgs e)
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
                UIHelper.AplicarEstiloDataGridView(dgvAreas);
                
                dgvAreas.Columns.Clear();
                dgvAreas.AutoGenerateColumns = false;
                dgvAreas.ScrollBars = ScrollBars.Vertical;
                dgvAreas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                
                // Columna Nombre
                var colNombre = new DataGridViewTextBoxColumn
                {
                    Name = "Nombre",
                    HeaderText = "NOMBRE",
                    Width = 200,
                    ReadOnly = true
                };
                colNombre.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                colNombre.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                dgvAreas.Columns.Add(colNombre);
                
                // Columna Descripción
                var colDescripcion = new DataGridViewTextBoxColumn
                {
                    Name = "Descripcion",
                    HeaderText = "DESCRIPCIÓN",
                    Width = 400,
                    ReadOnly = true
                };
                colDescripcion.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                colDescripcion.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                dgvAreas.Columns.Add(colDescripcion);
                
                // Columna Estado
                var colEstado = new DataGridViewTextBoxColumn
                {
                    Name = "Estado",
                    HeaderText = "ESTADO",
                    Width = 120,
                    ReadOnly = true
                };
                dgvAreas.Columns.Add(colEstado);
                
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
                dgvAreas.Columns.Add(colAcciones);
                
                // Calcular ancho disponible
                int anchoDisponible = dgvAreas.Width - SystemInformation.VerticalScrollBarWidth - 4;
                int anchoTotalColumnas = dgvAreas.Columns.Cast<DataGridViewColumn>().Sum(c => c.Width);
                
                // Ajustar columnas para que quepan sin scrollbar horizontal
                if (anchoTotalColumnas > anchoDisponible)
                {
                    double factor = (double)anchoDisponible / anchoTotalColumnas;
                    foreach (DataGridViewColumn col in dgvAreas.Columns)
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
                    colDescripcion.Width += espacioExtra;
                }
                
                // Eventos
                dgvAreas.CellFormatting += DgvAreas_CellFormatting;
                dgvAreas.CellPainting += DgvAreas_CellPainting;
                dgvAreas.RowsAdded += DgvAreas_RowsAdded;
                dgvAreas.Scroll += DgvAreas_Scroll;
                dgvAreas.Resize += DgvAreas_Resize;
                dgvAreas.CellDoubleClick += DgvAreas_CellDoubleClick;
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al configurar el DataGridView: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void DgvAreas_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.CellStyle != null && e.Value != null)
            {
                string columnName = dgvAreas.Columns[e.ColumnIndex].Name;
                
                if (columnName == "Estado")
                {
                    string estado = e.Value.ToString() ?? "";
                    if (estado.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.BackColor = Color.FromArgb(46, 204, 113);
                        e.CellStyle.ForeColor = Color.White;
                        e.CellStyle.SelectionBackColor = Color.FromArgb(46, 204, 113);
                        e.CellStyle.SelectionForeColor = Color.White;
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.FromArgb(231, 76, 60);
                        e.CellStyle.ForeColor = Color.White;
                        e.CellStyle.SelectionBackColor = Color.FromArgb(231, 76, 60);
                        e.CellStyle.SelectionForeColor = Color.White;
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
        }

        private void DgvAreas_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            // Ocultar el contenido de texto de la columna Acciones
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && 
                dgvAreas.Columns[e.ColumnIndex].Name == "Acciones")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                e.Handled = true;
            }
        }

        private void DgvAreas_RowsAdded(object? sender, DataGridViewRowsAddedEventArgs e)
        {
            // Agregar botones a las nuevas filas
            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                if (i < dgvAreas.Rows.Count)
                {
                    AgregarBotonesAcciones(i);
                }
            }
        }

        private void DgvAreas_Scroll(object? sender, ScrollEventArgs e)
        {
            AjustarPosicionBotones();
        }

        private void DgvAreas_Resize(object? sender, EventArgs e)
        {
            AjustarPosicionBotones();
        }

        private void LimpiarBotonesAcciones()
        {
            var controlesAEliminar = new List<Control>();
            foreach (Control ctrl in dgvAreas.Controls)
            {
                if (ctrl.Tag != null && ctrl.Tag.ToString()?.StartsWith("btn_") == true)
                {
                    controlesAEliminar.Add(ctrl);
                }
            }
            
            foreach (var ctrl in controlesAEliminar)
            {
                dgvAreas.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
        }

        private void AgregarBotonesAcciones(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvAreas.Rows.Count) return;
            
            var colAcciones = dgvAreas.Columns["Acciones"];
            if (colAcciones == null) return;

            var cellBounds = dgvAreas.GetCellDisplayRectangle(colAcciones.Index, rowIndex, false);
            if (cellBounds.Width == 0 || cellBounds.Height == 0) return;

            // Limpiar botones existentes de esta fila
            var controlesAEliminar = new List<Control>();
            foreach (Control ctrl in dgvAreas.Controls)
            {
                if (ctrl.Tag != null && ctrl.Tag.ToString()?.StartsWith($"btn_{rowIndex}_") == true)
                {
                    controlesAEliminar.Add(ctrl);
                }
            }
            
            foreach (var ctrl in controlesAEliminar)
            {
                dgvAreas.Controls.Remove(ctrl);
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
                if (dgvAreas.Rows[rowIndex].Tag != null)
                {
                    int areaId = (int)(dgvAreas.Rows[rowIndex].Tag ?? 0);
                    if (areaId > 0)
                    {
                        CargarAreaEnFormulario(areaId);
                    }
                }
            };

            // Crear botón Eliminar
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
                if (dgvAreas.Rows[rowIndex].Tag != null)
                {
                    int areaId = (int)(dgvAreas.Rows[rowIndex].Tag ?? 0);
                    if (areaId > 0)
                    {
                        _presenter.EliminarArea(areaId);
                    }
                }
            };

            // Agregar botones al DataGridView
            dgvAreas.Controls.Add(btnEditar);
            dgvAreas.Controls.Add(btnEliminar);
        }

        private void AjustarPosicionBotones()
        {
            var colAcciones = dgvAreas.Columns["Acciones"];
            if (colAcciones == null) return;

            for (int i = 0; i < dgvAreas.Rows.Count; i++)
            {
                var cellBounds = dgvAreas.GetCellDisplayRectangle(colAcciones.Index, i, false);
                if (cellBounds.Width == 0 || cellBounds.Height == 0) continue;

                foreach (Control ctrl in dgvAreas.Controls)
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
                        ctrl.Visible = cellBounds.IntersectsWith(dgvAreas.ClientRectangle);
                    }
                }
            }
        }

        private void CargarAreaEnFormulario(int areaId)
        {
            SeleccionarArea?.Invoke(this, areaId);
        }

        public void ActualizarInterfazModoEdicion(bool esEdicion, Area? area = null)
        {
            if (esEdicion && area != null)
            {
                gbNuevaArea.Text = $"EDITAR ÁREA - {area.Nombre}";
                btnGuardar.Text = "Actualizar Área";
                btnLimpiar.Text = "Cancelar Edición";
            }
            else
            {
                gbNuevaArea.Text = "NUEVA ÁREA";
                btnGuardar.Text = "Guardar Área";
                btnLimpiar.Text = "Crear Nueva Área";
            }
        }

        private void DgvAreas_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                
                // No hacer nada si se hace doble clic en la columna de acciones
                if (e.ColumnIndex == dgvAreas.Columns["Acciones"].Index)
                    return;
                
                var row = dgvAreas.Rows[e.RowIndex];
                if (row.Tag == null) return;
                
                int areaId = (int)row.Tag;
                CargarAreaEnFormulario(areaId);
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al procesar el doble clic: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }


        public void CargarAreas(List<Area> areas)
        {
            try
            {
                if (dgvAreas.Columns.Count == 0)
                {
                    ConfigurarDataGridView();
                }
                
                // Limpiar botones existentes antes de limpiar las filas
                LimpiarBotonesAcciones();
                
                dgvAreas.Rows.Clear();
                
                foreach (var area in areas)
                {
                    int rowIndex = dgvAreas.Rows.Add(
                        area.Nombre,
                        area.Descripcion,
                        area.Activo ? "Activo" : "Inactivo",
                        "" // Acciones - se llenará con botones
                    );
                    
                    dgvAreas.Rows[rowIndex].Tag = area.Id;
                }
                
                // Agregar botones a todas las filas después de cargar los datos
                for (int i = 0; i < dgvAreas.Rows.Count; i++)
                {
                    AgregarBotonesAcciones(i);
                }
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al cargar áreas: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarArea?.Invoke(this, EventArgs.Empty);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            _presenter.CancelarEdicion();
        }

        public void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            chkActivo.Checked = true;
            
            // Actualizar interfaz para modo nuevo
            ActualizarInterfazModoEdicion(false);
        }

        private void dgvAreas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Este método ya no es necesario, se maneja desde los botones y doble clic
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarArea?.Invoke(this, EventArgs.Empty);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var areas = _presenter.BuscarAreas(txtBuscar.Text);
                
                if (dgvAreas.Columns.Count == 0)
                {
                    ConfigurarDataGridView();
                }
                
                // Limpiar botones existentes antes de limpiar las filas
                LimpiarBotonesAcciones();
                
                dgvAreas.Rows.Clear();
                
                if (areas.Count == 0)
                {
                    // Mostrar mensaje si no hay resultados
                    dgvAreas.Rows.Add("", "", "", "");
                    dgvAreas.Rows[0].Height = 200;
                    dgvAreas.Rows[0].ReadOnly = true;
                    dgvAreas.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
                    dgvAreas.Rows[0].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 250, 250);
                    dgvAreas.Rows[0].DefaultCellStyle.ForeColor = UIHelper.ColorTextoGris;
                    dgvAreas.Rows[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    string mensaje = string.IsNullOrWhiteSpace(txtBuscar.Text)
                        ? "No hay áreas registradas.\nHaga clic en 'Nuevo' para agregar la primera área."
                        : $"No se encontraron áreas con el término '{txtBuscar.Text.Trim()}'.\nIntente con otro término de búsqueda.";
                    dgvAreas.Rows[0].Cells[0].Value = mensaje;
                    dgvAreas.Rows[0].Cells[0].Style.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
                    // Combinar celdas visualmente
                    for (int i = 1; i < dgvAreas.Columns.Count; i++)
                    {
                        dgvAreas.Rows[0].Cells[i].Value = "";
                    }
                }
                else
                {
                    foreach (var area in areas)
                    {
                        int rowIndex = dgvAreas.Rows.Add(
                            area.Nombre,
                            area.Descripcion,
                            area.Activo ? "Activo" : "Inactivo",
                            "" // Acciones - se llenará con botones
                        );
                        
                        dgvAreas.Rows[rowIndex].Tag = area.Id;
                    }
                    
                    // Agregar botones a todas las filas después de cargar los datos
                    for (int i = 0; i < dgvAreas.Rows.Count; i++)
                    {
                        AgregarBotonesAcciones(i);
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al buscar: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        public void MostrarArea(Area area)
        {
            Nombre = area.Nombre;
            Descripcion = area.Descripcion ?? "";
            Activo = area.Activo;
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
                "Nombre" => txtNombre,
                "Descripcion" => txtDescripcion,
                _ => null
            };
        }
    }
}

