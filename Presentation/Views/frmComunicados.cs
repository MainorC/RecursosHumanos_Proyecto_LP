using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Common.Helpers;
using System.Linq;

namespace RecursosHumanos.Presentation.Views
{
    // formulario para comunicados
    public partial class frmComunicados : Form
    {
        private readonly ComunicadoBLL _comunicadoBLL = new();
        private Comunicado? _comunicadoActual = null;
        private ErrorProvider _errorProvider = new ErrorProvider();
        private List<Comunicado> _todosLosComunicados = new();
        public frmComunicados()
        {
            InitializeComponent();
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        private void frmComunicados_Load(object sender, EventArgs e)
        {
            CargarComunicados();
            ActualizarInterfazModoEdicion();
            ActualizarEstadisticas();
        }

        private void CargarComunicados()
        {
            try
            {
                _todosLosComunicados = _comunicadoBLL.ObtenerTodos();
                AplicarFiltros();
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al cargar comunicados: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void AplicarFiltros()
        {
            try
            {
                var comunicadosFiltrados = _todosLosComunicados.AsEnumerable();

                // Filtro de búsqueda
                if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    string termino = txtBuscar.Text.Trim().ToLower();
                    comunicadosFiltrados = comunicadosFiltrados.Where(c =>
                        c.Titulo.ToLower().Contains(termino) ||
                        c.Contenido.ToLower().Contains(termino)
                    );
                }

                // Filtro de fecha
                string filtroFecha = cmbFiltroFecha.SelectedItem?.ToString() ?? "Todos";
                DateTime fechaLimite = DateTime.Now;
                switch (filtroFecha)
                {
                    case "Hoy":
                        fechaLimite = DateTime.Today;
                        comunicadosFiltrados = comunicadosFiltrados.Where(c => c.FechaPublicacion.Date == fechaLimite);
                        break;
                    case "Esta semana":
                        fechaLimite = DateTime.Today.AddDays(-7);
                        comunicadosFiltrados = comunicadosFiltrados.Where(c => c.FechaPublicacion >= fechaLimite);
                        break;
                    case "Este mes":
                        fechaLimite = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                        comunicadosFiltrados = comunicadosFiltrados.Where(c => c.FechaPublicacion >= fechaLimite);
                        break;
                    case "Últimos 3 meses":
                        fechaLimite = DateTime.Today.AddMonths(-3);
                        comunicadosFiltrados = comunicadosFiltrados.Where(c => c.FechaPublicacion >= fechaLimite);
                        break;
                }

                // Ordenar por fecha más reciente primero
                comunicadosFiltrados = comunicadosFiltrados.OrderByDescending(c => c.FechaPublicacion);

                flpComunicados.Controls.Clear();
                var comunicados = comunicadosFiltrados.ToList();
                
                foreach (var comunicado in comunicados)
                {
                    // Calcular altura del panel según el contenido
                    int alturaPanel = 160;
                    string contenidoPreview = comunicado.Contenido.Length > 200 ? comunicado.Contenido.Substring(0, 200) + "..." : comunicado.Contenido;
                    int lineas = (int)Math.Ceiling((double)contenidoPreview.Length / 80);
                    alturaPanel = Math.Max(alturaPanel, 100 + (lineas * 20));
                    
                    var panel = new Panel
                    {
                        Size = new Size(900, alturaPanel),
                        BackColor = Color.White,
                        Margin = new Padding(10),
                        BorderStyle = BorderStyle.FixedSingle,
                        Padding = new Padding(15),
                        Cursor = Cursors.Hand
                    };
                    
                    // Badge de estado (clickeable para cambiar)
                    var lblEstado = new Label
                    {
                        Text = comunicado.Activo ? "● Activo" : "○ Inactivo",
                        Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                        ForeColor = comunicado.Activo ? UIHelper.ColorExito : UIHelper.ColorTextoGris,
                        Location = new Point(15, 15),
                        AutoSize = true,
                        BackColor = Color.Transparent,
                        Cursor = Cursors.Hand
                    };
                    lblEstado.Click += (s, e) => CambiarEstadoComunicado(comunicado);
                    
                    var lblTitulo = new Label
                    {
                        Text = comunicado.Titulo,
                        Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                        Location = new Point(15, 35),
                        Size = new Size(700, 25),
                        AutoEllipsis = true,
                        ForeColor = UIHelper.ColorTextoOscuro,
                        BackColor = Color.Transparent
                    };
                    
                    var lblFecha = new Label
                    {
                        Text = $"📅 {comunicado.FechaPublicacion:dd 'de' MMMM 'de' yyyy 'a las' HH:mm}",
                        Font = new Font("Segoe UI", 8.5F),
                        ForeColor = UIHelper.ColorTextoGris,
                        Location = new Point(15, 65),
                        AutoSize = true,
                        BackColor = Color.Transparent
                    };
                    
                    var lblContenido = new Label
                    {
                        Text = contenidoPreview,
                        Font = UIHelper.FuentePrincipal,
                        Location = new Point(15, 90),
                        Size = new Size(700, Math.Min(60, lineas * 20)),
                        AutoEllipsis = true,
                        ForeColor = UIHelper.ColorTextoOscuro,
                        BackColor = Color.Transparent
                    };
                    
                    // Botón Ver Completo
                    var btnVerCompleto = new Button
                    {
                        Text = "Ver Completo",
                        Location = new Point(730, 20),
                        Size = new Size(100, 30),
                        Tag = comunicado
                    };
                    UIHelper.AplicarEstiloBoton(btnVerCompleto, true);
                    btnVerCompleto.Click += (s, e) => VerComunicadoCompleto(comunicado);
                    
                    var btnEditar = new Button
                    {
                        Text = "Editar",
                        Location = new Point(730, 60),
                        Size = new Size(100, 30),
                        Tag = comunicado.Id
                    };
                    UIHelper.AplicarEstiloBotonAccion(btnEditar, TipoBotonAccion.Advertencia);
                    btnEditar.Click += (s, e) => EditarComunicado(comunicado);
                    
                    // Botón Activar/Desactivar
                    var btnEstado = new Button
                    {
                        Text = comunicado.Activo ? "Desactivar" : "Activar",
                        Location = new Point(730, 100),
                        Size = new Size(100, 30),
                        Tag = comunicado
                    };
                    UIHelper.AplicarEstiloBotonAccion(btnEstado, comunicado.Activo ? TipoBotonAccion.Advertencia : TipoBotonAccion.Exito);
                    btnEstado.Click += (s, e) => CambiarEstadoComunicado(comunicado);
                    
                    var btnEliminar = new Button
                    {
                        Text = "Eliminar",
                        Location = new Point(730, 140),
                        Size = new Size(100, 30),
                        Tag = comunicado.Id
                    };
                    UIHelper.AplicarEstiloBotonAccion(btnEliminar, TipoBotonAccion.Error);
                    btnEliminar.Click += (s, e) => EliminarComunicado(comunicado.Id);
                    
                    // Ajustar altura del panel si es necesario
                    if (alturaPanel < 180) alturaPanel = 180;
                    panel.Size = new Size(900, alturaPanel);
                    
                    // Hacer el panel clickeable para ver completo (pero no cuando se hace clic en controles)
                    panel.Click += (s, e) => 
                    {
                        if (e is MouseEventArgs me && me.Button == MouseButtons.Left)
                        {
                            // Solo si el clic fue directamente en el panel, no en un control hijo
                            if (panel.GetChildAtPoint(me.Location) == null)
                            {
                                VerComunicadoCompleto(comunicado);
                            }
                        }
                    };
                    lblTitulo.Click += (s, e) => VerComunicadoCompleto(comunicado);
                    lblContenido.Click += (s, e) => VerComunicadoCompleto(comunicado);
                    
                    panel.Controls.AddRange(new Control[] { lblEstado, lblTitulo, lblFecha, lblContenido, btnVerCompleto, btnEditar, btnEstado, btnEliminar });
                    flpComunicados.Controls.Add(panel);
                }

                // Mostrar mensaje si no hay comunicados
                if (comunicados.Count == 0)
                {
                    var panelVacio = new Panel
                    {
                        Size = new Size(900, 150),
                        BackColor = Color.FromArgb(250, 250, 250),
                        Margin = new Padding(10),
                        BorderStyle = BorderStyle.FixedSingle,
                        Padding = new Padding(15)
                    };
                    var lblVacio = new Label
                    {
                        Text = string.IsNullOrWhiteSpace(txtBuscar.Text)
                            ? "No hay comunicados registrados.\n\nUse el formulario a la izquierda para crear un nuevo comunicado."
                            : $"🔍 No se encontraron comunicados que coincidan con '{txtBuscar.Text.Trim()}'.\n\nIntente con otro término de búsqueda o ajuste los filtros.",
                        Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                        Location = new Point(15, 40),
                        Size = new Size(870, 70),
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = UIHelper.ColorTextoGris
                    };
                    panelVacio.Controls.Add(lblVacio);
                    flpComunicados.Controls.Add(panelVacio);
                }
                
                ActualizarEstadisticas();
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

        private void cmbFiltroFecha_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private bool ValidarFormulario()
        {
            bool esValido = true;
            UIHelper.LimpiarErrores(_errorProvider);

            if (!UIHelper.ValidarCampoRequerido(txtTitulo, _errorProvider, "El título es obligatorio"))
                esValido = false;

            if (!UIHelper.ValidarCampoRequerido(txtContenido, _errorProvider, "El contenido es obligatorio"))
                esValido = false;

            // validar longitud minima
            if (txtContenido.Text.Trim().Length < 10)
            {
                _errorProvider.SetError(txtContenido, "El contenido debe tener al menos 10 caracteres");
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

                var comunicado = new Comunicado
                {
                    Id = _comunicadoActual?.Id ?? 0,
                    Titulo = txtTitulo.Text.Trim(),
                    Contenido = txtContenido.Text.Trim(),
                    FechaPublicacion = _comunicadoActual?.FechaPublicacion ?? DateTime.Now,
                    Activo = true
                };

                _comunicadoBLL.Guardar(comunicado);
                string mensaje = _comunicadoActual != null 
                    ? "Comunicado actualizado exitosamente." 
                    : "Comunicado guardado exitosamente.";
                UIHelper.MostrarMensaje(mensaje, "Éxito", MessageBoxIcon.Information);
                LimpiarFormulario();
                CargarComunicados();
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al guardar comunicado: {ex.Message}\n\nDetalles técnicos: {ex.GetType().Name}", "Error", MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void ActualizarInterfazModoEdicion()
        {
            if (_comunicadoActual != null)
            {
                btnGuardar.Text = "Actualizar Comunicado";
                btnLimpiar.Text = "✕ Cancelar Edición";
                btnLimpiar.BackColor = UIHelper.ColorError;
                btnLimpiar.ForeColor = Color.White;
                btnLimpiar.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 57, 43);
            }
            else
            {
                btnGuardar.Text = "Guardar Comunicado";
                btnLimpiar.Text = "Limpiar Formulario";
                btnLimpiar.BackColor = Color.FromArgb(189, 195, 199);
                btnLimpiar.ForeColor = UIHelper.ColorTextoOscuro;
                btnLimpiar.FlatAppearance.MouseOverBackColor = Color.FromArgb(149, 165, 166);
            }
        }

        private void LimpiarFormulario()
        {
            _comunicadoActual = null;
            UIHelper.LimpiarErrores(_errorProvider);
            txtTitulo.Clear();
            txtContenido.Clear();
            ActualizarInterfazModoEdicion();
        }

        private void EditarComunicado(Comunicado comunicado)
        {
            _comunicadoActual = comunicado;
            txtTitulo.Text = comunicado.Titulo;
            txtContenido.Text = comunicado.Contenido;
            ActualizarInterfazModoEdicion();
        }

        /// <summary>
        /// Cambia el estado activo/inactivo de un comunicado
        /// </summary>
        private void CambiarEstadoComunicado(Comunicado comunicado)
        {
            try
            {
                comunicado.Activo = !comunicado.Activo;
                _comunicadoBLL.Guardar(comunicado);
                
                string mensaje = comunicado.Activo 
                    ? "Comunicado activado exitosamente." 
                    : "Comunicado desactivado exitosamente.";
                    
                UIHelper.MostrarMensaje(mensaje, "Éxito", MessageBoxIcon.Information);
                CargarComunicados();
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al cambiar estado del comunicado: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Elimina un comunicado después de confirmación
        /// </summary>
        private void EliminarComunicado(int id)
        {
            var mensaje = "¿Está seguro que desea eliminar este comunicado?\n\nEsta acción no se puede deshacer.";
            if (UIHelper.MostrarConfirmacion(mensaje, "Confirmar Eliminación") == DialogResult.Yes)
            {
                try
                {
                    _comunicadoBLL.Eliminar(id);
                    UIHelper.MostrarMensaje("Comunicado eliminado exitosamente.", "Éxito", MessageBoxIcon.Information);
                    CargarComunicados();
                }
                catch (Exception ex)
                {
                    UIHelper.MostrarMensaje($"Error al eliminar comunicado: {ex.Message}\n\nDetalles técnicos: {ex.GetType().Name}", "Error", MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Muestra el comunicado completo en un diálogo
        /// </summary>
        private void VerComunicadoCompleto(Comunicado comunicado)
        {
            var form = new Form
            {
                Text = comunicado.Titulo,
                Size = new Size(700, 500),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.White
            };

            var lblTitulo = new Label
            {
                Text = comunicado.Titulo,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = UIHelper.ColorTextoOscuro,
                Location = new Point(20, 20),
                Size = new Size(640, 30),
                AutoEllipsis = true
            };

            var lblFecha = new Label
            {
                Text = $"📅 Publicado el {comunicado.FechaPublicacion:dd 'de' MMMM 'de' yyyy 'a las' HH:mm}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = UIHelper.ColorTextoGris,
                Location = new Point(20, 60),
                Size = new Size(640, 20),
                AutoSize = true
            };

            var lblEstado = new Label
            {
                Text = comunicado.Activo ? "● Estado: Activo" : "○ Estado: Inactivo",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = comunicado.Activo ? UIHelper.ColorExito : UIHelper.ColorTextoGris,
                Location = new Point(20, 85),
                Size = new Size(640, 20),
                AutoSize = true
            };

            var txtContenido = new TextBox
            {
                Text = comunicado.Contenido,
                Font = UIHelper.FuentePrincipal,
                Location = new Point(20, 115),
                Size = new Size(640, 300),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(250, 250, 250)
            };

            var btnCerrar = new Button
            {
                Text = "Cerrar",
                Location = new Point(580, 425),
                Size = new Size(80, 30),
                DialogResult = DialogResult.OK
            };
            UIHelper.AplicarEstiloBoton(btnCerrar, false);
            btnCerrar.Click += (s, e) => form.Close();

            form.Controls.AddRange(new Control[] { lblTitulo, lblFecha, lblEstado, txtContenido, btnCerrar });
            form.ShowDialog();
        }

        /// <summary>
        /// Actualiza las estadísticas de comunicados
        /// </summary>
        private void ActualizarEstadisticas()
        {
            try
            {
                var total = _todosLosComunicados.Count;
                var activos = _todosLosComunicados.Count(c => c.Activo);
                var inactivos = total - activos;
                var esteMes = _todosLosComunicados.Count(c => c.FechaPublicacion.Month == DateTime.Now.Month && 
                                                               c.FechaPublicacion.Year == DateTime.Now.Year);

                // Actualizar el título del GroupBox con estadísticas
                gbLista.Text = $"Lista de Comunicados (Total: {total} | Activos: {activos} | Este mes: {esteMes})";
            }
            catch
            {
                // Ignorar errores en estadísticas
            }
        }
    }
}

