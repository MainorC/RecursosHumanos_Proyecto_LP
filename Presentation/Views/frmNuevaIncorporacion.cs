using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Common.Helpers;
using System.Linq;

namespace RecursosHumanos.Presentation.Views
{
    public partial class frmNuevaIncorporacion : Form
    {
        private readonly IncorporacionBLL _incorporacionBLL = new();
        private readonly EmpleadoBLL _empleadoBLL = new();
        private List<string> _tareasSeleccionadas = new();
        private Incorporacion? _incorporacionActual = null;
        private bool _modoEdicion = false;
        private Dictionary<string, int> _diccionarioEmpleados = new(); // Mapeo nombre -> ID para autocompletado

        public frmNuevaIncorporacion()
        {
            InitializeComponent();
        }

        public frmNuevaIncorporacion(Incorporacion incorporacion) : this()
        {
            _incorporacionActual = incorporacion;
            _modoEdicion = true;
        }

        private void frmNuevaIncorporacion_Load(object sender, EventArgs e)
        {
            CargarEmpleados();
            
            if (_modoEdicion && _incorporacionActual != null)
            {
                // Cargar datos para edición
                cmbTipoProceso.Text = _incorporacionActual.TipoProceso;
                dtpFechaInicio.Value = _incorporacionActual.FechaInicio;
                cmbTipoProceso.Enabled = false; // No se puede cambiar el tipo en edición
                
                // Cargar tareas existentes
                var tareas = _incorporacionBLL.ObtenerTareas(_incorporacionActual.Id);
                clbTareas.Items.Clear();
                foreach (var tarea in tareas)
                {
                    clbTareas.Items.Add(tarea.Descripcion, tarea.Completada);
                }
                
                this.Text = "Editar Proceso de Incorporación";
            }
            else
            {
                CargarTareasPredefinidas();
                cmbTipoProceso.SelectedIndex = 0;
                this.Text = "Nuevo Proceso de Incorporación";
            }
        }

        private void CargarEmpleados()
        {
            try
            {
                var empleados = _empleadoBLL.ObtenerTodos(soloActivos: true);
                
                // Configurar autocompletado en lugar de ComboBox
                UIHelper.ConfigurarAutocompletadoEmpleado(txtEmpleado, empleados, _diccionarioEmpleados);
                txtEmpleado.Leave += (s, e) => ValidarEmpleadoSeleccionado();
                
                // Si estamos en modo edición, seleccionar el empleado correspondiente
                if (_modoEdicion && _incorporacionActual != null && _incorporacionActual.EmpleadoId.HasValue)
                {
                    var empleado = empleados.FirstOrDefault(e => e.Id == _incorporacionActual.EmpleadoId.Value);
                    if (empleado != null)
                    {
                        txtEmpleado.Text = empleado.NombreCompleto;
                        txtEmpleado.Enabled = false; // No permitir cambiar el empleado al editar
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al cargar empleados: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }
        
        private void ValidarEmpleadoSeleccionado()
        {
            if (!string.IsNullOrWhiteSpace(txtEmpleado.Text))
            {
                if (!_diccionarioEmpleados.ContainsKey(txtEmpleado.Text))
                {
                    UIHelper.MostrarMensaje("Empleado no encontrado. Use el autocompletado para seleccionar.", "Validación", MessageBoxIcon.Warning);
                    txtEmpleado.Focus();
                }
            }
        }

        private void CargarTareasPredefinidas()
        {
            // Tareas predefinidas para incorporación
            var tareasOnboarding = new List<string>
            {
                "Firmar contrato de trabajo",
                "Configurar cuenta de correo electrónico",
                "Entregar equipo de cómputo",
                "Realizar tour por la oficina",
                "Capacitación de políticas internas",
                "Configurar acceso a sistemas",
                "Presentación con el equipo",
                "Revisar manual de bienvenida"
            };

            // Tareas predefinidas para desincorporación
            var tareasOffboarding = new List<string>
            {
                "Entregar equipo de cómputo",
                "Devolver credenciales y accesos",
                "Cerrar cuentas de sistemas",
                "Firmar documentos de salida",
                "Entrevista de salida",
                "Transferir responsabilidades",
                "Actualizar documentación",
                "Procesar liquidación"
            };

            // Cargar según el tipo de proceso
            if (cmbTipoProceso.SelectedItem?.ToString() == "Incorporación")
            {
                clbTareas.Items.Clear();
                foreach (var tarea in tareasOnboarding)
                {
                    clbTareas.Items.Add(tarea, true);
                }
            }
            else if (cmbTipoProceso.SelectedItem?.ToString() == "Desincorporación")
            {
                clbTareas.Items.Clear();
                foreach (var tarea in tareasOffboarding)
                {
                    clbTareas.Items.Add(tarea, true);
                }
            }
        }

        private void cmbTipoProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTareasPredefinidas();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtEmpleado.Text) || !_diccionarioEmpleados.ContainsKey(txtEmpleado.Text))
                {
                    UIHelper.MostrarMensaje("Debe seleccionar un empleado válido.", "Validación", MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmbTipoProceso.Text))
                {
                    UIHelper.MostrarMensaje("Debe seleccionar un tipo de proceso.", "Validación", MessageBoxIcon.Warning);
                    return;
                }

                var empleadoId = _diccionarioEmpleados[txtEmpleado.Text];
                var empleado = _empleadoBLL.ObtenerPorId(empleadoId);
                if (empleado == null)
                {
                    UIHelper.MostrarMensaje("Empleado no encontrado.", "Error", MessageBoxIcon.Error);
                    return;
                }

                // Obtener tareas seleccionadas
                var tareasSeleccionadas = new List<string>();
                foreach (string item in clbTareas.CheckedItems)
                {
                    tareasSeleccionadas.Add(item);
                }

                if (tareasSeleccionadas.Count == 0)
                {
                    UIHelper.MostrarMensaje("Debe seleccionar al menos una tarea.", "Validación", MessageBoxIcon.Warning);
                    return;
                }

                if (_modoEdicion && _incorporacionActual != null)
                {
                    // Actualizar incorporación existente
                    _incorporacionActual.NombreEmpleado = empleado.NombreCompleto;
                    _incorporacionActual.EmpleadoId = empleadoId;
                    _incorporacionActual.FechaInicio = dtpFechaInicio.Value;
                    
                    // Actualizar tareas
                    var tareasExistentes = _incorporacionBLL.ObtenerTareas(_incorporacionActual.Id);
                    var tareasExistentesDesc = new HashSet<string>(tareasExistentes.Select(t => t.Descripcion));
                    
                    // Agregar nuevas tareas que no existen
                    foreach (var descripcion in tareasSeleccionadas)
                    {
                        if (!tareasExistentesDesc.Contains(descripcion))
                        {
                            var tarea = new TareaIncorporacion
                            {
                                IncorporacionId = _incorporacionActual.Id,
                                Descripcion = descripcion,
                                Completada = false
                            };
                            _incorporacionBLL.GuardarTarea(tarea);
                        }
                    }
                    
                    // Recalcular totales
                    var todasTareas = _incorporacionBLL.ObtenerTareas(_incorporacionActual.Id);
                    var completadas = todasTareas.Count(t => t.Completada);
                    _incorporacionActual.TareasCompletadas = completadas;
                    _incorporacionActual.TotalTareas = todasTareas.Count;
                    
                    // Si todas están completadas, marcar como completado
                    if (completadas == todasTareas.Count && todasTareas.Count > 0 && _incorporacionActual.Estado == "En Proceso")
                    {
                        _incorporacionActual.Estado = "Completado";
                        _incorporacionActual.FechaFin = DateTime.Now;
                    }
                    else if (completadas < todasTareas.Count && _incorporacionActual.Estado == "Completado")
                    {
                        _incorporacionActual.Estado = "En Proceso";
                        _incorporacionActual.FechaFin = null;
                    }
                    
                    _incorporacionBLL.Actualizar(_incorporacionActual);
                    
                    UIHelper.MostrarMensaje("Proceso de incorporación actualizado exitosamente.", "Éxito", MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Crear nueva incorporación
                    var incorporacion = new Incorporacion
                    {
                        EmpleadoId = empleadoId,
                        NombreEmpleado = empleado.NombreCompleto,
                        TipoProceso = cmbTipoProceso.Text,
                        FechaInicio = dtpFechaInicio.Value,
                        Estado = "En Proceso",
                        TareasCompletadas = 0,
                        TotalTareas = tareasSeleccionadas.Count
                    };

                    var incorporacionId = _incorporacionBLL.GuardarConId(incorporacion);
                    if (incorporacionId > 0)
                    {
                        // Crear tareas
                        foreach (var descripcion in tareasSeleccionadas)
                        {
                            var tarea = new TareaIncorporacion
                            {
                                IncorporacionId = incorporacionId,
                                Descripcion = descripcion,
                                Completada = false
                            };
                            _incorporacionBLL.GuardarTarea(tarea);
                        }

                        UIHelper.MostrarMensaje("Proceso de incorporación creado exitosamente.", "Éxito", MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        UIHelper.MostrarMensaje("Error al crear el proceso de incorporación.", "Error", MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAgregarTarea_Click(object sender, EventArgs e)
        {
            AgregarTareaPersonalizada();
        }

        private void txtTareaPersonalizada_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AgregarTareaPersonalizada();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void AgregarTareaPersonalizada()
        {
            string tarea = txtTareaPersonalizada.Text.Trim();
            if (string.IsNullOrWhiteSpace(tarea))
            {
                UIHelper.MostrarMensaje("Por favor, ingrese una descripción para la tarea.", "Validación", MessageBoxIcon.Warning);
                return;
            }

            // Verificar si la tarea ya existe
            if (clbTareas.Items.Cast<string>().Any(t => t.Equals(tarea, StringComparison.OrdinalIgnoreCase)))
            {
                UIHelper.MostrarMensaje("Esta tarea ya está en la lista.", "Validación", MessageBoxIcon.Warning);
                return;
            }

            // Agregar la tarea y marcarla como seleccionada
            clbTareas.Items.Add(tarea, true);
            txtTareaPersonalizada.Clear();
            txtTareaPersonalizada.Focus();
        }
    }
}
