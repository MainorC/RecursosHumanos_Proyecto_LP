using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Common.Helpers;
using RecursosHumanos.Presentation.Interfaces;
using RecursosHumanos.Presentation.Presenters;
using System.Linq;

namespace RecursosHumanos.Presentation.Views
{
    // formulario para evaluaciones
    public partial class frmEvaluaciones : Form, IEvaluacionView
    {
        private readonly EvaluacionPresenter _presenter;
        private ErrorProvider _errorProvider = new ErrorProvider();
        private List<Evaluacion> _todasLasEvaluaciones = new();
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
        public DateTime Fecha { get => dtpFecha.Value; set => dtpFecha.Value = value; }
        public int Puntaje { get => (int)nudPuntaje.Value; set => nudPuntaje.Value = value; }
        public string Fortalezas { get => txtFortalezas.Text; set => txtFortalezas.Text = value; }
        public string OportunidadesMejora { get => txtOportunidades.Text; set => txtOportunidades.Text = value; }
        public string Comentarios { get => txtComentarios.Text; set => txtComentarios.Text = value; }

        // Eventos de IEvaluacionView
        public event EventHandler? CargarDatos;
        public event EventHandler? GuardarEvaluacion;
#pragma warning disable CS0067 // Evento nunca usado - parte del contrato de la interfaz MVP
        public event EventHandler? EliminarEvaluacion;
#pragma warning restore CS0067
        public event EventHandler<int>? SeleccionarEvaluacion;

        public frmEvaluaciones()
        {
            InitializeComponent();
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            _presenter = new EvaluacionPresenter(this);
        }

        private void frmEvaluaciones_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            dtpFecha.Value = DateTime.Now;
            CargarDatos?.Invoke(this, EventArgs.Empty);
        }

        private void ConfigurarDataGridView()
        {
            try
            {
                // Aplicar estilo estándar
                UIHelper.AplicarEstiloDataGridView(dgvEvaluaciones);
                
                dgvEvaluaciones.Columns.Clear();
                dgvEvaluaciones.AutoGenerateColumns = false;
                dgvEvaluaciones.ScrollBars = ScrollBars.Vertical;
                dgvEvaluaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                
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
                dgvEvaluaciones.Columns.Add(colEmpleado);
                
                // Columna Fecha
                var colFecha = new DataGridViewTextBoxColumn
                {
                    Name = "Fecha",
                    HeaderText = "FECHA",
                    Width = 150,
                    ReadOnly = true
                };
                colFecha.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                colFecha.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                dgvEvaluaciones.Columns.Add(colFecha);
                
                // Columna Puntaje
                var colPuntaje = new DataGridViewTextBoxColumn
                {
                    Name = "Puntaje",
                    HeaderText = "PUNTAJE",
                    Width = 120,
                    ReadOnly = true
                };
                colPuntaje.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                colPuntaje.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                colPuntaje.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEvaluaciones.Columns.Add(colPuntaje);
                
                // Columna Comentarios
                var colComentarios = new DataGridViewTextBoxColumn
                {
                    Name = "Comentarios",
                    HeaderText = "COMENTARIOS",
                    Width = 400,
                    ReadOnly = true
                };
                colComentarios.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                colComentarios.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                dgvEvaluaciones.Columns.Add(colComentarios);
                
                // Eventos
                dgvEvaluaciones.CellFormatting += DgvEvaluaciones_CellFormatting;
                dgvEvaluaciones.CellDoubleClick += DgvEvaluaciones_CellDoubleClick;
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al configurar el DataGridView: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }


        private void DgvEvaluaciones_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.CellStyle != null && e.Value != null)
            {
                string columnName = dgvEvaluaciones.Columns[e.ColumnIndex].Name;
                
                if (columnName == "Puntaje")
                {
                    if (int.TryParse(e.Value.ToString(), out int puntaje))
                    {
                        if (puntaje >= 90)
                        {
                            e.CellStyle.BackColor = UIHelper.ColorExito;
                            e.CellStyle.ForeColor = UIHelper.ColorTextoClaro;
                            e.CellStyle.SelectionBackColor = UIHelper.ColorExito;
                            e.CellStyle.SelectionForeColor = UIHelper.ColorTextoClaro;
                        }
                        else if (puntaje >= 70)
                        {
                            e.CellStyle.BackColor = UIHelper.ColorAcento;
                            e.CellStyle.ForeColor = UIHelper.ColorTextoClaro;
                            e.CellStyle.SelectionBackColor = UIHelper.ColorAcento;
                            e.CellStyle.SelectionForeColor = UIHelper.ColorTextoClaro;
                        }
                        else if (puntaje >= 50)
                        {
                            e.CellStyle.BackColor = UIHelper.ColorAdvertencia;
                            e.CellStyle.ForeColor = UIHelper.ColorTextoOscuro;
                            e.CellStyle.SelectionBackColor = UIHelper.ColorAdvertencia;
                            e.CellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                        }
                        else
                        {
                            e.CellStyle.BackColor = UIHelper.ColorError;
                            e.CellStyle.ForeColor = UIHelper.ColorTextoClaro;
                            e.CellStyle.SelectionBackColor = UIHelper.ColorError;
                            e.CellStyle.SelectionForeColor = UIHelper.ColorTextoClaro;
                        }
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
        }

        public void CargarEmpleados(List<Empleado> empleados)
        {
            // Configurar autocompletado en lugar de ComboBox
            UIHelper.ConfigurarAutocompletadoEmpleado(txtEmpleado, empleados, _diccionarioEmpleados);
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

        public void CargarEvaluaciones(List<Evaluacion> evaluaciones)
        {
            try
            {
                _todasLasEvaluaciones = evaluaciones;
                AplicarFiltros();
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al cargar evaluaciones: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void AplicarFiltros()
        {
            try
            {
                if (dgvEvaluaciones.Columns.Count == 0)
                {
                    ConfigurarDataGridView();
                }

                var evaluacionesFiltradas = _todasLasEvaluaciones.AsEnumerable();

                // Filtro de búsqueda
                if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    string termino = txtBuscar.Text.Trim().ToLower();
                    evaluacionesFiltradas = evaluacionesFiltradas.Where(e =>
                        (e.NombreEmpleado ?? "").ToLower().Contains(termino) ||
                        (e.Comentarios ?? "").ToLower().Contains(termino) ||
                        (e.Fortalezas ?? "").ToLower().Contains(termino) ||
                        (e.OportunidadesMejora ?? "").ToLower().Contains(termino)
                    );
                }

                // Ordenar por fecha más reciente primero
                evaluacionesFiltradas = evaluacionesFiltradas.OrderByDescending(e => e.Fecha);

                dgvEvaluaciones.Rows.Clear();
                var evaluaciones = evaluacionesFiltradas.ToList();
                
                foreach (var evaluacion in evaluaciones)
                {
                    int rowIndex = dgvEvaluaciones.Rows.Add(
                        evaluacion.NombreEmpleado,
                        evaluacion.Fecha.ToString("yyyy-MM-dd"),
                        evaluacion.Puntaje.ToString(),
                        evaluacion.Comentarios ?? ""
                    );
                    
                    dgvEvaluaciones.Rows[rowIndex].Tag = evaluacion.Id;
                }
                
                // Mostrar mensaje si no hay evaluaciones
                if (evaluaciones.Count == 0)
                {
                    dgvEvaluaciones.Rows.Clear();
                    dgvEvaluaciones.Rows.Add("", "", "", "");
                    dgvEvaluaciones.Rows[0].Height = 200;
                    dgvEvaluaciones.Rows[0].ReadOnly = true;
                    dgvEvaluaciones.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
                    dgvEvaluaciones.Rows[0].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 250, 250);
                    dgvEvaluaciones.Rows[0].DefaultCellStyle.ForeColor = UIHelper.ColorTextoGris;
                    dgvEvaluaciones.Rows[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    string mensaje = string.IsNullOrWhiteSpace(txtBuscar.Text)
                        ? "No hay evaluaciones registradas.\nUse el formulario para crear una nueva evaluación."
                        : $"No se encontraron evaluaciones que coincidan con '{txtBuscar.Text.Trim()}'.\nIntente con otro término de búsqueda.";
                    dgvEvaluaciones.Rows[0].Cells[0].Value = mensaje;
                    dgvEvaluaciones.Rows[0].Cells[0].Style.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
                    // Combinar celdas visualmente
                    for (int i = 1; i < dgvEvaluaciones.Columns.Count; i++)
                    {
                        dgvEvaluaciones.Rows[0].Cells[i].Value = "";
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al aplicar filtros: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private bool ValidarFormulario()
        {
            bool esValido = true;
            UIHelper.LimpiarErrores(_errorProvider);

            if (string.IsNullOrWhiteSpace(txtEmpleado.Text) || !_diccionarioEmpleados.ContainsKey(txtEmpleado.Text))
            {
                _errorProvider.SetError(txtEmpleado, "Debe seleccionar un empleado válido.");
                esValido = false;
            }
            else
            {
                _errorProvider.SetError(txtEmpleado, "");
            }
            
            if (nudPuntaje.Value < 0 || nudPuntaje.Value > 100)
            {
                _errorProvider.SetError(nudPuntaje, "El puntaje debe estar entre 0 y 100");
                esValido = false;
            }

            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarEvaluacion?.Invoke(this, EventArgs.Empty);
        }

        public void LimpiarFormulario()
        {
            UIHelper.LimpiarErrores(_errorProvider);
            txtEmpleado.Text = "";
            dtpFecha.Value = DateTime.Now;
            nudPuntaje.Value = 0;
            txtFortalezas.Clear();
            txtOportunidades.Clear();
            txtComentarios.Clear();
            btnGuardar.Text = "Guardar Evaluación";
        }

        public void MostrarEvaluacion(Evaluacion evaluacion)
        {
            EmpleadoId = evaluacion.EmpleadoId;
            Fecha = evaluacion.Fecha;
            Puntaje = evaluacion.Puntaje;
            Fortalezas = evaluacion.Fortalezas ?? "";
            OportunidadesMejora = evaluacion.OportunidadesMejora ?? "";
            Comentarios = evaluacion.Comentarios ?? "";
            btnGuardar.Text = "Actualizar Evaluación";
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
                "Fecha" => dtpFecha,
                "Puntaje" => nudPuntaje,
                "Fortalezas" => txtFortalezas,
                "OportunidadesMejora" => txtOportunidades,
                "Comentarios" => txtComentarios,
                _ => null
            };
        }

        private void DgvEvaluaciones_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                
                var row = dgvEvaluaciones.Rows[e.RowIndex];
                if (row.Tag == null) return;
                
                int evaluacionId = (int)row.Tag;
                SeleccionarEvaluacion?.Invoke(this, evaluacionId);
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al procesar el doble clic: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }
    }
}

