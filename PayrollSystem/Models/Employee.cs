namespace PayrollSystem.Models;

/// <summary>
/// Abstract base class representing any employee in the payroll system.
/// Encapsulates shared identity data and a collection of deductions.
/// Concrete subclasses must implement CalculateGrossPay().
/// </summary>
public abstract class Employee
{
    // Private backing field — only this class can add/remove deductions (encapsulation)
    private readonly List<IDeduction> _deductions = new();

    public string EmployeeId   { get; }
    public string FirstName    { get; protected set; }
    public string LastName     { get; protected set; }
    public DateTime DateOfBirth { get; protected set; }
    public DateTime HireDate   { get; protected set; }
    public string Email        { get; protected set; }
    public string Phone        { get; protected set; }

    /// <summary>The department this employee belongs to (association).</summary>
    public Department? Department { get; set; }

    /// <summary>Derived convenience property — not stored separately.</summary>
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>Calculated from HireDate; no redundant storage.</summary>
    public int YearsOfService =>
        (int)((DateTime.Today - HireDate).TotalDays / 365.25);

    protected Employee(
        string employeeId,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        DateTime hireDate,
        string email,
        string phone)
    {
        EmployeeId   = employeeId;
        FirstName    = firstName;
        LastName     = lastName;
        DateOfBirth  = dateOfBirth;
        HireDate     = hireDate;
        Email        = email;
        Phone        = phone;
    }

    // ── Abstract method — each subclass provides its own pay calculation ──

    /// <summary>
    /// Returns the gross pay for the current pay period.
    /// Polymorphic: each concrete type calculates differently.
    /// </summary>
    public abstract decimal CalculateGrossPay();

    // ── Deduction management (encapsulation) ──

    public void AddDeduction(IDeduction deduction)    => _deductions.Add(deduction);
    public void RemoveDeduction(IDeduction deduction) => _deductions.Remove(deduction);

    /// <summary>Read-only view; callers cannot mutate the internal list.</summary>
    public IReadOnlyList<IDeduction> GetDeductions() => _deductions.AsReadOnly();

    public decimal CalculateTotalDeductions()
    {
        decimal gross = CalculateGrossPay();
        return _deductions.Sum(d => d.Calculate(gross));
    }

    public decimal CalculateNetPay() =>
        CalculateGrossPay() - CalculateTotalDeductions();

    public override string ToString() => $"[{EmployeeId}] {FullName}";
}
