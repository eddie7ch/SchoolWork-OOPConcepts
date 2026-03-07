namespace PayrollSystem.Deductions;

/// <summary>Identifies the type of employee benefit.</summary>
public enum BenefitType
{
    Health,
    Dental,
    Vision,
    Retirement,
    LifeInsurance
}

/// <summary>
/// Fixed monthly-premium deduction split across the pay periods in a month.
/// For a bi-weekly payroll (26 periods/year) divide monthly premium by 2.1667
/// to spread it evenly; here we use the common convention of monthly ÷ 2.
/// </summary>
public class BenefitsDeduction : Deduction
{
    public BenefitType Type           { get; }
    public decimal     MonthlyPremium { get; }

    public BenefitsDeduction(BenefitType type, decimal monthlyPremium)
        : base($"{type} Insurance")
    {
        if (monthlyPremium < 0)
            throw new ArgumentOutOfRangeException(nameof(monthlyPremium), "Premium cannot be negative.");

        Type           = type;
        MonthlyPremium = monthlyPremium;
    }

    /// <summary>
    /// Per-period deduction: the monthly premium split across two pay periods.
    /// grossPay is accepted for interface compliance but not used — benefit premiums
    /// are fixed regardless of earnings.
    /// </summary>
    public override decimal Calculate(decimal grossPay) =>
        Math.Round(MonthlyPremium / 2m, 2);
}
