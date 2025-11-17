using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Common.Helpers;
using System.Linq;

namespace RecursosHumanos.Presentation.Views
{
    public partial class frmIncorporacion : Form
    {
        private readonly IncorporacionBLL _incorporacionBLL = new();
        private readonly EmpleadoBLL _empleadoBLL = new();

        public frmIncorporacion()
        {
            InitializeComponent();
        }

        private void frmIncorporacion_Load(object sender, EventArgs e)
        {
            CargarIncorporaciones();
        }

        private void btnNuevoProceso_Click(object sender, EventArgs e)
        {
            using (var frmNuevo = new frmNuevaIncorporacion())
            {
                if (frmNuevo.ShowDialog() == DialogResult.OK)
                {
                    CargarIncorporaciones();
                }
            }
        }

        private void CargarIncorporaciones()
        {
            try
            {
                var incorporaciones = _incorporacionBLL.ObtenerTodas();
                flpIncorporaciones.Controls.Clear();
                
                // Mostrar mensaje si no hay incorporaciones
                if (incorporaciones.Count == 0)
                {
                    var panelVacio = new Panel
                    {
                        Size = new Size(flpIncorporaciones.Width - SystemInformation.VerticalScrollBarWidth - 30, 200),
                        BackColor = Color.FromArgb(250, 250, 250),
                        Margin = new Padding(10),
                        BorderStyle = BorderStyle.FixedSingle,
                        Padding = new Padding(15)
                    };
                    var lblVacio = new Label
                    {
                        Text = "No hay procesos de incorporación registrados.\n\nHaga clic en 'Crear Nuevo Proceso' para comenzar.",
                        Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                        Location = new Point(15, 70),
                        Size = new Size(panelVacio.Width - 30, 80),
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = UIHelper.ColorTextoGris,
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                    };
                    panelVacio.Controls.Add(lblVacio);
                    flpIncorporaciones.Controls.Add(panelVacio);
                    return;
                }
                
                foreach (var inc in incorporaciones)
                {
                    // Obtener las tareas de esta incorporación
                    var tareas = _incorporacionBLL.ObtenerTareas(inc.Id);
                    
                    // Calcular altura del panel según cantidad de tareas
                    int alturaPanel = 150 + (tareas.Count * 30) + 50; // Espacio para botones
                    if (alturaPanel < 220) alturaPanel = 220;
                    
                    // Calcular ancho disponible (considerando scrollbar vertical)
                    int anchoDisponible = flpIncorporaciones.Width - SystemInformation.VerticalScrollBarWidth - 30;
                    
                    var panel = new Panel
                    {
                        Size = new Size(anchoDisponible, alturaPanel),
                        BackColor = Color.White,
                        Margin = new Padding(10, 10, 10, 10),
                        BorderStyle = BorderStyle.FixedSingle,
                        Padding = new Padding(15, 15, 15, 15)
                    };
                    
                    // Nombre del empleado
                    var lblNombre = new Label
                    {
                        Text = inc.NombreEmpleado,
                        Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                        Location = new Point(15, 15),
                        Size = new Size(anchoDisponible - 250, 25),
                        ForeColor = UIHelper.ColorTextoOscuro,
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                    };
                    
                    // Tipo de proceso (azul)
                    var lblTipo = new Label
                    {
                        Text = inc.TipoProceso,
                        Font = UIHelper.FuentePrincipal,
                        Location = new Point(15, 45),
                        Size = new Size(anchoDisponible - 250, 20),
                        ForeColor = UIHelper.ColorAcento,
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                    };
                    
                    // Fecha de inicio
                    var lblFecha = new Label
                    {
                        Text = $"Iniciado el {inc.FechaInicio:dd/MM/yyyy}",
                        Font = new Font("Segoe UI", 8F),
                        ForeColor = UIHelper.ColorTextoGris,
                        Location = new Point(15, 70),
                        Size = new Size(anchoDisponible - 250, 20),
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                    };
                    
                    // Barra de progreso
                    var porcentaje = Math.Max(0, Math.Min(100, (int)inc.PorcentajeCompletado));
                    var progressBar = new ProgressBar
                    {
                        Location = new Point(15, 95),
                        Size = new Size(Math.Min(400, anchoDisponible - 300), 20),
                        Minimum = 0,
                        Maximum = 100,
                        Value = porcentaje,
                        Style = ProgressBarStyle.Continuous,
                        Anchor = AnchorStyles.Top | AnchorStyles.Left
                    };
                    
                    // Texto de progreso
                    var lblProgreso = new Label
                    {
                        Text = $"{inc.TareasCompletadas}/{inc.TotalTareas} Tareas completadas",
                        Font = UIHelper.FuentePrincipal,
                        ForeColor = UIHelper.ColorTextoGris,
                        Location = new Point(425, 95),
                        Size = new Size(anchoDisponible - 450, 20),
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                    };
                    
                    // Lista de tareas con checkboxes
                    int yPos = 125;
                    foreach (var tarea in tareas)
                    {
                        var chkTarea = new CheckBox
                        {
                            Text = tarea.Descripcion,
                            Location = new Point(15, yPos),
                            Size = new Size(anchoDisponible - 250, 25),
                            Font = UIHelper.FuentePrincipal,
                            ForeColor = UIHelper.ColorTextoOscuro,
                            Checked = tarea.Completada,
                            Enabled = inc.Estado == "En Proceso",
                            AutoSize = false,
                            Tag = new { IncorporacionId = inc.Id, TareaId = tarea.Id },
                            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                        };
                        // Si está completada, usar color azul para el check
                        if (tarea.Completada)
                        {
                            chkTarea.ForeColor = UIHelper.ColorAcento;
                        }
                        chkTarea.CheckedChanged += (s, args) => 
                        {
                            if (s != null) ChkTarea_CheckedChanged(s, args, inc.Id, tarea.Id, progressBar, lblProgreso, inc);
                        };
                        panel.Controls.Add(chkTarea);
                        yPos += 30;
                    }
                    
                    // Botones de acción (Editar y Eliminar) - posicionados en la esquina superior derecha
                    var btnEditar = new Button
                    {
                        Text = "Editar",
                        Size = new Size(90, 30),
                        Location = new Point(anchoDisponible - 200, 15),
                        Cursor = Cursors.Hand,
                        Tag = inc.Id,
                        Anchor = AnchorStyles.Top | AnchorStyles.Right
                    };
                    UIHelper.AplicarEstiloBotonAccion(btnEditar, TipoBotonAccion.Advertencia);
                    btnEditar.Click += (s, e) => EditarIncorporacion(inc.Id);
                    
                    var btnEliminar = new Button
                    {
                        Text = "Eliminar",
                        Size = new Size(90, 30),
                        Location = new Point(anchoDisponible - 100, 15),
                        Cursor = Cursors.Hand,
                        Tag = inc.Id,
                        Anchor = AnchorStyles.Top | AnchorStyles.Right
                    };
                    UIHelper.AplicarEstiloBotonAccion(btnEliminar, TipoBotonAccion.Error);
                    btnEliminar.Click += (s, e) => EliminarIncorporacion(inc.Id);
                    
                    panel.Controls.AddRange(new Control[] { lblNombre, lblTipo, lblFecha, progressBar, lblProgreso, btnEditar, btnEliminar });
                    flpIncorporaciones.Controls.Add(panel);
                }
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al cargar incorporaciones: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void ChkTarea_CheckedChanged(object sender, EventArgs e, int incorporacionId, int tareaId, ProgressBar progressBar, Label lblProgreso, Incorporacion inc)
        {
            try
            {
                var chk = sender as CheckBox;
                if (chk == null) return;

                var todasTareas = _incorporacionBLL.ObtenerTareas(incorporacionId);
                var tarea = todasTareas.FirstOrDefault(t => t.Id == tareaId);
                if (tarea == null) return;

                tarea.Completada = chk.Checked;
                tarea.FechaCompletada = chk.Checked ? DateTime.Now : null;

                if (_incorporacionBLL.GuardarTarea(tarea))
                {
                    // Actualizar contadores
                    todasTareas = _incorporacionBLL.ObtenerTareas(incorporacionId);
                    var completadas = todasTareas.Count(t => t.Completada);
                    var total = todasTareas.Count;

                    inc.TareasCompletadas = completadas;
                    inc.TotalTareas = total;

                    // Actualizar en BD
                    if (completadas == total && total > 0 && inc.Estado == "En Proceso")
                    {
                        inc.Estado = "Completado";
                        inc.FechaFin = DateTime.Now;
                    }
                    else if (completadas < total && inc.Estado == "Completado")
                    {
                        // Si se desmarca una tarea, volver a "En Proceso"
                        inc.Estado = "En Proceso";
                        inc.FechaFin = null;
                    }

                    _incorporacionBLL.Actualizar(inc);

                    // Actualizar UI
                    var porcentaje = Math.Max(0, Math.Min(100, (int)inc.PorcentajeCompletado));
                    progressBar.Value = porcentaje;
                    lblProgreso.Text = $"{completadas}/{total} Tareas completadas";
                    
                    // Si hay inconsistencia de datos, mostrar advertencia
                    if (completadas > total)
                    {
                        lblProgreso.ForeColor = UIHelper.ColorError;
                        lblProgreso.Text += " ⚠ (Inconsistencia de datos)";
                    }
                    else
                    {
                        lblProgreso.ForeColor = UIHelper.ColorTextoGris;
                    }

                    if (chk.Checked)
                    {
                        chk.ForeColor = UIHelper.ColorAcento;
                    }
                    else
                    {
                        chk.ForeColor = Color.FromArgb(44, 62, 80);
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al actualizar tarea: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void EditarIncorporacion(int incorporacionId)
        {
            try
            {
                var incorporacion = _incorporacionBLL.ObtenerPorId(incorporacionId);
                if (incorporacion == null)
                {
                    UIHelper.MostrarMensaje("No se encontró el proceso de incorporación.", "Error", MessageBoxIcon.Error);
                    return;
                }

                using (var frmEditar = new frmNuevaIncorporacion(incorporacion))
                {
                    if (frmEditar.ShowDialog() == DialogResult.OK)
                    {
                        CargarIncorporaciones();
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al editar incorporación: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void EliminarIncorporacion(int incorporacionId)
        {
            try
            {
                var incorporacion = _incorporacionBLL.ObtenerPorId(incorporacionId);
                if (incorporacion == null) return;

                var mensaje = $"¿Está seguro que desea eliminar el proceso de {incorporacion.TipoProceso} de {incorporacion.NombreEmpleado}?\n\n" +
                             "Esta acción no se puede deshacer.";
                
                if (UIHelper.MostrarConfirmacion(mensaje, "Confirmar Eliminación") == DialogResult.Yes)
                {
                    if (_incorporacionBLL.Eliminar(incorporacionId))
                    {
                        UIHelper.MostrarMensaje("Proceso de incorporación eliminado exitosamente.", "Éxito", MessageBoxIcon.Information);
                        CargarIncorporaciones();
                    }
                    else
                    {
                        UIHelper.MostrarMensaje("Error al eliminar el proceso de incorporación.", "Error", MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al eliminar incorporación: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }
    }
}

