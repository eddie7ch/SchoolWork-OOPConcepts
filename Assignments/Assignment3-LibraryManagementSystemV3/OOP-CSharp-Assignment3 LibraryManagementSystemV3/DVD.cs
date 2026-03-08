namespace OOP_CSharp_Assignment3_LibraryManagementSystemV3
{
    /// <summary>
    /// Represents a DVD in the library.
    /// Inherits from <see cref="Media"/> and implements <see cref="IBorrowable"/>.
    /// </summary>
    public class DVD : Media, IBorrowable
    {
        /// <summary>Director of the DVD.</summary>
        public string Director { get; set; }

        /// <summary>Runtime of the DVD in minutes.</summary>
        public int RunTime { get; set; }

        /// <summary>The borrower who currently has this DVD checked out, or null if available.</summary>
        public Borrower? CurrentBorrower { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DVD"/> class.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <param name="title">Title of the DVD.</param>
        /// <param name="director">Director of the DVD.</param>
        /// <param name="runTime">Runtime in minutes.</param>
        public DVD(int id, string title, string director, int runTime)
            : base(id, title)
        {
            Director = director;
            RunTime = runTime;
        }

        /// <inheritdoc/>
        public void Borrow(Borrower borrower)
        {
            IsAvailable = false;
            CurrentBorrower = borrower;
        }

        /// <inheritdoc/>
        public void Return()
        {
            IsAvailable = true;
            CurrentBorrower = null;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            string status = IsAvailable ? "Available" : $"Borrowed by {CurrentBorrower?.Name}";
            return $"[DVD]   Id: {Id} | Title: \"{Title}\" | Director: {Director} | Runtime: {RunTime} min | Status: {status}";
        }
    }
}
