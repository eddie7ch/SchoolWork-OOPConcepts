using System.Text;

namespace OOP_CSharp_Assignment3_LibraryManagementSystemV3
{
    /// <summary>
    /// Entry point and demonstration class for the Library Management System v3.
    /// Exercises all required features:
    ///   - Factory pattern (IMediaFactory / MediaFactory)
    ///   - Inheritance and polymorphism (Media -> Book, DVD)
    ///   - Interface implementation (IBorrowable)
    ///   - Delegation (MediaSearchDelegate)
    ///   - LINQ queries (inside Library and search strategies)
    ///   - Exception handling (try/catch for invalid operations)
    ///   - SOLID principles throughout
    /// </summary>
    internal class LibraryApp
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            IMediaFactory factory = new MediaFactory();
            Library library = new Library(factory);

            Console.WriteLine("==========================================================");
            Console.WriteLine("        LIBRARY MANAGEMENT SYSTEM  v3.0");
            Console.WriteLine("==========================================================");

            // ════════════════════════════════════════════════════════════════
            // 1. SEED MEDIA ITEMS
            // ════════════════════════════════════════════════════════════════
            Console.WriteLine("\n--- Adding Books ---");
            // Capture only the items whose IDs are used later; discard the rest
            Media book1 = library.AddMedia("book", "The Great Gatsby",        "F. Scott Fitzgerald", "Classical Literature");
            Media book2 = library.AddMedia("book", "To Kill a Mockingbird",   "Harper Lee",          "Classical Literature");
            Media book3 = library.AddMedia("book", "1984",                    "George Orwell",       "Dystopian Fiction");
            Media book4 = library.AddMedia("book", "Pride and Prejudice",     "Jane Austen",         "Classic Literature");
            Media book5 = library.AddMedia("book", "The Catcher in the Rye",  "J.D. Salinger",       "Coming-of-Age Fiction");
            Media book6 = library.AddMedia("book", "The Lord of the Rings",   "J.R.R. Tolkien",      "Fantasy");
            Media book7 = library.AddMedia("book", "To the Lighthouse",       "Virginia Woolf",      "Modernist Literature");
            Console.WriteLine("  7 books added.");

            Console.WriteLine("\n--- Adding DVDs ---");
            Media dvd1 = library.AddMedia("dvd", "Inception",                "Christopher Nolan", "148");
            Media dvd2 = library.AddMedia("dvd", "The Shawshank Redemption", "Frank Darabont",    "142");
            Console.WriteLine("  2 DVDs added.");

            // ════════════════════════════════════════════════════════════════
            // 2. SEED BORROWERS
            // ════════════════════════════════════════════════════════════════
            Console.WriteLine("\n--- Adding Borrowers ---");
            Borrower borrower1 = library.AddBorrower("John Doe",   "124 Main St.", "456-123-7890", "john.doe@example.com");
            Borrower borrower2 = library.AddBorrower("Jane Smith", "456 Elm St.",  "987-654-3210", "jane.smith@example.com");
            Console.WriteLine("  2 borrowers added.");

            // ════════════════════════════════════════════════════════════════
            // 3. DISPLAY ALL MEDIA AND BORROWERS (initial state)
            // ════════════════════════════════════════════════════════════════
            library.DisplayAllMedia();
            library.DisplayAllBorrowers();

            // ════════════════════════════════════════════════════════════════
            // 4. BORROW MEDIA — normal cases
            // ════════════════════════════════════════════════════════════════
            Console.WriteLine("\n\n--- Borrowing Media ---");
            BorrowItem(library, book1.Id, borrower1.Id);  // John borrows The Great Gatsby
            BorrowItem(library, book2.Id, borrower1.Id);  // John borrows To Kill a Mockingbird
            BorrowItem(library, book3.Id, borrower1.Id);  // John borrows 1984
            BorrowItem(library, dvd1.Id,  borrower2.Id);  // Jane borrows Inception
            BorrowItem(library, book4.Id, borrower2.Id);  // Jane borrows Pride and Prejudice

            // ════════════════════════════════════════════════════════════════
            // 5. BORROW ALREADY-BORROWED MEDIA (defensive programming)
            // ════════════════════════════════════════════════════════════════
            Console.WriteLine("\n--- Attempting to borrow an already-borrowed item ---");
            BorrowItem(library, book1.Id, borrower2.Id);  // book1 is already with John — should fail

            // ════════════════════════════════════════════════════════════════
            // 6. OVER-LIMIT BORROWING (defensive programming)
            // ════════════════════════════════════════════════════════════════
            Console.WriteLine("\n--- Attempting to exceed borrow limit (limit = 5) ---");
            BorrowItem(library, book5.Id, borrower1.Id);  // John borrows 4th item  (OK)
            BorrowItem(library, book6.Id, borrower1.Id);  // John borrows 5th item  (OK — at limit)
            BorrowItem(library, book7.Id, borrower1.Id);  // John tries  6th item   (FAIL — over limit)

            // ════════════════════════════════════════════════════════════════
            // 7. DISPLAY BORROWED ITEMS PER BORROWER
            // ════════════════════════════════════════════════════════════════
            library.DisplayBorrowedByBorrower(borrower1.Id);
            library.DisplayBorrowedByBorrower(borrower2.Id);

            // ════════════════════════════════════════════════════════════════
            // 8. RETURN MEDIA
            // ════════════════════════════════════════════════════════════════
            Console.WriteLine("\n--- Returning Media ---");
            ReturnItem(library, book1.Id, borrower1.Id);  // John returns The Great Gatsby
            ReturnItem(library, dvd1.Id,  borrower2.Id);  // Jane returns Inception

            Console.WriteLine("\n--- Borrowed items after returns ---");
            library.DisplayBorrowedByBorrower(borrower1.Id);

            // ════════════════════════════════════════════════════════════════
            // 9. REMOVE MEDIA
            // ════════════════════════════════════════════════════════════════
            Console.WriteLine("\n--- Removing Media ---");
            RemoveItem(library, book1.Id);  // now available  — should succeed
            RemoveItem(library, book3.Id);  // still borrowed by John — should fail

            // ════════════════════════════════════════════════════════════════
            // 10. ADD AND REMOVE A BORROWER
            // ════════════════════════════════════════════════════════════════
            Console.WriteLine("\n--- Adding and removing a borrower ---");
            Borrower tempBorrower = library.AddBorrower("Temp User", "789 Oak Ave.", "111-222-3333", "temp@example.com");
            Console.WriteLine($"  Added: {tempBorrower}");
            RemoveBorrowerItem(library, tempBorrower.Id);  // no items — should succeed
            RemoveBorrowerItem(library, borrower1.Id);     // still has items — should fail

            // ════════════════════════════════════════════════════════════════
            // 11. SEARCH USING MediaSearchDelegate
            // ════════════════════════════════════════════════════════════════
            string searchTerm = "the";

            Console.WriteLine($"\n\n==========================================================");
            Console.WriteLine($" SEARCH BY TITLE  --  term: \"{searchTerm}\"");
            Console.WriteLine($"==========================================================");

            // Assign the title-search method to the delegate
            MediaSearchDelegate titleSearch = MediaSearchStrategies.SearchByTitle;
            var titleResults = library.SearchMedia(searchTerm, titleSearch).ToList();
            if (titleResults.Any())
                foreach (var result in titleResults)
                    Console.WriteLine($"  {result}");
            else
                Console.WriteLine($"  (no results for title containing \"{searchTerm}\")");

            Console.WriteLine($"\n==========================================================");
            Console.WriteLine($" SEARCH BY AUTHOR / DIRECTOR  --  term: \"{searchTerm}\"");
            Console.WriteLine($"==========================================================");

            // Swap delegate to the creator-search method at runtime — demonstrates runtime delegate switching
            MediaSearchDelegate creatorSearch = MediaSearchStrategies.SearchByCreator;
            var creatorResults = library.SearchMedia(searchTerm, creatorSearch).ToList();
            if (creatorResults.Any())
                foreach (var result in creatorResults)
                    Console.WriteLine($"  {result}");
            else
                Console.WriteLine($"  (no results for author/director containing \"{searchTerm}\")");

            // ════════════════════════════════════════════════════════════════
            // 12. FINAL STATE DISPLAY
            // ════════════════════════════════════════════════════════════════
            library.DisplayAllMedia();
            library.DisplayAllBorrowers();

            Console.WriteLine("\n==========================================================");
            Console.WriteLine("  Demo complete.");
            Console.WriteLine("==========================================================");

            // Suppress "variable assigned but never used" warnings for seeded media
            // that are only accessed via .Id above (already accessed; these are no-ops)
            _ = book2; _ = book4; _ = book5; _ = book6; _ = book7; _ = dvd2;
        }

        // ── Helper wrappers with try/catch — demonstrate defensive programming ──

        /// <summary>Attempts to borrow a media item; prints result to console.</summary>
        private static void BorrowItem(Library library, int mediaId, int borrowerId)
        {
            try
            {
                // Read names before the operation so they are available even on failure path
                string mediaTitle    = library.GetMediaById(mediaId).Title;
                string borrowerName  = library.GetBorrowerById(borrowerId).Name;
                library.BorrowMedia(mediaId, borrowerId);
                Console.WriteLine($"  \u2714 \"{mediaTitle}\" checked out to {borrowerName}.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"  \u2718 Borrow failed: {ex.Message}");
            }
        }

        /// <summary>Attempts to return a media item; prints result to console.</summary>
        private static void ReturnItem(Library library, int mediaId, int borrowerId)
        {
            try
            {
                string mediaTitle   = library.GetMediaById(mediaId).Title;
                string borrowerName = library.GetBorrowerById(borrowerId).Name;
                library.ReturnMedia(mediaId, borrowerId);
                Console.WriteLine($"  \u2714 \"{mediaTitle}\" returned by {borrowerName}.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"  \u2718 Return failed: {ex.Message}");
            }
        }

        /// <summary>Attempts to remove a media item from the library; prints result to console.</summary>
        private static void RemoveItem(Library library, int mediaId)
        {
            try
            {
                string title = library.GetMediaById(mediaId).Title;
                library.RemoveMedia(mediaId);
                Console.WriteLine($"  \u2714 \"{title}\" removed from inventory.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  \u2718 Remove failed: {ex.Message}");
            }
        }

        /// <summary>Attempts to remove a borrower from the system; prints result to console.</summary>
        private static void RemoveBorrowerItem(Library library, int borrowerId)
        {
            try
            {
                string name = library.GetBorrowerById(borrowerId).Name;
                library.RemoveBorrower(borrowerId);
                Console.WriteLine($"  \u2714 Borrower \"{name}\" removed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  \u2718 Remove borrower failed: {ex.Message}");
            }
        }
    }
}
