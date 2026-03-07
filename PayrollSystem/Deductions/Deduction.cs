namespace PayrollSystem.Deductions;

/// <summary>
/// Abstract base implementing IDeduction.
/// Stores the Name; subclasses only need to implement Calculate().
/// </summary>
public abstract class Deduction : IDeduction
{
    public string Name { get; protected set; }

    protected Deduction(string name)
    {
        Name = name;
    }

    public abstract decimal Calculate(decimal grossPay);
}
