namespace PayrollSystem.Payroll
{
    /// <summary>
    /// Abstract base class for all payroll deductions.
    /// Enforces a common interface via CalculateDeduction().
    /// </summary>
    public abstract class Deduction
    {
        private static int _idCounter = 0;

        public string DeductionId { get; private set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        protected Deduction(string name)
        {
            DeductionId = $"DED-{++_idCounter:D3}";
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Deduction name cannot be empty.");
        }

        /// <summary>
        /// Calculates the deduction amount based on gross pay.
        /// Must be implemented by each deduction subclass.
        /// </summary>
        public abstract decimal CalculateDeduction(decimal grossPay);

        public override string ToString() => $"[{DeductionId}] {Name}";
    }
}
