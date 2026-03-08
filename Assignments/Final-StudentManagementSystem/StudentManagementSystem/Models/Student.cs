using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    /// <summary>
    /// Concrete Student class.
    /// Part 1 – basic student model.
    /// Part 2 – inherits from Person (abstraction + polymorphism).
    /// </summary>
    public class Student : Person
    {
        // ── Attributes ────────────────────────────────────────────────────────
        public string StudentID => ID;                        // alias for clarity
        public List<string> CoursesEnrolled { get; private set; } = new();

        // ── Part 3: Delegates / Events ────────────────────────────────────────
        public delegate void EnrollmentChangedHandler(string studentName, string courseName, string action);
        public event EnrollmentChangedHandler? OnEnrollmentChanged;

        // ── Constructor ───────────────────────────────────────────────────────
        public Student(string studentID, string name, int age)
            : base(studentID, name, age) { }

        // ── Methods ───────────────────────────────────────────────────────────

        /// <summary>Enroll the student in a course.</summary>
        public void EnrollCourse(string courseName)
        {
            if (CoursesEnrolled.Contains(courseName))
            {
                Console.WriteLine($"  [Warning] {Name} is already enrolled in '{courseName}'.");
                return;
            }
            CoursesEnrolled.Add(courseName);
            OnEnrollmentChanged?.Invoke(Name, courseName, "enrolled in");
        }

        /// <summary>Drop a course the student is enrolled in.</summary>
        public void DropCourse(string courseName)
        {
            if (!CoursesEnrolled.Remove(courseName))
            {
                Console.WriteLine($"  [Warning] {Name} is not enrolled in '{courseName}'.");
                return;
            }
            OnEnrollmentChanged?.Invoke(Name, courseName, "dropped");
        }

        /// <summary>Print student details to the console (Part 1 + Part 2 polymorphism).</summary>
        public override void DisplayInformation()
        {
            Console.WriteLine($"  Student ID : {StudentID}");
            Console.WriteLine($"  Name       : {Name}");
            Console.WriteLine($"  Age        : {Age}");
            Console.WriteLine($"  Enrolled   : {(CoursesEnrolled.Count == 0 ? "None" : string.Join(", ", CoursesEnrolled))}");
        }
    }
}
