using RecursosHumanos.Common.Helpers;

namespace RecursosHumanos.Presentation.Views
{
    public partial class frmFiltrosReporte : Form
    {
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }

        public frmFiltrosReporte(string tipoReporte)
        {
            InitializeComponent();
            this.Text = $"Filtros - Reporte de {tipoReporte}";
            dtpFechaInicio.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                UIHelper.MostrarMensaje("La fecha de inicio no puede ser mayor que la fecha de fin.", "Validación", MessageBoxIcon.Warning);
                return;
            }

            FechaInicio = dtpFechaInicio.Value;
            FechaFin = dtpFechaFin.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

