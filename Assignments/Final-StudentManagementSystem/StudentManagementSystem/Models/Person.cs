// ============================================================
//  Part 2 – Abstraction & Polymorphism
//  Abstract base class "Person" for all people in the system.
// ============================================================
using StudentManagementSystem.Collections;

namespace StudentManagementSystem.Models
{
    /// <summary>
    /// Abstract base class representing any person in the system.
    /// Enforces a common contract for all concrete person types.
    /// Implements IIdentifiable so Person subtypes can be stored in Collection<T>.
    /// </summary>
    public abstract class Person : IIdentifiable
    {
        // ── Shared attributes ─────────────────────────────────────────────────
        public string ID   { get; protected set; }
        public string Name { get; set; }
        public int    Age  { get; set; }

        protected Person(string id, string name, int age)
        {
            ID   = id;
            Name = name;
            Age  = age;
        }

        /// <summary>
        /// Every person type must be able to display its own information.
        /// Demonstrates polymorphism – caller uses Person reference, correct
        /// override is invoked at runtime.
        /// </summary>
        public abstract void DisplayInformation();

        public override string ToString() => $"{Name} (ID: {ID}, Age: {Age})";
    }
}
