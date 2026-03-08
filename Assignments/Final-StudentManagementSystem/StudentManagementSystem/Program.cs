using System;
using System.Linq;
using StudentManagementSystem.Collections;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

// ============================================================
//  Fairview County College – Student Management System
//  Main entry point – exercises all four project parts.
// ============================================================

namespace StudentManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================================");
            Console.WriteLine("   Fairview County College – Student Management System  ");
            Console.WriteLine("========================================================\n");

            // ── Shared repository ─────────────────────────────────────────────
            var repo = new DataRepository();

            // Wire up collection-change logging (Part 3 – delegates/events)
            repo.Students.OnCollectionChanged    += (action, id) => Console.WriteLine($"  [Event] Student    {action}: {id}");
            repo.Instructors.OnCollectionChanged += (action, id) => Console.WriteLine($"  [Event] Instructor {action}: {id}");
            repo.Courses.OnCollectionChanged     += (action, id) => Console.WriteLine($"  [Event] Course     {action}: {id}");
            repo.Departments.OnCollectionChanged += (action, id) => Console.WriteLine($"  [Event] Dept       {action}: {id}");
            repo.Classes.OnCollectionChanged     += (action, id) => Console.WriteLine($"  [Event] Class      {action}: {id}");

            DemoPart1(repo);
            DemoPart2(repo);
            DemoPart3(repo);
            DemoPart4(repo);

            Console.WriteLine("\n========================================================");
            Console.WriteLine("   All demonstrations complete. Press any key to exit.  ");
            Console.WriteLine("========================================================");
            Console.ReadKey();
        }

        // ────────────────────────────────────────────────────────────────────
        //  PART 1 – Basic System Design
        // ────────────────────────────────────────────────────────────────────
        static void DemoPart1(DataRepository repo)
        {
            PrintHeader("PART 1 – Basic System Design");

            // ── Courses ───────────────────────────────────────────────────────
            var cOOP  = new Course("CS101", "Object-Oriented Programming", "CS");
            var cDB   = new Course("CS202", "Database Systems",            "CS");
            var cMath = new Course("MA101", "Calculus I",                  "MA");
            repo.Courses.Add(cOOP);
            repo.Courses.Add(cDB);
            repo.Courses.Add(cMath);

            // ── Departments ───────────────────────────────────────────────────
            var deptCS = new Department("CS", "Computer Science");
            var deptMA = new Department("MA", "Mathematics");
            repo.Departments.Add(deptCS);
            repo.Departments.Add(deptMA);

            // ── Instructors ───────────────────────────────────────────────────
            var instAlice = new Instructor("I001", "Alice Thompson", 45);
            var instBob   = new Instructor("I002", "Bob Nguyen",     38);
            instAlice.AddClass("CS101-A");
            instBob.AddClass("MA101-A");
            repo.Instructors.Add(instAlice);
            repo.Instructors.Add(instBob);

            // ── School Classes ────────────────────────────────────────────────
            var classCS = new SchoolClass("CS101-A", "OOP – Section A",       "I001", "CS", "CS101");
            var classMA = new SchoolClass("MA101-A", "Calculus – Section A",   "I002", "MA", "MA101");
            repo.Classes.Add(classCS);
            repo.Classes.Add(classMA);
            deptCS.AddClass(classCS);
            deptMA.AddClass(classMA);

            // ── Students ──────────────────────────────────────────────────────
            var s1 = new Student("S001", "Emma Davis",   20);
            var s2 = new Student("S002", "Liam Park",    22);
            var s3 = new Student("S003", "Sofia Patel",  19);
            var s4 = new Student("S004", "Noah Kim",     24);
            var s5 = new Student("S005", "Olivia Brown", 21);

            // Wire enrollment event per student (Part 3 – delegates/events)
            foreach (var s in new[] { s1, s2, s3, s4, s5 })
                s.OnEnrollmentChanged += (name, course, action) =>
                    Console.WriteLine($"  [Enroll Event] {name} {action} '{course}'");

            s1.EnrollCourse("Object-Oriented Programming");
            s1.EnrollCourse("Database Systems");
            s2.EnrollCourse("Object-Oriented Programming");
            s2.EnrollCourse("Calculus I");
            s3.EnrollCourse("Calculus I");
            s4.EnrollCourse("Database Systems");
            s5.EnrollCourse("Object-Oriented Programming");
            s5.EnrollCourse("Calculus I");

            classCS.AddStudent(s1);
            classCS.AddStudent(s2);
            classCS.AddStudent(s5);
            classMA.AddStudent(s2);
            classMA.AddStudent(s3);

            repo.Students.Add(s1);
            repo.Students.Add(s2);
            repo.Students.Add(s3);
            repo.Students.Add(s4);
            repo.Students.Add(s5);

            // ── Display Information ───────────────────────────────────────────
            Console.WriteLine("\n--- Student Information ---");
            foreach (var s in repo.Students.GetAll())
            {
                s.DisplayInformation();
                Console.WriteLine();
            }

            Console.WriteLine("--- Instructor Information ---");
            foreach (var i in repo.Instructors.GetAll())
            {
                i.DisplayInformation();
                Console.WriteLine();
            }

            Console.WriteLine("--- Department Information ---");
            foreach (var d in repo.Departments.GetAll())
            {
                d.DisplayInformation();
                Console.WriteLine();
            }

            Console.WriteLine("--- Class Information ---");
            foreach (var c in repo.Classes.GetAll())
            {
                c.DisplayInformation();
                Console.WriteLine();
            }

            // Drop a course demo
            Console.WriteLine("--- Drop Course Demo ---");
            s3.DropCourse("Calculus I");
            Console.WriteLine($"  {s3.Name} courses after drop: {(s3.CoursesEnrolled.Count == 0 ? "None" : string.Join(", ", s3.CoursesEnrolled))}\n");
        }

        // ────────────────────────────────────────────────────────────────────
        //  PART 2 – Abstraction & Polymorphism
        // ────────────────────────────────────────────────────────────────────
        static void DemoPart2(DataRepository repo)
        {
            PrintHeader("PART 2 – Abstraction & Polymorphism");

            // Build a mixed list of Person objects – demonstrates polymorphism
            var people = new System.Collections.Generic.List<Person>();
            foreach (var s in repo.Students.GetAll())    people.Add(s);
            foreach (var i in repo.Instructors.GetAll()) people.Add(i);

            Console.WriteLine($"--- Polymorphic DisplayInformation() on {people.Count} Person objects ---");
            foreach (Person p in people)
            {
                Console.WriteLine($"\n  [{p.GetType().Name}]");
                p.DisplayInformation();   // runtime polymorphism – correct override called
            }

            Console.WriteLine("\n--- Polymorphic ToString() ---");
            foreach (Person p in people)
                Console.WriteLine($"  {p}");
        }

        // ────────────────────────────────────────────────────────────────────
        //  PART 3 – Generics & Delegates
        // ────────────────────────────────────────────────────────────────────
        static void DemoPart3(DataRepository repo)
        {
            PrintHeader("PART 3 – Generics & Delegates");

            // Generic Collection<T> search predicate
            Console.WriteLine("--- Generic Search: students whose name contains 'a' ---");
            var matches = repo.Students.Search(
                s => s.Name.Contains("a", StringComparison.OrdinalIgnoreCase));
            foreach (var s in matches)
                Console.WriteLine($"  Found: {s}");

            Console.WriteLine("\n--- Generic FindByID demo ---");
            var found = repo.Students.FindByID("S003");
            Console.WriteLine($"  FindByID('S003'): {(found != null ? found.ToString() : "not found")}");

            Console.WriteLine("\n--- Remove from generic collection (fires delegate event) ---");
            repo.Students.Remove("S004");
            Console.WriteLine($"  Students remaining: {repo.Students.Count}");

            Console.WriteLine("\n--- Re-add S004 ---");
            repo.Students.Add(new Student("S004", "Noah Kim", 24));
        }

        // ────────────────────────────────────────────────────────────────────
        //  PART 4 – LINQ and Data Manipulation
        // ────────────────────────────────────────────────────────────────────
        static void DemoPart4(DataRepository repo)
        {
            PrintHeader("PART 4 – LINQ & Data Manipulation");

            // Filter: students older than 20
            Console.WriteLine("--- LINQ Filter: students older than 20 ---");
            foreach (var s in repo.GetStudentsOlderThan(20))
                Console.WriteLine($"  {s.Name}, Age {s.Age}");

            // Filter: enrolled in a specific course
            Console.WriteLine("\n--- LINQ Filter: enrolled in 'Object-Oriented Programming' ---");
            foreach (var s in repo.GetStudentsEnrolledIn("Object-Oriented Programming"))
                Console.WriteLine($"  {s.Name}");

            // Sort by name
            Console.WriteLine("\n--- LINQ Sort: students by name ---");
            foreach (var s in repo.GetStudentsSortedByName())
                Console.WriteLine($"  {s.Name}");

            // Sort by age
            Console.WriteLine("\n--- LINQ Sort: students by age ---");
            foreach (var s in repo.GetStudentsSortedByAge())
                Console.WriteLine($"  {s.Name}, Age {s.Age}");

            // Group by course count
            Console.WriteLine("\n--- LINQ Group: by number of courses enrolled ---");
            foreach (var group in repo.GetStudentsGroupedByCourseCount())
                Console.WriteLine($"  [{group.Key} course(s)]: {string.Join(", ", group.Select(s => s.Name))}");

            // Search by partial name
            Console.WriteLine("\n--- LINQ Search: name contains 'o' ---");
            foreach (var s in repo.SearchStudentsByName("o"))
                Console.WriteLine($"  {s.Name}");

            // Courses by department
            Console.WriteLine("\n--- LINQ Filter: courses in 'CS' department ---");
            foreach (var c in repo.GetCoursesByDepartment("CS"))
                Console.WriteLine($"  {c}");

            // Persistence – save
            Console.WriteLine("\n--- Data Persistence: save to CSV files ---");
            repo.SaveStudentsToFile("students.csv");
            repo.SaveInstructorsToFile("instructors.csv");

            // Persistence – reload
            Console.WriteLine("\n--- Data Persistence: reload students from CSV ---");
            var freshRepo = new DataRepository();
            freshRepo.LoadStudentsFromFile("students.csv");
            Console.WriteLine("  Loaded students:");
            foreach (var s in freshRepo.Students.GetAll())
                Console.WriteLine($"    {s.Name} – Courses: {string.Join(", ", s.CoursesEnrolled)}");
        }

        // ── Helper ────────────────────────────────────────────────────────────
        static void PrintHeader(string title)
        {
            Console.WriteLine($"\n{"─",1}{"─",54}");
            Console.WriteLine($"  {title}");
            Console.WriteLine($"{"─",1}{"─",54}");
        }
    }
}
