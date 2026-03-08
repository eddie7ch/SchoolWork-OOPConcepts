using StudentManagementSystem.Collections;

namespace StudentManagementSystem.Models
{
    /// <summary>
    /// Represents an academic course offered by a department.
    /// Part 1: Basic System Design
    /// </summary>
    public class Course : IIdentifiable
    {
        // IIdentifiable
        public string ID => CourseID;
        // ── Attributes ────────────────────────────────────────────────────────
        public string CourseID { get; private set; }
        public string Name { get; set; }
        public string DepartmentID { get; set; }

        // ── Constructor ───────────────────────────────────────────────────────
        public Course(string courseID, string name, string departmentID)
        {
            CourseID     = courseID;
            Name         = name;
            DepartmentID = departmentID;
        }

        public override string ToString() => $"[Course] {CourseID} – {Name} (Dept: {DepartmentID})";
    }
}
