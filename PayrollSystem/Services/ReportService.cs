using PayrollSystem.Models;

namespace PayrollSystem.Services;

/// <summary>
/// Generates formatted payroll reports from a collection of PayStub objects.
/// Demonstrates the Single Responsibility Principle: reporting logic is
/// separated from pay calculation logic.
/// </summary>
public class ReportService
{
    /// <summary>
    /// Prints a payroll summary table showing all employees' gross, deductions,
    /// and net pay for the period, plus company-wide totals.
    /// </summary>
    public void GeneratePayrollSummary(List<PayStub> stubs)
    {
        if (stubs.Count == 0)
        {
            Console.WriteLine("No pay stubs to report.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine(new string('=', 72));
        Console.WriteLine("  PAYROLL SUMMARY REPORT");
        Console.WriteLine($"  Pay Date: {stubs[0].PayDate:MM/dd/yyyy}   " +
                          $"Period: {stubs[0].PeriodStart:MM/dd/yyyy} – {stubs[0].PeriodEnd:MM/dd/yyyy}");
        Console.WriteLine(new string('=', 72));
        Console.WriteLine($"  {"Employee",-28} {"Gross Pay",12} {"Deductions",12} {"Net Pay",12}");
        Console.WriteLine(new string('-', 72));

        foreach (var stub in stubs)
        {
            Console.WriteLine(
                $"  {stub.Employee.FullName,-28} {stub.GrossPay,12:C} " +
                $"{stub.TotalDeductions,12:C} {stub.NetPay,12:C}");
        }

        Console.WriteLine(new string('-', 72));
        Console.WriteLine(
            $"  {"TOTALS",-28} {stubs.Sum(s => s.GrossPay),12:C} " +
            $"{stubs.Sum(s => s.TotalDeductions),12:C} {stubs.Sum(s => s.NetPay),12:C}");
        Console.WriteLine(new string('=', 72));
        Console.WriteLine();
    }

    /// <summary>
    /// Prints a detailed earnings history for a single employee across multiple stubs.
    /// </summary>
    public void GenerateEmployeeReport(Employee employee, List<PayStub> stubs)
    {
        var employeeStubs = stubs
            .Where(s => s.Employee.EmployeeId == employee.EmployeeId)
            .OrderBy(s => s.PayDate)
            .ToList();

        Console.WriteLine();
        Console.WriteLine(new string('=', 48));
        Console.WriteLine($"  EMPLOYEE REPORT — {employee.FullName}");
        Console.WriteLine($"  ID: {employee.EmployeeId}   " +
                          $"Dept: {employee.Department?.DepartmentName ?? "N/A"}");
        Console.WriteLine(new string('-', 48));

        if (employeeStubs.Count == 0)
        {
            Console.WriteLine("  No pay history available.");
        }
        else
        {
            foreach (var stub in employeeStubs)
            {
                Console.WriteLine(
                    $"  {stub.PayDate:MM/dd/yyyy}  Gross: {stub.GrossPay,10:C}  " +
                    $"Net: {stub.NetPay,10:C}");
            }

            Console.WriteLine(new string('-', 48));
            Console.WriteLine(
                $"  YTD Gross: {employeeStubs.Sum(s => s.GrossPay),10:C}  " +
                $"YTD Net: {employeeStubs.Sum(s => s.NetPay),10:C}");
        }

        Console.WriteLine(new string('=', 48));
        Console.WriteLine();
    }

    /// <summary>
    /// Writes the payroll summary to a plain-text file.
    /// </summary>
    public void ExportSummaryToFile(string filePath, List<PayStub> stubs)
    {
        // Build the same content that would be printed to the console
        using var writer = new System.IO.StreamWriter(filePath);

        writer.WriteLine("PAYROLL SUMMARY REPORT");
        writer.WriteLine($"Pay Date : {stubs[0].PayDate:MM/dd/yyyy}");
        writer.WriteLine($"Period   : {stubs[0].PeriodStart:MM/dd/yyyy} - {stubs[0].PeriodEnd:MM/dd/yyyy}");
        writer.WriteLine(new string('-', 60));
        writer.WriteLine($"{"Employee",-28} {"Gross",10} {"Deductions",12} {"Net",10}");
        writer.WriteLine(new string('-', 60));

        foreach (var stub in stubs)
        {
            writer.WriteLine(
                $"{stub.Employee.FullName,-28} {stub.GrossPay,10:C} " +
                $"{stub.TotalDeductions,12:C} {stub.NetPay,10:C}");
        }

        writer.WriteLine(new string('-', 60));
        writer.WriteLine(
            $"{"TOTALS",-28} {stubs.Sum(s => s.GrossPay),10:C} " +
            $"{stubs.Sum(s => s.TotalDeductions),12:C} {stubs.Sum(s => s.NetPay),10:C}");

        Console.WriteLine($"  Report exported to: {filePath}");
    }
}
