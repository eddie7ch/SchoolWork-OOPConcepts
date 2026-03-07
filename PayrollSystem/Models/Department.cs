namespace PayrollSystem.Models;

/// <summary>
/// Represents an organizational department.
/// Aggregates employees; employees are associated via Employee.Department.
/// </summary>
public class Department
{
    private readonly List<Employee> _employees = new();

    public string DepartmentId   { get; }
    public string DepartmentName { get; private set; }

    /// <summary>Read-only view of the department's employees.</summary>
    public IReadOnlyList<Employee> Employees => _employees.AsReadOnly();

    public Department(string departmentId, string departmentName)
    {
        DepartmentId   = departmentId;
        DepartmentName = departmentName;
    }

    public void AddEmployee(Employee employee)
    {
        if (!_employees.Contains(employee))
        {
            _employees.Add(employee);
            employee.Department = this;
        }
    }

    public void RemoveEmployee(Employee employee)
    {
        if (_employees.Remove(employee))
            employee.Department = null;
    }

    public int GetHeadCount() => _employees.Count;

    public override string ToString() => $"[{DepartmentId}] {DepartmentName} ({_employees.Count} employees)";
}
