namespace PayrollSystem.Deductions;

/// <summary>Identifies the type of tax being withheld.</summary>
public enum TaxType
{
    Federal,
    State,
    Local,
    SocialSecurity,
    Medicare
}

/// <summary>
/// Percentage-based tax deduction (e.g. 22% federal withholding).
/// Amount = grossPay × Rate.
/// </summary>
public class TaxDeduction : Deduction
{
    public TaxType Type { get; }
    public decimal Rate { get; }

    public TaxDeduction(TaxType type, decimal rate)
        : base($"{type} Tax")
    {
        if (rate is < 0 or > 1)
            throw new ArgumentOutOfRangeException(nameof(rate), "Rate must be between 0 and 1.");

        Type = type;
        Rate = rate;
    }

    /// <summary>Withholds a flat percentage of gross pay.</summary>
    public override decimal Calculate(decimal grossPay) =>
        Math.Round(grossPay * Rate, 2);
}
