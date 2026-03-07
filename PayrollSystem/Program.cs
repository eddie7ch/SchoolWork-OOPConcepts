// ============================================================
//  Payroll System — Demo Entry Point
//  Demonstrates all OOP principles via a sample payroll run.
// ============================================================

using PayrollSystem.Deductions;
using PayrollSystem.Models;
using PayrollSystem.Services;

Console.WriteLine("╔══════════════════════════════════════════════╗");
Console.WriteLine("║         PAYROLL SYSTEM DEMONSTRATION         ║");
Console.WriteLine("╚══════════════════════════════════════════════╝");
Console.WriteLine();

// ── 1. Create departments ──────────────────────────────────────────────

var engineering = new Department("D01", "Engineering");
var hr          = new Department("D02", "Human Resources");
var operations  = new Department("D03", "Operations");

// ── 2. Create employees (polymorphism: three concrete Employee subtypes) ──

var alice = new FullTimeEmployee(
    employeeId:  "E001",
    firstName:   "Alice",
    lastName:    "Johnson",
    dateOfBirth: new DateTime(1985, 4, 15),
    hireDate:    new DateTime(2018, 6, 1),
    email:       "alice.johnson@company.com",
    phone:       "555-1001",
    annualSalary: 95_000m)
{
    BenefitsPackage = new Benefits
    {
        HasHealthInsurance         = true,
        HasDentalInsurance         = true,
        HasVisionInsurance         = false,
        RetirementContributionRate = 0.06m,  // 6 %
        EmployerMatchRate          = 0.04m   // 4 % match
    }
};

var bob = new PartTimeEmployee(
    employeeId:  "E002",
    firstName:   "Bob",
    lastName:    "Martinez",
    dateOfBirth: new DateTime(1995, 9, 22),
    hireDate:    new DateTime(2021, 3, 15),
    email:       "bob.martinez@company.com",
    phone:       "555-1002",
    hourlyRate:  22.50m);

bob.HoursWorked = 72; // hours in this bi-weekly period

var carol = new ContractEmployee(
    employeeId:    "E003",
    firstName:     "Carol",
    lastName:      "Lee",
    dateOfBirth:   new DateTime(1990, 12, 5),
    hireDate:      new DateTime(2024, 1, 10),
    email:         "carol.lee@company.com",
    phone:         "555-1003",
    contractRate:  4_500m,
    contractEndDate: new DateTime(2026, 12, 31));

var dave = new FullTimeEmployee(
    employeeId:   "E004",
    firstName:    "Dave",
    lastName:     "Kim",
    dateOfBirth:  new DateTime(1978, 7, 30),
    hireDate:     new DateTime(2015, 9, 1),
    email:        "dave.kim@company.com",
    phone:        "555-1004",
    annualSalary: 78_000m);

// ── 3. Assign employees to departments ────────────────────────────────

engineering.AddEmployee(alice);
engineering.AddEmployee(dave);
hr.AddEmployee(bob);
operations.AddEmployee(carol);

// ── 4. Attach deductions (Dependency Inversion: Employee depends on IDeduction) ──

// Standard deductions for full-time salaried employees
void AttachStandardDeductions(Employee emp)
{
    emp.AddDeduction(new TaxDeduction(TaxType.Federal,        0.22m));
    emp.AddDeduction(new TaxDeduction(TaxType.State,          0.05m));
    emp.AddDeduction(new TaxDeduction(TaxType.SocialSecurity, 0.062m));
    emp.AddDeduction(new TaxDeduction(TaxType.Medicare,       0.0145m));
}

// Alice — full-time with benefits
AttachStandardDeductions(alice);
alice.AddDeduction(new BenefitsDeduction(BenefitType.Health,      320m));
alice.AddDeduction(new BenefitsDeduction(BenefitType.Dental,       45m));
alice.AddDeduction(new BenefitsDeduction(BenefitType.Retirement,   // 6% of gross handled as fixed premium for demo
    Math.Round(alice.CalculateGrossPay() * 0.06m, 2)));

// Bob — part-time, fewer withholdings
bob.AddDeduction(new TaxDeduction(TaxType.Federal,        0.12m));
bob.AddDeduction(new TaxDeduction(TaxType.State,          0.04m));
bob.AddDeduction(new TaxDeduction(TaxType.SocialSecurity, 0.062m));
bob.AddDeduction(new TaxDeduction(TaxType.Medicare,       0.0145m));

// Carol — contractor; no benefits, only simple tax
carol.AddDeduction(new TaxDeduction(TaxType.Federal, 0.24m));
carol.AddDeduction(new TaxDeduction(TaxType.State,   0.05m));

// Dave — full-time, no benefit elections this period
AttachStandardDeductions(dave);

// ── 5. Run payroll ─────────────────────────────────────────────────────

var processor = new PayrollProcessor();
processor.AddEmployee(alice);
processor.AddEmployee(bob);
processor.AddEmployee(carol);
processor.AddEmployee(dave);

var payDate     = new DateTime(2026, 3, 14);
var periodStart = new DateTime(2026, 3, 1);
var periodEnd   = new DateTime(2026, 3, 14);

Console.WriteLine($"Running payroll for period: {periodStart:MM/dd/yyyy} – {periodEnd:MM/dd/yyyy}");
Console.WriteLine();

List<PayStub> stubs = processor.ProcessPayroll(payDate, periodStart, periodEnd);

// ── 6. Print individual pay stubs ──────────────────────────────────────

Console.WriteLine("─── Individual Pay Stubs ───────────────────────────────");
foreach (var stub in stubs)
    stub.Print();

// ── 7. Reports ─────────────────────────────────────────────────────────

var reporter = new ReportService();

reporter.GeneratePayrollSummary(stubs);

Console.WriteLine("─── Alice's Individual Report ──────────────────────────");
reporter.GenerateEmployeeReport(alice, stubs);

// Export to file
string exportPath = Path.Combine(AppContext.BaseDirectory, "payroll_summary.txt");
reporter.ExportSummaryToFile(exportPath, stubs);

// ── 8. Department info ─────────────────────────────────────────────────

Console.WriteLine();
Console.WriteLine("─── Departments ────────────────────────────────────────");
foreach (var dept in new[] { engineering, hr, operations })
    Console.WriteLine($"  {dept}");

Console.WriteLine();
Console.WriteLine("Payroll run complete.");
