using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Common.Helpers;
using RecursosHumanos.Presentation.Interfaces;
using RecursosHumanos.Presentation.Presenters;
using System.Linq;

namespace RecursosHumanos.Presentation.Views
{
    // formulario para gestion de asistencias
    public partial class frmAsistencia : Form, IAsistenciaView
    {
        private readonly AsistenciaPresenter _presenter;
        private ErrorProvider _errorProvider = new ErrorProvider();
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
        public TimeSpan? HoraEntrada { get => dtpEntrada.Value.TimeOfDay; set => dtpEntrada.Value = DateTime.Today.Add(value ?? TimeSpan.Zero); }
        public TimeSpan? HoraSalida { get => dtpSalida.Value.TimeOfDay; set => dtpSalida.Value = DateTime.Today.Add(value ?? TimeSpan.Zero); }
        public string Estado { get => cmbEstado.Text; set => cmbEstado.Text = value; }

        // Eventos de IAsistenciaView
        public event EventHandler? CargarDatos;
        public event EventHandler? GuardarAsistencia;
#pragma warning disable CS0067 // Evento nunca usado - parte del contrato de la interfaz MVP
        public event EventHandler? EliminarAsistencia;
#pragma warning restore CS0067
        public event EventHandler<int>? SeleccionarAsistencia;

        public frmAsistencia()
        {
            InitializeComponent();
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            _presenter = new AsistenciaPresenter(this);
        }

        private void frmAsistencia_Load(object sender, EventArgs e)
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
                UIHelper.AplicarEstiloDataGridView(dgvAsistencia);
                
                dgvAsistencia.Columns.Clear();
                dgvAsistencia.AutoGenerateColumns = false;
                dgvAsistencia.ScrollBars = ScrollBars.Vertical;
                dgvAsistencia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                
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
                dgvAsistencia.Columns.Add(colEmpleado);
                
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
                dgvAsistencia.Columns.Add(colFecha);
                
                // Columna Entrada
                var colEntrada = new DataGridViewTextBoxColumn
                {
                    Name = "Entrada",
                    HeaderText = "ENTRADA",
                    Width = 100,
                    ReadOnly = true
                };
                colEntrada.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                colEntrada.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                colEntrada.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAsistencia.Columns.Add(colEntrada);
                
                // Columna Salida
                var colSalida = new DataGridViewTextBoxColumn
                {
                    Name = "Salida",
                    HeaderText = "SALIDA",
                    Width = 100,
                    ReadOnly = true
                };
                colSalida.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                colSalida.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                colSalida.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAsistencia.Columns.Add(colSalida);
                
                // Columna Horas Trabajadas
                var colHorasTrab = new DataGridViewTextBoxColumn
                {
                    Name = "HorasTrab",
                    HeaderText = "HORAS TRAB.",
                    Width = 120,
                    ReadOnly = true
                };
                colHorasTrab.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                colHorasTrab.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                colHorasTrab.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvAsistencia.Columns.Add(colHorasTrab);
                
                // Columna Estado
                var colEstado = new DataGridViewTextBoxColumn
                {
                    Name = "Estado",
                    HeaderText = "ESTADO",
                    Width = 150,
                    ReadOnly = true
                };
                dgvAsistencia.Columns.Add(colEstado);
                
                // Eventos
                dgvAsistencia.CellFormatting += DgvAsistencia_CellFormatting;
                dgvAsistencia.CellDoubleClick += DgvAsistencia_CellDoubleClick;
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al configurar el DataGridView: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }


        private void DgvAsistencia_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.CellStyle != null && e.Value != null)
            {
                string columnName = dgvAsistencia.Columns[e.ColumnIndex].Name;
                
                if (columnName == "Estado")
                {
                    string estado = e.Value.ToString() ?? "";
                    if (estado.Equals("Presente", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.BackColor = UIHelper.ColorExito;
                        e.CellStyle.ForeColor = UIHelper.ColorTextoClaro;
                        e.CellStyle.SelectionBackColor = UIHelper.ColorExito;
                        e.CellStyle.SelectionForeColor = UIHelper.ColorTextoClaro;
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    else if (estado.Equals("Tarde", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.BackColor = UIHelper.ColorAdvertencia;
                        e.CellStyle.ForeColor = UIHelper.ColorTextoOscuro;
                        e.CellStyle.SelectionBackColor = UIHelper.ColorAdvertencia;
                        e.CellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    else if (estado.Equals("Ausente", StringComparison.OrdinalIgnoreCase))
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

        public void CargarEmpleados(List<Empleado> empleados)
        {
            try
            {
                // Configurar autocompletado en lugar de ComboBox
                UIHelper.ConfigurarAutocompletadoEmpleado(txtEmpleado, empleados, _diccionarioEmpleados);
                txtEmpleado.Leave += (s, e) => ValidarEmpleadoSeleccionado();
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al cargar empleados: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        public void CargarAsistencias(List<Asistencia> asistencias)
        {
            try
            {
                if (dgvAsistencia.Columns.Count == 0)
                {
                    ConfigurarDataGridView();
                }
                
                dgvAsistencia.Rows.Clear();
                
                if (asistencias.Count == 0)
                {
                    // Mostrar mensaje si no hay asistencias
                    dgvAsistencia.Rows.Add("", "", "", "", "", "");
                    dgvAsistencia.Rows[0].Height = 200;
                    dgvAsistencia.Rows[0].ReadOnly = true;
                    dgvAsistencia.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
                    dgvAsistencia.Rows[0].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 250, 250);
                    dgvAsistencia.Rows[0].DefaultCellStyle.ForeColor = UIHelper.ColorTextoGris;
                    dgvAsistencia.Rows[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAsistencia.Rows[0].Cells[0].Value = "No hay registros de asistencia para este mes.\nUse el formulario para registrar asistencias.";
                    dgvAsistencia.Rows[0].Cells[0].Style.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
                    // Combinar celdas visualmente
                    for (int i = 1; i < dgvAsistencia.Columns.Count; i++)
                    {
                        dgvAsistencia.Rows[0].Cells[i].Value = "";
                    }
                }
                else
                {
                    foreach (var asistencia in asistencias)
                    {
                        int rowIndex = dgvAsistencia.Rows.Add(
                            asistencia.NombreEmpleado,
                            asistencia.Fecha.ToString("yyyy-MM-dd"),
                            asistencia.HoraEntrada?.ToString(@"hh\:mm") ?? "",
                            asistencia.HoraSalida?.ToString(@"hh\:mm") ?? "",
                            asistencia.HorasTrabajadas?.ToString("F2") ?? "0.00",
                            asistencia.Estado
                        );
                        
                        dgvAsistencia.Rows[rowIndex].Tag = asistencia.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al cargar asistencias: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
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

            if (cmbEstado.SelectedIndex < 0)
            {
                _errorProvider.SetError(cmbEstado, "Debe seleccionar un estado");
                esValido = false;
            }

            // Validar horas solo si ambas están seleccionadas
            if (cmbEstado.SelectedItem?.ToString() == "Presente" || 
                (dtpEntrada.Value.TimeOfDay != TimeSpan.Zero && dtpSalida.Value.TimeOfDay != TimeSpan.Zero))
            {
                // Permitir turnos nocturnos (salida del día siguiente)
                var entrada = dtpEntrada.Value.TimeOfDay;
                var salida = dtpSalida.Value.TimeOfDay;
                
                // Si la salida es menor que la entrada, asumimos que es del día siguiente (turno nocturno)
                // Pero validamos que no exceda 24 horas
                TimeSpan diferencia;
                if (salida < entrada)
                {
                    diferencia = TimeSpan.FromDays(1) - entrada + salida;
                }
                else
                {
                    diferencia = salida - entrada;
                }
                
                if (diferencia.TotalHours > 24)
                {
                    _errorProvider.SetError(dtpSalida, "Las horas trabajadas no pueden exceder 24 horas");
                    esValido = false;
                }
                else if (diferencia.TotalHours <= 0 && entrada != TimeSpan.Zero && salida != TimeSpan.Zero)
                {
                    _errorProvider.SetError(dtpSalida, "La hora de salida debe ser posterior a la hora de entrada");
                    esValido = false;
                }
            }

            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarAsistencia?.Invoke(this, EventArgs.Empty);
        }

        public void LimpiarFormulario()
        {
            UIHelper.LimpiarErrores(_errorProvider);
            txtEmpleado.Text = "";
            dtpFecha.Value = DateTime.Now;
            dtpFecha.Enabled = true; // Rehabilitar fecha al limpiar
            dtpEntrada.Value = DateTime.Today.AddHours(8).AddMinutes(30);
            dtpSalida.Value = DateTime.Today.AddHours(17).AddMinutes(30);
            cmbEstado.SelectedIndex = 0;
        }

        public void MostrarAsistencia(Asistencia asistencia)
        {
            EmpleadoId = asistencia.EmpleadoId;
            Fecha = asistencia.Fecha;
            HoraEntrada = asistencia.HoraEntrada;
            HoraSalida = asistencia.HoraSalida;
            Estado = asistencia.Estado;
            dtpFecha.Enabled = false; // Deshabilitar fecha al editar
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
                "HoraEntrada" => dtpEntrada,
                "HoraSalida" => dtpSalida,
                "Estado" => cmbEstado,
                _ => null
            };
        }

        private void dtpMes_ValueChanged(object sender, EventArgs e)
        {
            _presenter.CargarAsistenciasPorMes(dtpMes.Value);
        }

        private void DgvAsistencia_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                
                var row = dgvAsistencia.Rows[e.RowIndex];
                if (row.Tag == null) return;
                
                int asistenciaId = (int)row.Tag;
                SeleccionarAsistencia?.Invoke(this, asistenciaId);
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al procesar el doble clic: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }
    }
}

