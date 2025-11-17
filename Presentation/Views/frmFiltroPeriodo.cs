using RecursosHumanos.Common.Helpers;

namespace RecursosHumanos.Presentation.Views
{
    public partial class frmFiltroPeriodo : Form
    {
        public string Periodo { get; private set; } = string.Empty;

        public frmFiltroPeriodo()
        {
            InitializeComponent();
            dtpPeriodo.Value = DateTime.Now;
            dtpPeriodo.Format = DateTimePickerFormat.Custom;
            dtpPeriodo.CustomFormat = "MMMM yyyy";
            dtpPeriodo.ShowUpDown = true;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            Periodo = dtpPeriodo.Value.ToString("MMMM yyyy");
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

