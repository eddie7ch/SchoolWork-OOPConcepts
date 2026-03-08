namespace Library_Management_System_v2._0
{
    /// <summary>
    /// Entry-point class that demonstrates every feature of the Library Management System v2.0.
    /// </summary>
    internal class LibraryApp
    {
        static void Main(string[] args)
        {
            Library library = new();

            // ── 1. Add Books ───────────────────────────────────────────────
            PrintHeader("1. Adding Books");
            Book book1 = new("The Great Gatsby",      "F. Scott Fitzgerald", "Classic");
            Book book2 = new("To Kill a Mockingbird", "Harper Lee",          "Fiction");
            Book book3 = new("1984",                  "George Orwell",       "Dystopian");
            library.AddBook(book1);
            library.AddBook(book2);
            library.AddBook(book3);

            // ── 2. Add DVDs ────────────────────────────────────────────────
            PrintHeader("2. Adding DVDs");
            DVD dvd1 = new("Inception",    "Christopher Nolan", 148);
            DVD dvd2 = new("Interstellar", "Christopher Nolan", 169);
            DVD dvd3 = new("The Matrix",   "The Wachowskis",    136);
            library.AddDVD(dvd1);
            library.AddDVD(dvd2);
            library.AddDVD(dvd3);

            // ── 3. Register Borrowers ──────────────────────────────────────
            PrintHeader("3. Registering Borrowers");
            Borrower borrower1 = new("Alice Johnson", "123 Maple St",  "403-555-0101", "alice@email.com");
            Borrower borrower2 = new("Bob Smith",     "456 Oak Ave",   "403-555-0202", "bob@email.com");
            Borrower borrower3 = new("Carol White",   "789 Pine Blvd", "403-555-0303", "carol@email.com");
            library.AddBorrower(borrower1);
            library.AddBorrower(borrower2);
            library.AddBorrower(borrower3);

            // ── 4. Display Inventory & Borrowers ──────────────────────────
            PrintHeader("4. Full Inventory & Borrower List");
            library.DisplayInventory();
            library.DisplayBorrowers();

            // ── 5. Borrow Media ────────────────────────────────────────────
            PrintHeader("5. Borrowing Media Items");
            library.BorrowMedia(book1.MediaId, borrower1.BorrowerId);  // Alice borrows Gatsby
            library.BorrowMedia(dvd1.MediaId,  borrower2.BorrowerId);  // Bob borrows Inception
            library.BorrowMedia(book3.MediaId,  borrower3.BorrowerId); // Carol borrows 1984

            // Attempt to borrow an already-borrowed item
            Console.WriteLine("\n  Attempting to borrow an already-borrowed book:");
            library.BorrowMedia(book1.MediaId, borrower2.BorrowerId);

            // ── 6. Display Updated Inventory ──────────────────────────────
            PrintHeader("6. Inventory After Borrowing");
            library.DisplayInventory();

            // ── 7. Return Media ────────────────────────────────────────────
            PrintHeader("7. Returning Media Items");
            library.ReturnMedia(book1.MediaId);
            library.ReturnMedia(dvd1.MediaId);

            // Attempt to return a non-borrowed item
            Console.WriteLine("\n  Attempting to return a non-borrowed book:");
            library.ReturnMedia(book2.MediaId);

            // ── 8. Delegate-based Search ───────────────────────────────────
            PrintHeader("8. Searching Media with Delegates");

            // 8a. Search by title keyword
            Console.WriteLine("  Search by title containing \"the\" (case-insensitive):");
            MediaSearchCriteria byTitle =
                media => media.Title.Contains("the", StringComparison.OrdinalIgnoreCase);
            PrintSearchResults(library.SearchMedia(byTitle));

            // 8b. Search by author
            Console.WriteLine("\n  Search books by author \"George Orwell\":");
            MediaSearchCriteria byAuthor =
                media => media is Book book && book.Author.Equals("George Orwell", StringComparison.OrdinalIgnoreCase);
            PrintSearchResults(library.SearchMedia(byAuthor));

            // 8c. Search by genre
            Console.WriteLine("\n  Search books by genre \"Fiction\":");
            MediaSearchCriteria byGenre =
                media => media is Book book && book.Genre.Equals("Fiction", StringComparison.OrdinalIgnoreCase);
            PrintSearchResults(library.SearchMedia(byGenre));

            // 8d. Search DVDs by director
            Console.WriteLine("\n  Search DVDs by director \"Christopher Nolan\":");
            MediaSearchCriteria byDirector =
                media => media is DVD dvd && dvd.Director.Equals("Christopher Nolan", StringComparison.OrdinalIgnoreCase);
            PrintSearchResults(library.SearchMedia(byDirector));

            // 8e. Search only available items
            Console.WriteLine("\n  Search all currently available media:");
            MediaSearchCriteria availableOnly = media => media.IsAvailable;
            PrintSearchResults(library.SearchMedia(availableOnly));

            // ── 9. Remove Media & Borrower ─────────────────────────────────
            PrintHeader("9. Removing a Book, a DVD, and a Borrower");
            library.RemoveBook(book2.MediaId);
            library.RemoveDVD(dvd3.MediaId);
            library.RemoveBorrower(borrower3.BorrowerId);

            // Non-existent ID
            library.RemoveBook(999);

            // ── 10. Final State ────────────────────────────────────────────
            PrintHeader("10. Final Inventory & Borrower List");
            library.DisplayInventory();
            library.DisplayBorrowers();

            Console.WriteLine("\nDone.");
        }

        // ── Helper methods ─────────────────────────────────────────────────
        /// <summary>Prints a formatted section header.</summary>
        private static void PrintHeader(string title)
        {
            Console.WriteLine($"\n{'=', -1}{'=', -1} {title} ");
            Console.WriteLine(new string('─', 60));
        }

        /// <summary>Prints a list of search results, or a no-results message.</summary>
        private static void PrintSearchResults(List<Media> results)
        {
            if (results.Count == 0)
            {
                Console.WriteLine("    No results found.");
                return;
            }
            foreach (Media m in results)
                Console.WriteLine($"    {m.GetDetails()}");
        }
    }
}
