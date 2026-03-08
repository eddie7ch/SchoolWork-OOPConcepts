using PayrollSystem.Models;

namespace PayrollSystem.Models
{
    /// <summary>
    /// Represents a company department that groups employees together.
    /// </summary>
    public class Department
    {
        private static int _deptCounter = 100;

        public string DepartmentId { get; private set; }
        public string Name { get; set; }
        public Employee? Manager { get; set; }
        private List<Employee> _employees = new List<Employee>();

        public IReadOnlyList<Employee> Employees => _employees.AsReadOnly();

        public Department(string name)
        {
            DepartmentId = $"DEPT-{++_deptCounter}";
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Department name cannot be empty.");
        }

        public void AddEmployee(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            if (_employees.Contains(employee)) return;
            employee.Department = this;
            _employees.Add(employee);
        }

        public bool RemoveEmployee(string employeeId)
        {
            var emp = _employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (emp == null) return false;
            emp.Department = null;
            _employees.Remove(emp);
            return true;
        }

        public int GetEmployeeCount() => _employees.Count;

        public override string ToString() =>
            $"[{DepartmentId}] {Name} | Employees: {_employees.Count} | Manager: {Manager?.GetFullName() ?? "None"}";
    }
}
