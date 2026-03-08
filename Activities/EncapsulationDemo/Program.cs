// ============================================================
// Program.cs — Entry Point
// Encapsulation, Modifiers, Properties and Methods Demo
// ============================================================

using EncapsulationDemo;

// ─────────────────────────────────────────────────────────────
// SECTION 1 — ACCESS MODIFIERS
// ─────────────────────────────────────────────────────────────
Console.WriteLine("=== SECTION 1: ACCESS MODIFIERS ===\n");

var car = new Vehicle("Toyota", "ENG-001-XYZ", 15000, "PLANT-TX", "WR-2024-A");
Console.WriteLine("Vehicle info (via public method):");
car.DisplayInfo();

// Public member — directly accessible
Console.WriteLine($"\n  [public]             Make = {car.Make}");

// Private member — NOT accessible directly; must use the public method
Console.WriteLine($"  [private via method] Engine Serial = {car.GetEngineSerialNumber()}");

// Internal member — accessible within the same assembly
Console.WriteLine($"  [internal]           Assembly Plant = {car.AssemblyPlantCode}");

// Protected internal — accessible within the same assembly
Console.WriteLine($"  [protected internal] Warranty Code  = {car.WarrantyCode}");

Console.WriteLine("\nElectric Vehicle (derived class — can access protected members):");
var ev = new ElectricVehicle("Tesla", "ENG-002-EV", 5000, "PLANT-CA", "WR-2024-B", 100);
ev.DisplayExtendedInfo();

// ─────────────────────────────────────────────────────────────
// SECTION 2 — PROPERTIES
// ─────────────────────────────────────────────────────────────
Console.WriteLine("\n=== SECTION 2: PROPERTIES ===\n");

var student = new StudentRecord("STU-101", "pass123", 88);
student.DisplayInfo();

// Read-only property — can read StudentId, cannot assign it
Console.WriteLine($"\n  [read-only]  StudentId = {student.StudentId}");
// student.StudentId = "STU-999";   // COMPILE ERROR — no setter

// Write-only property — can set Password, cannot read it back
student.Password = "newPass456";
// Console.WriteLine(student.Password);  // COMPILE ERROR — no getter
Console.WriteLine("  [write-only] Password updated. Verify old: " +
                  student.VerifyPassword("pass123") +
                  " | Verify new: " + student.VerifyPassword("newPass456"));

// Read-write property — can both get and set; setter validates range
Console.WriteLine($"\n  [read-write] Current Grade = {student.Grade}");
student.Grade = 95;
Console.WriteLine($"  [read-write] Grade updated to {student.Grade}");

try
{
    student.Grade = 150;   // should throw
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"  [read-write] Validation caught: {ex.Message.Split('\n')[0]}");
}

// ─────────────────────────────────────────────────────────────
// SECTION 3 — METHODS
// ─────────────────────────────────────────────────────────────
Console.WriteLine("\n=== SECTION 3: METHODS ===\n");

var rect = new Rectangle(5.0, 3.0, "Blue");
Console.WriteLine("Rectangle info:");
rect.DisplayInfo();

// Getter method
Console.WriteLine($"\n  [getter] GetWidth()  = {rect.GetWidth()}");
Console.WriteLine($"  [getter] GetHeight() = {rect.GetHeight()}");
Console.WriteLine($"  [getter] GetColor()  = {rect.GetColor()}");

// Setter method (with validation)
rect.SetWidth(10.0);
rect.SetColor("Red");
Console.WriteLine($"\n  [setter] Width updated to {rect.GetWidth()}, Color to '{rect.GetColor()}'");

try
{
    rect.SetHeight(-5);   // should throw
}
catch (ArgumentException ex)
{
    Console.WriteLine($"  [setter] Validation caught: {ex.Message}");
}

// Calculation methods — use private fields without exposing them
Console.WriteLine($"\n  [calculation] Area      = {rect.CalculateArea()}");
Console.WriteLine($"  [calculation] Perimeter = {rect.CalculatePerimeter()}");
Console.WriteLine($"  [calculation] Is Square = {rect.IsSquare()}");

// ─────────────────────────────────────────────────────────────
// SECTION 4 — ENCAPSULATION (DATA HIDING & ABSTRACTION)
// ─────────────────────────────────────────────────────────────
Console.WriteLine("\n=== SECTION 4: ENCAPSULATION — BANK ACCOUNT ===\n");

var account = new BankAccount("ACC-8821", "Alice Johnson", 500.00m, "1234");
Console.WriteLine("Account summary:");
account.DisplaySummary();

Console.WriteLine("\nPerforming transactions:");
account.Deposit(200.00m);
account.Withdraw(100.00m, "1234");   // correct PIN — succeeds
account.Withdraw(50.00m,  "9999");   // wrong PIN   — denied
account.Withdraw(9999.00m, "1234");  // insufficient funds — denied

Console.WriteLine("\nChanging PIN:");
account.ChangePin("0000", "1234");   // wrong old PIN — denied
account.ChangePin("1234", "5678");   // correct — succeeds

Console.WriteLine("\nTransaction history (read-only view — internal list is protected):");
foreach (var entry in account.GetTransactionHistory())
    Console.WriteLine($"  {entry}");

Console.WriteLine("\nFinal account summary:");
account.DisplaySummary();

