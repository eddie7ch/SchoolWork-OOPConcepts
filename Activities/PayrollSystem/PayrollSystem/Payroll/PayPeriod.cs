using PayrollSystem.Enums;

namespace PayrollSystem.Payroll
{
    /// <summary>
    /// Represents a pay period with start and end dates.
    /// </summary>
    public class PayPeriod
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public PayPeriodType PeriodType { get; set; }

        public PayPeriod(DateTime startDate, PayPeriodType periodType = PayPeriodType.BiWeekly)
        {
            StartDate = startDate;
            PeriodType = periodType;
            EndDate = periodType switch
            {
                PayPeriodType.Weekly       => startDate.AddDays(6),
                PayPeriodType.BiWeekly     => startDate.AddDays(13),
                PayPeriodType.SemiMonthly  => startDate.AddDays(14),
                PayPeriodType.Monthly      => startDate.AddMonths(1).AddDays(-1),
                _                          => startDate.AddDays(13)
            };
        }

        public string GetPeriodDescription() =>
            $"{PeriodType} | {StartDate:yyyy-MM-dd} to {EndDate:yyyy-MM-dd}";

        public override string ToString() => GetPeriodDescription();
    }
}
