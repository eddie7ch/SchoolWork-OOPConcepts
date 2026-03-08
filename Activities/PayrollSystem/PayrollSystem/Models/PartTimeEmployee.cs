using PayrollSystem.Enums;

namespace PayrollSystem.Models
{
    /// <summary>
    /// Represents an hourly part-time employee.
    /// Gross pay is calculated from hourly rate multiplied by hours worked.
    /// </summary>
    public class PartTimeEmployee : Employee
    {
        private decimal _hourlyRate;
        private double _hoursWorked;

        public decimal HourlyRate
        {
            get => _hourlyRate;
            set => _hourlyRate = value > 0 ? value : throw new ArgumentException("Hourly rate must be positive.");
        }

        public double HoursWorked
        {
            get => _hoursWorked;
            set => _hoursWorked = value >= 0 ? value : throw new ArgumentException("Hours worked cannot be negative.");
        }

        public PartTimeEmployee(string firstName, string lastName, string position, decimal hourlyRate)
            : base(firstName, lastName, position)
        {
            HourlyRate = hourlyRate;
            EmployeeType = EmployeeType.PartTime;
        }

        /// <summary>
        /// Gross pay = HourlyRate * HoursWorked.
        /// </summary>
        public override decimal CalculateGrossPay() => Math.Round(HourlyRate * (decimal)HoursWorked, 2);

        public override string ToString() =>
            $"{base.ToString()} | Rate: {HourlyRate:C}/hr | Hours: {HoursWorked}";
    }
}
