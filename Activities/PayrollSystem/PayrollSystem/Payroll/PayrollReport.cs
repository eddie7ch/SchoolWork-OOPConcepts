using PayrollSystem.Enums;

namespace PayrollSystem.Payroll
{
    /// <summary>
    /// Generates and displays payroll reports for a set of pay stubs.
    /// </summary>
    public class PayrollReport
    {
        private static int _reportCounter = 0;

        public string ReportId { get; private set; }
        public DateTime GeneratedDate { get; private set; }
        public ReportType ReportType { get; private set; }
        public IReadOnlyList<PayStub> PayStubs { get; private set; }

        public decimal TotalGrossPay => PayStubs.Sum(s => s.GrossPay);
        public decimal TotalDeductions => PayStubs.Sum(s => s.TotalDeductions);
        public decimal TotalNetPay => PayStubs.Sum(s => s.NetPay);

        public PayrollReport(List<PayStub> stubs, ReportType reportType)
        {
            ReportId = $"RPT-{++_reportCounter:D4}";
            GeneratedDate = DateTime.Now;
            ReportType = reportType;
            PayStubs = stubs?.AsReadOnly() ?? throw new ArgumentNullException(nameof(stubs));
        }

        /// <summary>
        /// Prints the report to the console based on the ReportType.
        /// </summary>
        public void Generate()
        {
            Console.WriteLine();
            Console.WriteLine(new string('*', 60));
            Console.WriteLine($"  PAYROLL REPORT  [{ReportId}]  —  {ReportType}");
            Console.WriteLine($"  Generated: {GeneratedDate:yyyy-MM-dd HH:mm}");
            Console.WriteLine(new string('*', 60));

            if (ReportType == ReportType.Detailed)
            {
                foreach (var stub in PayStubs)
                    stub.Print();
            }
            else if (ReportType == ReportType.Summary)
            {
                Console.WriteLine($"  {"Employee",-30} {"Gross",10} {"Deductions",12} {"Net",10}");
                Console.WriteLine(new string('-', 65));
                foreach (var stub in PayStubs)
                    Console.WriteLine($"  {stub.Employee.GetFullName(),-30} {stub.GrossPay,10:C} {stub.TotalDeductions,12:C} {stub.NetPay,10:C}");
            }
            else if (ReportType == ReportType.TaxReport)
            {
                Console.WriteLine($"  {"Employee",-30} {"Gross",10} {"Total Tax",12}");
                Console.WriteLine(new string('-', 55));
                foreach (var stub in PayStubs)
                {
                    decimal taxTotal = stub.Deductions
                        .OfType<TaxDeduction>()
                        .Sum(t => t.CalculateDeduction(stub.GrossPay));
                    Console.WriteLine($"  {stub.Employee.GetFullName(),-30} {stub.GrossPay,10:C} {taxTotal,12:C}");
                }
            }
            else if (ReportType == ReportType.YearToDate)
            {
                Console.WriteLine("  Year-to-Date report is cumulative across all processed stubs.");
                Console.WriteLine($"  {"Employee",-30} {"YTD Gross",12} {"YTD Net",10}");
                Console.WriteLine(new string('-', 55));
                foreach (var stub in PayStubs)
                    Console.WriteLine($"  {stub.Employee.GetFullName(),-30} {stub.GrossPay,12:C} {stub.NetPay,10:C}");
            }

            Console.WriteLine(new string('-', 65));
            Console.WriteLine($"  {"TOTALS",-30} {TotalGrossPay,10:C} {TotalDeductions,12:C} {TotalNetPay,10:C}");
            Console.WriteLine(new string('*', 60));
        }

        public void Export(string filePath)
        {
            // Redirects console output to a text file
            using var writer = new StreamWriter(filePath, append: false);
            var original = Console.Out;
            Console.SetOut(writer);
            Generate();
            Console.SetOut(original);
            Console.WriteLine($"  Report exported to: {filePath}");
        }

        public override string ToString() =>
            $"[{ReportId}] {ReportType} | {GeneratedDate:yyyy-MM-dd} | Stubs: {PayStubs.Count} | Net Total: {TotalNetPay:C}";
    }
}
