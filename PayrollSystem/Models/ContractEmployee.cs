namespace PayrollSystem.Models;

/// <summary>
/// Fixed-fee contractor paid a flat rate per pay period.
/// The contract has a defined end date; payroll should check IsContractActive().
/// </summary>
public class ContractEmployee : Employee
{
    /// <summary>Flat fee paid each pay period.</summary>
    public decimal  ContractRate    { get; private set; }
    public DateTime ContractEndDate { get; private set; }

    public ContractEmployee(
        string   employeeId,
        string   firstName,
        string   lastName,
        DateTime dateOfBirth,
        DateTime hireDate,
        string   email,
        string   phone,
        decimal  contractRate,
        DateTime contractEndDate)
        : base(employeeId, firstName, lastName, dateOfBirth, hireDate, email, phone)
    {
        if (contractRate < 0)
            throw new ArgumentOutOfRangeException(nameof(contractRate), "Contract rate cannot be negative.");

        ContractRate    = contractRate;
        ContractEndDate = contractEndDate;
    }

    /// <summary>Returns true if the contract has not yet expired.</summary>
    public bool IsContractActive() => DateTime.Today <= ContractEndDate;

    /// <summary>Gross pay is the flat contract rate for the period.</summary>
    public override decimal CalculateGrossPay() => ContractRate;
}
