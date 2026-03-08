using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StudentManagementSystem.Collections;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    // ============================================================
    //  Part 4 – LINQ and Data Manipulation
    //  DataRepository: a central store that exposes full LINQ
    //  querying, sorting, filtering, and file-based persistence.
    // ============================================================

    /// <summary>
    /// Central repository for all system entities.
    /// Wraps generic Collection<T> instances and adds LINQ-powered
    /// query methods plus CSV file persistence.
    /// </summary>
    public class DataRepository
    {
        // ── Generic Collections (Part 3) ──────────────────────────────────────
        public Collection<Student>    Students    { get; } = new();
        public Collection<Instructor> Instructors { get; } = new();
        public Collection<Department> Departments { get; } = new();
        public Collection<SchoolClass> Classes   { get; } = new();
        public Collection<Course>     Courses    { get; } = new();

        // ── LINQ Queries (Part 4) ─────────────────────────────────────────────

        /// <summary>Filter students by minimum age using LINQ.</summary>
        public IEnumerable<Student> GetStudentsOlderThan(int age) =>
            Students.GetAll().Where(s => s.Age > age);

        /// <summary>Filter students enrolled in a specific course using LINQ.</summary>
        public IEnumerable<Student> GetStudentsEnrolledIn(string courseName) =>
            Students.GetAll()
                    .Where(s => s.CoursesEnrolled
                                 .Contains(courseName, StringComparer.OrdinalIgnoreCase));

        /// <summary>Sort all students alphabetically by name using LINQ.</summary>
        public IEnumerable<Student> GetStudentsSortedByName() =>
            Students.GetAll().OrderBy(s => s.Name);

        /// <summary>Sort all students by age using LINQ.</summary>
        public IEnumerable<Student> GetStudentsSortedByAge() =>
            Students.GetAll().OrderBy(s => s.Age);

        /// <summary>Group students by how many courses they are enrolled in.</summary>
        public IEnumerable<IGrouping<int, Student>> GetStudentsGroupedByCourseCount() =>
            Students.GetAll().GroupBy(s => s.CoursesEnrolled.Count);

        /// <summary>Search students by partial name (case-insensitive) using LINQ.</summary>
        public IEnumerable<Student> SearchStudentsByName(string partialName) =>
            Students.GetAll()
                    .Where(s => s.Name.Contains(partialName, StringComparison.OrdinalIgnoreCase));

        /// <summary>Get all courses offered by a given department using LINQ.</summary>
        public IEnumerable<Course> GetCoursesByDepartment(string departmentID) =>
            Courses.GetAll()
                   .Where(c => c.DepartmentID.Equals(departmentID, StringComparison.OrdinalIgnoreCase));

        // ── File Persistence (Part 4) ─────────────────────────────────────────

        private const string StudentsFile    = "students.csv";
        private const string InstructorsFile = "instructors.csv";

        /// <summary>Persist all students to a CSV file.</summary>
        public void SaveStudentsToFile(string path = StudentsFile)
        {
            var lines = new List<string> { "StudentID,Name,Age,CoursesEnrolled" };
            foreach (var s in Students.GetAll())
                lines.Add($"{s.StudentID},{EscapeCsv(s.Name)},{s.Age},{EscapeCsv(string.Join("|", s.CoursesEnrolled))}");
            File.WriteAllLines(path, lines);
            Console.WriteLine($"  [Persistence] Students saved to '{path}' ({Students.Count} records).");
        }

        /// <summary>Load students from a CSV file, replacing the current collection.</summary>
        public void LoadStudentsFromFile(string path = StudentsFile)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"  [Persistence] File '{path}' not found.");
                return;
            }
            var lines = File.ReadAllLines(path).Skip(1); // skip header
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length < 4) continue;
                var student = new Student(parts[0], parts[1], int.Parse(parts[2]));
                if (!string.IsNullOrWhiteSpace(parts[3]))
                    foreach (var course in parts[3].Split('|'))
                        student.EnrollCourse(course);
                Students.Add(student);
            }
            Console.WriteLine($"  [Persistence] Loaded {Students.Count} students from '{path}'.");
        }

        /// <summary>Persist all instructors to a CSV file.</summary>
        public void SaveInstructorsToFile(string path = InstructorsFile)
        {
            var lines = new List<string> { "InstructorID,Name,Age,ClassesTaught" };
            foreach (var i in Instructors.GetAll())
                lines.Add($"{i.InstructorID},{EscapeCsv(i.Name)},{i.Age},{EscapeCsv(string.Join("|", i.ClassesTaught))}");
            File.WriteAllLines(path, lines);
            Console.WriteLine($"  [Persistence] Instructors saved to '{path}' ({Instructors.Count} records).");
        }

        private static string EscapeCsv(string value) =>
            value.Contains(',') ? $"\"{value}\"" : value;
    }
}
