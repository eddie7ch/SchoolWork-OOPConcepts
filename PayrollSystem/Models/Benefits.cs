namespace PayrollSystem.Models;

/// <summary>
/// Describes employer-sponsored benefit elections for a full-time employee.
/// Tracks which benefits are elected and calculates both employee and employer
/// contributions to retirement savings.
/// </summary>
public class Benefits
{
    public bool    HasHealthInsurance         { get; set; }
    public bool    HasDentalInsurance         { get; set; }
    public bool    HasVisionInsurance         { get; set; }

    /// <summary>Employee's retirement contribution as a fraction of gross pay (e.g. 0.05 = 5%).</summary>
    public decimal RetirementContributionRate { get; set; }

    /// <summary>Employer match as a fraction of gross pay (e.g. 0.03 = 3% match).</summary>
    public decimal EmployerMatchRate          { get; set; }

    /// <summary>Per-period employee retirement contribution.</summary>
    public decimal CalculateEmployeeContribution(decimal grossPay) =>
        Math.Round(grossPay * RetirementContributionRate, 2);

    /// <summary>Per-period employer retirement match (capped at EmployerMatchRate).</summary>
    public decimal CalculateEmployerContribution(decimal grossPay) =>
        Math.Round(grossPay * Math.Min(RetirementContributionRate, EmployerMatchRate), 2);
}
