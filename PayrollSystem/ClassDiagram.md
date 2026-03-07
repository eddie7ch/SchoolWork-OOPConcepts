# Payroll System — Class Diagram

## UML Class Diagram (Mermaid)

```mermaid
classDiagram
    direction TB

    class Employee {
        <<abstract>>
        -string _employeeId
        -List~IDeduction~ _deductions
        +string EmployeeId
        +string FirstName
        +string LastName
        +DateTime DateOfBirth
        +DateTime HireDate
        +string Email
        +string Phone
        +Department Department
        +string FullName
        +int YearsOfService
        +CalculateGrossPay()* decimal
        +AddDeduction(IDeduction) void
        +RemoveDeduction(IDeduction) void
        +GetDeductions() IReadOnlyList~IDeduction~
        +CalculateTotalDeductions() decimal
        +CalculateNetPay() decimal
    }

    class FullTimeEmployee {
        +decimal AnnualSalary
        +Benefits BenefitsPackage
        +FullTimeEmployee(id, firstName, lastName, dob, hireDate, email, phone, salary)
        +CalculateGrossPay() decimal
    }

    class PartTimeEmployee {
        +decimal HourlyRate
        +double HoursWorked
        +PartTimeEmployee(id, firstName, lastName, dob, hireDate, email, phone, rate)
        +CalculateGrossPay() decimal
    }

    class ContractEmployee {
        +decimal ContractRate
        +DateTime ContractEndDate
        +ContractEmployee(id, firstName, lastName, dob, hireDate, email, phone, rate, endDate)
        +CalculateGrossPay() decimal
        +IsContractActive() bool
    }

    class Department {
        -List~Employee~ _employees
        +string DepartmentId
        +string DepartmentName
        +IReadOnlyList~Employee~ Employees
        +Department(departmentId, departmentName)
        +AddEmployee(Employee) void
        +RemoveEmployee(Employee) void
        +GetHeadCount() int
    }

    class Benefits {
        +bool HasHealthInsurance
        +bool HasDentalInsurance
        +bool HasVisionInsurance
        +decimal RetirementContributionRate
        +decimal EmployerMatchRate
        +CalculateEmployeeContribution(decimal grossPay) decimal
        +CalculateEmployerContribution(decimal grossPay) decimal
    }

    class PayStub {
        -string _payStubId
        +string PayStubId
        +Employee Employee
        +DateTime PayDate
        +DateTime PeriodStart
        +DateTime PeriodEnd
        +decimal GrossPay
        +decimal TotalDeductions
        +decimal NetPay
        +List~DeductionLineItem~ DeductionItems
        +PayStub(employee, payDate, periodStart, periodEnd)
        +Print() void
        +GetSummary() string
    }

    class DeductionLineItem {
        +string Name
        +decimal Amount
    }

    class IDeduction {
        <<interface>>
        +string Name
        +Calculate(decimal grossPay)* decimal
    }

    class Deduction {
        <<abstract>>
        +string Name
        +Calculate(decimal grossPay)* decimal
    }

    class TaxDeduction {
        +TaxType Type
        +decimal Rate
        +TaxDeduction(type, rate)
        +Calculate(decimal grossPay) decimal
    }

    class BenefitsDeduction {
        +BenefitType Type
        +decimal MonthlyPremium
        +BenefitsDeduction(type, monthlyPremium)
        +Calculate(decimal grossPay) decimal
    }

    class TaxType {
        <<enumeration>>
        Federal
        State
        Local
        SocialSecurity
        Medicare
    }

    class BenefitType {
        <<enumeration>>
        Health
        Dental
        Vision
        Retirement
        LifeInsurance
    }

    class PayrollProcessor {
        -List~Employee~ _employees
        +AddEmployee(Employee) void
        +RemoveEmployee(Employee) void
        +GetAllEmployees() IReadOnlyList~Employee~
        +ProcessEmployee(employee, payDate, periodStart, periodEnd) PayStub
        +ProcessPayroll(payDate, periodStart, periodEnd) List~PayStub~
    }

    class ReportService {
        +GeneratePayrollSummary(List~PayStub~) void
        +GenerateEmployeeReport(Employee, List~PayStub~) void
        +ExportSummaryToFile(filePath, List~PayStub~) void
    }

    %% Inheritance
    Employee      <|-- FullTimeEmployee  : extends
    Employee      <|-- PartTimeEmployee  : extends
    Employee      <|-- ContractEmployee  : extends
    IDeduction    <|.. Deduction         : implements
    Deduction     <|-- TaxDeduction      : extends
    Deduction     <|-- BenefitsDeduction : extends

    %% Associations
    TaxDeduction      --> TaxType    : uses
    BenefitsDeduction --> BenefitType : uses

    Employee          "1" --> "0..1" Department   : belongs to
    Employee          "1" o--  "*"   IDeduction   : has
    FullTimeEmployee  "1" --> "0..1" Benefits     : has

    PayStub "1"  --> "1"  Employee         : generated for
    PayStub "1"  *--  "*" DeductionLineItem : contains

    PayrollProcessor "1" o-- "*" Employee  : manages
    PayrollProcessor       ..>      PayStub          : creates
    ReportService          ..>      PayStub          : uses
    Department       "1"  o-- "*" Employee : employs
```

---

## Class Descriptions & Relationships

### Core Model Classes

| Class | Type | Purpose |
|-------|------|---------|
| `Employee` | Abstract | Base class for all employee types. Encapsulates shared identity data and deduction management. |
| `FullTimeEmployee` | Concrete | Salaried employee paid on a bi-weekly basis (AnnualSalary ÷ 26). Eligible for a `Benefits` package. |
| `PartTimeEmployee` | Concrete | Hourly employee; gross pay = `HourlyRate × HoursWorked`. |
| `ContractEmployee` | Concrete | Fixed-fee contractor paid a flat `ContractRate` per pay period with a defined end date. |
| `Department` | Concrete | Groups employees; tracks head-count. |
| `Benefits` | Concrete | Describes employer-sponsored benefit elections and contribution rates. |
| `PayStub` | Concrete | Immutable snapshot of a single employee's pay calculation for one period. |
| `DeductionLineItem` | Concrete (DTO) | A name-value pair capturing a single deduction line on a pay stub. |

### Deduction Hierarchy

| Class | Type | Purpose |
|-------|------|---------|
| `IDeduction` | Interface | Contract for any deduction: exposes `Name` and `Calculate(grossPay)`. |
| `Deduction` | Abstract | Base class implementing `IDeduction`; reduces boilerplate for concrete deductions. |
| `TaxDeduction` | Concrete | Percentage-based deduction (Federal, State, Local, Social Security, Medicare). |
| `BenefitsDeduction` | Concrete | Fixed monthly-premium deduction split across pay periods (Health, Dental, Vision, etc.). |

### Service Classes

| Class | Purpose |
|-------|---------|
| `PayrollProcessor` | Orchestrates payroll: iterates employees, calculates pay/deductions, produces `PayStub` list. |
| `ReportService` | Formats and outputs payroll summaries and per-employee earnings reports. |

---

## OOP Principles Applied

### Encapsulation
- `Employee` stores its deduction list in a `private` field and exposes it only through controlled public methods (`AddDeduction`, `GetDeductions`).  
- Sensitive pay fields on `PayStub` (gross, net) are set only at construction time via `init`-style setters, preventing post-creation mutation.  
- `EmployeeId` is read-only after construction.

### Inheritance
- `Employee` defines the common interface (`CalculateGrossPay()` abstract) and shared implementation (`CalculateNetPay()`, `CalculateTotalDeductions()`), eliminating code duplication across the three employee types.  
- `Deduction` provides a concrete base for tax and benefits deductions; only `Calculate()` must be overridden.

### Polymorphism
- `PayrollProcessor.ProcessPayroll()` holds a `List<Employee>` and calls `CalculateGrossPay()` on each without knowing the concrete type — each subclass provides its own implementation at runtime.  
- Any new deduction type can be slotted in by implementing `IDeduction` without changing `Employee` or `PayrollProcessor`.

### Abstraction
- `IDeduction` abstracts *what* a deduction does (produce an amount from gross pay) from *how* it is calculated.  
- `Employee` abstracts the concept of "a person who gets paid" without prescribing the pay calculation.

---

## Relationship Summary

| Relationship | Description |
|---|---|
| `Employee` → `Department` | Many-to-one association; an employee belongs to one department. |
| `Employee` ◇→ `IDeduction` | Aggregation; an employee owns zero or more deductions. |
| `FullTimeEmployee` → `Benefits` | Association; full-time employees may have a benefits package. |
| `Department` ◇→ `Employee` | Aggregation; a department contains many employees. |
| `PayStub` ♦→ `DeductionLineItem` | Composition; line items exist only within their pay stub. |
| `PayrollProcessor` ◇→ `Employee` | Aggregation; the processor manages the employee roster. |
| `PayrollProcessor` ⟶ `PayStub` | Dependency; the processor creates pay stubs. |
| `ReportService` ⟶ `PayStub` | Dependency; the report service reads pay stubs to produce output. |
