# Student Management System — Final Project Document
### Fairview County College | OOP with C# Final Project
**Parts 3 & 4: Generics, Delegates, LINQ, and Data Persistence**

---

## 1. Executive Summary

The Fairview County College Student Management System is a fully object-oriented C# console application built across four incremental project parts. The final system demonstrates all core OOP principles (encapsulation, inheritance, abstraction, polymorphism), generic programming, delegate-based event handling, LINQ data manipulation, and file-based data persistence.

---

## 2. Final Architecture Overview

![UML Class Diagram](UML_ClassDiagram.svg)

```
StudentManagementSystem/
├── Models/
│   ├── Person.cs          ← Abstract base (Part 2)
│   ├── Student.cs         ← Concrete person, enrollment events (Parts 1,2,3)
│   ├── Instructor.cs      ← Concrete person (Parts 1,2)
│   ├── Course.cs          ← Data entity (Part 1)
│   ├── SchoolClass.cs     ← Data entity (Part 1)
│   └── Department.cs      ← Data entity (Part 1)
│
├── Collections/
│   └── Collection.cs      ← Generic Collection<T> + delegates (Part 3)
│
├── Data/
│   └── DataRepository.cs  ← LINQ queries + CSV persistence (Part 4)
│
└── Program.cs             ← Entry point, full demo of all 4 parts
```

---

## 3. Part 3: Generics and Delegates

### 3.1 IIdentifiable Interface

To make the generic collection work across *all* entity types, every entity implements the `IIdentifiable` interface:

```csharp
public interface IIdentifiable
{
    string ID { get; }
}
```

This gives `Collection<T>` a guaranteed way to identify and find items without knowing their concrete type.

**Entities implementing `IIdentifiable`:** `Person` (and by inheritance `Student`, `Instructor`), `Course`, `SchoolClass`, `Department`.

---

### 3.2 Generic Collection\<T\>

`Collection<T>` is a reusable generic class constrained to `IIdentifiable` types:

```csharp
public class Collection<T> where T : IIdentifiable
```

**Operations supported:**

| Method | Description |
|--------|-------------|
| `Add(T item)` | Adds an item; prevents duplicate IDs |
| `Remove(string id)` | Removes by ID; returns `bool` indicating success |
| `FindByID(string id)` | Returns matching item or `null` |
| `Search(Func<T, bool> predicate)` | Finds all items matching a predicate |
| `GetAll()` | Returns a read-only snapshot of all items |

**Why generics?**
Without generics, we would need separate `StudentCollection`, `InstructorCollection`, `CourseCollection` classes that duplicate identical logic. A single `Collection<T>` handles all entity types — this is the DRY (Don't Repeat Yourself) principle applied through the C# type system.

---

### 3.3 Delegates and Events

Two delegate-based event systems are implemented:

#### Collection-Level Events
Every `Collection<T>` instance exposes:
```csharp
public delegate void CollectionChangedHandler(string action, string itemID);
public event CollectionChangedHandler? OnCollectionChanged;
```
This fires whenever an item is added or removed, letting external code react without the collection needing to know about them (Observer pattern).

#### Student Enrollment Events
`Student` exposes its own domain-level event:
```csharp
public delegate void EnrollmentChangedHandler(string studentName, string courseName, string action);
public event EnrollmentChangedHandler? OnEnrollmentChanged;
```
This fires when `EnrollCourse()` or `DropCourse()` is called — useful for logging, UI updates, or audit trails.

**Sample output from event firing:**
```
[Event] Student    Added: S001
[Enroll Event] Emma Davis enrolled in 'Object-Oriented Programming'
[Event] Student    Removed: S004
```

---

## 4. Part 4: LINQ and Data Manipulation

### 4.1 What is LINQ?

LINQ (Language-Integrated Query) is a C# feature that lets you write SQL-style queries directly against in-memory collections, files, or databases using a consistent syntax. It eliminates manual loops for filtering, sorting, and grouping.

---

### 4.2 LINQ Queries Implemented

All queries are centralised in `DataRepository.cs`. Below is a description of each with the actual C# code:

#### Filter: Students older than a given age
```csharp
public IEnumerable<Student> GetStudentsOlderThan(int age) =>
    Students.GetAll().Where(s => s.Age > age);
```
**What it does:** Returns only students whose `Age` property is greater than the supplied threshold.

---

#### Filter: Students enrolled in a specific course
```csharp
public IEnumerable<Student> GetStudentsEnrolledIn(string courseName) =>
    Students.GetAll()
            .Where(s => s.CoursesEnrolled
                         .Contains(courseName, StringComparer.OrdinalIgnoreCase));
```
**What it does:** Searches each student's course list for a match (case-insensitive).

---

#### Sort: Students alphabetically by name
```csharp
public IEnumerable<Student> GetStudentsSortedByName() =>
    Students.GetAll().OrderBy(s => s.Name);
```

---

#### Sort: Students by age (ascending)
```csharp
public IEnumerable<Student> GetStudentsSortedByAge() =>
    Students.GetAll().OrderBy(s => s.Age);
```

---

#### Group: Students by number of courses enrolled
```csharp
public IEnumerable<IGrouping<int, Student>> GetStudentsGroupedByCourseCount() =>
    Students.GetAll().GroupBy(s => s.CoursesEnrolled.Count);
```
**Sample output:**
```
[2 course(s)]: Emma Davis, Liam Park, Olivia Brown
[0 course(s)]: Sofia Patel, Noah Kim
```

---

#### Search: Partial name match (case-insensitive)
```csharp
public IEnumerable<Student> SearchStudentsByName(string partialName) =>
    Students.GetAll()
            .Where(s => s.Name.Contains(partialName, StringComparison.OrdinalIgnoreCase));
```

---

#### Filter: Courses by department
```csharp
public IEnumerable<Course> GetCoursesByDepartment(string departmentID) =>
    Courses.GetAll()
           .Where(c => c.DepartmentID.Equals(departmentID, StringComparison.OrdinalIgnoreCase));
```

---

### 4.3 Data Persistence

Data is persisted to **CSV (Comma-Separated Values) text files** — a lightweight, human-readable format that requires no database server.

#### Save Students
```csharp
public void SaveStudentsToFile(string path = "students.csv")
```
Writes a header row followed by one row per student:
```
StudentID,Name,Age,CoursesEnrolled
S001,Emma Davis,20,Object-Oriented Programming|Database Systems
S002,Liam Park,22,Object-Oriented Programming|Calculus I
```
Multiple courses are separated by `|` within the single CSV field.

#### Load Students
```csharp
public void LoadStudentsFromFile(string path = "students.csv")
```
Reads the CSV, skips the header, parses each row, reconstructs `Student` objects (including re-enrolling all courses), and adds them to a fresh collection.

#### Save Instructors
```csharp
public void SaveInstructorsToFile(string path = "instructors.csv")
```
Same pattern for instructors.

**Why CSV instead of a database?**
For a college project, CSV provides persistence without requiring SQL Server, SQLite, or any additional dependencies. It can be opened in Excel for verification, making it ideal for demonstration purposes.

---

## 5. OOP Principles — Final Summary

| Principle | Implementation |
|-----------|---------------|
| **Encapsulation** | Private fields, public properties with controlled setters, all state changes through methods |
| **Inheritance** | `Student` and `Instructor` inherit shared state and behaviour from `Person` |
| **Abstraction** | `Person` abstract class hides implementation details; `IIdentifiable` interface defines minimal contract |
| **Polymorphism** | `List<Person>` holds mixed types; `DisplayInformation()` dispatches correctly at runtime; `Collection<T>` works for any `IIdentifiable` type |

---

## 6. Lessons Learned

1. **Start with abstraction** — defining `Person` early made adding new person types trivial and kept the codebase DRY.
2. **Generics eliminate boilerplate** — `Collection<T>` replaced what would have been five near-identical collection classes.
3. **Events decouple components** — the enrollment event system means the `Student` class doesn't need to know about logging, UI, or any other consumer.
4. **LINQ improves readability** — replacing manual `for` loops with LINQ queries makes data operations self-documenting.
5. **Separate entities from logic** — keeping `DataRepository` separate from the models made it easy to add new queries without touching the model classes.

---

## 7. Future Enhancements

- Persistent storage using a real database (SQLite via Entity Framework Core)
- Grades tracking per student per course
- A web or desktop UI (ASP.NET Core or WPF)
- Authentication and role-based access for students vs. staff
- Export reports to PDF
