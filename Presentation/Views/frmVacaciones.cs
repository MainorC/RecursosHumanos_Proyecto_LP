using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Common.Helpers;
using RecursosHumanos.Presentation.Interfaces;
using RecursosHumanos.Presentation.Presenters;
using System.Linq;
using System.Collections.Generic;

namespace RecursosHumanos.Presentation.Views
{
    // formulario para gestion de vacaciones
    public partial class frmVacaciones : Form, IVacacionView
    {
        private readonly VacacionPresenter _presenter;
        private ErrorProvider _errorProvider = new ErrorProvider();
        private List<Vacacion> _todasLasVacaciones = new();
        private Dictionary<string, int> _diccionarioEmpleados = new(); // mapeo nombre -> id

        public int? EmpleadoId 
        { 
            get 
            {
                if (string.IsNullOrWhiteSpace(txtEmpleado.Text))
                    return null;
                
                if (_diccionarioEmpleados.TryGetValue(txtEmpleado.Text, out int empleadoId))
                    return empleadoId;
                
                return null;
            }
            set 
            {
                if (value.HasValue)
                {
                    var empleado = _diccionarioEmpleados.FirstOrDefault(e => e.Value == value.Value);
                    if (empleado.Key != null)
                    {
                        txtEmpleado.Text = empleado.Key;
                    }
                }
                else
                {
                    txtEmpleado.Text = "";
                }
            }
        }
        public DateTime FechaInicio { get => dtpInicio.Value; set => dtpInicio.Value = value; }
        public DateTime FechaFin { get => dtpFin.Value; set => dtpFin.Value = value; }
        public string Estado { get => "Pendiente"; set { } } // Por defecto siempre es Pendiente al crear
        public string Motivo { get => ""; set { } } // La entidad Vacacion no tiene propiedad Motivo

        // Eventos de IVacacionView
        public event EventHandler? CargarDatos;
        public event EventHandler? GuardarVacacion;
#pragma warning disable CS0067 // Eventos nunca usados - parte del contrato de la interfaz MVP
        public event EventHandler? EliminarVacacion;
        public event EventHandler<int>? SeleccionarVacacion;
#pragma warning restore CS0067
#pragma warning disable CS0067 // Evento nunca usado - se mantiene por compatibilidad con la interfaz
        public event EventHandler<int>? AprobarVacacion;
        public event EventHandler<int>? RechazarVacacion;
#pragma warning restore CS0067

        /// <summary>
        /// Constructor del formulario
        /// </summary>
        public frmVacaciones()
        {
            InitializeComponent();
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            _presenter = new VacacionPresenter(this);
        }

        private void frmVacaciones_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            
            // Configurar filtros
            cmbFiltroEstado.Items.AddRange(new object[] { "Todos", "Pendiente", "Aprobado", "Rechazado" });
            cmbFiltroEstado.SelectedIndex = 0;
            cmbFiltroEstado.SelectedIndexChanged += CmbFiltroEstado_SelectedIndexChanged;
            
            CargarDatos?.Invoke(this, EventArgs.Empty);
        }

        private void ConfigurarDataGridView()
        {
            try
            {
                // Aplicar estilo estándar
                UIHelper.AplicarEstiloDataGridView(dgvVacaciones);
                
                dgvVacaciones.Columns.Clear();
                dgvVacaciones.AutoGenerateColumns = false;
                dgvVacaciones.ScrollBars = ScrollBars.Vertical;
                dgvVacaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                
                // Columna Empleado
                var colEmpleado = new DataGridViewTextBoxColumn
                {
                    Name = "Empleado",
                    HeaderText = "EMPLEADO",
                    Width = 200,
                    ReadOnly = true
                };
                colEmpleado.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                colEmpleado.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                dgvVacaciones.Columns.Add(colEmpleado);
                
                // Columna Periodo
                var colPeriodo = new DataGridViewTextBoxColumn
                {
                    Name = "Periodo",
                    HeaderText = "PERIODO",
                    Width = 280,
                    ReadOnly = true
                };
                colPeriodo.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                colPeriodo.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                dgvVacaciones.Columns.Add(colPeriodo);
                
                // Columna Días
                var colDias = new DataGridViewTextBoxColumn
                {
                    Name = "Dias",
                    HeaderText = "DÍAS",
                    Width = 80,
                    ReadOnly = true
                };
                colDias.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                colDias.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                colDias.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvVacaciones.Columns.Add(colDias);
                
                // Columna Estado
                var colEstado = new DataGridViewTextBoxColumn
                {
                    Name = "Estado",
                    HeaderText = "ESTADO",
                    Width = 120,
                    ReadOnly = true
                };
                dgvVacaciones.Columns.Add(colEstado);
                
                // Columna Acciones (usando columna de texto con botones reales)
                var colAcciones = new DataGridViewTextBoxColumn
                {
                    Name = "Acciones",
                    HeaderText = "ACCIONES",
                    Width = 250,
                    ReadOnly = true,
                    Resizable = DataGridViewTriState.False
                };
                colAcciones.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                colAcciones.DefaultCellStyle.BackColor = Color.White;
                colAcciones.DefaultCellStyle.Padding = new Padding(0);
                colAcciones.DefaultCellStyle.SelectionBackColor = Color.White;
                colAcciones.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvVacaciones.Columns.Add(colAcciones);
                
                // Calcular ancho disponible
                int anchoDisponible = dgvVacaciones.Width - SystemInformation.VerticalScrollBarWidth - 4;
                int anchoTotalColumnas = dgvVacaciones.Columns.Cast<DataGridViewColumn>().Sum(c => c.Width);
                
                // Ajustar columnas para que quepan sin scrollbar horizontal
                if (anchoTotalColumnas > anchoDisponible)
                {
                    double factor = (double)anchoDisponible / anchoTotalColumnas;
                    foreach (DataGridViewColumn col in dgvVacaciones.Columns)
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
                    colPeriodo.Width += espacioExtra;
                }
                
                // Eventos
                dgvVacaciones.CellFormatting += DgvVacaciones_CellFormatting;
                dgvVacaciones.CellPainting += DgvVacaciones_CellPainting;
                dgvVacaciones.RowsAdded += DgvVacaciones_RowsAdded;
                dgvVacaciones.Scroll += DgvVacaciones_Scroll;
                dgvVacaciones.Resize += DgvVacaciones_Resize;
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al configurar el DataGridView: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void DgvVacaciones_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.CellStyle != null && e.Value != null)
            {
                string columnName = dgvVacaciones.Columns[e.ColumnIndex].Name;
                
                if (columnName == "Estado")
                {
                    string estado = e.Value.ToString() ?? "";
                    if (estado.Equals("Aprobado", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.BackColor = UIHelper.ColorExito;
                        e.CellStyle.ForeColor = UIHelper.ColorTextoClaro;
                        e.CellStyle.SelectionBackColor = UIHelper.ColorExito;
                        e.CellStyle.SelectionForeColor = UIHelper.ColorTextoClaro;
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    else if (estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.BackColor = UIHelper.ColorAdvertencia;
                        e.CellStyle.ForeColor = UIHelper.ColorTextoOscuro;
                        e.CellStyle.SelectionBackColor = UIHelper.ColorAdvertencia;
                        e.CellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    else if (estado.Equals("Rechazado", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.BackColor = UIHelper.ColorError;
                        e.CellStyle.ForeColor = UIHelper.ColorTextoClaro;
                        e.CellStyle.SelectionBackColor = UIHelper.ColorError;
                        e.CellStyle.SelectionForeColor = UIHelper.ColorTextoClaro;
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
        }

        private void DgvVacaciones_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            // Ocultar el contenido de texto de la columna Acciones
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && 
                dgvVacaciones.Columns[e.ColumnIndex].Name == "Acciones")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                e.Handled = true;
            }
        }

        private void DgvVacaciones_RowsAdded(object? sender, DataGridViewRowsAddedEventArgs e)
        {
            // Este evento se deshabilita porque los botones se agregan directamente en AplicarFiltros
            // para evitar duplicados. Los botones se agregan inmediatamente después de agregar cada fila.
        }

        private void DgvVacaciones_Scroll(object? sender, ScrollEventArgs e)
        {
            // Solo ajustar posición, no crear nuevos botones
            AjustarPosicionBotones();
        }

        private void DgvVacaciones_Resize(object? sender, EventArgs e)
        {
            // Solo ajustar posición, no crear nuevos botones
            AjustarPosicionBotones();
        }

        private void LimpiarBotonesAcciones()
        {
            var controlesAEliminar = new List<Control>();
            foreach (Control ctrl in dgvVacaciones.Controls)
            {
                // Eliminar todos los botones (ahora tienen el ID directamente en el Tag, no un string)
                if (ctrl is Button)
                {
                    controlesAEliminar.Add(ctrl);
                }
            }
            
            foreach (var ctrl in controlesAEliminar)
            {
                dgvVacaciones.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
        }

        private void AgregarBotonesAcciones(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvVacaciones.Rows.Count) return;
            
            var colAcciones = dgvVacaciones.Columns["Acciones"];
            if (colAcciones == null) return;

            // Verificar si el estado es Pendiente
            var row = dgvVacaciones.Rows[rowIndex];
            string estado = row.Cells["Estado"]?.Value?.ToString() ?? "";
            bool esPendiente = estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase);

            // Obtener el ID de la vacación desde el Tag de la fila
            int vacacionId = 0;
            if (row.Tag != null && row.Tag is int id)
            {
                vacacionId = id;
            }
            
            if (vacacionId == 0) return; // No agregar botones si no hay ID válido

            // Limpiar botones existentes de esta fila (buscar por ID de vacación)
            var controlesAEliminar = new List<Control>();
            foreach (Control ctrl in dgvVacaciones.Controls)
            {
                if (ctrl is Button btn && btn.Tag != null && btn.Tag is int btnVacacionId && btnVacacionId == vacacionId)
                {
                    controlesAEliminar.Add(ctrl);
                }
            }
            
            foreach (var ctrl in controlesAEliminar)
            {
                dgvVacaciones.Controls.Remove(ctrl);
                ctrl.Dispose();
            }

            // Obtener los bounds de la celda (usar true para incluir celdas no visibles)
            var cellBounds = dgvVacaciones.GetCellDisplayRectangle(colAcciones.Index, rowIndex, true);
            
            // Si la celda no está visible, usar valores por defecto
            if (cellBounds.Width == 0 || cellBounds.Height == 0)
            {
                // Calcular posición aproximada basándose en el índice de la fila
                int rowHeight = dgvVacaciones.Rows[rowIndex].Height;
                int headerHeight = dgvVacaciones.ColumnHeadersHeight;
                int yPos = headerHeight;
                for (int i = 0; i < rowIndex; i++)
                {
                    yPos += dgvVacaciones.Rows[i].Height;
                }
                
                cellBounds = new Rectangle(
                    colAcciones.Width - 200, // Posición aproximada
                    yPos,
                    200,
                    rowHeight
                );
            }

            // Botón Eliminar (siempre visible)
            var btnEliminar = new Button
            {
                Text = "Eliminar",
                Size = new Size(80, 28),
                Location = new Point(cellBounds.Left + cellBounds.Width - 85, cellBounds.Top + (cellBounds.Height - 28) / 2),
                Tag = vacacionId, // Guardar el ID directamente en el Tag del botón
                Cursor = Cursors.Hand
            };
            UIHelper.AplicarEstiloBotonAccion(btnEliminar, TipoBotonAccion.Error);
            btnEliminar.Click += (s, e) =>
            {
                var btn = s as Button;
                if (btn != null && btn.Tag != null && btn.Tag is int idVacacion && idVacacion > 0)
                {
                    _presenter.EliminarVacacion(idVacacion);
                }
            };
            dgvVacaciones.Controls.Add(btnEliminar);
            
            // Solo agregar botones Aprobar/Rechazar si el estado es Pendiente
            if (esPendiente && vacacionId > 0)
            {
                // Crear botón Aprobar
                var btnAprobar = new Button
                {
                    Text = "Aprobar",
                    Size = new Size(80, 28),
                    Location = new Point(cellBounds.Left + 5, cellBounds.Top + (cellBounds.Height - 28) / 2),
                    Tag = vacacionId, // Guardar el ID directamente en el Tag del botón
                    Cursor = Cursors.Hand
                };
                UIHelper.AplicarEstiloBotonAccion(btnAprobar, TipoBotonAccion.Exito);
                btnAprobar.Click += (s, e) =>
                {
                    var btn = s as Button;
                    if (btn != null && btn.Tag != null && btn.Tag is int idVacacion && idVacacion > 0)
                    {
                        _presenter.AprobarVacacion(idVacacion);
                    }
                };
                
                // Botón Rechazar (solo para solicitudes pendientes)
                var btnRechazar = new Button
                {
                    Text = "Rechazar",
                    Size = new Size(80, 28),
                    Location = new Point(cellBounds.Left + 90, cellBounds.Top + (cellBounds.Height - 28) / 2),
                    Tag = vacacionId, // Guardar el ID directamente en el Tag del botón
                    Cursor = Cursors.Hand,
                    Visible = estado == "Pendiente"
                };
                UIHelper.AplicarEstiloBotonAccion(btnRechazar, TipoBotonAccion.Advertencia);
                btnRechazar.Click += (s, e) =>
                {
                    var btn = s as Button;
                    if (btn != null && btn.Tag != null && btn.Tag is int idVacacion && idVacacion > 0)
                    {
                        _presenter.RechazarVacacion(idVacacion);
                    }
                };

                // Agregar botones al DataGridView
                dgvVacaciones.Controls.Add(btnAprobar);
                dgvVacaciones.Controls.Add(btnRechazar);
            }
            
        }

        private void AjustarPosicionBotones()
        {
            var colAcciones = dgvVacaciones.Columns["Acciones"];
            if (colAcciones == null) return;

            // Recorrer todas las filas y ajustar los botones que pertenecen a cada fila
            for (int i = 0; i < dgvVacaciones.Rows.Count; i++)
            {
                var cellBounds = dgvVacaciones.GetCellDisplayRectangle(colAcciones.Index, i, true);
                if (cellBounds.Width == 0 || cellBounds.Height == 0) continue;

                var row = dgvVacaciones.Rows[i];
                string estado = row.Cells["Estado"]?.Value?.ToString() ?? "";
                bool esPendiente = estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase);
                
                // Obtener el ID de la vacación de la fila
                int rowVacacionId = 0;
                if (row.Tag != null && row.Tag is int id)
                {
                    rowVacacionId = id;
                }
                
                if (rowVacacionId == 0) continue;
                
                // Buscar botones que pertenezcan a esta fila por ID
                var botonesAEliminar = new List<Button>();
                foreach (Control ctrl in dgvVacaciones.Controls)
                {
                    if (ctrl is Button btn && btn.Tag != null && btn.Tag is int btnVacacionId && btnVacacionId == rowVacacionId)
                    {
                        // Ajustar posición según el tipo de botón
                        if (btn.Text == "Eliminar")
                        {
                            btn.Location = new Point(cellBounds.Left + cellBounds.Width - 85, cellBounds.Top + (cellBounds.Height - btn.Height) / 2);
                            btn.Visible = cellBounds.IntersectsWith(dgvVacaciones.ClientRectangle);
                        }
                        else if (btn.Text == "Aprobar" || btn.Text == "Rechazar")
                        {
                            // Ocultar completamente los botones Aprobar/Rechazar si no es pendiente
                            if (!esPendiente)
                            {
                                botonesAEliminar.Add(btn);
                            }
                            else
                            {
                                if (btn.Text == "Aprobar")
                                {
                                    btn.Location = new Point(cellBounds.Left + 5, cellBounds.Top + (cellBounds.Height - btn.Height) / 2);
                                }
                                else
                                {
                                    btn.Location = new Point(cellBounds.Left + 90, cellBounds.Top + (cellBounds.Height - btn.Height) / 2);
                                }
                                btn.Visible = cellBounds.IntersectsWith(dgvVacaciones.ClientRectangle);
                            }
                        }
                    }
                }
                
                // Eliminar botones que ya no deben mostrarse
                foreach (var btn in botonesAEliminar)
                {
                    dgvVacaciones.Controls.Remove(btn);
                    btn.Dispose();
                }
            }
        }


        public void CargarEmpleados(List<Empleado> empleados)
        {
            // configurar autocompletado
            UIHelper.ConfigurarAutocompletadoEmpleado(txtEmpleado, empleados, _diccionarioEmpleados);
            
            // recalcular dias cuando cambie empleado
            txtEmpleado.TextChanged += (s, e) => CalcularDias();
            txtEmpleado.Leave += (s, e) => ValidarEmpleadoSeleccionado();
        }
        
        private void ValidarEmpleadoSeleccionado()
        {
            if (!string.IsNullOrWhiteSpace(txtEmpleado.Text))
            {
                if (!_diccionarioEmpleados.ContainsKey(txtEmpleado.Text))
                {
                    _errorProvider.SetError(txtEmpleado, "Empleado no encontrado. Use el autocompletado para seleccionar.");
                    txtEmpleado.Focus();
                }
                else
                {
                    _errorProvider.SetError(txtEmpleado, "");
                }
            }
        }

        public void CargarVacaciones(List<Vacacion> vacaciones)
        {
            try
            {
                // Limpiar todo antes de cargar nuevas vacaciones
                LimpiarBotonesAcciones();
                _todasLasVacaciones = vacaciones ?? new List<Vacacion>();
                AplicarFiltros();
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al cargar vacaciones: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void AplicarFiltros()
        {
            try
            {
                if (_todasLasVacaciones == null || _todasLasVacaciones.Count == 0)
                {
                    // Si no hay vacaciones, limpiar todo
                    LimpiarBotonesAcciones();
                    dgvVacaciones.Rows.Clear();
                    return;
                }

                var vacacionesFiltradas = _todasLasVacaciones.AsEnumerable();

                // Filtro de búsqueda
                if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    string termino = txtBuscar.Text.Trim().ToLower();
                    vacacionesFiltradas = vacacionesFiltradas.Where(v =>
                        (v.NombreEmpleado ?? "").ToLower().Contains(termino)
                    );
                }

                // Filtro por estado
                string filtroEstado = cmbFiltroEstado.SelectedItem?.ToString() ?? "Todos";
                if (filtroEstado != "Todos")
                {
                    vacacionesFiltradas = vacacionesFiltradas.Where(v => v.Estado == filtroEstado);
                }

                // Ordenar por fecha más reciente primero
                vacacionesFiltradas = vacacionesFiltradas.OrderByDescending(v => v.FechaInicio);

                if (dgvVacaciones.Columns.Count == 0)
                {
                    ConfigurarDataGridView();
                }
                
                // Limpiar botones existentes antes de limpiar las filas
                LimpiarBotonesAcciones();
                
                // Limpiar filas existentes
                dgvVacaciones.Rows.Clear();
                
                // Convertir a lista y eliminar duplicados por ID
                var vacaciones = vacacionesFiltradas
                    .GroupBy(v => v.Id)
                    .Select(g => g.First())
                    .ToList();
                
                // Mostrar mensaje si no hay vacaciones
                if (vacaciones.Count == 0)
                {
                    // Crear una fila con el mensaje (similar a frmEmpleados)
                    int rowIndex = dgvVacaciones.Rows.Add();
                    dgvVacaciones.Rows[rowIndex].ReadOnly = true;
                    dgvVacaciones.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
                    dgvVacaciones.Rows[rowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 250, 250);
                    dgvVacaciones.Rows[rowIndex].DefaultCellStyle.ForeColor = UIHelper.ColorTextoGris;
                    
                    string mensaje = string.IsNullOrWhiteSpace(txtBuscar.Text) && (cmbFiltroEstado.SelectedItem?.ToString() == "Todos" || cmbFiltroEstado.SelectedIndex == 0)
                        ? "No hay solicitudes de vacaciones registradas."
                        : "No se encontraron solicitudes que coincidan con los filtros seleccionados.";
                    
                    // Mostrar el mensaje en la primera celda (similar al estilo de frmEmpleados)
                    var cell = dgvVacaciones.Rows[rowIndex].Cells[0];
                    cell.Value = mensaje;
                    cell.Style.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
                    cell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    cell.Style.BackColor = Color.FromArgb(250, 250, 250);
                    cell.Style.SelectionBackColor = Color.FromArgb(250, 250, 250);
                    cell.Style.ForeColor = UIHelper.ColorTextoGris;
                    
                    // Limpiar y estilizar las demás celdas
                    for (int i = 1; i < dgvVacaciones.Columns.Count; i++)
                    {
                        var otherCell = dgvVacaciones.Rows[rowIndex].Cells[i];
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
                    // Hay vacaciones, agregar las filas primero
                    foreach (var vacacion in vacaciones)
                    {
                        int rowIndex = dgvVacaciones.Rows.Add(
                            vacacion.NombreEmpleado,
                            $"{vacacion.FechaInicio:dd/MM/yyyy} - {vacacion.FechaFin:dd/MM/yyyy}",
                            vacacion.DiasTotales.ToString(),
                            vacacion.Estado,
                            "" // Acciones - se llenará con botones
                        );
                        
                        dgvVacaciones.Rows[rowIndex].Tag = vacacion.Id;
                    }
                    
                    // Agregar botones después de que todas las filas estén agregadas
                    // Esto asegura que los bounds de las celdas estén correctos
                    dgvVacaciones.Refresh();
                    Application.DoEvents();
                    
                    // Agregar botones a todas las filas
                    for (int i = 0; i < dgvVacaciones.Rows.Count; i++)
                    {
                        if (dgvVacaciones.Rows[i].Tag != null && dgvVacaciones.Rows[i].Tag is int vacacionId && vacacionId > 0)
                        {
                            AgregarBotonesAcciones(i);
                        }
                    }
                    
                    // Ajustar posiciones de todos los botones después de agregarlos
                    // Esto asegura que los botones estén en la posición correcta
                    dgvVacaciones.Refresh();
                    Application.DoEvents();
                    AjustarPosicionBotones();
                }
                
                // Actualizar estadísticas
                var pendientes = _todasLasVacaciones.Count(v => v.Estado == "Pendiente");
                var aprobadas = _todasLasVacaciones.Count(v => v.Estado == "Aprobado");
                var rechazadas = _todasLasVacaciones.Count(v => v.Estado == "Rechazado");
                ActualizarEstadisticas(pendientes, aprobadas, rechazadas);
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al aplicar filtros: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void CmbFiltroEstado_SelectedIndexChanged(object? sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void TxtBuscar_TextChanged(object? sender, EventArgs e)
        {
            AplicarFiltros();
        }

        public void ActualizarEstadisticas(int pendientes, int aprobadas, int rechazadas)
        {
            try
            {
                var total = _todasLasVacaciones.Count;
                var diasAprobados = _todasLasVacaciones.Where(v => v.Estado == "Aprobado").Sum(v => v.DiasTotales);
                var diasPendientes = _todasLasVacaciones.Where(v => v.Estado == "Pendiente").Sum(v => v.DiasTotales);
                var diasRechazados = _todasLasVacaciones.Where(v => v.Estado == "Rechazado").Sum(v => v.DiasTotales);

                // Actualizar el título del GroupBox con estadísticas
                gbTodasSolicitudes.Text = $"Todas las Solicitudes (Total: {total} | Pendientes: {pendientes} | Aprobadas: {aprobadas} | Días aprobados: {diasAprobados})";
                
                // Actualizar labels de estadísticas si existen
                if (lblEstadisticas != null)
                {
                    lblEstadisticas.Text = $"Resumen: {pendientes} pendientes ({diasPendientes} días) | {aprobadas} aprobadas ({diasAprobados} días) | {rechazadas} rechazadas ({diasRechazados} días)";
                    lblEstadisticas.ForeColor = UIHelper.ColorTextoOscuro;
                }
            }
            catch
            {
                // Ignorar errores en estadísticas
            }
        }

        private bool ValidarFormulario()
        {
            UIHelper.LimpiarErrores(_errorProvider);

            // validar empleado
            if (string.IsNullOrWhiteSpace(txtEmpleado.Text) || !_diccionarioEmpleados.ContainsKey(txtEmpleado.Text))
            {
                _errorProvider.SetError(txtEmpleado, "Debe seleccionar un empleado válido.");
                return false;
            }
            _errorProvider.SetError(txtEmpleado, "");
            return true;
        }
        
        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            GuardarVacacion?.Invoke(this, EventArgs.Empty);
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            GuardarVacacion?.Invoke(this, EventArgs.Empty);
        }

        private void dtpInicio_ValueChanged(object sender, EventArgs e)
        {
            CalcularDias();
        }

        private void dtpFin_ValueChanged(object sender, EventArgs e)
        {
            CalcularDias();
        }

        private void CalcularDias()
        {
            // Validar que la fecha de fin no sea anterior a la de inicio
            if (dtpFin.Value < dtpInicio.Value)
            {
                lblDiasTotales.Text = "0 días (Fecha inválida)";
                lblDiasTotales.ForeColor = UIHelper.ColorError;
                if (lblInfoDias != null)
                {
                    lblInfoDias.Text = "ERROR: La fecha de fin debe ser posterior o igual a la fecha de inicio.";
                    lblInfoDias.ForeColor = UIHelper.ColorError;
                }
                return;
            }
            
            int dias = (dtpFin.Value - dtpInicio.Value).Days + 1;
            
            if (string.IsNullOrWhiteSpace(txtEmpleado.Text) || !_diccionarioEmpleados.ContainsKey(txtEmpleado.Text))
            {
                lblDiasTotales.Text = $"{dias} días";
                lblDiasTotales.ForeColor = UIHelper.ColorTextoOscuro;
                if (lblInfoDias != null)
                {
                    lblInfoDias.Text = "Seleccione un empleado para ver información detallada de días disponibles y su historial de vacaciones.";
                    lblInfoDias.ForeColor = UIHelper.ColorTextoGris;
                }
                return;
            }
            
            int empleadoId = _diccionarioEmpleados[txtEmpleado.Text];
            
            try
            {
                int diasDisponibles = _presenter.ObtenerDiasDisponibles(empleadoId);
                int diasUsados = _presenter.ObtenerDiasUsados(empleadoId);
                int diasRestantes = diasDisponibles - dias;
                
                lblDiasTotales.Text = $"{dias} días";
                lblDiasTotales.ForeColor = dias <= diasDisponibles ? UIHelper.ColorExito : UIHelper.ColorError;
                
                // Obtener vacaciones del empleado para mostrar historial
                var vacacionesEmpleado = _presenter.ObtenerPorEmpleado(empleadoId);
                var vacacionesAprobadas = vacacionesEmpleado.Where(v => v.Estado == "Aprobado").ToList();
                var vacacionesPendientes = vacacionesEmpleado.Where(v => v.Estado == "Pendiente").ToList();
                
                // Mostrar información adicional
                if (dias > diasDisponibles)
                {
                    lblDiasTotales.Text += $" (Excede {diasDisponibles} disponibles)";
                    if (lblInfoDias != null)
                    {
                        lblInfoDias.Text = $"ADVERTENCIA: El empleado solo tiene {diasDisponibles} días disponibles.\n\n" +
                                          $"Resumen del empleado:\n" +
                                          $"• Días usados este año: {diasUsados} de 30 días\n" +
                                          $"• Días restantes: {diasDisponibles}\n" +
                                          $"• Vacaciones aprobadas: {vacacionesAprobadas.Count} solicitudes\n" +
                                          $"• Solicitudes pendientes: {vacacionesPendientes.Count}";
                        lblInfoDias.ForeColor = UIHelper.ColorError;
                    }
                }
                else
                {
                    lblDiasTotales.Text += $" (Disponibles: {diasRestantes})";
                    if (lblInfoDias != null)
                    {
                        lblInfoDias.Text = $"DÍAS DISPONIBLES: {diasDisponibles}\n\n" +
                                          $"Resumen del empleado:\n" +
                                          $"• Días usados este año: {diasUsados} de 30 días\n" +
                                          $"• Días restantes: {diasRestantes}\n" +
                                          $"• Vacaciones aprobadas: {vacacionesAprobadas.Count} solicitudes ({vacacionesAprobadas.Sum(v => v.DiasTotales)} días)\n" +
                                          $"• Solicitudes pendientes: {vacacionesPendientes.Count} ({vacacionesPendientes.Sum(v => v.DiasTotales)} días)";
                        lblInfoDias.ForeColor = UIHelper.ColorExito;
                    }
                }
                
                // Mostrar próximas vacaciones si existen
                if (vacacionesAprobadas.Any() && lblInfoDias != null)
                {
                    var proximaVacacion = vacacionesAprobadas
                        .Where(v => v.FechaInicio >= DateTime.Today)
                        .OrderBy(v => v.FechaInicio)
                        .FirstOrDefault();
                    
                    if (proximaVacacion != null)
                    {
                        lblInfoDias.Text += $"\n\nPróxima vacación: {proximaVacacion.FechaInicio:dd/MM/yyyy} - {proximaVacacion.FechaFin:dd/MM/yyyy} ({proximaVacacion.DiasTotales} días)";
                    }
                }
            }
            catch
            {
                lblDiasTotales.Text = $"{dias} días";
                lblDiasTotales.ForeColor = UIHelper.ColorTextoOscuro;
                if (lblInfoDias != null)
                {
                    lblInfoDias.Text = "Error al calcular días disponibles.";
                    lblInfoDias.ForeColor = UIHelper.ColorError;
                }
            }
        }

        public void LimpiarFormulario()
        {
            UIHelper.LimpiarErrores(_errorProvider);
            txtEmpleado.Text = "";
            dtpInicio.Value = DateTime.Now;
            dtpFin.Value = DateTime.Now;
            lblDiasTotales.Text = "1";
            if (lblInfoDias != null)
            {
                lblInfoDias.Text = "👤 Seleccione un empleado para ver información detallada de días disponibles y su historial de vacaciones.";
                lblInfoDias.ForeColor = UIHelper.ColorTextoGris;
            }
        }

        public void MostrarVacacion(Vacacion vacacion)
        {
            EmpleadoId = vacacion.EmpleadoId;
            FechaInicio = vacacion.FechaInicio;
            FechaFin = vacacion.FechaFin;
            Motivo = ""; // La entidad Vacacion no tiene propiedad Motivo
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
                "EmpleadoId" => txtEmpleado,
                "FechaInicio" => dtpInicio,
                "FechaFin" => dtpFin,
                _ => null
            };
        }

    }
}

