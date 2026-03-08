using PayrollSystem.Enums;
using PayrollSystem.Payroll;

namespace PayrollSystem.Models
{
    /// <summary>
    /// Abstract class representing an employee. Inherits from Person.
    /// Forces subclasses to implement CalculateGrossPay() via abstract method.
    /// </summary>
    public abstract class Employee : Person
    {
        private static int _idCounter = 1000;

        public string EmployeeId { get; private set; }
        public DateTime HireDate { get; set; }
        public string Position { get; set; }
        public Department? Department { get; set; }
        public EmployeeType EmployeeType { get; protected set; }
        public List<BenefitDeduction> Benefits { get; private set; } = new List<BenefitDeduction>();

        protected Employee(string firstName, string lastName, string position)
            : base(firstName, lastName)
        {
            EmployeeId = $"EMP-{++_idCounter}";
            Position = position;
            HireDate = DateTime.Today;
        }

        /// <summary>
        /// Calculates gross pay for the pay period. Must be implemented by subclasses.
        /// </summary>
        public abstract decimal CalculateGrossPay();

        /// <summary>
        /// Calculates net pay by applying all deductions to gross pay.
        /// </summary>
        public decimal CalculateNetPay(List<TaxDeduction> taxes)
        {
            decimal gross = CalculateGrossPay();
            decimal totalDeductions = 0;

            foreach (var tax in taxes)
                totalDeductions += tax.CalculateDeduction(gross);

            foreach (var benefit in Benefits)
                totalDeductions += benefit.CalculateDeduction(gross);

            return gross - totalDeductions;
        }

        public void AddBenefit(BenefitDeduction benefit) => Benefits.Add(benefit);

        public PayStub GeneratePayStub(PayPeriod period, List<TaxDeduction> taxes)
        {
            decimal gross = CalculateGrossPay();
            var stub = new PayStub(this, period, gross);

            foreach (var tax in taxes)
                stub.AddDeduction(tax);

            foreach (var benefit in Benefits)
                stub.AddDeduction(benefit);

            return stub;
        }

        public override string ToString() =>
            $"[{EmployeeId}] {GetFullName()} | {Position} | {EmployeeType}";
    }
}
