using System;
using System.Collections.Generic;
using StudentManagementSystem.Collections;

namespace StudentManagementSystem.Models
{
    /// <summary>
    /// Represents a class (section) run by an instructor for a specific course.
    /// Note: named "SchoolClass" to avoid collision with the C# keyword "class".
    /// Part 1 – basic design.
    /// </summary>
    public class SchoolClass : IIdentifiable
    {
        // IIdentifiable – expose ClassID as ID
        public string ID => ClassID;
        // ── Attributes ────────────────────────────────────────────────────────
        public string ClassID      { get; private set; }
        public string Name         { get; set; }
        public string InstructorID { get; set; }
        public string DepartmentID { get; set; }
        public string CourseID     { get; set; }
        public List<string> StudentIDs { get; private set; } = new();

        // ── Constructor ───────────────────────────────────────────────────────
        public SchoolClass(string classID, string name, string instructorID,
                           string departmentID, string courseID)
        {
            ClassID      = classID;
            Name         = name;
            InstructorID = instructorID;
            DepartmentID = departmentID;
            CourseID     = courseID;
        }

        // ── Methods ───────────────────────────────────────────────────────────

        public void AddStudent(Student student)
        {
            if (!StudentIDs.Contains(student.StudentID))
                StudentIDs.Add(student.StudentID);
        }

        public void RemoveStudent(Student student) => StudentIDs.Remove(student.StudentID);

        public void DisplayInformation()
        {
            Console.WriteLine($"  Class ID    : {ClassID}");
            Console.WriteLine($"  Name        : {Name}");
            Console.WriteLine($"  Instructor  : {InstructorID}");
            Console.WriteLine($"  Department  : {DepartmentID}");
            Console.WriteLine($"  Course      : {CourseID}");
            Console.WriteLine($"  Students    : {(StudentIDs.Count == 0 ? "None" : string.Join(", ", StudentIDs))}");
        }
    }
}
