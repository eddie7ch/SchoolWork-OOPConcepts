using PayrollSystem.Enums;

namespace PayrollSystem.Payroll
{
    /// <summary>
    /// Represents a tax deduction (Federal, Provincial, CPP, EI).
    /// Inherits from Deduction and applies a percentage rate to gross pay.
    /// </summary>
    public class TaxDeduction : Deduction
    {
        private decimal _taxRate;

        public TaxType TaxType { get; set; }

        public decimal TaxRate
        {
            get => _taxRate;
            set => _taxRate = (value >= 0 && value <= 1)
                ? value
                : throw new ArgumentException("Tax rate must be between 0 and 1 (e.g. 0.15 for 15%).");
        }

        public TaxDeduction(TaxType taxType, decimal taxRate)
            : base($"{taxType} Tax")
        {
            TaxType = taxType;
            TaxRate = taxRate;
        }

        /// <summary>
        /// Deduction = GrossPay * TaxRate
        /// </summary>
        public override decimal CalculateDeduction(decimal grossPay) =>
            Math.Round(grossPay * TaxRate, 2);

        public override string ToString() =>
            $"{base.ToString()} | {TaxType} @ {TaxRate:P2}";
    }
}
