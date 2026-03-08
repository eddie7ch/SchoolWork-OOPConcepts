using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Models
{
    /// <summary>
    /// Concrete Instructor class.
    /// Part 1 – basic instructor model.
    /// Part 2 – inherits from Person (abstraction + polymorphism).
    /// </summary>
    public class Instructor : Person
    {
        // ── Attributes ────────────────────────────────────────────────────────
        public string InstructorID => ID;
        public List<string> ClassesTaught { get; private set; } = new();

        // ── Constructor ───────────────────────────────────────────────────────
        public Instructor(string instructorID, string name, int age)
            : base(instructorID, name, age) { }

        // ── Methods ───────────────────────────────────────────────────────────

        public void AddClass(string className)
        {
            if (!ClassesTaught.Contains(className))
                ClassesTaught.Add(className);
        }

        public void RemoveClass(string className) => ClassesTaught.Remove(className);

        public override void DisplayInformation()
        {
            Console.WriteLine($"  Instructor ID : {InstructorID}");
            Console.WriteLine($"  Name          : {Name}");
            Console.WriteLine($"  Age           : {Age}");
            Console.WriteLine($"  Classes Taught: {(ClassesTaught.Count == 0 ? "None" : string.Join(", ", ClassesTaught))}");
        }
    }
}
