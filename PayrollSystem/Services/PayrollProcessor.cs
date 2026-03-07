using PayrollSystem.Models;

namespace PayrollSystem.Services;

/// <summary>
/// Orchestrates the payroll run for a roster of employees.
/// For each employee it calls CalculateGrossPay() (polymorphic),
/// then snapshots the results into an immutable PayStub.
/// </summary>
public class PayrollProcessor
{
    private readonly List<Employee> _employees = new();

    // ── Roster management ──────────────────────────────────────────────────

    public void AddEmployee(Employee employee)
    {
        if (!_employees.Contains(employee))
            _employees.Add(employee);
    }

    public void RemoveEmployee(Employee employee) => _employees.Remove(employee);

    public IReadOnlyList<Employee> GetAllEmployees() => _employees.AsReadOnly();

    // ── Pay processing ─────────────────────────────────────────────────────

    /// <summary>
    /// Processes payroll for a single employee and returns their PayStub.
    /// For ContractEmployee, skips processing if the contract has expired.
    /// </summary>
    public PayStub? ProcessEmployee(
        Employee employee,
        DateTime payDate,
        DateTime periodStart,
        DateTime periodEnd)
    {
        // Guard: skip expired contracts
        if (employee is ContractEmployee ce && !ce.IsContractActive())
        {
            Console.WriteLine($"  Skipping {employee.FullName}: contract expired on {ce.ContractEndDate:MM/dd/yyyy}.");
            return null;
        }

        return new PayStub(employee, payDate, periodStart, periodEnd);
    }

    /// <summary>
    /// Runs payroll for all employees and returns the complete list of pay stubs.
    /// Null stubs (e.g. expired contracts) are filtered out.
    /// </summary>
    public List<PayStub> ProcessPayroll(
        DateTime payDate,
        DateTime periodStart,
        DateTime periodEnd)
    {
        var stubs = new List<PayStub>();

        foreach (var employee in _employees)
        {
            var stub = ProcessEmployee(employee, payDate, periodStart, periodEnd);
            if (stub is not null)
                stubs.Add(stub);
        }

        return stubs;
    }
}
