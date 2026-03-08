namespace PayrollSystem.Models
{
    /// <summary>
    /// Abstract base class representing a person.
    /// Demonstrates encapsulation via private fields with public properties.
    /// </summary>
    public abstract class Person
    {
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string? _email;
        private string? _phoneNumber;

        public string FirstName
        {
            get => _firstName;
            set => _firstName = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("First name cannot be empty.");
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Last name cannot be empty.");
        }

        public string? Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? Email
        {
            get => _email;
            set => _email = value;
        }

        public string? PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }

        protected Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string GetFullName() => $"{FirstName} {LastName}";

        public override string ToString() => $"{GetFullName()} | DOB: {DateOfBirth:yyyy-MM-dd}";
    }
}
