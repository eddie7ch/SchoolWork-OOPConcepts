namespace OOP_CSharp_Assignment3_LibraryManagementSystemV3
{
    /// <summary>
    /// Defines borrowing and returning behaviour for media items.
    /// Follows the Interface Segregation Principle � clients implement only what they need.
    /// </summary>
    public interface IBorrowable
    {
        /// <summary>Marks the media item as borrowed and records the borrower.</summary>
        /// <param name="borrower">The borrower checking out this item.</param>
        void Borrow(Borrower borrower);

        /// <summary>Marks the media item as returned and clears the borrower record.</summary>
        void Return();
    }
}
