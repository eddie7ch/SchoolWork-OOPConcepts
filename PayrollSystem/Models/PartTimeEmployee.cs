namespace PayrollSystem.Models;

/// <summary>
/// Hourly, part-time employee. Gross pay = HourlyRate × HoursWorked.
/// HoursWorked is updated each pay period before ProcessPayroll is called.
/// </summary>
public class PartTimeEmployee : Employee
{
    public decimal HourlyRate  { get; private set; }
    public double  HoursWorked { get; set; }

    public PartTimeEmployee(
        string   employeeId,
        string   firstName,
        string   lastName,
        DateTime dateOfBirth,
        DateTime hireDate,
        string   email,
        string   phone,
        decimal  hourlyRate)
        : base(employeeId, firstName, lastName, dateOfBirth, hireDate, email, phone)
    {
        if (hourlyRate < 0)
            throw new ArgumentOutOfRangeException(nameof(hourlyRate), "Hourly rate cannot be negative.");

        HourlyRate = hourlyRate;
    }

    /// <summary>Gross pay for the period based on hours logged.</summary>
    public override decimal CalculateGrossPay() =>
        Math.Round(HourlyRate * (decimal)HoursWorked, 2);
}
