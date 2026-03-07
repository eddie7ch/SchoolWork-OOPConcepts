namespace PayrollSystem.Models;

/// <summary>
/// Immutable snapshot of a single employee's pay for one pay period.
/// Created by PayrollProcessor; composed of DeductionLineItems (composition).
/// </summary>
public class PayStub
{
    public string    PayStubId      { get; }
    public Employee  Employee       { get; }
    public DateTime  PayDate        { get; }
    public DateTime  PeriodStart    { get; }
    public DateTime  PeriodEnd      { get; }
    public decimal   GrossPay       { get; }
    public decimal   TotalDeductions { get; }
    public decimal   NetPay         { get; }

    /// <summary>Composition: line items only exist within this pay stub.</summary>
    public IReadOnlyList<DeductionLineItem> DeductionItems { get; }

    public PayStub(
        Employee   employee,
        DateTime   payDate,
        DateTime   periodStart,
        DateTime   periodEnd)
    {
        Employee    = employee;
        PayDate     = payDate;
        PeriodStart = periodStart;
        PeriodEnd   = periodEnd;
        PayStubId   = $"{employee.EmployeeId}-{payDate:yyyyMMdd}";

        GrossPay = employee.CalculateGrossPay();

        // Snapshot each deduction as an immutable line item
        var items = employee.GetDeductions()
            .Select(d => new DeductionLineItem(d.Name, d.Calculate(GrossPay)))
            .ToList();

        DeductionItems  = items.AsReadOnly();
        TotalDeductions = items.Sum(i => i.Amount);
        NetPay          = GrossPay - TotalDeductions;
    }

    /// <summary>Prints a formatted pay stub to the console.</summary>
    public void Print()
    {
        Console.WriteLine(GetSummary());
    }

    public string GetSummary()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine(new string('=', 48));
        sb.AppendLine($"  PAY STUB — {PayStubId}");
        sb.AppendLine(new string('-', 48));
        sb.AppendLine($"  Employee   : {Employee.FullName} ({Employee.EmployeeId})");
        sb.AppendLine($"  Pay Date   : {PayDate:MM/dd/yyyy}");
        sb.AppendLine($"  Pay Period : {PeriodStart:MM/dd/yyyy} – {PeriodEnd:MM/dd/yyyy}");
        sb.AppendLine(new string('-', 48));
        sb.AppendLine($"  Gross Pay  : {GrossPay,12:C}");
        sb.AppendLine();
        sb.AppendLine("  Deductions:");
        foreach (var item in DeductionItems)
            sb.AppendLine($"    {item.Name,-28} {item.Amount,10:C}");
        sb.AppendLine(new string('-', 48));
        sb.AppendLine($"  Total Deductions:            {TotalDeductions,10:C}");
        sb.AppendLine($"  NET PAY:                     {NetPay,10:C}");
        sb.AppendLine(new string('=', 48));
        return sb.ToString();
    }
}
