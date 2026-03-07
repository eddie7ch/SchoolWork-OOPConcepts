namespace PayrollSystem;

/// <summary>
/// Contract that any deduction must satisfy.
/// Follows the Dependency Inversion Principle: high-level modules (Employee,
/// PayrollProcessor) depend on this abstraction, not on concrete deduction types.
/// </summary>
public interface IDeduction
{
    /// <summary>Human-readable label shown on the pay stub.</summary>
    string Name { get; }

    /// <summary>
    /// Calculates the deduction amount for the given gross pay.
    /// </summary>
    /// <param name="grossPay">The employee's gross pay for the period.</param>
    /// <returns>Dollar amount to deduct.</returns>
    decimal Calculate(decimal grossPay);
}
