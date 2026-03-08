using PayrollSystem.Models;

namespace PayrollSystem.Payroll
{
    /// <summary>
    /// Orchestrates payroll processing for all registered employees.
    /// Applies company-wide taxes and generates pay stubs.
    /// </summary>
    public class PayrollProcessor
    {
        private List<Employee> _employees = new List<Employee>();
        private List<TaxDeduction> _companyTaxes = new List<TaxDeduction>();

        public IReadOnlyList<Employee> Employees => _employees.AsReadOnly();

        public void RegisterEmployee(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            if (!_employees.Contains(employee))
                _employees.Add(employee);
        }

        public bool RemoveEmployee(string employeeId)
        {
            var emp = _employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (emp == null) return false;
            _employees.Remove(emp);
            return true;
        }

        public void AddCompanyTax(TaxDeduction tax)
        {
            if (tax != null) _companyTaxes.Add(tax);
        }

        /// <summary>
        /// Processes payroll for the given period and returns a list of pay stubs.
        /// </summary>
        public List<PayStub> ProcessPayroll(PayPeriod period)
        {
            if (period == null) throw new ArgumentNullException(nameof(period));

            var stubs = new List<PayStub>();
            foreach (var employee in _employees)
            {
                var stub = employee.GeneratePayStub(period, _companyTaxes);
                stubs.Add(stub);
            }
            return stubs;
        }

        /// <summary>
        /// Generates a payroll report from a list of pay stubs.
        /// </summary>
        public PayrollReport GenerateReport(List<PayStub> stubs, Enums.ReportType reportType)
        {
            return new PayrollReport(stubs, reportType);
        }
    }
}
