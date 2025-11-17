using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Common.Helpers;
using System.Linq;

namespace RecursosHumanos.Presentation.Views
{
    public partial class frmDashboard : Form
    {
        private readonly EmpleadoBLL _empleadoBLL = new();
        private readonly VacacionBLL _vacacionBLL = new();
        private readonly ComunicadoBLL _comunicadoBLL = new();
        private readonly IncorporacionBLL _incorporacionBLL = new();
        
        // Evento para solicitar abrir un formulario desde el formulario principal
        public event Action<Type, object?>? SolicitarAbrirFormulario;

        public frmDashboard()
        {
            InitializeComponent();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                var empleados = _empleadoBLL.ObtenerTodos(soloActivos: true);
                var empleadosActivos = empleados.Count(e => e.Activo);
                var vacaciones = _vacacionBLL.ObtenerTodas();
                var vacacionesPendientes = vacaciones.Count(v => v.Estado == "Pendiente");
                var salarioPromedio = empleados.Count > 0 ? empleados.Average(e => e.SalarioBase) : 0;
                var comunicados = _comunicadoBLL.ObtenerTodos(soloActivos: true).Take(3).ToList();
                var incorporaciones = _incorporacionBLL.ObtenerTodas().Where(i => i.Estado == "En Proceso").ToList();

                // Actualizar KPIs con formato mejorado
                lblTotalEmpleados.Text = empleados.Count > 0 ? empleados.Count.ToString() : "0";
                lblEmpleadosActivos.Text = empleadosActivos > 0 ? empleadosActivos.ToString() : "0";
                lblVacacionesPendientes.Text = vacacionesPendientes > 0 ? vacacionesPendientes.ToString() : "0";
                lblSalarioPromedio.Text = salarioPromedio > 0 ? $"S/{salarioPromedio:F2}" : "S/0.00";
                
                // Actualizar mensaje de bienvenida con información dinámica
                string nombreUsuario = SessionManager.UsuarioActual?.NombreCompleto ?? "Usuario";
                string mensajeBienvenida = empleados.Count > 0
                    ? $"Bienvenido, {nombreUsuario}. Aquí tienes un resumen del estado actual de tu equipo de {empleados.Count} empleado{(empleados.Count > 1 ? "s" : "")}."
                    : $"Bienvenido, {nombreUsuario}. Comienza agregando empleados para ver el resumen aquí.";
                lblBienvenida.Text = mensajeBienvenida;

                // Cargar acciones pendientes
                CargarAccionesPendientes(vacaciones, incorporaciones);

                // Cargar comunicados
                CargarComunicados(comunicados);

                // Cargar próximos eventos (cumpleaños)
                CargarProximosEventos(empleados);
            }
            catch (Exception ex)
            {
                UIHelper.MostrarMensaje($"Error al cargar datos: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void CargarAccionesPendientes(List<Vacacion> vacaciones, List<Incorporacion> incorporaciones)
        {
            flpAccionesPendientes.Controls.Clear();
            
            // Calcular ancho disponible considerando el scrollbar vertical
            int anchoDisponible = flpAccionesPendientes.Width - SystemInformation.VerticalScrollBarWidth - 20;

            var vacacionesPendientes = vacaciones.Where(v => v.Estado == "Pendiente").Take(2).ToList();
            var incorporacionesPendientes = incorporaciones.Take(2).ToList();
            var totalItems = vacacionesPendientes.Count + incorporacionesPendientes.Count;

            // Vacaciones pendientes
            foreach (var vacacion in vacacionesPendientes)
            {
                var vacacionId = vacacion.Id; // Capturar el ID para el evento
                var panel = new Panel
                {
                    Size = new Size(anchoDisponible, 60),
                    BackColor = Color.White,
                    Margin = new Padding(5),
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(10),
                    Cursor = Cursors.Hand
                };
                
                // Efecto hover
                panel.MouseEnter += (s, e) => panel.BackColor = Color.FromArgb(245, 245, 250);
                panel.MouseLeave += (s, e) => panel.BackColor = Color.White;
                
                // Click en el panel para abrir vacaciones
                panel.Click += (s, e) => SolicitarAbrirFormulario?.Invoke(typeof(frmVacaciones), vacacionId);
                
                var lblTexto = new Label
                {
                    Text = $"Solicitud de Vacaciones de {vacacion.NombreEmpleado}",
                    Font = UIHelper.FuentePrincipal,
                    Location = new Point(15, 20),
                    Size = new Size(panel.Width - 50, 20),
                    AutoEllipsis = true,
                    ForeColor = UIHelper.ColorTextoOscuro,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Cursor = Cursors.Hand
                };
                lblTexto.Click += (s, e) => SolicitarAbrirFormulario?.Invoke(typeof(frmVacaciones), vacacionId);
                
                var lblFlecha = new Label
                {
                    Text = ">",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    Location = new Point(panel.Width - 35, 18),
                    Size = new Size(25, 25),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Cursor = Cursors.Hand,
                    ForeColor = UIHelper.ColorAcento,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };
                lblFlecha.Click += (s, e) => SolicitarAbrirFormulario?.Invoke(typeof(frmVacaciones), vacacionId);
                
                panel.Controls.AddRange(new Control[] { lblTexto, lblFlecha });
                flpAccionesPendientes.Controls.Add(panel);
            }

            // Incorporaciones incompletas
            foreach (var inc in incorporacionesPendientes)
            {
                var incorporacionId = inc.Id; // Capturar el ID para el evento
                var panel = new Panel
                {
                    Size = new Size(anchoDisponible, 60),
                    BackColor = Color.White,
                    Margin = new Padding(5),
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(10),
                    Cursor = Cursors.Hand
                };
                
                // Efecto hover
                panel.MouseEnter += (s, e) => panel.BackColor = Color.FromArgb(245, 245, 250);
                panel.MouseLeave += (s, e) => panel.BackColor = Color.White;
                
                // Click en el panel para abrir incorporación
                panel.Click += (s, e) => SolicitarAbrirFormulario?.Invoke(typeof(frmIncorporacion), incorporacionId);
                
                var lblTexto = new Label
                {
                    Text = $"Proceso de {inc.TipoProceso} de {inc.NombreEmpleado} incompleto",
                    Font = UIHelper.FuentePrincipal,
                    Location = new Point(15, 20),
                    Size = new Size(panel.Width - 50, 20),
                    AutoEllipsis = true,
                    ForeColor = UIHelper.ColorTextoOscuro,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Cursor = Cursors.Hand
                };
                lblTexto.Click += (s, e) => SolicitarAbrirFormulario?.Invoke(typeof(frmIncorporacion), incorporacionId);
                
                var lblFlecha = new Label
                {
                    Text = ">",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    Location = new Point(panel.Width - 35, 18),
                    Size = new Size(25, 25),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Cursor = Cursors.Hand,
                    ForeColor = UIHelper.ColorAcento,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };
                lblFlecha.Click += (s, e) => SolicitarAbrirFormulario?.Invoke(typeof(frmIncorporacion), incorporacionId);
                
                panel.Controls.AddRange(new Control[] { lblTexto, lblFlecha });
                flpAccionesPendientes.Controls.Add(panel);
            }

            // Mostrar mensaje si no hay acciones pendientes
            if (totalItems == 0)
            {
                var panelVacio = new Panel
                {
                    Size = new Size(anchoDisponible, 100),
                    BackColor = Color.FromArgb(250, 250, 250),
                    Margin = new Padding(5),
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(10)
                };
                var lblVacio = new Label
                {
                    Text = "No hay acciones pendientes en este momento.\n¡Todo está al día!",
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    Location = new Point(15, 30),
                    Size = new Size(panelVacio.Width - 30, 50),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = UIHelper.ColorTextoGris,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                panelVacio.Controls.Add(lblVacio);
                flpAccionesPendientes.Controls.Add(panelVacio);
            }
        }

        private void CargarComunicados(List<Comunicado> comunicados)
        {
            flpComunicados.Controls.Clear();
            
            // Calcular ancho disponible considerando el scrollbar vertical
            int anchoDisponible = flpComunicados.Width - SystemInformation.VerticalScrollBarWidth - 20;
            
            foreach (var comunicado in comunicados)
            {
                var comunicadoId = comunicado.Id; // Capturar el ID para el evento
                var panel = new Panel
                {
                    Size = new Size(anchoDisponible, 140),
                    BackColor = Color.White,
                    Margin = new Padding(5, 8, 5, 8),
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(15),
                    Cursor = Cursors.Hand
                };
                
                // Efecto hover
                panel.MouseEnter += (s, e) => panel.BackColor = Color.FromArgb(245, 245, 250);
                panel.MouseLeave += (s, e) => panel.BackColor = Color.White;
                
                // Click en el panel para abrir comunicados
                panel.Click += (s, e) => SolicitarAbrirFormulario?.Invoke(typeof(frmComunicados), comunicadoId);
                
                var lblTitulo = new Label
                {
                    Text = comunicado.Titulo,
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    Location = new Point(15, 15),
                    Size = new Size(panel.Width - 30, 25),
                    AutoEllipsis = true,
                    ForeColor = UIHelper.ColorTextoOscuro,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Cursor = Cursors.Hand
                };
                lblTitulo.Click += (s, e) => SolicitarAbrirFormulario?.Invoke(typeof(frmComunicados), comunicadoId);
                
                var lblContenido = new Label
                {
                    Text = comunicado.Contenido,
                    Font = UIHelper.FuentePrincipal,
                    Location = new Point(15, 45),
                    Size = new Size(panel.Width - 30, 60),
                    AutoEllipsis = true,
                    ForeColor = UIHelper.ColorTextoGris,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Cursor = Cursors.Hand
                };
                lblContenido.Click += (s, e) => SolicitarAbrirFormulario?.Invoke(typeof(frmComunicados), comunicadoId);
                
                var lblFecha = new Label
                {
                    Text = $"Publicado el {comunicado.FechaPublicacion:dd 'de' MMMM 'de' yyyy}",
                    Font = new Font("Segoe UI", 8F),
                    ForeColor = UIHelper.ColorTextoGris,
                    Location = new Point(15, 110),
                    Size = new Size(panel.Width - 30, 20),
                    AutoEllipsis = true,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Cursor = Cursors.Hand
                };
                lblFecha.Click += (s, e) => SolicitarAbrirFormulario?.Invoke(typeof(frmComunicados), comunicadoId);
                
                panel.Controls.Add(lblTitulo);
                panel.Controls.Add(lblContenido);
                panel.Controls.Add(lblFecha);
                flpComunicados.Controls.Add(panel);
            }

            // Mostrar mensaje si no hay comunicados
            if (comunicados.Count == 0)
            {
                var panelVacio = new Panel
                {
                    Size = new Size(anchoDisponible, 150),
                    BackColor = Color.FromArgb(250, 250, 250),
                    Margin = new Padding(5, 8, 5, 8),
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(15)
                };
                var lblVacio = new Label
                {
                    Text = "No hay comunicados recientes.\nLos comunicados importantes aparecerán aquí.",
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    Location = new Point(15, 50),
                    Size = new Size(panelVacio.Width - 30, 50),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = UIHelper.ColorTextoGris,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                panelVacio.Controls.Add(lblVacio);
                flpComunicados.Controls.Add(panelVacio);
            }
        }

        private void CargarProximosEventos(List<Empleado> empleados)
        {
            flpProximosEventos.Controls.Clear();
            
            // Calcular próximos cumpleaños desde FechaNacimiento
            var eventos = empleados
                .Where(e => e.FechaNacimiento.HasValue)
                .Select(e => 
                {
                    var fechaNac = e.FechaNacimiento!.Value;
                    return new
                    {
                        Nombre = e.NombreCompleto,
                        Tipo = "Cumpleaños",
                        FechaNacimiento = fechaNac,
                        ProximoCumpleanos = new DateTime(DateTime.Now.Year, fechaNac.Month, fechaNac.Day)
                    };
                })
                .Where(e => e.ProximoCumpleanos >= DateTime.Now && e.ProximoCumpleanos <= DateTime.Now.AddDays(30))
                .OrderBy(e => e.ProximoCumpleanos)
                .Take(3)
                .Select(e => new
                {
                    e.Nombre,
                    e.Tipo,
                    Fecha = e.ProximoCumpleanos.ToString("dd 'de' MMMM")
                })
                .ToList();

            // No mostrar eventos falsos - solo mostrar si hay datos reales

            // Calcular ancho disponible considerando el scrollbar vertical
            int anchoDisponible = flpProximosEventos.Width - SystemInformation.VerticalScrollBarWidth - 20;
            
            foreach (var evento in eventos)
            {
                // Buscar el empleado por nombre para obtener su ID
                var empleado = empleados.FirstOrDefault(e => e.NombreCompleto == evento.Nombre);
                var empleadoId = empleado?.Id;
                
                var panel = new Panel
                {
                    Size = new Size(anchoDisponible, 70),
                    BackColor = Color.White,
                    Margin = new Padding(5),
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(10),
                    Cursor = Cursors.Hand
                };
                
                // Efecto hover
                panel.MouseEnter += (s, e) => panel.BackColor = Color.FromArgb(245, 245, 250);
                panel.MouseLeave += (s, e) => panel.BackColor = Color.White;
                
                // Click en el panel para abrir empleados (y filtrar por ese empleado si es posible)
                panel.Click += (s, e) => SolicitarAbrirFormulario?.Invoke(typeof(frmEmpleados), empleadoId);
                
                var lblNombre = new Label
                {
                    Text = evento.Nombre,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Location = new Point(15, 15),
                    Size = new Size(panel.Width - 30, 25),
                    AutoEllipsis = true,
                    ForeColor = UIHelper.ColorTextoOscuro,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Cursor = Cursors.Hand
                };
                lblNombre.Click += (s, e) => SolicitarAbrirFormulario?.Invoke(typeof(frmEmpleados), empleadoId);
                
                var lblDetalle = new Label
                {
                    Text = $"{evento.Tipo} - {evento.Fecha}",
                    Font = UIHelper.FuentePrincipal,
                    ForeColor = UIHelper.ColorTextoGris,
                    Location = new Point(15, 40),
                    Size = new Size(panel.Width - 30, 20),
                    AutoEllipsis = true,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Cursor = Cursors.Hand
                };
                lblDetalle.Click += (s, e) => SolicitarAbrirFormulario?.Invoke(typeof(frmEmpleados), empleadoId);
                
                panel.Controls.AddRange(new Control[] { lblNombre, lblDetalle });
                flpProximosEventos.Controls.Add(panel);
            }

            // Mostrar mensaje si no hay eventos próximos
            if (eventos.Count == 0)
            {
                var panelVacio = new Panel
                {
                    Size = new Size(anchoDisponible, 100),
                    BackColor = Color.FromArgb(250, 250, 250),
                    Margin = new Padding(5),
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(10)
                };
                var lblVacio = new Label
                {
                    Text = "No hay eventos próximos en los próximos 30 días.\nLos cumpleaños y eventos importantes aparecerán aquí.",
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    Location = new Point(15, 30),
                    Size = new Size(panelVacio.Width - 30, 50),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = UIHelper.ColorTextoGris,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                panelVacio.Controls.Add(lblVacio);
                flpProximosEventos.Controls.Add(panelVacio);
            }
        }
    }
}

