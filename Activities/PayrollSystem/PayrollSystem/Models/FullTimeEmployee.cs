using PayrollSystem.Enums;

namespace PayrollSystem.Models
{
    /// <summary>
    /// Represents a salaried full-time employee.
    /// Gross pay is the annual salary divided by the number of pay periods per year.
    /// </summary>
    public class FullTimeEmployee : Employee
    {
        private decimal _annualSalary;

        public decimal AnnualSalary
        {
            get => _annualSalary;
            set => _annualSalary = value > 0 ? value : throw new ArgumentException("Annual salary must be positive.");
        }

        public FullTimeEmployee(string firstName, string lastName, string position, decimal annualSalary)
            : base(firstName, lastName, position)
        {
            AnnualSalary = annualSalary;
            EmployeeType = EmployeeType.FullTime;
        }

        /// <summary>
        /// Gross pay = AnnualSalary / 26 (bi-weekly pay periods by default).
        /// </summary>
        public override decimal CalculateGrossPay() => Math.Round(AnnualSalary / 26, 2);

        public override string ToString() =>
            $"{base.ToString()} | Salary: {AnnualSalary:C}/yr";
    }
}
