using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Common.Helpers;
using RecursosHumanos.Presentation.Interfaces;
using RecursosHumanos.Presentation.Presenters;
using System.Linq;

namespace RecursosHumanos.Presentation.Views
{
    // formulario para gestion de nomina
  public partial class frmNomina : Form, INominaView
    {
        private readonly NominaPresenter _presenter;
   private bool _eventosConfigurados = false;

        public string Periodo 
     { 
      get => dtpPeriodo.Value.ToString("MMMM yyyy"); 
            set 
            {
  if (DateTime.TryParse(value, out DateTime fecha))
    {
          dtpPeriodo.Value = fecha;
          }
  }
        }

        // Eventos de INominaView
        public event EventHandler? CargarDatos;
        public event EventHandler<string>? PrepararNomina;
        public event EventHandler<int>? MarcarComoPagada;
        public event EventHandler<int>? SeleccionarNomina;

      public frmNomina()
        {
      InitializeComponent();
            _presenter = new NominaPresenter(this);
     }

  private void frmNomina_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            dtpPeriodo.Value = DateTime.Now;
            dtpPeriodo.Format = DateTimePickerFormat.Custom;
     dtpPeriodo.CustomFormat = "MMMM yyyy";
         dtpPeriodo.ShowUpDown = true;
      CargarDatos?.Invoke(this, EventArgs.Empty);
        }

 private void ConfigurarDataGridView()
        {
        try
  {
 // Aplicar estilo estándar
              UIHelper.AplicarEstiloDataGridView(dgvNomina);
    
                dgvNomina.Columns.Clear();
                dgvNomina.AutoGenerateColumns = false;
        dgvNomina.ScrollBars = ScrollBars.Vertical;
     dgvNomina.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
       
    // Columna Empleado
       var colEmpleado = new DataGridViewTextBoxColumn
                {
     Name = "Empleado",
          HeaderText = "EMPLEADO",
          Width = 250,
     ReadOnly = true
  };
        colEmpleado.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
  colEmpleado.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
         dgvNomina.Columns.Add(colEmpleado);
                
                // Columna Salario Bruto
         var colSalarioBruto = new DataGridViewTextBoxColumn
            {
  Name = "SalarioBruto",
  HeaderText = "SALARIO BRUTO",
           Width = 150,
   ReadOnly = true
   };
        colSalarioBruto.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
    colSalarioBruto.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
       colSalarioBruto.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
     dgvNomina.Columns.Add(colSalarioBruto);
    
          // Columna Bonificaciones
         var colBonificaciones = new DataGridViewTextBoxColumn
             {
Name = "Bonificaciones",
           HeaderText = "BONIFICACIONES",
    Width = 150,
ReadOnly = true
     };
          colBonificaciones.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
  colBonificaciones.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
        colBonificaciones.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
          dgvNomina.Columns.Add(colBonificaciones);
           
            // Columna Deducciones
                var colDeducciones = new DataGridViewTextBoxColumn
  {
         Name = "Deducciones",
         HeaderText = "DEDUCCIONES",
        Width = 150,
    ReadOnly = true
 };
    colDeducciones.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                colDeducciones.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
           colDeducciones.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
dgvNomina.Columns.Add(colDeducciones);
                
      // Columna Salario Neto
      var colSalarioNeto = new DataGridViewTextBoxColumn
   {
            Name = "SalarioNeto",
         HeaderText = "SALARIO NETO",
        Width = 150,
      ReadOnly = true
      };
   colSalarioNeto.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
         colSalarioNeto.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
             colSalarioNeto.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
     colSalarioNeto.DefaultCellStyle.SelectionForeColor = UIHelper.ColorTextoOscuro;
      dgvNomina.Columns.Add(colSalarioNeto);
  
              // Columna Estado
     var colEstado = new DataGridViewTextBoxColumn
    {
             Name = "Estado",
          HeaderText = "ESTADO",
                 Width = 120,
     ReadOnly = true
                };
     colEstado.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
         dgvNomina.Columns.Add(colEstado);
      
      // Columna Acciones
    var colAcciones = new DataGridViewTextBoxColumn
              {
      Name = "Acciones",
        HeaderText = "ACCIONES",
         Width = 200,
      ReadOnly = true
     };
        colAcciones.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
colAcciones.DefaultCellStyle.BackColor = Color.White;
        colAcciones.DefaultCellStyle.SelectionBackColor = Color.White;
                colAcciones.DefaultCellStyle.SelectionForeColor = Color.White;
   dgvNomina.Columns.Add(colAcciones);
       
            // Registrar eventos solo una vez
   if (!_eventosConfigurados)
     {
        dgvNomina.CellDoubleClick += DgvNomina_CellDoubleClick;
        dgvNomina.CellFormatting += DgvNomina_CellFormatting;
               dgvNomina.Scroll += DgvNomina_Scroll;
        dgvNomina.Resize += DgvNomina_Resize;
   _eventosConfigurados = true;
     }
                
      // Calcular ancho disponible
    int anchoDisponible = dgvNomina.Width - SystemInformation.VerticalScrollBarWidth - 4;
            int anchoTotalColumnas = dgvNomina.Columns.Cast<DataGridViewColumn>().Sum(c => c.Width);
   
                // Ajustar columnas para que quepan sin scrollbar horizontal
          if (anchoTotalColumnas > anchoDisponible)
      {
     double factor = (double)anchoDisponible / anchoTotalColumnas;
       foreach (DataGridViewColumn col in dgvNomina.Columns)
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
        colEmpleado.Width += espacioExtra;
            }
   }
      catch (Exception ex)
            {
  UIHelper.MostrarMensaje($"Error al configurar el DataGridView: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
  }

        private void btnPreparar_Click(object sender, EventArgs e)
        {
            var periodo = dtpPeriodo.Value.ToString("MMMM yyyy");
        
   // Confirmar antes de preparar
         var mensaje = $"¿Está seguro que desea preparar la nómina para el período {periodo}?\n\n" +
             "Esto eliminará cualquier nómina pendiente existente para este período.";
     if (UIHelper.MostrarConfirmacion(mensaje, "Confirmar Preparación de Nómina") == DialogResult.Yes)
    {
PrepararNomina?.Invoke(this, periodo);
            }
   }

        private void dtpPeriodo_ValueChanged(object sender, EventArgs e)
        {
          _presenter.CargarNominaPorPeriodo(Periodo);
        }

      public void CargarNomina(List<Nomina> nominas)
{
    try
        {
                if (dgvNomina.Columns.Count == 0)
     {
        ConfigurarDataGridView();
         }
     
                // IMPORTANTE: Limpiar TODOS los controles de acciones antes de limpiar las filas
     LimpiarBotonesAcciones();
        
     dgvNomina.Rows.Clear();
         
    // Mostrar mensaje si no hay nómina
          if (nominas.Count == 0)
             {
               dgvNomina.Rows.Add("", "", "", "", "", "", "");
        dgvNomina.Rows[0].Height = 200;
            dgvNomina.Rows[0].ReadOnly = true;
  dgvNomina.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
         dgvNomina.Rows[0].DefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 250, 250);
          dgvNomina.Rows[0].DefaultCellStyle.ForeColor = UIHelper.ColorTextoGris;
           dgvNomina.Rows[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
     dgvNomina.Rows[0].Cells[0].Value = $"No hay nómina generada para {Periodo}.\nHaga clic en 'Preparar Nómina' para generar la nómina del período.";
            dgvNomina.Rows[0].Cells[0].Style.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
       // Combinar celdas visualmente
        for (int i = 1; i < dgvNomina.Columns.Count; i++)
                 {
     dgvNomina.Rows[0].Cells[i].Value = "";
               }
   return;
                }
           
       foreach (var nomina in nominas)
                {
int rowIndex = dgvNomina.Rows.Add(
   nomina.NombreEmpleado,
       $"S/ {nomina.SalarioBruto:F2}",
   $"S/ {nomina.Bonificaciones:F2}",
         $"S/ {nomina.Deducciones:F2}",
       $"S/ {nomina.SalarioNeto:F2}",
 nomina.Estado,
         "" // Acciones
             );
            
            dgvNomina.Rows[rowIndex].Tag = nomina;
                 
          // Aplicar formato de estado
       var estadoCell = dgvNomina.Rows[rowIndex].Cells["Estado"];
         if (estadoCell != null)
           {
   if (nomina.Estado.Equals("Pagada", StringComparison.OrdinalIgnoreCase))
       {
   estadoCell.Style.BackColor = UIHelper.ColorExito;
       estadoCell.Style.ForeColor = UIHelper.ColorTextoClaro;
    estadoCell.Style.SelectionBackColor = UIHelper.ColorExito;
       estadoCell.Style.SelectionForeColor = UIHelper.ColorTextoClaro;
           }
      else
          {
                estadoCell.Style.BackColor = UIHelper.ColorAdvertencia;
        estadoCell.Style.ForeColor = UIHelper.ColorTextoOscuro;
 estadoCell.Style.SelectionBackColor = UIHelper.ColorAdvertencia;
    estadoCell.Style.SelectionForeColor = UIHelper.ColorTextoOscuro;
        }
       }
      }
        
         // Agregar botones de acción después de agregar todas las filas
       for (int i = 0; i < dgvNomina.Rows.Count; i++)
    {
          AgregarBotonesAcciones(i);
         }
            }
   catch (Exception ex)
    {
                UIHelper.MostrarMensaje($"Error al cargar nómina: {ex.Message}", "Error", MessageBoxIcon.Error);
     }
        }

 private void DgvNomina_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
 if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
if (e.ColumnIndex == dgvNomina.Columns["Acciones"].Index) return;
     
          var row = dgvNomina.Rows[e.RowIndex];
  if (row.Tag is Nomina nomina && nomina.Estado == "Pendiente")
     {
    EditarNomina(nomina);
   }
        }

  private void DgvNomina_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
 {
    // El formato de estado ya se aplica en CargarNomina
        }

        private void DgvNomina_Scroll(object? sender, ScrollEventArgs e)
        {
 ReposicionarBotonesAcciones();
        }

        private void DgvNomina_Resize(object? sender, EventArgs e)
        {
         ReposicionarBotonesAcciones();
      }

        private void LimpiarBotonesAcciones()
        {
    // Obtener todos los controles que son botones o labels de nómina
       var controlesAEliminar = dgvNomina.Controls.Cast<Control>()
          .Where(c => c.Tag != null && c.Tag.ToString()!.StartsWith("btn_nomina_"))
    .ToList();
            
   foreach (var control in controlesAEliminar)
            {
                dgvNomina.Controls.Remove(control);
 control.Dispose();
            }
        }

        private void AgregarBotonesAcciones(int rowIndex)
      {
            if (rowIndex < 0 || rowIndex >= dgvNomina.Rows.Count) return;
        
            var row = dgvNomina.Rows[rowIndex];
            if (row.Tag is not Nomina nomina) return;

   var accionesCell = row.Cells["Acciones"];
     if (accionesCell == null) return;
        
            var cellBounds = dgvNomina.GetCellDisplayRectangle(accionesCell.ColumnIndex, rowIndex, false);
        if (cellBounds.Width == 0 || cellBounds.Height == 0) return;
            
            // Limpiar botones existentes de esta fila específica
     LimpiarBotonesAccionesFila(rowIndex);
            
       if (nomina.Estado == "Pendiente")
            {
    // Botón Editar
           var btnEditar = new Button
            {
              Text = "Editar",
         Size = new Size(80, 28),
        Location = new Point(cellBounds.Left + 5, cellBounds.Top + (cellBounds.Height - 28) / 2),
                 Tag = $"btn_nomina_{rowIndex}_editar",
        Cursor = Cursors.Hand
   };
          UIHelper.AplicarEstiloBotonAccion(btnEditar, TipoBotonAccion.Advertencia);
       btnEditar.Click += (s, e) => EditarNomina(nomina);
    dgvNomina.Controls.Add(btnEditar);
           
      // Botón Marcar como Pagada
          var btnPagar = new Button
        {
   Text = "Pagar",
            Size = new Size(80, 28),
              Location = new Point(cellBounds.Left + 90, cellBounds.Top + (cellBounds.Height - 28) / 2),
          Tag = $"btn_nomina_{rowIndex}_pagar",
    Cursor = Cursors.Hand
       };
   UIHelper.AplicarEstiloBotonAccion(btnPagar, TipoBotonAccion.Exito);
  btnPagar.Click += (s, e) => MarcarComoPagada?.Invoke(this, nomina.Id);
   dgvNomina.Controls.Add(btnPagar);
            }
         else if (nomina.Estado == "Pagada")
            {
             var lblPagada = new Label
                {
       Text = "✓ Pagada",
      Size = new Size(100, 28),
 Location = new Point(cellBounds.Left + 5, cellBounds.Top + (cellBounds.Height - 28) / 2),
      TextAlign = ContentAlignment.MiddleCenter,
 ForeColor = Color.FromArgb(46, 204, 113),
         Font = new Font("Segoe UI", 9F, FontStyle.Bold),
 Tag = $"btn_nomina_{rowIndex}_label"
 };
                dgvNomina.Controls.Add(lblPagada);
 }
        }

        private void LimpiarBotonesAccionesFila(int rowIndex)
        {
            var controlesAEliminar = dgvNomina.Controls.Cast<Control>()
      .Where(c => c.Tag != null && c.Tag.ToString()!.StartsWith($"btn_nomina_{rowIndex}_"))
        .ToList();
            
       foreach (var control in controlesAEliminar)
            {
      dgvNomina.Controls.Remove(control);
  control.Dispose();
     }
        }

 private void ReposicionarBotonesAcciones()
 {
            for (int i = 0; i < dgvNomina.Rows.Count; i++)
  {
                var row = dgvNomina.Rows[i];
    var accionesCell = row.Cells["Acciones"];
       if (accionesCell == null) continue;

       var cellBounds = dgvNomina.GetCellDisplayRectangle(accionesCell.ColumnIndex, i, false);
      
     var controles = dgvNomina.Controls.Cast<Control>()
         .Where(c => c.Tag != null && c.Tag.ToString()!.StartsWith($"btn_nomina_{i}_"))
             .ToList();
                
 foreach (var control in controles)
              {
      string? tag = control.Tag?.ToString();
  if (tag == null) continue;
        
           // Ocultar si la celda no es visible
if (cellBounds.Width == 0 || cellBounds.Height == 0)
    {
  control.Visible = false;
         continue;
 }
               
control.Visible = true;
     
      if (tag.EndsWith("_editar"))
  {
   control.Location = new Point(cellBounds.Left + 5, cellBounds.Top + (cellBounds.Height - control.Height) / 2);
          }
      else if (tag.EndsWith("_pagar"))
    {
   control.Location = new Point(cellBounds.Left + 90, cellBounds.Top + (cellBounds.Height - control.Height) / 2);
   }
        else if (tag.EndsWith("_label"))
{
              control.Location = new Point(cellBounds.Left + 5, cellBounds.Top + (cellBounds.Height - control.Height) / 2);
        }
     }
       }
        }

        private void EditarNomina(Nomina nomina)
      {
            SeleccionarNomina?.Invoke(this, nomina.Id);
     using (var frmEditar = new frmEditarNomina(nomina))
   {
        if (frmEditar.ShowDialog() == DialogResult.OK)
    {
       _presenter.CargarNominaPorPeriodo(Periodo);
             }
            }
        }

        public void MostrarMensaje(string mensaje, string titulo, MessageBoxIcon icono)
        {
   UIHelper.MostrarMensaje(mensaje, titulo, icono);
        }

      public void MostrarError(string mensaje, string campo)
        {
          // No hay campos de formulario en esta vista, solo mostrar mensaje
         UIHelper.MostrarMensaje(mensaje, "Error", MessageBoxIcon.Error);
      }

        public void LimpiarErrores()
     {
            // No hay ErrorProvider en esta vista
     }
    }
}



