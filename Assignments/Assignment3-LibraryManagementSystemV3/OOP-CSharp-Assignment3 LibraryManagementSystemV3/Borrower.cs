namespace OOP_CSharp_Assignment3_LibraryManagementSystemV3
{
    /// <summary>
    /// Represents a library borrower.
    /// Follows the Single Responsibility Principle � encapsulates only borrower-related state.
    /// </summary>
    public class Borrower
    {
        /// <summary>Maximum number of media items a borrower may have checked out simultaneously.</summary>
        public const int MaxBorrowLimit = 5;

        /// <summary>Unique identifier of the borrower.</summary>
        public int Id { get; set; }

        /// <summary>Full name of the borrower.</summary>
        public string Name { get; set; }

        /// <summary>Postal address of the borrower.</summary>
        public string Address { get; set; }

        /// <summary>Contact phone number of the borrower.</summary>
        public string ContactNumber { get; set; }

        /// <summary>Email address of the borrower.</summary>
        public string Email { get; set; }

        /// <summary>List of media items currently borrowed by this borrower.</summary>
        public List<Media> BorrowedMedia { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Borrower"/> class.
        /// </summary>
        public Borrower(int id, string name, string address, string contactNumber, string email)
        {
            Id = id;
            Name = name;
            Address = address;
            ContactNumber = contactNumber;
            Email = email;
            BorrowedMedia = new List<Media>();
        }

        /// <summary>
        /// Indicates whether the borrower has reached their borrowing limit.
        /// </summary>
        public bool HasReachedBorrowLimit() => BorrowedMedia.Count >= MaxBorrowLimit;

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[Borrower] Id: {Id} | Name: {Name} | Address: {Address} | " +
                   $"Phone: {ContactNumber} | Email: {Email} | Items borrowed: {BorrowedMedia.Count}/{MaxBorrowLimit}";
        }
    }
}
