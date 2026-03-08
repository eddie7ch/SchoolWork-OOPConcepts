using System;
using System.Collections.Generic;
using StudentManagementSystem.Collections;

namespace StudentManagementSystem.Models
{
    /// <summary>
    /// Represents an academic department that offers multiple classes.
    /// Part 1 – basic design.
    /// </summary>
    public class Department : IIdentifiable
    {
        // IIdentifiable
        public string ID => DepartmentID;
        // ── Attributes ────────────────────────────────────────────────────────
        public string DepartmentID   { get; private set; }
        public string Name           { get; set; }
        public List<string> ClassIDs { get; private set; } = new();

        // ── Constructor ───────────────────────────────────────────────────────
        public Department(string departmentID, string name)
        {
            DepartmentID = departmentID;
            Name         = name;
        }

        // ── Methods ───────────────────────────────────────────────────────────

        public void AddClass(SchoolClass schoolClass)
        {
            if (!ClassIDs.Contains(schoolClass.ClassID))
                ClassIDs.Add(schoolClass.ClassID);
        }

        public void RemoveClass(SchoolClass schoolClass) => ClassIDs.Remove(schoolClass.ClassID);

        public void DisplayInformation()
        {
            Console.WriteLine($"  Department ID : {DepartmentID}");
            Console.WriteLine($"  Name          : {Name}");
            Console.WriteLine($"  Classes       : {(ClassIDs.Count == 0 ? "None" : string.Join(", ", ClassIDs))}");
        }
    }
}
