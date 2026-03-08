# Payroll System — Class Diagram & Design Document

**Course:** Object-Oriented Programming (C#)  
**Date:** March 7, 2026  

> **Visual UML diagram:** Open `UML_ClassDiagram.drawio` in [draw.io](https://app.diagrams.net) to view the full colour-coded class diagram.

---

## Overview

This document describes the class diagram and OOP design for a payroll system that handles employee salaries, wages, deductions, taxes, benefits, and report generation.

---

## UML Class Diagram (Text Notation)

```
┌─────────────────────────────────┐
│          <<abstract>>           │
│             Person              │
├─────────────────────────────────┤
│ - _firstName : string           │
│ - _lastName  : string           │
│ - _email     : string?          │
│ - _phoneNumber: string?         │
│ + Address    : string?          │
│ + DateOfBirth: DateTime         │
├─────────────────────────────────┤
│ + GetFullName() : string        │
│ + ToString()    : string        │
└───────────────┬─────────────────┘
                │  inherits
                ▼
┌─────────────────────────────────┐
│          <<abstract>>           │
│             Employee            │
├─────────────────────────────────┤
│ + EmployeeId  : string          │
│ + HireDate    : DateTime        │
│ + Position    : string          │
│ + Department  : Department?     │
│ + EmployeeType: EmployeeType    │
│ + Benefits    : List<BenefitDeduction> │
├─────────────────────────────────┤
│ + CalculateGrossPay() : decimal  <<abstract>> │
│ + CalculateNetPay(taxes) : decimal │
│ + AddBenefit(benefit)  : void   │
│ + GeneratePayStub(period, taxes): PayStub │
└──────┬──────────────────┬───────┘
       │ inherits         │ inherits
       ▼                  ▼
┌──────────────┐  ┌──────────────────┐
│FullTimeEmployee│ │PartTimeEmployee  │
├──────────────┤  ├──────────────────┤
│+AnnualSalary │  │+ HourlyRate      │
│  : decimal   │  │+ HoursWorked     │
├──────────────┤  │  : double        │
│+CalculateGross│ ├──────────────────┤
│  Pay():decimal│ │+ CalculateGross  │
└──────────────┘  │  Pay() : decimal │
                  └──────────────────┘

┌───────────────────────────────┐
│           Department          │
├───────────────────────────────┤
│ + DepartmentId : string       │
│ + Name         : string       │
│ + Manager      : Employee?    │
│ - _employees   : List<Employee>│
├───────────────────────────────┤
│ + AddEmployee(e)    : void    │
│ + RemoveEmployee(id): bool    │
│ + GetEmployeeCount(): int     │
└───────────────────────────────┘

         Employee ──── Department
              (many)        (one)

┌───────────────────────────────────┐
│           <<abstract>>            │
│             Deduction             │
├───────────────────────────────────┤
│ + DeductionId  : string           │
│ + Name         : string           │
│ + Description  : string?          │
├───────────────────────────────────┤
│ + CalculateDeduction(grossPay)    │
│     : decimal   <<abstract>>      │
└──────────┬─────────────────┬──────┘
           │ inherits        │ inherits
           ▼                 ▼
┌──────────────────┐  ┌─────────────────────┐
│   TaxDeduction   │  │  BenefitDeduction   │
├──────────────────┤  ├─────────────────────┤
│ + TaxType        │  │ + BenefitType       │
│ + TaxRate:decimal│  │ + EmployeeContrib.  │
├──────────────────┤  │ + EmployerContrib.  │
│ + CalculateDeduct│  ├─────────────────────┤
│   ion(): decimal │  │ + CalculateDeduct   │
└──────────────────┘  │   ion() : decimal   │
                      └─────────────────────┘

┌───────────────────────────────────┐
│             PayPeriod             │
├───────────────────────────────────┤
│ + StartDate    : DateTime         │
│ + EndDate      : DateTime         │
│ + PeriodType   : PayPeriodType    │
├───────────────────────────────────┤
│ + GetPeriodDescription() : string │
└───────────────────────────────────┘

┌───────────────────────────────────┐
│              PayStub              │
├───────────────────────────────────┤
│ + PayStubId    : string           │
│ + Employee     : Employee         │
│ + PayPeriod    : PayPeriod        │
│ + GrossPay     : decimal          │
│ + IssuedDate   : DateTime         │
│ + TotalDeductions : decimal       │
│ + NetPay       : decimal          │
├───────────────────────────────────┤
│ + AddDeduction(d) : void          │
│ + Print()         : void          │
└───────────────────────────────────┘

      PayStub ──── Employee    (association)
      PayStub ──── PayPeriod   (association)
      PayStub ──── Deduction[] (aggregation)

┌───────────────────────────────────┐
│          PayrollProcessor         │
├───────────────────────────────────┤
│ - _employees   : List<Employee>   │
│ - _companyTaxes: List<TaxDeduction>│
├───────────────────────────────────┤
│ + RegisterEmployee(e)  : void     │
│ + RemoveEmployee(id)   : bool     │
│ + AddCompanyTax(tax)   : void     │
│ + ProcessPayroll(period): List<PayStub> │
│ + GenerateReport(stubs, type)     │
│     : PayrollReport               │
└───────────────────────────────────┘

┌───────────────────────────────────┐
│           PayrollReport           │
├───────────────────────────────────┤
│ + ReportId      : string          │
│ + GeneratedDate : DateTime        │
│ + ReportType    : ReportType      │
│ + PayStubs      : IReadOnlyList   │
│ + TotalGrossPay : decimal         │
│ + TotalNetPay   : decimal         │
├───────────────────────────────────┤
│ + Generate()        : void        │
│ + Export(filePath)  : void        │
└───────────────────────────────────┘
```

---

## Enumerations

| Enum | Values |
|------|--------|
| `EmployeeType` | FullTime, PartTime |
| `TaxType` | Federal, Provincial, CPP, EI |
| `BenefitType` | Health, Dental, Vision, Pension, LifeInsurance |
| `PayPeriodType` | Weekly, BiWeekly, SemiMonthly, Monthly |
| `ReportType` | Summary, Detailed, TaxReport, YearToDate |

---

## Class Relationships

### Inheritance (IS-A)
- **`FullTimeEmployee` IS-A `Employee` IS-A `Person`** — A full-time employee is an employee who is a person. The hierarchy avoids code duplication for shared person and employee attributes.
- **`PartTimeEmployee` IS-A `Employee` IS-A `Person`** — Same hierarchy; only `CalculateGrossPay()` differs (hourly vs. salaried).
- **`TaxDeduction` IS-A `Deduction`** — A tax is a specific kind of deduction computed as a percentage of gross pay.
- **`BenefitDeduction` IS-A `Deduction`** — A benefit is a specific kind of deduction computed as a fixed amount per period.

### Association / Aggregation (HAS-A)
- **`Department` HAS-A list of `Employee`** — A department groups many employees. An employee belongs to one department. Removing the department sets the employee's reference to null (loose coupling).
- **`PayStub` HAS-A `Employee`** — A pay stub records the earnings of one employee.
- **`PayStub` HAS-A `PayPeriod`** — A pay stub covers one pay period.
- **`PayStub` HAS-A list of `Deduction`** — A pay stub aggregates all applicable deductions (taxes + benefits).
- **`PayrollProcessor` HAS-A list of `Employee`** — The processor manages all registered employees.
- **`PayrollReport` HAS-A list of `PayStub`** — A report summarizes a collection of pay stubs.

---

## OOP Principles Applied

### 1. Encapsulation
- All sensitive fields (`_firstName`, `_lastName`, `_annualSalary`, `_hourlyRate`, etc.) are **private** with controlled access through public properties.
- Validation is enforced inside property setters (e.g., salary must be positive, tax rate must be 0–1).
- `Department._employees` is exposed only as `IReadOnlyList` to prevent external modification.

### 2. Inheritance
- `Person → Employee → FullTimeEmployee / PartTimeEmployee` avoids duplicating name, contact, and hire-date logic.
- `Deduction → TaxDeduction / BenefitDeduction` avoids duplicating deduction ID and name logic.

### 3. Abstraction
- `Person` is **abstract** — you cannot instantiate a generic "person", only a specific employee type.
- `Employee` is **abstract** — `CalculateGrossPay()` is declared but not implemented there.
- `Deduction` is **abstract** — `CalculateDeduction()` forces each subclass to define its own calculation logic.

### 4. Polymorphism
- `CalculateGrossPay()` is overridden differently in `FullTimeEmployee` (annual salary ÷ 26) and `PartTimeEmployee` (hourly rate × hours worked).
- `CalculateDeduction()` is overridden differently in `TaxDeduction` (percentage of gross) and `BenefitDeduction` (fixed amount).
- `PayrollProcessor.ProcessPayroll()` calls `employee.CalculateGrossPay()` on each employee without knowing the concrete type.

---

## Sample Output (Bi-Weekly Period: 2026-03-01 to 2026-03-14)

```
Employee          Gross Pay    Deductions    Net Pay
Alice Johnson     $3,269.23    $1,525.90     $1,743.33
Bob Smith         $4,038.46    $1,719.06     $2,319.40
Carol Martinez    $1,350.00    $  514.48     $  835.52
─────────────────────────────────────────────────────
TOTALS            $8,657.69    $3,759.44     $4,898.25
```

---

## Project Structure

```
PayrollSystem/
├── PayrollSystem.slnx
└── PayrollSystem/
    ├── Program.cs               ← Demo / entry point
    ├── Enums/
    │   ├── EmployeeType.cs
    │   ├── TaxType.cs
    │   ├── BenefitType.cs
    │   ├── PayPeriodType.cs
    │   └── ReportType.cs
    ├── Models/
    │   ├── Person.cs            ← Abstract base
    │   ├── Employee.cs          ← Abstract employee
    │   ├── FullTimeEmployee.cs  ← Salaried employee
    │   ├── PartTimeEmployee.cs  ← Hourly employee
    │   └── Department.cs
    └── Payroll/
        ├── Deduction.cs         ← Abstract deduction
        ├── TaxDeduction.cs
        ├── BenefitDeduction.cs
        ├── PayPeriod.cs
        ├── PayStub.cs
        ├── PayrollProcessor.cs
        └── PayrollReport.cs
```
