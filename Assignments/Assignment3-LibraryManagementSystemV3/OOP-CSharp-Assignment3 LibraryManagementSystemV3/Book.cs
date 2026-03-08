namespace OOP_CSharp_Assignment3_LibraryManagementSystemV3
{
    /// <summary>
    /// Represents a book in the library.
    /// Inherits from <see cref="Media"/> and implements <see cref="IBorrowable"/>.
    /// </summary>
    public class Book : Media, IBorrowable
    {
        /// <summary>Author of the book.</summary>
        public string Author { get; set; }

        /// <summary>Genre of the book.</summary>
        public string Genre { get; set; }

        /// <summary>The borrower who currently has this book checked out, or null if available.</summary>
        public Borrower? CurrentBorrower { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <param name="title">Title of the book.</param>
        /// <param name="author">Author of the book.</param>
        /// <param name="genre">Genre of the book.</param>
        public Book(int id, string title, string author, string genre)
            : base(id, title)
        {
            Author = author;
            Genre = genre;
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
            return $"[Book]  Id: {Id} | Title: \"{Title}\" | Author: {Author} | Genre: {Genre} | Status: {status}";
        }
    }
}
