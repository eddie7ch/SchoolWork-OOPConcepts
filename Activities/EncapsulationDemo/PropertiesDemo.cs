// ============================================================
// PROPERTIES DEMO
// ============================================================
// This file demonstrates how C# properties wrap private fields
// to achieve encapsulation. Properties look like public fields
// to callers but can contain validation or access control logic.
// Three property types are shown:
//   1. Read-only  — caller can GET but never SET
//   2. Write-only — caller can SET but never GET
//   3. Read-write — caller can both GET and SET (with validation)
// ============================================================

namespace EncapsulationDemo
{
    public class StudentRecord
    {
        // ── Private backing fields ────────────────────────────────────────────
        // These fields are hidden from outside code. All access goes through
        // the properties below, giving this class full control over its data.

        private readonly string _studentId;   // set once at construction, never changes
        private string _password;             // sensitive — write-only access from outside
        private int _grade;                   // validated on write; readable on read

        // ── Constructor ───────────────────────────────────────────────────────
        public StudentRecord(string studentId, string initialPassword, int grade)
        {
            _studentId = studentId;
            _password  = initialPassword;
            _grade     = grade;
        }

        // ── 1. READ-ONLY PROPERTY ─────────────────────────────────────────────
        // Only a public getter is declared; the setter is omitted entirely.
        // The backing field _studentId is readonly, so it can only be set in
        // the constructor — a common pattern for immutable identity values.
        // Encapsulation benefit: once a student ID is assigned it can never be
        // changed by external code, preventing accidental or malicious tampering.
        public string StudentId
        {
            get { return _studentId; }
            // No setter — this property is read-only from outside the class.
        }

        // ── 2. WRITE-ONLY PROPERTY ────────────────────────────────────────────
        // Only a public setter is declared; the getter is omitted entirely.
        // External code can change the password, but it can never read it back.
        // Encapsulation benefit: sensitive data (passwords, PINs) is never
        // exposed through the API, reducing the risk of information leakage.
        public string Password
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 6)
                    throw new ArgumentException("Password must be at least 6 characters.");
                _password = value;
            }
            // No getter — this property is write-only from outside the class.
        }

        // ── 3. READ-WRITE PROPERTY ────────────────────────────────────────────
        // Both getter and setter are exposed, but the setter includes validation
        // logic. This lets external code read and update the grade while ensuring
        // the value is always within a meaningful range (0–100).
        // Encapsulation benefit: business rules live inside the class; callers
        // cannot store an invalid grade even if they try.
        public int Grade
        {
            get { return _grade; }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(nameof(value),
                        "Grade must be between 0 and 100.");
                _grade = value;
            }
        }

        // Helper to verify a supplied password without exposing the stored one.
        // This is the controlled access pattern enabled by write-only encapsulation.
        public bool VerifyPassword(string candidate)
        {
            return _password == candidate;
        }

        public void DisplayInfo()
        {
            // Note: _password is intentionally NOT printed — encapsulation in action.
            Console.WriteLine($"  Student ID : {StudentId}");
            Console.WriteLine($"  Grade      : {Grade}");
            Console.WriteLine($"  Password   : [hidden — write-only]");
        }
    }
}
