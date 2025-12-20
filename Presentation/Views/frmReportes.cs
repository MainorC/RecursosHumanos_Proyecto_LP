using RecursosHumanos.Business.Services;
using RecursosHumanos.Common.Helpers;
using System.Text;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace RecursosHumanos.Presentation.Views
{
    public partial class frmReportes : Form
    {
        private readonly EmpleadoBLL _empleadoBLL = new();
        private readonly AsistenciaBLL _asistenciaBLL = new();
private readonly VacacionBLL _vacacionBLL = new();
        private readonly EvaluacionBLL _evaluacionBLL = new();
        private readonly NominaBLL _nominaBLL = new();

        public frmReportes()
        {
InitializeComponent();
 }

        private void btnEmpleadosActivos_Click(object sender, EventArgs e)
        {
         try
          {
        var empleados = _empleadoBLL.ObtenerTodos(soloActivos: true);
     ExportarExcel(empleados.Select(emp => new
    {
    emp.DNI,
 Nombre = emp.Nombre,
    Apellido = emp.Apellido,
            emp.Email,
        Telefono = emp.Telefono ?? "",
     FechaNacimiento = emp.FechaNacimiento?.ToString("dd/MM/yyyy") ?? "",
           Direccion = emp.Direccion ?? "",
  Area = emp.NombreArea ?? "Sin área",
      emp.Puesto,
                TipoContrato = emp.TipoContrato ?? "",
    FechaContrato = emp.FechaContrato?.ToString("dd/MM/yyyy") ?? "",
               SalarioBase = emp.SalarioBase,
           SistemaPension = emp.SistemaPension ?? "",
      Estado = emp.Estado,
     FechaCreacion = emp.FechaCreacion.ToString("dd/MM/yyyy")
             }).ToList(), "EmpleadosActivos");
            }
    catch (Exception ex)
     {
    UIHelper.MostrarMensaje($"Error: {ex.Message}", "Error", MessageBoxIcon.Error);
   }
        }

        private void btnAsistenciaMensual_Click(object sender, EventArgs e)
        {
      try
   {
            using (var frmFiltros = new frmFiltrosReporte("Asistencia"))
    {
        if (frmFiltros.ShowDialog() == DialogResult.OK)
        {
  var fechaInicio = frmFiltros.FechaInicio;
         var fechaFin = frmFiltros.FechaFin;
       var todasAsistencias = _asistenciaBLL.ObtenerTodas();
  var asistencias = todasAsistencias
        .Where(a => a.Fecha.Date >= fechaInicio.Date && a.Fecha.Date <= fechaFin.Date)
     .ToList();
   ExportarExcel(asistencias.Select(a => new
             {
   Empleado = a.NombreEmpleado,
           Fecha = a.Fecha.ToString("dd/MM/yyyy"),
           Entrada = a.HoraEntrada?.ToString(@"hh\:mm") ?? "",
    Salida = a.HoraSalida?.ToString(@"hh\:mm") ?? "",
      HorasTrabajadas = a.HorasTrabajadas ?? 0,
       Estado = a.Estado,
 FechaCreacion = a.FechaCreacion.ToString("dd/MM/yyyy HH:mm")
  }).ToList(), $"Asistencia_{fechaInicio:yyyyMMdd}_{fechaFin:yyyyMMdd}");
   }
         }
  }
            catch (Exception ex)
            {
   UIHelper.MostrarMensaje($"Error: {ex.Message}", "Error", MessageBoxIcon.Error);
          }
 }

        private void btnVacaciones_Click(object sender, EventArgs e)
        {
            try
      {
      using (var frmFiltros = new frmFiltrosReporte("Vacaciones"))
      {
          if (frmFiltros.ShowDialog() == DialogResult.OK)
     {
     var fechaInicio = frmFiltros.FechaInicio;
      var fechaFin = frmFiltros.FechaFin;
             var vacaciones = _vacacionBLL.ObtenerTodas()
   .Where(v => v.FechaInicio >= fechaInicio && v.FechaInicio <= fechaFin)
           .ToList();
    ExportarExcel(vacaciones.Select(v => new
            {
           Empleado = v.NombreEmpleado,
     FechaInicio = v.FechaInicio.ToString("dd/MM/yyyy"),
         FechaFin = v.FechaFin.ToString("dd/MM/yyyy"),
  v.DiasTotales,
             v.Estado,
       FechaCreacion = v.FechaCreacion.ToString("dd/MM/yyyy"),
             FechaAprobacion = v.FechaAprobacion?.ToString("dd/MM/yyyy") ?? ""
            }).ToList(), $"Vacaciones_{fechaInicio:yyyyMMdd}_{fechaFin:yyyyMMdd}");
      }
 }
 }
            catch (Exception ex)
    {
      UIHelper.MostrarMensaje($"Error: {ex.Message}", "Error", MessageBoxIcon.Error);
         }
        }

   private void btnEvaluaciones_Click(object sender, EventArgs e)
        {
            try
            {
       using (var frmFiltros = new frmFiltrosReporte("Evaluaciones"))
     {
                    if (frmFiltros.ShowDialog() == DialogResult.OK)
 {
               var fechaInicio = frmFiltros.FechaInicio;
         var fechaFin = frmFiltros.FechaFin;
   var evaluaciones = _evaluacionBLL.ObtenerTodas()
    .Where(ev => ev.Fecha >= fechaInicio && ev.Fecha <= fechaFin)
        .ToList();
           ExportarExcel(evaluaciones.Select(ev => new
  {
                Empleado = ev.NombreEmpleado,
           Fecha = ev.Fecha.ToString("dd/MM/yyyy"),
        ev.Puntaje,
     Fortalezas = ev.Fortalezas ?? "",
        OportunidadesMejora = ev.OportunidadesMejora ?? "",
            Comentarios = ev.Comentarios ?? "",
   FechaCreacion = ev.FechaCreacion.ToString("dd/MM/yyyy HH:mm")
     }).ToList(), $"Evaluaciones_{fechaInicio:yyyyMMdd}_{fechaFin:yyyyMMdd}");
   }
        }
    }
            catch (Exception ex)
      {
  UIHelper.MostrarMensaje($"Error: {ex.Message}", "Error", MessageBoxIcon.Error);
     }
    }

        private void btnNomina_Click(object sender, EventArgs e)
        {
   try
  {
 using (var frmPeriodo = new frmFiltroPeriodo())
        {
      if (frmPeriodo.ShowDialog() == DialogResult.OK)
         {
         var periodo = frmPeriodo.Periodo;
                var nominas = _nominaBLL.ObtenerPorPeriodo(periodo);
     ExportarExcel(nominas.Select(n => new
              {
    Empleado = n.NombreEmpleado,
             n.Periodo,
                n.SalarioBruto,
                n.Bonificaciones,
       n.Deducciones,
   n.SalarioNeto,
        n.Estado,
 FechaCreacion = n.FechaCreacion.ToString("dd/MM/yyyy HH:mm"),
   FechaPago = n.FechaPago?.ToString("dd/MM/yyyy") ?? ""
     }).ToList(), $"Nomina_{periodo.Replace(" ", "_")}");
        }
  }
       }
   catch (Exception ex)
          {
           UIHelper.MostrarMensaje($"Error: {ex.Message}", "Error", MessageBoxIcon.Error);
   }
        }

        private void ExportarExcel<T>(List<T> datos, string nombreArchivo)
        {
       ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var saveDialog = new SaveFileDialog())
        {
   saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
         saveDialog.FileName = $"{nombreArchivo}_{DateTime.Now:yyyyMMdd}";
           if (saveDialog.ShowDialog() == DialogResult.OK)
                {
           using (var package = new ExcelPackage())
      {
  var worksheet = package.Workbook.Worksheets.Add("Reporte");
           var properties = typeof(T).GetProperties();

              var nombresColumnas = new Dictionary<string, string>
    {
                { "DNI", "DNI" },
       { "Nombre", "Nombre" },
          { "Apellido", "Apellido" },
            { "Email", "Correo Electrónico" },
            { "Telefono", "Teléfono" },
   { "FechaNacimiento", "Fecha de Nacimiento" },
         { "Direccion", "Dirección" },
        { "Area", "Área" },
        { "Puesto", "Puesto" },
           { "TipoContrato", "Tipo de Contrato" },
              { "FechaContrato", "Fecha de Contrato" },
         { "SalarioBase", "Salario Base" },
           { "SistemaPension", "Sistema de Pensión" },
            { "Estado", "Estado" },
      { "FechaCreacion", "Fecha de Creación" },
 { "Empleado", "Empleado" },
       { "Fecha", "Fecha" },
            { "FechaInicio", "Fecha Inicio" },
      { "FechaFin", "Fecha Fin" },
        { "Entrada", "Hora Entrada" },
          { "Salida", "Hora Salida" },
       { "HorasTrabajadas", "Horas Trabajadas" },
     { "DiasTotales", "Días Totales" },
       { "FechaAprobacion", "Fecha de Aprobación" },
          { "Puntaje", "Puntaje" },
        { "Fortalezas", "Fortalezas" },
           { "OportunidadesMejora", "Oportunidades de Mejora" },
        { "Comentarios", "Comentarios" },
    { "Periodo", "Período" },
        { "SalarioBruto", "Salario Bruto" },
               { "Bonificaciones", "Bonificaciones" },
       { "Deducciones", "Deducciones" },
             { "SalarioNeto", "Salario Neto" },
                { "FechaPago", "Fecha de Pago" }
   };

            // Escribir encabezados
   for (int i = 0; i < properties.Length; i++)
      {
            var cell = worksheet.Cells[1, i + 1];
       var nombreColumna = properties[i].Name;
   cell.Value = nombresColumnas.ContainsKey(nombreColumna)
           ? nombresColumnas[nombreColumna]
     : nombreColumna;
             cell.Style.Font.Bold = true;
            cell.Style.Font.Size = 11;
       cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
      cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(52, 152, 219));
      cell.Style.Font.Color.SetColor(Color.White);
        cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
      cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
      cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
    }

 // Escribir datos
     for (int row = 0; row < datos.Count; row++)
{
         var item = datos[row];
    for (int col = 0; col < properties.Length; col++)
         {
             var cell = worksheet.Cells[row + 2, col + 1];
     var value = properties[col].GetValue(item);

             if (value != null)
            {
      if (value is decimal || value is double || value is float)
          {
      cell.Value = Convert.ToDouble(value);
        cell.Style.Numberformat.Format = "#,##0.00";
  }
 else if (value is DateTime)
            {
   cell.Value = (DateTime)value;
         cell.Style.Numberformat.Format = "dd/MM/yyyy";
    }
      else if (value is TimeSpan)
          {
   cell.Value = ((TimeSpan)value).ToString(@"hh\:mm");
     }
              else
         {
            cell.Value = value?.ToString() ?? "";
 }
          }

 cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
     cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
         cell.Style.WrapText = true;

          if (row % 2 == 0)
            {
        cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
 cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(245, 245, 245));
  }
        }
   }

            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

     for (int i = 1; i <= properties.Length; i++)
             {
        if (worksheet.Column(i).Width < 12)
    worksheet.Column(i).Width = 12;
  if (worksheet.Column(i).Width > 60)
 worksheet.Column(i).Width = 60;
             }

             worksheet.Cells[1, 1, datos.Count + 1, properties.Length].AutoFilter = true;
  worksheet.Row(1).Height = 25;
    worksheet.View.FreezePanes(2, 1);

       package.SaveAs(new FileInfo(saveDialog.FileName));
  }

         UIHelper.MostrarMensaje("Reporte exportado exitosamente a Excel.", "Éxito", MessageBoxIcon.Information);
           }
       }
        }
  }
}
