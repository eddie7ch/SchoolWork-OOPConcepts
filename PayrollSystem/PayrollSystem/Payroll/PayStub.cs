using PayrollSystem.Models;

namespace PayrollSystem.Payroll
{
    /// <summary>
    /// Represents a pay stub issued to an employee for a specific pay period.
    /// Aggregates deductions and computes net pay.
    /// </summary>
    public class PayStub
    {
        private static int _stubCounter = 0;
        private List<Deduction> _deductions = new List<Deduction>();

        public string PayStubId { get; private set; }
        public Employee Employee { get; private set; }
        public PayPeriod PayPeriod { get; private set; }
        public decimal GrossPay { get; private set; }
        public DateTime IssuedDate { get; private set; }

        public IReadOnlyList<Deduction> Deductions => _deductions.AsReadOnly();

        public decimal TotalDeductions => _deductions.Sum(d => d.CalculateDeduction(GrossPay));

        public decimal NetPay => Math.Round(GrossPay - TotalDeductions, 2);

        public PayStub(Employee employee, PayPeriod period, decimal grossPay)
        {
            PayStubId = $"STUB-{++_stubCounter:D5}";
            Employee = employee ?? throw new ArgumentNullException(nameof(employee));
            PayPeriod = period ?? throw new ArgumentNullException(nameof(period));
            GrossPay = grossPay >= 0 ? grossPay : throw new ArgumentException("Gross pay cannot be negative.");
            IssuedDate = DateTime.Today;
        }

        public void AddDeduction(Deduction deduction)
        {
            if (deduction != null) _deductions.Add(deduction);
        }

        /// <summary>
        /// Prints a formatted pay stub to the console.
        /// </summary>
        public void Print()
        {
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"  PAY STUB  [{PayStubId}]");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"  Employee  : {Employee.GetFullName()} ({Employee.EmployeeId})");
            Console.WriteLine($"  Position  : {Employee.Position}");
            Console.WriteLine($"  Period    : {PayPeriod.GetPeriodDescription()}");
            Console.WriteLine($"  Issued    : {IssuedDate:yyyy-MM-dd}");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"  Gross Pay : {GrossPay,10:C}");
            Console.WriteLine();
            Console.WriteLine("  Deductions:");
            foreach (var d in _deductions)
                Console.WriteLine($"    {d.Name,-25} -{d.CalculateDeduction(GrossPay),8:C}");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"  Net Pay   : {NetPay,10:C}");
            Console.WriteLine(new string('=', 50));
        }

        public override string ToString() =>
            $"[{PayStubId}] {Employee.GetFullName()} | Gross: {GrossPay:C} | Net: {NetPay:C}";
    }
}
