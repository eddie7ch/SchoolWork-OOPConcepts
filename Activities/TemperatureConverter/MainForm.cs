using System.Drawing;
using System.Windows.Forms;

namespace TemperatureConverter;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    // ─── Button: Convert ────────────────────────────────────────────────────────
    private void btnConvert_Click(object sender, EventArgs e)
    {
        string input = txtFahrenheit.Text.Trim();

        if (double.TryParse(input, out double fahrenheit))
        {
            double celsius = (fahrenheit - 32.0) * 5.0 / 9.0;
            double kelvin  = celsius + 273.15;

            txtCelsius.Text = $"{celsius:F2}";
            txtKelvin.Text  = $"{kelvin:F2}";

            lblStatus.Text      = $"✓  {fahrenheit}°F  =  {celsius:F2}°C  =  {kelvin:F2} K";
            lblStatus.ForeColor = Color.FromArgb(46, 204, 113);
        }
        else
        {
            txtCelsius.Clear();
            txtKelvin.Clear();
            lblStatus.Text      = "⚠  Please enter a valid numeric temperature.";
            lblStatus.ForeColor = Color.FromArgb(231, 76, 60);
        }
    }

    // ─── Button: Clear ──────────────────────────────────────────────────────────
    private void btnClear_Click(object sender, EventArgs e)
    {
        txtFahrenheit.Clear();
        txtCelsius.Clear();
        txtKelvin.Clear();
        lblStatus.Text = string.Empty;
        txtFahrenheit.Focus();
    }

    // ─── Input validation (numeric only, allow '-' and '.') ─────────────────────
    private void txtFahrenheit_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
            && e.KeyChar != '.' && e.KeyChar != '-')
        {
            e.Handled = true;
            return;
        }

        // Prevent duplicate decimal points
        if (e.KeyChar == '.' && txtFahrenheit.Text.Contains('.'))
        {
            e.Handled = true;
            return;
        }

        // Only allow minus sign at position 0
        if (e.KeyChar == '-' && txtFahrenheit.SelectionStart != 0)
            e.Handled = true;
    }

    // ─── Allow pressing Enter to trigger Convert ────────────────────────────────
    private void txtFahrenheit_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            btnConvert_Click(sender, e);
            e.SuppressKeyPress = true;
        }
    }

    // ─── Hover effects ──────────────────────────────────────────────────────────
    private void btnConvert_MouseEnter(object sender, EventArgs e) =>
        btnConvert.BackColor = Color.FromArgb(192, 57, 43);

    private void btnConvert_MouseLeave(object sender, EventArgs e) =>
        btnConvert.BackColor = Color.FromArgb(231, 76, 60);

    private void btnClear_MouseEnter(object sender, EventArgs e) =>
        btnClear.BackColor = Color.FromArgb(100, 110, 111);

    private void btnClear_MouseLeave(object sender, EventArgs e) =>
        btnClear.BackColor = Color.FromArgb(127, 140, 141);
}
