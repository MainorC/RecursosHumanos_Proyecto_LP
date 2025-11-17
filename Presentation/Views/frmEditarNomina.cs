using RecursosHumanos.Business.Services;
using RecursosHumanos.Domain.Models;
using RecursosHumanos.Common.Helpers;

namespace RecursosHumanos.Presentation.Views
{
    public partial class frmEditarNomina : Form
    {
        private readonly NominaBLL _nominaBLL = new();
        private Nomina _nomina;
        private ErrorProvider _errorProvider = new ErrorProvider();

        public frmEditarNomina(Nomina nomina)
        {
            InitializeComponent();
            _nomina = nomina;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        private void frmEditarNomina_Load(object sender, EventArgs e)
        {
            lblEmpleado.Text = _nomina.NombreEmpleado ?? "Empleado";
            txtSalarioBruto.Text = _nomina.SalarioBruto.ToString("F2");
            txtBonificaciones.Text = _nomina.Bonificaciones.ToString("F2");
            txtDeducciones.Text = _nomina.Deducciones.ToString("F2");
            CalcularSalarioNeto();
        }

        private void CalcularSalarioNeto()
        {
            if (decimal.TryParse(txtSalarioBruto.Text, out decimal salarioBruto) &&
                decimal.TryParse(txtBonificaciones.Text, out decimal bonificaciones) &&
                decimal.TryParse(txtDeducciones.Text, out decimal deducciones))
            {
                decimal salarioNeto = salarioBruto + bonificaciones - deducciones;
                txtSalarioNeto.Text = salarioNeto.ToString("F2");
            }
        }

        private void txtSalarioBruto_TextChanged(object sender, EventArgs e)
        {
            CalcularSalarioNeto();
        }

        private void txtBonificaciones_TextChanged(object sender, EventArgs e)
        {
            CalcularSalarioNeto();
        }

        private void txtDeducciones_TextChanged(object sender, EventArgs e)
        {
            CalcularSalarioNeto();
        }

        private bool ValidarFormulario()
        {
            bool esValido = true;
            UIHelper.LimpiarErrores(_errorProvider);

            if (!decimal.TryParse(txtSalarioBruto.Text, out decimal salarioBruto) || salarioBruto < 0)
            {
                _errorProvider.SetError(txtSalarioBruto, "El salario bruto debe ser un número válido mayor o igual a 0");
                esValido = false;
            }

            if (!decimal.TryParse(txtBonificaciones.Text, out decimal bonificaciones) || bonificaciones < 0)
            {
                _errorProvider.SetError(txtBonificaciones, "Las bonificaciones deben ser un número válido mayor o igual a 0");
                esValido = false;
            }

            if (!decimal.TryParse(txtDeducciones.Text, out decimal deducciones) || deducciones < 0)
            {
                _errorProvider.SetError(txtDeducciones, "Las deducciones deben ser un número válido mayor o igual a 0");
                esValido = false;
            }

            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario())
                {
                    UIHelper.MostrarMensaje("Por favor, corrija los errores en el formulario.", "Validación", MessageBoxIcon.Warning);
                    return;
                }

                _nomina.SalarioBruto = decimal.Parse(txtSalarioBruto.Text);
                _nomina.Bonificaciones = decimal.Parse(txtBonificaciones.Text);
                _nomina.Deducciones = decimal.Parse(txtDeducciones.Text);
                _nomina.SalarioNeto = decimal.Parse(txtSalarioNeto.Text);

                if (_nominaBLL.Guardar(_nomina))
                {
                    UIHelper.MostrarMensaje("Nómina actualizada exitosamente.", "Éxito", MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    UIHelper.MostrarMensaje("Error al actualizar la nómina.", "Error", MessageBoxIcon.Error);
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
    }
}

