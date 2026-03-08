using PayrollSystem.Enums;
using PayrollSystem.Models;
using PayrollSystem.Payroll;

// ─────────────────────────────────────────────
//  Payroll System — Demo Program
// ─────────────────────────────────────────────

Console.WriteLine("=== Payroll System Demo ===\n");

// 1. Create departments
var engineering = new Department("Engineering");
var marketing   = new Department("Marketing");

// 2. Create full-time employees
var alice = new FullTimeEmployee("Alice", "Johnson", "Software Engineer", 85_000m)
{
    DateOfBirth = new DateTime(1990, 4, 12),
    Email = "alice@company.com"
};

var bob = new FullTimeEmployee("Bob", "Smith", "Team Lead", 105_000m)
{
    DateOfBirth = new DateTime(1985, 7, 22),
    Email = "bob@company.com"
};

// 3. Create part-time employee
var carol = new PartTimeEmployee("Carol", "Martinez", "Marketing Coordinator", 22.50m)
{
    HoursWorked = 60,   // hours in the bi-weekly period
    DateOfBirth = new DateTime(1998, 11, 3),
    Email = "carol@company.com"
};

// 4. Assign benefits to full-time employees
var healthBenefit  = new BenefitDeduction(BenefitType.Health,  150m, 300m);
var dentalBenefit  = new BenefitDeduction(BenefitType.Dental,   30m,  60m);
var pensionBenefit = new BenefitDeduction(BenefitType.Pension, 100m, 100m);

alice.AddBenefit(healthBenefit);
alice.AddBenefit(dentalBenefit);
alice.AddBenefit(pensionBenefit);

bob.AddBenefit(healthBenefit);
bob.AddBenefit(dentalBenefit);

// 5. Assign employees to departments
engineering.AddEmployee(alice);
engineering.AddEmployee(bob);
engineering.Manager = bob;

marketing.AddEmployee(carol);

// 6. Set up payroll processor with company-wide taxes
var processor = new PayrollProcessor();
processor.RegisterEmployee(alice);
processor.RegisterEmployee(bob);
processor.RegisterEmployee(carol);

// Standard approximate Canadian payroll deduction rates
processor.AddCompanyTax(new TaxDeduction(TaxType.Federal,    0.205m));
processor.AddCompanyTax(new TaxDeduction(TaxType.Provincial, 0.100m));
processor.AddCompanyTax(new TaxDeduction(TaxType.CPP,        0.0595m));
processor.AddCompanyTax(new TaxDeduction(TaxType.EI,         0.0166m));

// 7. Process payroll for a bi-weekly period
var period = new PayPeriod(new DateTime(2026, 3, 1), PayPeriodType.BiWeekly);
Console.WriteLine($"Processing payroll for: {period}\n");

var stubs = processor.ProcessPayroll(period);

// 8. Print individual pay stubs
Console.WriteLine("--- Individual Pay Stubs ---");
foreach (var stub in stubs)
    stub.Print();

// 9. Generate summary report
Console.WriteLine("\n--- Summary Report ---");
var summaryReport = processor.GenerateReport(stubs, ReportType.Summary);
summaryReport.Generate();

// 10. Generate tax report
Console.WriteLine("\n--- Tax Report ---");
var taxReport = processor.GenerateReport(stubs, ReportType.TaxReport);
taxReport.Generate();

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();
