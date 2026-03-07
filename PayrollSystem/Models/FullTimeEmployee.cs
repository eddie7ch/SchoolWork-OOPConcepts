namespace PayrollSystem.Models;

/// <summary>
/// Salaried, full-time employee paid bi-weekly (AnnualSalary / 26 pay periods).
/// May hold a Benefits package describing insurance and retirement elections.
/// </summary>
public class FullTimeEmployee : Employee
{
    public decimal  AnnualSalary    { get; private set; }
    public Benefits? BenefitsPackage { get; set; }

    public FullTimeEmployee(
        string   employeeId,
        string   firstName,
        string   lastName,
        DateTime dateOfBirth,
        DateTime hireDate,
        string   email,
        string   phone,
        decimal  annualSalary)
        : base(employeeId, firstName, lastName, dateOfBirth, hireDate, email, phone)
    {
        if (annualSalary < 0)
            throw new ArgumentOutOfRangeException(nameof(annualSalary), "Salary cannot be negative.");

        AnnualSalary = annualSalary;
    }

    /// <summary>Bi-weekly gross pay = annual salary divided by 26 pay periods.</summary>
    public override decimal CalculateGrossPay() => Math.Round(AnnualSalary / 26m, 2);
}
