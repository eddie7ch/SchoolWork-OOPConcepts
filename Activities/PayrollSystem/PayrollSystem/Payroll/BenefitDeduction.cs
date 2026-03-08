using PayrollSystem.Enums;

namespace PayrollSystem.Payroll
{
    /// <summary>
    /// Represents a benefit deduction (Health, Dental, Pension, etc.).
    /// Tracks both the employee's and employer's contribution portions.
    /// </summary>
    public class BenefitDeduction : Deduction
    {
        private decimal _employeeContribution;

        public BenefitType BenefitType { get; set; }

        /// <summary>
        /// Fixed dollar amount deducted from the employee's pay per period.
        /// </summary>
        public decimal EmployeeContribution
        {
            get => _employeeContribution;
            set => _employeeContribution = value >= 0
                ? value
                : throw new ArgumentException("Employee contribution cannot be negative.");
        }

        /// <summary>
        /// Amount the employer contributes per period (for record-keeping).
        /// </summary>
        public decimal EmployerContribution { get; set; }

        public BenefitDeduction(BenefitType benefitType, decimal employeeContribution, decimal employerContribution = 0)
            : base($"{benefitType} Benefit")
        {
            BenefitType = benefitType;
            EmployeeContribution = employeeContribution;
            EmployerContribution = employerContribution;
        }

        /// <summary>
        /// Deduction = fixed EmployeeContribution (not percentage-based).
        /// grossPay is accepted to satisfy the base interface.
        /// </summary>
        public override decimal CalculateDeduction(decimal grossPay) => EmployeeContribution;

        public override string ToString() =>
            $"{base.ToString()} | {BenefitType} | Employee: {EmployeeContribution:C} | Employer: {EmployerContribution:C}";
    }
}
