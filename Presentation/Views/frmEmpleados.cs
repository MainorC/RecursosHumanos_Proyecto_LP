using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Common.Helpers;
using System.Linq;

namespace RecursosHumanos.Presentation.Views
{
    // formulario para gestion de empleados
    public partial class frmEmpleados : Form
    {
        private readonly EmpleadoBLL _empleadoBLL = new();
        private readonly AreaBLL _areaBLL = new();
        private Empleado? _empleadoActual = null;
        private ErrorProvider _errorProvider = new ErrorProvider();
        public frmEmpleados()
        {
            InitializeComponent();
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            try
            {
                // configurar datagridview primero
                ConfigurarDataGridView();
                // Luego cargar datos
                CargarAreas();
                CargarEmpleados();
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al cargar el formulario: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
            
            // Ocultar botón eliminar (se elimina desde acciones en la grilla)
            btnEliminar.Visible = false;
            
            // Inicializar interfaz en modo nuevo
            ActualizarInterfazModoEdicion();
        }

        private void ConfigurarDataGridView()
        {
            try
            {
                // Remover eventos anteriores si existen
                dgvEmpleados.CellFormatting -= DgvEmpleados_CellFormatting;
                dgvEmpleados.CellContentClick -= DgvEmpleados_CellContentClick;
                dgvEmpleados.CellDoubleClick -= DgvEmpleados_CellDoubleClick;
                dgvEmpleados.RowsAdded -= DgvEmpleados_RowsAdded;
                dgvEmpleados.CellPainting -= DgvEmpleados_CellPainting;
                dgvEmpleados.Scroll -= DgvEmpleados_Scroll;
                dgvEmpleados.Resize -= DgvEmpleados_Resize;
                
                // Aplicar estilo estándar del UIHelper
                UIHelper.AplicarEstiloDataGridView(dgvEmpleados);
                
                // Limpiar columnas existentes
                dgvEmpleados.Columns.Clear();
                dgvEmpleados.AutoGenerateColumns = false;
                
                // Configuraciones específicas
                dgvEmpleados.ScrollBars = ScrollBars.Vertical;
                dgvEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                
                // Calcular el ancho disponible del DataGridView (considerando scrollbar vertical)
                int anchoDisponible = dgvEmpleados.Width - SystemInformation.VerticalScrollBarWidth - 4;
                
                // Columna Nombre (LinkColumn)
                var colNombre = new DataGridViewLinkColumn
                {
                    Name = "Nombre",
                    HeaderText = "NOMBRE",
                    Width = 200,
                    MinimumWidth = 150,
                    ReadOnly = true,
                    LinkColor = Color.FromArgb(52, 152, 219),
                    VisitedLinkColor = Color.FromArgb(41, 128, 185),
                    ActiveLinkColor = Color.FromArgb(41, 128, 185),
                    TrackVisitedState = false
                };
                colNombre.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 248);
                colNombre.DefaultCellStyle.SelectionForeColor = Color.FromArgb(41, 128, 185);
                dgvEmpleados.Columns.Add(colNombre);
                
                // Columna DNI
                var colDNI = new DataGridViewTextBoxColumn
                {
                    Name = "DNI",
                    HeaderText = "DNI",
                    Width = 120,
                    MinimumWidth = 100,
                    ReadOnly = true
                };
                colDNI.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                colDNI.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                dgvEmpleados.Columns.Add(colDNI);
                
                // Columna Puesto
                var colPuesto = new DataGridViewTextBoxColumn
                {
                    Name = "Puesto",
                    HeaderText = "PUESTO",
                    Width = 180,
                    MinimumWidth = 150,
                    ReadOnly = true
                };
                colPuesto.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                colPuesto.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                dgvEmpleados.Columns.Add(colPuesto);
                
                // Columna Área
                var colArea = new DataGridViewTextBoxColumn
                {
                    Name = "Area",
                    HeaderText = "ÁREA",
                    Width = 150,
                    MinimumWidth = 120,
                    ReadOnly = true
                };
                colArea.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                colArea.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                dgvEmpleados.Columns.Add(colArea);
                
                // Columna Estado
                var colEstado = new DataGridViewTextBoxColumn
                {
                    Name = "Estado",
                    HeaderText = "ESTADO",
                    Width = 120,
                    MinimumWidth = 100,
                    ReadOnly = true
                };
                dgvEmpleados.Columns.Add(colEstado);
                
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
                colAcciones.DefaultCellStyle.SelectionBackColor = Color.White; // No cambiar color al seleccionar
                colAcciones.DefaultCellStyle.SelectionForeColor = Color.White; // Texto invisible (se oculta con CellPainting)
                dgvEmpleados.Columns.Add(colAcciones);
                
                // Calcular ancho total de columnas
                int anchoTotalColumnas = dgvEmpleados.Columns.Cast<DataGridViewColumn>().Sum(c => c.Width);
                
                // Ajustar las columnas para que quepan sin scrollbar horizontal
                // Si el ancho total es mayor que el disponible, reducir proporcionalmente
                if (anchoTotalColumnas > anchoDisponible)
                {
                    // Reducir proporcionalmente todas las columnas excepto la de acciones
                    double factor = (double)anchoDisponible / anchoTotalColumnas;
                    foreach (DataGridViewColumn col in dgvEmpleados.Columns)
                    {
                        if (col.Name != "Acciones")
                        {
                            col.Width = (int)(col.Width * factor);
                        }
                    }
                }
                else if (anchoTotalColumnas < anchoDisponible)
                {
                    // Si hay espacio extra, ajustar la columna de Área para llenar el espacio
                    int espacioExtra = anchoDisponible - anchoTotalColumnas;
                    colArea.Width += espacioExtra;
                }
                
                // Eventos
                dgvEmpleados.CellFormatting += DgvEmpleados_CellFormatting;
                dgvEmpleados.RowsAdded += DgvEmpleados_RowsAdded;
                dgvEmpleados.CellPainting += DgvEmpleados_CellPainting;
                dgvEmpleados.CellDoubleClick += DgvEmpleados_CellDoubleClick;
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al configurar el DataGridView: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void DgvEmpleados_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.CellStyle != null && e.Value != null)
            {
                string columnName = dgvEmpleados.Columns[e.ColumnIndex].Name;
                
                if (columnName == "Estado")
                {
                    string estado = e.Value.ToString() ?? "";
                    if (estado.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.BackColor = Color.FromArgb(46, 204, 113);
                        e.CellStyle.ForeColor = Color.White;
                        // Mantener colores de badge incluso al seleccionar
                        e.CellStyle.SelectionBackColor = Color.FromArgb(46, 204, 113);
                        e.CellStyle.SelectionForeColor = Color.White;
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.FromArgb(149, 165, 166);
                        e.CellStyle.ForeColor = Color.White;
                        // Mantener colores de badge incluso al seleccionar
                        e.CellStyle.SelectionBackColor = Color.FromArgb(149, 165, 166);
                        e.CellStyle.SelectionForeColor = Color.White;
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
        }

        private void DgvEmpleados_RowsAdded(object? sender, DataGridViewRowsAddedEventArgs e)
        {
            // Agregar botones a las nuevas filas
            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                if (i < dgvEmpleados.Rows.Count)
                {
                    AgregarBotonesAcciones(i);
                }
            }
        }

        private void DgvEmpleados_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            // Ocultar el contenido de texto de la columna Acciones
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && 
                dgvEmpleados.Columns[e.ColumnIndex].Name == "Acciones")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                e.Handled = true;
            }
        }

        private void LimpiarBotonesAcciones()
        {
            // Limpiar todos los botones existentes
            var controlesAEliminar = new List<Control>();
            foreach (Control ctrl in dgvEmpleados.Controls)
            {
                if (ctrl.Tag != null && ctrl.Tag.ToString()?.StartsWith("btn_") == true)
                {
                    controlesAEliminar.Add(ctrl);
                }
            }
            
            foreach (var ctrl in controlesAEliminar)
            {
                dgvEmpleados.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
        }

        private void AgregarBotonesAcciones(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvEmpleados.Rows.Count) return;
            
            var colAcciones = dgvEmpleados.Columns["Acciones"];
            if (colAcciones == null) return;

            var cellBounds = dgvEmpleados.GetCellDisplayRectangle(colAcciones.Index, rowIndex, false);
            if (cellBounds.Width == 0 || cellBounds.Height == 0) return;

            // Limpiar botones existentes de esta fila
            var controlesAEliminar = new List<Control>();
            foreach (Control ctrl in dgvEmpleados.Controls)
            {
                if (ctrl.Tag != null && ctrl.Tag.ToString()?.StartsWith($"btn_{rowIndex}_") == true)
                {
                    controlesAEliminar.Add(ctrl);
                }
            }
            
            foreach (var ctrl in controlesAEliminar)
            {
                dgvEmpleados.Controls.Remove(ctrl);
                ctrl.Dispose();
            }

            // Crear botón Editar usando UIHelper
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
                if (dgvEmpleados.Rows[rowIndex].Tag != null)
                {
                    int empleadoId = (int)(dgvEmpleados.Rows[rowIndex].Tag ?? 0);
                    if (empleadoId > 0)
                    {
                        CargarEmpleadoEnFormulario(empleadoId);
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
                if (dgvEmpleados.Rows[rowIndex].Tag != null)
                {
                    int empleadoId = (int)(dgvEmpleados.Rows[rowIndex].Tag ?? 0);
                    if (empleadoId > 0)
                    {
                        EliminarEmpleado(empleadoId);
                    }
                }
            };

            // Agregar botones al DataGridView
            dgvEmpleados.Controls.Add(btnEditar);
            dgvEmpleados.Controls.Add(btnEliminar);
        }

        private void DgvEmpleados_Scroll(object? sender, ScrollEventArgs e)
        {
            AjustarPosicionBotones();
        }

        private void DgvEmpleados_Resize(object? sender, EventArgs e)
        {
            AjustarPosicionBotones();
        }

        private void AjustarPosicionBotones()
        {
            var colAcciones = dgvEmpleados.Columns["Acciones"];
            if (colAcciones == null) return;

            for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
            {
                var cellBounds = dgvEmpleados.GetCellDisplayRectangle(colAcciones.Index, i, false);
                if (cellBounds.Width == 0 || cellBounds.Height == 0) continue;

                // Buscar y ajustar botones de esta fila
                foreach (Control ctrl in dgvEmpleados.Controls)
                {
                    if (ctrl.Tag != null && ctrl.Tag.ToString()?.StartsWith($"btn_{i}_") == true)
                    {
                        if (ctrl.Tag.ToString()?.EndsWith("_editar") == true)
                        {
                            ctrl.Location = new Point(cellBounds.Left + 5, cellBounds.Top + (cellBounds.Height - ctrl.Height) / 2);
                        }
                        else if (ctrl.Tag.ToString()?.EndsWith("_eliminar") == true)
                        {
                            ctrl.Location = new Point(cellBounds.Left + 90, cellBounds.Top + (cellBounds.Height - 28) / 2);
                        }
                        ctrl.Visible = cellBounds.IntersectsWith(dgvEmpleados.ClientRectangle);
                    }
                }
            }
        }

        private void DgvEmpleados_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                
                var row = dgvEmpleados.Rows[e.RowIndex];
                if (row.Tag == null) return;
                
                int empleadoId = (int)row.Tag;
                
                if (e.ColumnIndex == dgvEmpleados.Columns["Nombre"].Index)
                {
                    // Click en el nombre (link) - cargar empleado en el formulario
                    CargarEmpleadoEnFormulario(empleadoId);
                }
                
                dgvEmpleados.EndEdit();
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al procesar el clic: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void DgvEmpleados_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                
                // No hacer nada si se hace doble clic en la columna de acciones
                if (e.ColumnIndex == dgvEmpleados.Columns["Acciones"].Index)
                    return;
                
                var row = dgvEmpleados.Rows[e.RowIndex];
                if (row.Tag == null) return;
                
                int empleadoId = (int)row.Tag;
                CargarEmpleadoEnFormulario(empleadoId);
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al procesar el doble clic: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void CargarEmpleadoEnFormulario(int empleadoId)
        {
            try
            {
                var empleado = _empleadoBLL.ObtenerPorId(empleadoId);
                if (empleado != null)
                {
                    _empleadoActual = empleado;
                    txtDNI.Text = empleado.DNI;
                    txtDNI.ReadOnly = true; // DNI no se puede cambiar al editar
                    txtDNI.BackColor = Color.FromArgb(240, 240, 240); // Indicador visual
                    txtNombre.Text = empleado.Nombre;
                    txtApellido.Text = empleado.Apellido;
                    txtEmail.Text = empleado.Email;
                    txtTelefono.Text = empleado.Telefono ?? "";
                    if (empleado.FechaNacimiento.HasValue)
                    {
                        dtpFechaNacimiento.Value = empleado.FechaNacimiento.Value;
                        dtpFechaNacimiento.Checked = true;
                    }
                    else
                    {
                        dtpFechaNacimiento.Checked = false;
                    }
                    txtDireccion.Text = empleado.Direccion ?? "";
                    if (empleado.AreaId.HasValue)
                    {
                        cmbArea.SelectedValue = empleado.AreaId.Value;
                    }
                    txtPuesto.Text = empleado.Puesto ?? "";
                    cmbTipoContrato.Text = empleado.TipoContrato ?? "";
                    if (empleado.FechaContrato.HasValue)
                    {
                        dtpFechaContrato.Value = empleado.FechaContrato.Value;
                        dtpFechaContrato.Checked = true;
                    }
                    else
                    {
                        dtpFechaContrato.Checked = false;
                    }
                    txtSalarioBase.Text = empleado.SalarioBase.ToString();
                    cmbSistemaPension.Text = empleado.SistemaPension ?? "";
                    cmbEstado.Text = empleado.Estado;
                    
                    // Actualizar título y botones para indicar modo edición
                    ActualizarInterfazModoEdicion();
                }
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al cargar empleado: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void ActualizarInterfazModoEdicion()
        {
            if (_empleadoActual != null)
            {
                gbNuevoEmpleado.Text = $"EDITAR EMPLEADO - {_empleadoActual.NombreCompleto}";
                btnGuardar.Text = "Actualizar Empleado";
                btnLimpiar.Text = "Cancelar Edición";
            }
            else
            {
                gbNuevoEmpleado.Text = "NUEVO EMPLEADO";
                btnGuardar.Text = "Guardar Empleado";
                btnLimpiar.Text = "Crear Nuevo Empleado";
            }
        }

        private void EliminarEmpleado(int empleadoId)
        {
            try
            {
                var empleado = _empleadoBLL.ObtenerPorId(empleadoId);
                if (empleado == null) return;
                
                var mensaje = $"¿Está seguro que desea eliminar al empleado {empleado.NombreCompleto}?\n\n" +
                             "Esta acción no se puede deshacer.";
                
                if (UIHelper.MostrarConfirmacion(mensaje, "Confirmar Eliminación") == DialogResult.Yes)
                {
                    _empleadoBLL.Eliminar(empleadoId);
                    UIHelper.MostrarMensaje("Empleado eliminado exitosamente.", "Éxito", MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarEmpleados();
                }
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al eliminar empleado: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void CargarAreas()
        {
            try
            {
                var areas = _areaBLL.ObtenerTodas(soloActivas: true);
                cmbArea.DataSource = areas;
                cmbArea.DisplayMember = "Nombre";
                cmbArea.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al cargar áreas: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void CargarEmpleados()
        {
            try
            {
                // Verificar que las columnas estén configuradas
                if (dgvEmpleados.Columns.Count == 0)
                {
                    ConfigurarDataGridView();
                }
                
                var empleados = _empleadoBLL.ObtenerTodos(soloActivos: true);
                
                // Calcular y mostrar información del tamaño (solo en modo debug)
                #if DEBUG
                CalcularTamanioInformacion();
                #endif
                
                // Limpiar botones existentes antes de limpiar las filas
                LimpiarBotonesAcciones();
                
                // Limpiar el DataGridView
                dgvEmpleados.Rows.Clear();
                
                // Agregar cada empleado como fila
                foreach (var empleado in empleados)
                {
                    int rowIndex = dgvEmpleados.Rows.Add(
                        empleado.NombreCompleto,  // Nombre (columna 0)
                        empleado.DNI,             // DNI (columna 1)
                        empleado.Puesto ?? "",    // Puesto (columna 2)
                        empleado.NombreArea ?? "Sin área", // Área (columna 3)
                        empleado.Estado ?? "Activo", // Estado (columna 4)
                        ""                        // Acciones (columna 5) - se llenará con botones
                    );
                    
                    // Guardar el ID del empleado en el Tag de la fila
                    dgvEmpleados.Rows[rowIndex].Tag = empleado.Id;
                }
                
                // Mostrar mensaje si no hay empleados
                if (empleados.Count == 0)
                {
                    // Limpiar botones antes de mostrar mensaje vacío
                    LimpiarBotonesAcciones();
                    dgvEmpleados.Rows.Clear();
                    
                    // Crear una fila con el mensaje
                    int rowIndex = dgvEmpleados.Rows.Add();
                    dgvEmpleados.Rows[rowIndex].Height = 120;
                    dgvEmpleados.Rows[rowIndex].ReadOnly = true;
                    dgvEmpleados.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
                    dgvEmpleados.Rows[rowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 250, 250);
                    dgvEmpleados.Rows[rowIndex].DefaultCellStyle.ForeColor = UIHelper.ColorTextoGris;
                    dgvEmpleados.Rows[rowIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    
                    string mensaje = string.IsNullOrWhiteSpace(txtBuscar.Text)
                        ? "No hay empleados registrados.\n\nHaga clic en 'Crear Nuevo Empleado' para agregar el primer empleado."
                        : "No se encontraron empleados que coincidan con la búsqueda.\n\nIntente con otros términos de búsqueda.";
                    
                    // Mostrar el mensaje en la primera celda y extenderlo visualmente
                    var cell = dgvEmpleados.Rows[rowIndex].Cells[0];
                    cell.Value = mensaje;
                    cell.Style.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
                    cell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    cell.Style.BackColor = Color.FromArgb(250, 250, 250);
                    cell.Style.SelectionBackColor = Color.FromArgb(250, 250, 250);
                    cell.Style.ForeColor = UIHelper.ColorTextoGris;
                    cell.Style.SelectionForeColor = UIHelper.ColorTextoGris;
                    // Asegurar que no se muestre como hipervínculo (la columna Nombre es LinkColumn)
                    if (cell is DataGridViewLinkCell linkCell)
                    {
                        linkCell.LinkBehavior = LinkBehavior.NeverUnderline;
                        linkCell.LinkColor = UIHelper.ColorTextoGris;
                        linkCell.VisitedLinkColor = UIHelper.ColorTextoGris;
                        linkCell.ActiveLinkColor = UIHelper.ColorTextoGris;
                    }
                    
                    // Limpiar y estilizar las demás celdas
                    for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
                    {
                        var otherCell = dgvEmpleados.Rows[rowIndex].Cells[i];
                        otherCell.Value = "";
                        otherCell.Style.BackColor = Color.FromArgb(250, 250, 250);
                        otherCell.Style.SelectionBackColor = Color.FromArgb(250, 250, 250);
                        otherCell.Style.ForeColor = UIHelper.ColorTextoGris;
                    }
                    
                    // Asegurar que no se muestren botones en la fila vacía
                    LimpiarBotonesAcciones();
                }
                else
                {
                    // Agregar botones a todas las filas después de cargar los datos
                    for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
                    {
                        AgregarBotonesAcciones(i);
                    }
                    
                    // Ajustar posición de botones cuando se hace scroll o se redimensiona
                    dgvEmpleados.Scroll += DgvEmpleados_Scroll;
                    dgvEmpleados.Resize += DgvEmpleados_Resize;
                }
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al cargar empleados: {ex.Message}\n\n{ex.StackTrace}", "Error", MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Calcula y muestra información sobre el tamaño del DataGridView y sus columnas
        /// Útil para ajustar el diseño y verificar que los scrollbars funcionen correctamente
        /// </summary>
        private void CalcularTamanioInformacion()
        {
            try
            {
                int anchoTotalColumnas = dgvEmpleados.Columns.Cast<DataGridViewColumn>().Sum(c => c.Width);
                int altoTotalFilas = dgvEmpleados.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
                int anchoDisponible = dgvEmpleados.ClientSize.Width;
                int altoDisponible = dgvEmpleados.ClientSize.Height;
                int anchoScrollbar = SystemInformation.VerticalScrollBarWidth;
                int altoScrollbar = SystemInformation.HorizontalScrollBarHeight;
                
                string info = $"=== INFORMACIÓN DEL DATAGRIDVIEW ===\n\n" +
                             $"Tamaño del DataGridView: {dgvEmpleados.Width} x {dgvEmpleados.Height}\n" +
                             $"Área cliente (disponible): {anchoDisponible} x {altoDisponible}\n\n" +
                             $"Ancho total de columnas: {anchoTotalColumnas} px\n" +
                             $"Alto total de filas: {altoTotalFilas} px\n\n" +
                             $"Ancho scrollbar vertical: {anchoScrollbar} px\n" +
                             $"Alto scrollbar horizontal: {altoScrollbar} px\n\n" +
                             $"¿Scrollbar horizontal necesario?: NO (solo vertical habilitado)\n" +
                             $"¿Scrollbar vertical necesario?: {(altoTotalFilas > altoDisponible ? "SÍ" : "NO")}\n\n" +
                             $"Columnas:\n";
                
                foreach (DataGridViewColumn col in dgvEmpleados.Columns)
                {
                    info += $"  - {col.HeaderText}: {col.Width} px (Min: {col.MinimumWidth} px)\n";
                }
                
                // Mostrar en la consola de debug
                System.Diagnostics.Debug.WriteLine(info);
                
                // También puedes descomentar la siguiente línea para verlo en un MessageBox
                // UIHelper.MostrarMensaje(info, "Información del DataGridView", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al calcular tamaño: {ex.Message}");
            }
        }

        private bool ValidarFormulario()
        {
            bool esValido = true;
            UIHelper.LimpiarErrores(_errorProvider);

            if (!UIHelper.ValidarDNI(txtDNI, _errorProvider))
                esValido = false;

            if (!UIHelper.ValidarCampoRequerido(txtNombre, _errorProvider, "El nombre es obligatorio"))
                esValido = false;

            if (!UIHelper.ValidarCampoRequerido(txtApellido, _errorProvider, "El apellido es obligatorio"))
                esValido = false;

            if (!UIHelper.ValidarEmail(txtEmail, _errorProvider))
                esValido = false;

            // telefono opcional
            if (!string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                if (!UIHelper.ValidarTelefono(txtTelefono, _errorProvider))
                    esValido = false;
            }

            if (!UIHelper.ValidarCampoRequerido(cmbArea, _errorProvider, "Debe seleccionar un área"))
                esValido = false;

            if (!UIHelper.ValidarCampoRequerido(txtPuesto, _errorProvider, "El puesto es obligatorio"))
                esValido = false;

            if (cmbTipoContrato.SelectedIndex < 0)
            {
                _errorProvider.SetError(cmbTipoContrato, "Debe seleccionar un tipo de contrato");
                esValido = false;
            }

            if (!decimal.TryParse(txtSalarioBase.Text, out decimal salario) || salario < 0)
            {
                _errorProvider.SetError(txtSalarioBase, "El salario debe ser un número válido mayor o igual a 0");
                esValido = false;
            }

            if (cmbSistemaPension.SelectedIndex < 0)
            {
                _errorProvider.SetError(cmbSistemaPension, "Debe seleccionar un sistema de pensión");
                esValido = false;
            }

            if (cmbEstado.SelectedIndex < 0)
            {
                _errorProvider.SetError(cmbEstado, "Debe seleccionar un estado");
                esValido = false;
            }

            // fecha nacimiento no puede ser futura
            if (dtpFechaNacimiento.Checked)
            {
                if (!UIHelper.ValidarFechaNoFutura(dtpFechaNacimiento, _errorProvider, "La fecha de nacimiento no puede ser futura"))
                    esValido = false;
            }

            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // validar antes de guardar
                if (!ValidarFormulario())
                {
                    UIHelper.MostrarMensaje("Por favor, corrija los errores en el formulario antes de guardar.", "Validación", MessageBoxIcon.Warning);
                    return;
                }

                var empleado = new Empleado
                {
                    Id = _empleadoActual?.Id ?? 0,
                    DNI = txtDNI.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim(),
                    FechaNacimiento = dtpFechaNacimiento.Checked ? dtpFechaNacimiento.Value : null,
                    Direccion = string.IsNullOrWhiteSpace(txtDireccion.Text) ? null : txtDireccion.Text.Trim(),
                    AreaId = cmbArea.SelectedValue != null ? (int?)cmbArea.SelectedValue : null,
                    Puesto = txtPuesto.Text.Trim(),
                    TipoContrato = cmbTipoContrato.Text,
                    FechaContrato = dtpFechaContrato.Checked ? dtpFechaContrato.Value : null,
                    SalarioBase = decimal.Parse(txtSalarioBase.Text),
                    SistemaPension = cmbSistemaPension.Text,
                    Estado = cmbEstado.Text,
                    Activo = true
                };

                _empleadoBLL.Guardar(empleado);
                string mensaje = _empleadoActual != null 
                    ? "Empleado actualizado exitosamente." 
                    : "Empleado guardado exitosamente.";
                UIHelper.MostrarMensaje(mensaje, "Éxito", MessageBoxIcon.Information);
                LimpiarFormulario();
                CargarEmpleados();
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
                // simplificar mensajes de error
                if (mensajeError.Contains("UNIQUE constraint") || mensajeError.Contains("duplicate"))
                {
                    mensajeError = "Ya existe un empleado con este DNI. Por favor, verifique los datos.";
                }
                else if (mensajeError.Contains("FOREIGN KEY") || mensajeError.Contains("constraint"))
                {
                    mensajeError = "No se puede realizar esta operación porque hay información relacionada. Contacte al administrador.";
                }
                UIHelper.MostrarMensaje(mensajeError, "Error", MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            _empleadoActual = null;
            UIHelper.LimpiarErrores(_errorProvider);
            
            // habilitar dni para nuevo empleado
            txtDNI.ReadOnly = false;
            txtDNI.BackColor = Color.White;
            
            txtDNI.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            dtpFechaNacimiento.Value = DateTime.Now;
            dtpFechaNacimiento.Checked = false;
            txtDireccion.Clear();
            cmbArea.SelectedIndex = -1;
            txtPuesto.Clear();
            cmbTipoContrato.SelectedIndex = -1;
            dtpFechaContrato.Value = DateTime.Now;
            dtpFechaContrato.Checked = false;
            txtSalarioBase.Text = "0";
            cmbSistemaPension.SelectedIndex = -1;
            cmbEstado.SelectedIndex = 0;
            
            // actualizar interfaz
            ActualizarInterfazModoEdicion();
        }

        // El evento CellClick ya no se usa, ahora usamos CellContentClick

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // ya no se usa, se elimina desde acciones
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    CargarEmpleados();
                }
                else
                {
                    // Verificar que las columnas estén configuradas
                    if (dgvEmpleados.Columns.Count == 0)
                    {
                        ConfigurarDataGridView();
                    }
                    
                    var terminoBusqueda = txtBuscar.Text.Trim();
                    var empleados = _empleadoBLL.Buscar(terminoBusqueda);
                    
                    // Limpiar el DataGridView
                    dgvEmpleados.Rows.Clear();
                    
                    // Limpiar botones existentes antes de limpiar las filas
                    LimpiarBotonesAcciones();
                    
                    // Agregar cada empleado como fila
                    foreach (var empleado in empleados)
                    {
                        int rowIndex = dgvEmpleados.Rows.Add(
                            empleado.NombreCompleto,  // Nombre
                            empleado.DNI,             // DNI
                            empleado.Puesto ?? "",    // Puesto
                            empleado.NombreArea ?? "Sin área", // Área
                            empleado.Estado ?? "Activo", // Estado
                            ""                        // Acciones - se llenará con botones
                        );
                        
                        // Guardar el ID del empleado en el Tag de la fila
                        dgvEmpleados.Rows[rowIndex].Tag = empleado.Id;
                    }
                    
                    // Mostrar mensaje si no hay resultados
                    if (empleados.Count == 0)
                    {
                        // Limpiar botones antes de mostrar mensaje vacío
                        LimpiarBotonesAcciones();
                        dgvEmpleados.Rows.Clear();
                        
                        // Crear una fila con el mensaje
                        int rowIndex = dgvEmpleados.Rows.Add();
                        dgvEmpleados.Rows[rowIndex].Height = 120;
                        dgvEmpleados.Rows[rowIndex].ReadOnly = true;
                        dgvEmpleados.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
                        dgvEmpleados.Rows[rowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 250, 250);
                        dgvEmpleados.Rows[rowIndex].DefaultCellStyle.ForeColor = UIHelper.ColorTextoGris;
                        dgvEmpleados.Rows[rowIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        
                        string mensaje = $"No se encontraron empleados con el término '{terminoBusqueda}'.\n\nIntente con otro término de búsqueda.";
                        
                        // Mostrar el mensaje en la primera celda y extenderlo visualmente
                        var cell = dgvEmpleados.Rows[rowIndex].Cells[0];
                        cell.Value = mensaje;
                        cell.Style.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
                        cell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        cell.Style.BackColor = Color.FromArgb(250, 250, 250);
                        cell.Style.SelectionBackColor = Color.FromArgb(250, 250, 250);
                        cell.Style.ForeColor = UIHelper.ColorTextoGris;
                        cell.Style.SelectionForeColor = UIHelper.ColorTextoGris;
                        // Asegurar que no se muestre como hipervínculo (la columna Nombre es LinkColumn)
                        if (cell is DataGridViewLinkCell linkCell)
                        {
                            linkCell.LinkBehavior = LinkBehavior.NeverUnderline;
                            linkCell.LinkColor = UIHelper.ColorTextoGris;
                            linkCell.VisitedLinkColor = UIHelper.ColorTextoGris;
                            linkCell.ActiveLinkColor = UIHelper.ColorTextoGris;
                        }
                        
                        // Limpiar y estilizar las demás celdas
                        for (int i = 1; i < dgvEmpleados.Columns.Count; i++)
                        {
                            var otherCell = dgvEmpleados.Rows[rowIndex].Cells[i];
                            otherCell.Value = "";
                            otherCell.Style.BackColor = Color.FromArgb(250, 250, 250);
                            otherCell.Style.SelectionBackColor = Color.FromArgb(250, 250, 250);
                            otherCell.Style.ForeColor = UIHelper.ColorTextoGris;
                        }
                        
                        // Asegurar que no se muestren botones en la fila vacía
                        LimpiarBotonesAcciones();
                    }
                    else
                    {
                        // Agregar botones a todas las filas después de cargar los datos
                        for (int i = 0; i < dgvEmpleados.Rows.Count; i++)
                        {
                            AgregarBotonesAcciones(i);
                        }
                        
                        // Ajustar posición de botones cuando se hace scroll o se redimensiona
                        dgvEmpleados.Scroll += DgvEmpleados_Scroll;
                        dgvEmpleados.Resize += DgvEmpleados_Resize;
                    }
                    
                    // Forzar redibujado después de cargar los datos
                    dgvEmpleados.Invalidate();
                }
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al realizar la búsqueda: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void cmbEstado_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Este evento se puede usar para validaciones en tiempo real si es necesario
        }
    }
}

