using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using RecursosHumanos.Domain.Models;

namespace RecursosHumanos.Common.Helpers
{
    // helper para estilos de la aplicacion
    public static class UIHelper
    {
        // Colores del sistema
        public static readonly Color ColorFondo = Color.WhiteSmoke;
        public static readonly Color ColorSidebar = Color.FromArgb(44, 62, 80);
        public static readonly Color ColorSidebarHover = Color.FromArgb(52, 73, 94);
        public static readonly Color ColorHeader = Color.FromArgb(236, 240, 241);
        public static readonly Color ColorAcento = Color.FromArgb(52, 152, 219);
        public static readonly Color ColorAcentoOscuro = Color.FromArgb(41, 128, 185);
        public static readonly Color ColorTextoOscuro = Color.FromArgb(44, 62, 80);
        public static readonly Color ColorTextoClaro = Color.White;
        public static readonly Color ColorExito = Color.FromArgb(46, 204, 113);
        public static readonly Color ColorAdvertencia = Color.FromArgb(241, 196, 15);
        public static readonly Color ColorError = Color.FromArgb(231, 76, 60);
        public static readonly Color ColorInfo = Color.FromArgb(52, 152, 219);
        public static readonly Color ColorTextoGris = Color.FromArgb(127, 140, 141);
        public static readonly Color ColorBorde = Color.FromArgb(189, 195, 199);

        // Fuentes del sistema
        public static readonly Font FuentePrincipal = new Font("Segoe UI", 9F, FontStyle.Regular);
        public static readonly Font FuenteTitulo = new Font("Segoe UI", 12F, FontStyle.Bold);
        public static readonly Font FuenteTituloFormulario = new Font("Segoe UI", 18F, FontStyle.Bold); // Título principal de formularios
        public static readonly Font FuenteTituloGrande = new Font("Segoe UI", 16F, FontStyle.Bold);
        public static readonly Font FuenteSubtitulo = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static readonly Font FuenteKPINumero = new Font("Segoe UI", 36F, FontStyle.Bold);
        public static readonly Font FuenteKPITexto = new Font("Segoe UI", 9F, FontStyle.Regular);

        // Tamaños estándar
        public static readonly Size TamañoBotonPrimario = new Size(150, 35);
        public static readonly Size TamañoBotonSecundario = new Size(120, 35);
        public static readonly Size TamañoPanelKPI = new Size(228, 173);
        public static readonly int EspaciadoEstándar = 20;
        public static readonly int EspaciadoPequeño = 10;

        public static void AplicarEstiloFormulario(Form form)
        {
            form.BackColor = ColorFondo;
            form.Font = FuentePrincipal;
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.StartPosition = FormStartPosition.CenterScreen;
        }

        public static void AplicarEstiloBoton(Button button, bool esPrimario = true)
        {
            button.Font = FuentePrincipal;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            
            if (esPrimario)
            {
                button.BackColor = ColorAcento;
                button.ForeColor = ColorTextoClaro;
                button.FlatAppearance.MouseOverBackColor = ColorAcentoOscuro;
            }
            else
            {
                button.BackColor = Color.FromArgb(189, 195, 199);
                button.ForeColor = ColorTextoOscuro;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(149, 165, 166);
            }
        }

        // aplica estilo a botones de accion
        public static void AplicarEstiloBotonAccion(Button button, TipoBotonAccion tipo)
        {
            button.Font = FuentePrincipal;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            button.ForeColor = ColorTextoClaro;

            switch (tipo)
            {
                case TipoBotonAccion.Exito:
                    button.BackColor = ColorExito;
                    button.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 174, 96);
                    break;
                case TipoBotonAccion.Error:
                    button.BackColor = ColorError;
                    button.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 57, 43);
                    break;
                case TipoBotonAccion.Advertencia:
                    button.BackColor = ColorAdvertencia;
                    button.FlatAppearance.MouseOverBackColor = Color.FromArgb(243, 156, 18);
                    break;
            }
        }

        public static void AplicarEstiloTextBox(TextBox textBox)
        {
            textBox.Font = FuentePrincipal;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = Color.White;
            textBox.ForeColor = ColorTextoOscuro;
        }

        public static void AplicarEstiloComboBox(ComboBox comboBox)
        {
            comboBox.Font = FuentePrincipal;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.BackColor = Color.White;
            comboBox.ForeColor = ColorTextoOscuro;
        }

        public static void AplicarEstiloDataGridView(DataGridView dgv)
        {
            dgv.Font = FuentePrincipal;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;
            
            // colores de seleccion
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgv.DefaultCellStyle.SelectionForeColor = ColorTextoOscuro;
            
            // filas alternas
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 248);
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = ColorTextoOscuro;
            
            // headers
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorHeader;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = ColorTextoOscuro;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
        }

        public static void AplicarEstiloTitulo(Label label)
        {
            label.Font = FuenteTitulo;
            label.ForeColor = ColorTextoOscuro;
            label.AutoSize = true;
        }

        public static void AplicarEstiloTituloFormulario(Label label)
        {
            label.Font = FuenteTituloFormulario;
            label.ForeColor = ColorTextoOscuro;
            label.AutoSize = true;
        }

        public static void AplicarEstiloSubtitulo(Label label)
        {
            label.Font = FuenteSubtitulo;
            label.ForeColor = ColorTextoGris;
            label.AutoSize = true;
        }

        public static void AplicarEstiloGroupBox(GroupBox groupBox)
        {
            groupBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox.ForeColor = ColorTextoOscuro;
        }

        public static Panel CrearPanelKPI(string texto, string valor, Color colorIcono)
        {
            var panel = new Panel
            {
                Size = TamañoPanelKPI,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(17, 20, 17, 20)
            };

            var lblValor = new Label
            {
                Text = valor,
                Font = FuenteKPINumero,
                ForeColor = ColorTextoOscuro,
                Location = new Point(17, 70),
                Size = new Size(194, 60),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblTexto = new Label
            {
                Text = texto,
                Font = FuenteKPITexto,
                ForeColor = ColorTextoGris,
                Location = new Point(17, 130),
                Size = new Size(194, 23)
            };

            panel.Controls.Add(lblValor);
            panel.Controls.Add(lblTexto);

            return panel;
        }

        public static void MostrarMensaje(string mensaje, string titulo = "Información", MessageBoxIcon icono = MessageBoxIcon.Information)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, icono);
        }

        /// <summary>
        /// Muestra un mensaje de confirmación
        /// </summary>
        public static DialogResult MostrarConfirmacion(string mensaje, string titulo = "Confirmar")
        {
            return MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static bool ValidarCampoRequerido(TextBox textBox, ErrorProvider errorProvider, string mensajeError = "Este campo es obligatorio")
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                errorProvider.SetError(textBox, mensajeError);
                return false;
            }
            errorProvider.SetError(textBox, "");
            return true;
        }

        public static bool ValidarCampoRequerido(ComboBox comboBox, ErrorProvider errorProvider, string mensajeError = "Debe seleccionar una opción")
        {
            if (comboBox.SelectedIndex < 0 || comboBox.SelectedValue == null)
            {
                errorProvider.SetError(comboBox, mensajeError);
                return false;
            }
            errorProvider.SetError(comboBox, "");
            return true;
        }

        public static bool ValidarEmail(TextBox textBox, ErrorProvider errorProvider)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                errorProvider.SetError(textBox, "El email es obligatorio");
                return false;
            }

            try
            {
                var regex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!regex.IsMatch(textBox.Text))
                {
                    errorProvider.SetError(textBox, "El formato del email no es válido");
                    return false;
                }
            }
            catch
            {
                errorProvider.SetError(textBox, "Error al validar el email");
                return false;
            }

            errorProvider.SetError(textBox, "");
            return true;
        }

        public static void LimpiarErrores(ErrorProvider errorProvider)
        {
            errorProvider.Clear();
        }

        // valida dni 8 digitos
        public static bool ValidarDNI(TextBox textBox, ErrorProvider errorProvider)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                errorProvider.SetError(textBox, "El DNI es obligatorio");
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox.Text, @"^\d{8}$"))
            {
                errorProvider.SetError(textBox, "El DNI debe tener 8 dígitos");
                return false;
            }

            errorProvider.SetError(textBox, "");
            return true;
        }

        public static bool ValidarTelefono(TextBox textBox, ErrorProvider errorProvider)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                errorProvider.SetError(textBox, "El teléfono es obligatorio");
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox.Text, @"^\d{9,15}$"))
            {
                errorProvider.SetError(textBox, "El teléfono debe tener entre 9 y 15 dígitos");
                return false;
            }

            errorProvider.SetError(textBox, "");
            return true;
        }

        public static bool ValidarRangoNumerico(NumericUpDown numericUpDown, ErrorProvider errorProvider, decimal min, decimal max, string mensajeError = "El valor está fuera del rango permitido")
        {
            if (numericUpDown.Value < min || numericUpDown.Value > max)
            {
                errorProvider.SetError(numericUpDown, mensajeError);
                return false;
            }

            errorProvider.SetError(numericUpDown, "");
            return true;
        }

        public static bool ValidarFechaNoFutura(DateTimePicker dateTimePicker, ErrorProvider errorProvider, string mensajeError = "La fecha no puede ser futura")
        {
            if (dateTimePicker.Value > DateTime.Now)
            {
                errorProvider.SetError(dateTimePicker, mensajeError);
                return false;
            }

            errorProvider.SetError(dateTimePicker, "");
            return true;
        }

        public static bool ValidarRangoFechas(DateTimePicker fechaInicio, DateTimePicker fechaFin, ErrorProvider errorProvider, string mensajeError = "La fecha de inicio debe ser anterior a la fecha de fin")
        {
            if (fechaInicio.Value > fechaFin.Value)
            {
                errorProvider.SetError(fechaFin, mensajeError);
                return false;
            }

            errorProvider.SetError(fechaFin, "");
            return true;
        }

        public static void AplicarEstiloLabelCampo(Label label)
        {
            label.Font = FuentePrincipal;
            label.ForeColor = ColorTextoOscuro;
            label.AutoSize = true;
        }

        public static Label CrearLabelCampo(string texto, Point ubicacion)
        {
            return new Label
            {
                Text = texto,
                Location = ubicacion,
                Size = new Size(100, 20),
                Font = FuentePrincipal,
                ForeColor = ColorTextoOscuro
            };
        }

        public static string FormatearMoneda(decimal valor)
        {
            return $"S/{valor:N2}";
        }

        public static string FormatearFecha(DateTime fecha)
        {
            return fecha.ToString("dd/MM/yyyy");
        }

        public static string FormatearFechaHora(DateTime fecha)
        {
            return fecha.ToString("dd/MM/yyyy HH:mm");
        }

        // configura autocompletado para empleados
        public static void ConfigurarAutocompletadoEmpleado(TextBox textBox, List<Empleado> empleados, Dictionary<string, int> diccionarioEmpleados)
        {
            textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            
            var autoCompleteSource = new AutoCompleteStringCollection();
            diccionarioEmpleados.Clear();
            
            foreach (var empleado in empleados)
            {
                string nombreCompleto = empleado.NombreCompleto;
                autoCompleteSource.Add(nombreCompleto);
                diccionarioEmpleados[nombreCompleto] = empleado.Id;
            }
            
            textBox.AutoCompleteCustomSource = autoCompleteSource;
        }
    }

    public enum TipoBotonAccion
    {
        Exito,
        Error,
        Advertencia
    }
}
