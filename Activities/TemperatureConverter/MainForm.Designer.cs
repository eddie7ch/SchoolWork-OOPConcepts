namespace TemperatureConverter
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlHeader      = new System.Windows.Forms.Panel();
            lblTitle       = new System.Windows.Forms.Label();
            lblSubtitle    = new System.Windows.Forms.Label();

            lblInputPrompt = new System.Windows.Forms.Label();
            txtFahrenheit  = new System.Windows.Forms.TextBox();
            lblFUnit       = new System.Windows.Forms.Label();

            btnConvert     = new System.Windows.Forms.Button();
            btnClear       = new System.Windows.Forms.Button();
            lblStatus      = new System.Windows.Forms.Label();

            pnlDivider        = new System.Windows.Forms.Panel();
            lblResultHeading  = new System.Windows.Forms.Label();

            lblCelsiusDesc = new System.Windows.Forms.Label();
            txtCelsius     = new System.Windows.Forms.TextBox();
            lblCUnit       = new System.Windows.Forms.Label();

            lblKelvinDesc  = new System.Windows.Forms.Label();
            txtKelvin      = new System.Windows.Forms.TextBox();
            lblKUnit       = new System.Windows.Forms.Label();

            lblFormula     = new System.Windows.Forms.Label();

            pnlHeader.SuspendLayout();
            SuspendLayout();

            // ── pnlHeader ────────────────────────────────────────────────────────
            pnlHeader.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Location  = new System.Drawing.Point(0, 0);
            pnlHeader.Size      = new System.Drawing.Size(460, 108);
            pnlHeader.TabIndex  = 0;

            // ── lblTitle ─────────────────────────────────────────────────────────
            lblTitle.AutoSize  = false;
            lblTitle.BackColor = System.Drawing.Color.Transparent;
            lblTitle.Font      = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTitle.ForeColor = System.Drawing.Color.White;
            lblTitle.Location  = new System.Drawing.Point(0, 12);
            lblTitle.Size      = new System.Drawing.Size(460, 50);
            lblTitle.Text      = "🌡  Temperature Converter";
            lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblTitle.TabIndex  = 0;

            // ── lblSubtitle ──────────────────────────────────────────────────────
            lblSubtitle.AutoSize  = false;
            lblSubtitle.BackColor = System.Drawing.Color.Transparent;
            lblSubtitle.Font      = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(255, 200, 195);
            lblSubtitle.Location  = new System.Drawing.Point(0, 65);
            lblSubtitle.Size      = new System.Drawing.Size(460, 30);
            lblSubtitle.Text      = "Convert between Fahrenheit, Celsius & Kelvin";
            lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblSubtitle.TabIndex  = 1;

            // ── lblInputPrompt ───────────────────────────────────────────────────
            lblInputPrompt.AutoSize  = false;
            lblInputPrompt.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblInputPrompt.ForeColor = System.Drawing.Color.White;
            lblInputPrompt.Location  = new System.Drawing.Point(30, 128);
            lblInputPrompt.Size      = new System.Drawing.Size(400, 27);
            lblInputPrompt.Text      = "Enter the temperature in °F:";
            lblInputPrompt.TabIndex  = 1;

            // ── txtFahrenheit ────────────────────────────────────────────────────
            txtFahrenheit.BackColor       = System.Drawing.Color.FromArgb(44, 62, 80);
            txtFahrenheit.BorderStyle     = System.Windows.Forms.BorderStyle.FixedSingle;
            txtFahrenheit.Font            = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtFahrenheit.ForeColor       = System.Drawing.Color.White;
            txtFahrenheit.Location        = new System.Drawing.Point(30, 159);
            txtFahrenheit.PlaceholderText = "e.g.  98.6";
            txtFahrenheit.Size            = new System.Drawing.Size(340, 38);
            txtFahrenheit.TabIndex        = 2;
            txtFahrenheit.KeyPress       += new System.Windows.Forms.KeyPressEventHandler(txtFahrenheit_KeyPress);
            txtFahrenheit.KeyDown        += new System.Windows.Forms.KeyEventHandler(txtFahrenheit_KeyDown);

            // ── lblFUnit ─────────────────────────────────────────────────────────
            lblFUnit.AutoSize  = false;
            lblFUnit.Font      = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblFUnit.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
            lblFUnit.Location  = new System.Drawing.Point(378, 159);
            lblFUnit.Size      = new System.Drawing.Size(52, 38);
            lblFUnit.Text      = "°F";
            lblFUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblFUnit.TabIndex  = 3;

            // ── btnConvert ───────────────────────────────────────────────────────
            btnConvert.BackColor                 = System.Drawing.Color.FromArgb(231, 76, 60);
            btnConvert.Cursor                    = System.Windows.Forms.Cursors.Hand;
            btnConvert.FlatAppearance.BorderSize = 0;
            btnConvert.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            btnConvert.Font                      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnConvert.ForeColor                 = System.Drawing.Color.White;
            btnConvert.Location                  = new System.Drawing.Point(30, 218);
            btnConvert.Size                      = new System.Drawing.Size(198, 46);
            btnConvert.TabIndex                  = 4;
            btnConvert.Text                      = "🔄  Convert";
            btnConvert.UseVisualStyleBackColor   = false;
            btnConvert.Click      += new System.EventHandler(btnConvert_Click);
            btnConvert.MouseEnter += new System.EventHandler(btnConvert_MouseEnter);
            btnConvert.MouseLeave += new System.EventHandler(btnConvert_MouseLeave);

            // ── btnClear ─────────────────────────────────────────────────────────
            btnClear.BackColor                 = System.Drawing.Color.FromArgb(127, 140, 141);
            btnClear.Cursor                    = System.Windows.Forms.Cursors.Hand;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle                 = System.Windows.Forms.FlatStyle.Flat;
            btnClear.Font                      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnClear.ForeColor                 = System.Drawing.Color.White;
            btnClear.Location                  = new System.Drawing.Point(242, 218);
            btnClear.Size                      = new System.Drawing.Size(128, 46);
            btnClear.TabIndex                  = 5;
            btnClear.Text                      = "✕  Clear";
            btnClear.UseVisualStyleBackColor   = false;
            btnClear.Click      += new System.EventHandler(btnClear_Click);
            btnClear.MouseEnter += new System.EventHandler(btnClear_MouseEnter);
            btnClear.MouseLeave += new System.EventHandler(btnClear_MouseLeave);

            // ── lblStatus ────────────────────────────────────────────────────────
            lblStatus.AutoSize  = false;
            lblStatus.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblStatus.ForeColor = System.Drawing.Color.FromArgb(149, 165, 166);
            lblStatus.Location  = new System.Drawing.Point(30, 275);
            lblStatus.Size      = new System.Drawing.Size(400, 22);
            lblStatus.Text      = "";
            lblStatus.TabIndex  = 6;

            // ── pnlDivider ───────────────────────────────────────────────────────
            pnlDivider.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            pnlDivider.Location  = new System.Drawing.Point(30, 305);
            pnlDivider.Size      = new System.Drawing.Size(400, 1);
            pnlDivider.TabIndex  = 7;

            // ── lblResultHeading ─────────────────────────────────────────────────
            lblResultHeading.AutoSize  = false;
            lblResultHeading.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblResultHeading.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);
            lblResultHeading.Location  = new System.Drawing.Point(30, 313);
            lblResultHeading.Size      = new System.Drawing.Size(400, 20);
            lblResultHeading.Text      = "RESULTS";
            lblResultHeading.TabIndex  = 8;

            // ── lblCelsiusDesc ───────────────────────────────────────────────────
            lblCelsiusDesc.AutoSize  = false;
            lblCelsiusDesc.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblCelsiusDesc.ForeColor = System.Drawing.Color.White;
            lblCelsiusDesc.Location  = new System.Drawing.Point(30, 340);
            lblCelsiusDesc.Size      = new System.Drawing.Size(400, 26);
            lblCelsiusDesc.Text      = "Temperature in °C:";
            lblCelsiusDesc.TabIndex  = 9;

            // ── txtCelsius ───────────────────────────────────────────────────────
            txtCelsius.BackColor   = System.Drawing.Color.FromArgb(39, 55, 70);
            txtCelsius.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtCelsius.Font        = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtCelsius.ForeColor   = System.Drawing.Color.FromArgb(46, 204, 113);
            txtCelsius.Location    = new System.Drawing.Point(30, 370);
            txtCelsius.ReadOnly    = true;
            txtCelsius.Size        = new System.Drawing.Size(340, 38);
            txtCelsius.TabIndex    = 10;
            txtCelsius.TabStop     = false;

            // ── lblCUnit ─────────────────────────────────────────────────────────
            lblCUnit.AutoSize  = false;
            lblCUnit.Font      = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblCUnit.ForeColor = System.Drawing.Color.FromArgb(46, 204, 113);
            lblCUnit.Location  = new System.Drawing.Point(378, 370);
            lblCUnit.Size      = new System.Drawing.Size(52, 38);
            lblCUnit.Text      = "°C";
            lblCUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblCUnit.TabIndex  = 11;

            // ── lblKelvinDesc ────────────────────────────────────────────────────
            lblKelvinDesc.AutoSize  = false;
            lblKelvinDesc.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblKelvinDesc.ForeColor = System.Drawing.Color.White;
            lblKelvinDesc.Location  = new System.Drawing.Point(30, 420);
            lblKelvinDesc.Size      = new System.Drawing.Size(400, 26);
            lblKelvinDesc.Text      = "Temperature in K:";
            lblKelvinDesc.TabIndex  = 12;

            // ── txtKelvin ────────────────────────────────────────────────────────
            txtKelvin.BackColor   = System.Drawing.Color.FromArgb(39, 55, 70);
            txtKelvin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtKelvin.Font        = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtKelvin.ForeColor   = System.Drawing.Color.FromArgb(52, 152, 219);
            txtKelvin.Location    = new System.Drawing.Point(30, 450);
            txtKelvin.ReadOnly    = true;
            txtKelvin.Size        = new System.Drawing.Size(340, 38);
            txtKelvin.TabIndex    = 13;
            txtKelvin.TabStop     = false;

            // ── lblKUnit ─────────────────────────────────────────────────────────
            lblKUnit.AutoSize  = false;
            lblKUnit.Font      = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblKUnit.ForeColor = System.Drawing.Color.FromArgb(52, 152, 219);
            lblKUnit.Location  = new System.Drawing.Point(378, 450);
            lblKUnit.Size      = new System.Drawing.Size(52, 38);
            lblKUnit.Text      = "K";
            lblKUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblKUnit.TabIndex  = 14;

            // ── lblFormula ───────────────────────────────────────────────────────
            lblFormula.AutoSize  = false;
            lblFormula.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            lblFormula.ForeColor = System.Drawing.Color.FromArgb(85, 99, 112);
            lblFormula.Location  = new System.Drawing.Point(30, 502);
            lblFormula.Size      = new System.Drawing.Size(400, 20);
            lblFormula.Text      = "°C = (°F − 32) × 5⁄9     |     K = °C + 273.15";
            lblFormula.TabIndex  = 15;

            // ── MainForm ─────────────────────────────────────────────────────────
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            BackColor           = System.Drawing.Color.FromArgb(30, 39, 46);
            ClientSize          = new System.Drawing.Size(460, 534);
            Controls.Add(pnlHeader);
            Controls.Add(lblInputPrompt);
            Controls.Add(txtFahrenheit);
            Controls.Add(lblFUnit);
            Controls.Add(btnConvert);
            Controls.Add(btnClear);
            Controls.Add(lblStatus);
            Controls.Add(pnlDivider);
            Controls.Add(lblResultHeading);
            Controls.Add(lblCelsiusDesc);
            Controls.Add(txtCelsius);
            Controls.Add(lblCUnit);
            Controls.Add(lblKelvinDesc);
            Controls.Add(txtKelvin);
            Controls.Add(lblKUnit);
            Controls.Add(lblFormula);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox     = false;
            Name            = "MainForm";
            StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text            = "Temperature Converter";

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel  pnlHeader;
        private System.Windows.Forms.Label  lblTitle;
        private System.Windows.Forms.Label  lblSubtitle;
        private System.Windows.Forms.Label  lblInputPrompt;
        private System.Windows.Forms.TextBox txtFahrenheit;
        private System.Windows.Forms.Label  lblFUnit;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label  lblStatus;
        private System.Windows.Forms.Panel  pnlDivider;
        private System.Windows.Forms.Label  lblResultHeading;
        private System.Windows.Forms.Label  lblCelsiusDesc;
        private System.Windows.Forms.TextBox txtCelsius;
        private System.Windows.Forms.Label  lblCUnit;
        private System.Windows.Forms.Label  lblKelvinDesc;
        private System.Windows.Forms.TextBox txtKelvin;
        private System.Windows.Forms.Label  lblKUnit;
        private System.Windows.Forms.Label  lblFormula;
    }
}
