namespace Library_Management_System_v2._0
{
    /// <summary>
    /// Interface that defines borrowing and returning behaviour for media items.
    /// </summary>
    public interface IBorrowable
    {
        /// <summary>Marks the media item as borrowed and records the borrower.</summary>
        /// <param name="borrower">The borrower checking out the item.</param>
        void Borrow(Borrower borrower);

        /// <summary>Marks the media item as returned and clears the borrower.</summary>
        void Return();
    }
}
