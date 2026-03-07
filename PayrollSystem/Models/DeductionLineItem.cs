namespace PayrollSystem.Models;

/// <summary>
/// A single deduction line on a pay stub (name + dollar amount).
/// These objects are created by PayStub and exist only within it — composition.
/// </summary>
public class DeductionLineItem
{
    public string  Name   { get; }
    public decimal Amount { get; }

    public DeductionLineItem(string name, decimal amount)
    {
        Name   = name;
        Amount = amount;
    }
}
