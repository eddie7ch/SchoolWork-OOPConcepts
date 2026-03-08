# Student Management System — Design Document
### Fairview County College | OOP with C# Final Project
**Parts 1 & 2: Basic System Design + Abstraction & Polymorphism**

---

## 1. Overview

This document describes the object-oriented design of the Fairview County College Student Management System. It explains each class, the relationships between them, and how the four core OOP principles — **Encapsulation**, **Inheritance**, **Abstraction**, and **Polymorphism** — are applied throughout the system.

---

## 2. System Classes

### 2.1 Person *(abstract)*
**File:** `Models/Person.cs`

`Person` is the abstract foundation for every person in the system. It cannot be instantiated directly — it exists solely to define a shared contract.

| Member | Type | Access | Notes |
|--------|------|--------|-------|
| `ID` | `string` | `protected set` | Unique identifier, set once at construction |
| `Name` | `string` | `public` | Person's full name |
| `Age` | `int` | `public` | Person's age |
| `DisplayInformation()` | `abstract void` | `public` | Must be overridden by every subclass |
| `ToString()` | `override string` | `public` | Returns `"Name (ID: x, Age: y)"` |

**OOP Principle — Abstraction:**
`Person` captures what every person in the system *has* (ID, name, age) and what every person *must be able to do* (`DisplayInformation()`), without dictating *how* each specific type does it. This abstraction hides irrelevant details and forces a consistent interface.

**Note — Instantiation Restriction:**
Because `Person` is `abstract`, it cannot be instantiated with `new Person(...)`. This is intentional: within this system, every individual is *either* a `Student` *or* an `Instructor`. There is no meaningful way to construct a plain "person" without assigning a role, so the language itself enforces this constraint.

**Design Choice — Why Both `abstract class` AND `IIdentifiable`?**
The project prompt asks for *either* an abstract class or an interface for `Person`. This implementation uses **both**, for different purposes:

| Construct | Purpose |
|---|---|
| `abstract class Person` | Captures shared *human* attributes (`Name`, `Age`) and enforces the `DisplayInformation()` contract across all people in the system |
| `interface IIdentifiable` | Provides a minimal `ID` contract that allows `Collection<T>` to accept *any* identifiable entity — not just people — without requiring them to inherit from `Person` |

In other words: `Person` models the concept of a human role; `IIdentifiable` models the concept of something that can be stored and looked up in a generic collection. Separating these two concerns is the standard "program to an interface" best practice — `Course`, `SchoolClass`, and `Department` all implement `IIdentifiable` without needing any relation to `Person`.

---

### 2.2 Student
**File:** `Models/Student.cs`

Represents an enrolled student. Inherits all shared attributes from `Person` and adds course-enrolment behaviour.

| Member | Type | Notes |
|--------|------|-------|
| `StudentID` | `string` | Alias for `ID` |
| `CoursesEnrolled` | `List<string>` | Names of enrolled courses |
| `OnEnrollmentChanged` | `event` | Fires when a student enrolls or drops a course (Part 3 delegate) |
| `EnrollCourse(string)` | `void` | Adds a course; prevents duplicates |
| `DropCourse(string)` | `void` | Removes a course; warns if not enrolled |
| `DisplayInformation()` | `override void` | Prints student-specific details |

**OOP Principle — Encapsulation:**
`CoursesEnrolled` is a private backing list exposed only through controlled methods (`EnrollCourse`, `DropCourse`). No external code can arbitrarily add or remove items from the list — all changes go through the defined interface, protecting data integrity.

**OOP Principle — Inheritance:**
`Student` inherits `ID`, `Name`, and `Age` from `Person` and provides a concrete implementation of `DisplayInformation()`. It does not need to redeclare shared attributes.

---

### 2.3 Instructor
**File:** `Models/Instructor.cs`

Represents a teaching staff member. Inherits from `Person` and tracks the classes they teach.

| Member | Type | Notes |
|--------|------|-------|
| `InstructorID` | `string` | Alias for `ID` |
| `ClassesTaught` | `List<string>` | IDs of assigned classes |
| `AddClass(string)` | `void` | Assigns a class to this instructor |
| `RemoveClass(string)` | `void` | Unassigns a class |
| `DisplayInformation()` | `override void` | Prints instructor-specific details |

**OOP Principle — Polymorphism:**
Both `Student` and `Instructor` override `DisplayInformation()`. When either is stored in a `List<Person>` and `DisplayInformation()` is called on a `Person` reference, the C# runtime dispatches to the correct subclass implementation at runtime. The caller doesn't need to know which type it is — this is **runtime polymorphism**.

---

### 2.4 Course
**File:** `Models/Course.cs`

Represents an academic course offered by a department.

| Member | Type | Notes |
|--------|------|-------|
| `CourseID` | `string` | Primary key; set once at construction |
| `Name` | `string` | Full course name |
| `DepartmentID` | `string` | Foreign key to the owning department |
| `ID` | `string` | Implements `IIdentifiable`; exposes the read-only contract required by `Collection<T>` |

No behaviour methods are required in Part 1. The class is a pure data entity.

**OOP Principle — Encapsulation:**
`CourseID` (and by extension `ID`) is set once in the constructor and exposed only via a `get`-only property. External code can read a course's ID but can never overwrite it after construction. This prevents accidental mutation of primary keys.

---

### 2.5 SchoolClass
**File:** `Models/SchoolClass.cs`

A single class section, taught by one instructor, for one course, in one department.

| Member | Type | Notes |
|--------|------|-------|
| `ClassID` | `string` | Unique section identifier |
| `Name` | `string` | Section description |
| `InstructorID` | `string` | Assigned instructor |
| `DepartmentID` | `string` | Owning department |
| `CourseID` | `string` | The course being taught |
| `StudentIDs` | `List<string>` | Enrolled student IDs |
| `AddStudent(Student)` | `void` | Enrols a student in this section |
| `RemoveStudent(Student)` | `void` | Removes a student |
| `DisplayInformation()` | `void` | Prints section details |

> **Naming Note:** The project prompt specifies a class named `Class`. In C#, `class` is a reserved keyword, so using it as an identifier causes a compiler error. This class was therefore named `SchoolClass` to avoid the naming conflict while preserving the intended meaning. All documentation and diagrams reflect this deliberate choice.

**OOP Principle — Encapsulation:**
`StudentIDs` stores only string IDs rather than live `Student` references. This loose coupling prevents tight dependency chains — SchoolClass does not need to know the internal structure of Student.

---

### 2.6 Department
**File:** `Models/Department.cs`

Represents an academic department that organises and offers classes.

| Member | Type | Notes |
|--------|------|-------|
| `DepartmentID` | `string` | Unique identifier |
| `Name` | `string` | Department name |
| `ClassIDs` | `List<string>` | Class sections offered |
| `AddClass(SchoolClass)` | `void` | Registers a class with the department |
| `RemoveClass(SchoolClass)` | `void` | Removes a class |
| `DisplayInformation()` | `void` | Prints department details |

---

## 3. Class Relationships

![UML Class Diagram](UML_ClassDiagram.svg)

```
Person (abstract)
  ├── Student        [Inheritance]
  └── Instructor     [Inheritance]

Department  ──(offers)──►  SchoolClass  ──(teaches)──►  Course
Student     ──(enrolls)──►  Course
Instructor  ──(assigned to)──►  SchoolClass
```

---

## 4. OOP Principles Summary

| Principle | Where Applied |
|-----------|--------------|
| **Encapsulation** | Private backing fields throughout; `CoursesEnrolled` only modified via `EnrollCourse`/`DropCourse`; all entity IDs (`CourseID`, `ClassID`, `DepartmentID`) are `get`-only — set once at construction and immutable thereafter; `ID` in `Person` uses `protected set` so only the subclass constructor can assign it |
| **Inheritance** | `Student` and `Instructor` both inherit `ID`, `Name`, `Age`, and `ToString()` from `Person` without duplicating code |
| **Abstraction** | `Person` is abstract and cannot be instantiated directly — a "person" in this system must be either a `Student` or an `Instructor`; `IIdentifiable` abstracts the concept of "something with an ID" independently of the class hierarchy, enabling the generic `Collection<T>` |
| **Polymorphism** | A `List<Person>` holds both Students and Instructors; calling `p.DisplayInformation()` on each invokes the correct subclass method at runtime — no type-checking required |

---

## 5. Design Choices

- **`SchoolClass` stores `StudentIDs` (strings) not `Student` objects** — reduces coupling; the class roster doesn't break if a `Student` object is rebuilt or replaced.
- **Separate `Course` entity** — decouples the concept of "a subject" from "a scheduled section". One course (e.g. CS101) can have multiple `SchoolClass` sections.
- **`Person` as abstract class, not interface** — because Students and Instructors share *state* (ID, Name, Age) and a default `ToString()`. An interface wouldn't allow shared implementation; an abstract class was the right tool.
- **`IIdentifiable` interface** — introduced in Part 3 to allow the generic `Collection<T>` to work across all entity types without knowing their concrete types.
