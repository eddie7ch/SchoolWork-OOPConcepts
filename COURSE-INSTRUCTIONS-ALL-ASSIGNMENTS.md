# OOP Concepts — All Assignments & Course Context
**Course:** Object-Oriented Programming Concepts (C#) — Bow Valley College
**Student:** Eddie Chongtham (eddie7ch)
**Repo:** SchoolWork-OOPConcepts (Public on GitHub)
**Submission filename format:** `chongtham_eddie_[assignment]_#`

---

## Course Overview

This course covers C# / .NET object-oriented design principles:
- Encapsulation, Inheritance, Abstraction, Polymorphism
- Interfaces and abstract classes
- Generics and generic collections
- Exception handling
- Delegates and events
- LINQ (Language Integrated Query)
- File I/O and CSV persistence

---

## Repository Structure

```
SchoolWork-OOPConcepts/
├── Activities/
│   ├── AbstractionAndInterfaces/
│   ├── DataProcessingGenerics/
│   ├── EncapsulationDemo/
│   ├── ExceptionHandling/
│   ├── MovieRecommendations/
│   ├── PayrollSystem/
│   └── VehicleInheritance/
└── Assignments/
    ├── Assignment1-LibraryManagementSystemV1/
    ├── Assignment2-LibraryManagementSystemV2/
    ├── Assignment3-LibraryManagementSystemV3/
    └── Final-StudentManagementSystem/
```

---

## Assignment 1 — Library Management System V1

**Folder:** `Assignments/Assignment1-LibraryManagementSystemV1/`
**Focus:** Basic OOP — classes, encapsulation, collections

### What was built
A console-based Library Management System demonstrating foundational OOP:
- `Book` class with properties (Id, Title, Author, Genre, Status)
- `Borrower` class with properties (Id, Name, Address, Phone, Email, BorrowedItems)
- Basic data management — add, list, search
- No inheritance; single-level class hierarchy

### Folder Structure
```
Assignment1-LibraryManagementSystemV1/
├── Models/         ← Book.cs, Borrower.cs
├── Services/       ← Library service class
├── UML/            ← UML diagram files
├── Program.cs
└── Assignment1.csproj
```

### Key OOP Concepts Demonstrated
- **Encapsulation:** Private fields with public properties
- **Classes and objects:** Instantiating and managing Book and Borrower objects
- **Collections:** Using `List<T>` to store and iterate items

---

## Assignment 2 — Library Management System V2

**Folder:** `Assignments/Assignment2-LibraryManagementSystemV2/`
**Focus:** Inheritance, polymorphism, interfaces

### What was built
An enhanced version of the Library system introducing media types and polymorphism:
- `MediaItem` base class (or equivalent) extended by `Book` and `DVD`
- `DVD` adds Director and Runtime properties
- Borrower with borrow limit enforcement
- More complex service layer

### Folder Structure
```
Assignment2-LibraryManagementSystemV2/
├── UML/
├── Program.cs
├── Library Management System v2.0.csproj
└── Library Management System v2.0.sln
```

### Key OOP Concepts Demonstrated
- **Inheritance:** Book and DVD inherit from a shared base class
- **Polymorphism:** Virtual/override methods for display
- **Abstraction:** Shared interface or abstract class for media items

---

## Assignment 3 — Library Management System V3

**Folder:** `Assignments/Assignment3-LibraryManagementSystemV3/`
**Focus:** Full OOP design — abstraction, interfaces, exception handling

### What was built
The fully complete Library Management System with all features:

**Classes:**
- `MediaItem` (abstract base) — Id, Title, Status
- `Book : MediaItem` — Author, Genre
- `DVD : MediaItem` — Director, RuntimeMinutes
- `Borrower` — Id, Name, Address, Phone, Email, BorrowedItems (max 5)
- `Library` — manages collections, business logic

**Features:**
- Add/remove Books and DVDs
- Add/remove Borrowers (cannot remove if items still borrowed)
- Borrow media (enforces availability + 5-item limit)
- Return media
- Search by title or author/director
- Full display of all items and borrowers

**Sample Output (from output.txt):**
```
LIBRARY MANAGEMENT SYSTEM v3.0
- 7 books, 2 DVDs, 2 borrowers added
- Borrow/return with error handling (already borrowed, limit exceeded)
- Search by title "the" returns matching items
- Remove borrowed item fails gracefully
```

### Folder Structure
```
Assignment3-LibraryManagementSystemV3/
├── UML/
├── Program.cs
├── output.txt      ← sample program output
├── OOP-CSharp-Assignment3 LibraryManagementSystemV3.csproj
└── OOP-CSharp-Assignment3 LibraryManagementSystemV3.sln
```

### Key OOP Concepts Demonstrated
- **Abstraction:** `MediaItem` abstract class with abstract/virtual members
- **Inheritance:** `Book` and `DVD` extend `MediaItem`
- **Polymorphism:** Overridden `ToString()` and display methods
- **Encapsulation:** Private backing fields, public properties, validation in setters
- **Exception handling:** Custom exceptions or guard clauses for invalid operations

---

## Final Assignment — Student Management System (Parts 1–4)

**Folder:** `Assignments/Final-StudentManagementSystem/`
**System name:** "Fairview County College Student Management System"
**Focus:** Full OOP mastery — all four pillars + Generics + Delegates + LINQ + CSV persistence

---

### Part 1 — Core OOP Design

**Class Hierarchy:**

| Class | Type | Inherits From | Key Properties |
|---|---|---|---|
| `Person` | Abstract | — | Id, FirstName, LastName, DateOfBirth, Email, PhoneNumber |
| `Student : Person` | Concrete | Person | StudentId, Major, GPA, EnrolledCourses |
| `Instructor : Person` | Concrete | Person | InstructorId, Department, Specialization, CoursesTeaching |
| `Course` | Concrete | — | CourseId, CourseName, CourseCode, Credits, MaxCapacity |
| `SchoolClass` | Concrete | — | ClassId, Course, Instructor, Students, Schedule |
| `Department` | Concrete | — | DepartmentId, Name, Courses, Instructors |

**OOP Principles:**
- **Encapsulation:** All fields private; properties with validation
- **Inheritance:** Student and Instructor both inherit from Person
- **Abstraction:** Person is abstract (cannot be instantiated directly)
- **Polymorphism:** Override `ToString()`, possibly virtual methods for display

---

### Part 2 — Abstraction & Interfaces

**IIdentifiable Interface:**
```csharp
public interface IIdentifiable
{
    string GetId();
    void DisplayInfo();
}
```
All major classes implement `IIdentifiable`.

**Abstract Person class:**
```csharp
public abstract class Person : IIdentifiable
{
    // Abstract: must be implemented by subclasses
    public abstract string GetRole();
    // Virtual: can be overridden
    public virtual void DisplayInfo() { ... }
}
```

---

### Part 3 — Generics & Delegates

**Generic Collection class:**
```csharp
public class Collection<T> where T : IIdentifiable
{
    private List<T> _items = new();
    public void Add(T item) { ... }
    public void Remove(string id) { ... }
    public T GetById(string id) { ... }
    public IEnumerable<T> GetAll() { ... }
    public event CollectionChangedHandler? CollectionChanged;
}
```

**Delegate and Event types:**
```csharp
public delegate void CollectionChangedHandler(string action, string id);
public delegate void EnrollmentChangedHandler(string studentId, string courseId, string action);
```

Events fire when:
- Items are added/removed from a `Collection<T>`
- A student's enrollment changes

---

### Part 4 — LINQ & CSV Persistence

**LINQ Queries implemented:**
```csharp
// Filter students by age range
var minors = students.Where(s => s.Age < 18);

// Students with GPA above threshold
var honorRoll = students.Where(s => s.GPA >= 3.5);

// Sort by last name
var sorted = students.OrderBy(s => s.LastName);

// Group by major
var byMajor = students.GroupBy(s => s.Major);

// Search by keyword (name or email)
var found = students.Where(s =>
    s.FirstName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
    s.LastName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
    s.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase));

// Filter instructors by department
var deptInstructors = instructors.Where(i => i.Department == deptName);

// Students enrolled in a specific course
var enrolled = students.Where(s => s.EnrolledCourses.Contains(courseId));
```

**CSV Persistence:**
- `students.csv` — saves/loads all Student records
- `instructors.csv` — saves/loads all Instructor records
- Format: CSV with header row
- On startup: load from CSV if file exists
- On any change: save back to CSV

---

## Activities Summary

| Activity | OOP Concept |
|---|---|
| `AbstractionAndInterfaces` | Abstract classes, interface implementation |
| `DataProcessingGenerics` | Generic classes and methods, constraints |
| `EncapsulationDemo` | Private fields, properties, validation |
| `ExceptionHandling` | Try/catch, custom exceptions, finally blocks |
| `MovieRecommendations` | Collections, filtering, display |
| `PayrollSystem` | Inheritance, polymorphism (Employee types) |
| `VehicleInheritance` | Class hierarchy, override, polymorphic calls |

---

## Key C# Patterns Used

```csharp
// Abstract class with interface
public abstract class Person : IIdentifiable { }

// Generic collection with constraint
public class Collection<T> where T : IIdentifiable { }

// Delegate and event
public delegate void CollectionChangedHandler(string action, string id);
public event CollectionChangedHandler? CollectionChanged;

// LINQ with lambda
var result = list.Where(x => x.Property == value).OrderBy(x => x.Name).ToList();

// CSV read/write
File.WriteAllLines("students.csv", students.Select(s => s.ToCsv()));
var students = File.ReadAllLines("students.csv").Skip(1).Select(Student.FromCsv);
```

---

## Build & Run

```bash
cd Assignments/Assignment3-LibraryManagementSystemV3
dotnet build
dotnet run

cd Assignments/Final-StudentManagementSystem
dotnet build
dotnet run
```
