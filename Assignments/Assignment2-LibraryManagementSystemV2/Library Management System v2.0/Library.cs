namespace Library_Management_System_v2._0
{
    // ── Delegate definition ───────────────────────────────────────────────────
    /// <summary>
    /// Delegate used to define a dynamic search criterion for media items.
    /// </summary>
    /// <param name="media">The media item to evaluate.</param>
    /// <returns><c>true</c> if the item matches the criterion.</returns>
    public delegate bool MediaSearchCriteria(Media media);

    /// <summary>
    /// Central library class that composes generic inventories for books and DVDs
    /// and manages the list of registered borrowers.
    /// </summary>
    public class Library
    {
        // ── Inventories ───────────────────────────────────────────────────────
        private readonly GenericLibrary<Book> _books = new();
        private readonly GenericLibrary<DVD>  _dvds  = new();

        // ── Borrowers ─────────────────────────────────────────────────────────
        private readonly List<Borrower> _borrowers = new();

        // ── Book management ───────────────────────────────────────────────────
        /// <summary>Adds a new book to the library inventory.</summary>
        public void AddBook(Book book)
        {
            _books.AddMedia(book);
            Console.WriteLine($"  + Added: {book.GetDetails()}");
        }

        /// <summary>Removes a book from the inventory by its media ID.</summary>
        public void RemoveBook(int mediaId)
        {
            bool removed = _books.RemoveMedia(mediaId);
            Console.WriteLine(removed
                ? $"  - Book #{mediaId} removed from inventory."
                : $"  ! Book #{mediaId} not found.");
        }

        // ── DVD management ────────────────────────────────────────────────────
        /// <summary>Adds a new DVD to the library inventory.</summary>
        public void AddDVD(DVD dvd)
        {
            _dvds.AddMedia(dvd);
            Console.WriteLine($"  + Added: {dvd.GetDetails()}");
        }

        /// <summary>Removes a DVD from the inventory by its media ID.</summary>
        public void RemoveDVD(int mediaId)
        {
            bool removed = _dvds.RemoveMedia(mediaId);
            Console.WriteLine(removed
                ? $"  - DVD #{mediaId} removed from inventory."
                : $"  ! DVD #{mediaId} not found.");
        }

        // ── Borrower management ───────────────────────────────────────────────
        /// <summary>Registers a new borrower with the library.</summary>
        public void AddBorrower(Borrower borrower)
        {
            _borrowers.Add(borrower);
            Console.WriteLine($"  + Registered: {borrower}");
        }

        /// <summary>Removes a borrower from the system by their ID.</summary>
        public void RemoveBorrower(int borrowerId)
        {
            Borrower? borrower = _borrowers.FirstOrDefault(b => b.BorrowerId == borrowerId);
            if (borrower == null)
            {
                Console.WriteLine($"  ! Borrower #{borrowerId} not found.");
                return;
            }
            _borrowers.Remove(borrower);
            Console.WriteLine($"  - Borrower #{borrowerId} ({borrower.Name}) removed.");
        }

        /// <summary>Looks up a borrower by their ID.</summary>
        public Borrower? GetBorrowerById(int borrowerId)
            => _borrowers.FirstOrDefault(b => b.BorrowerId == borrowerId);

        // ── Borrow / Return ───────────────────────────────────────────────────
        /// <summary>
        /// Checks out a media item (book or DVD) to a borrower.
        /// </summary>
        /// <param name="mediaId">The ID of the media item to borrow.</param>
        /// <param name="borrowerId">The ID of the borrower.</param>
        public void BorrowMedia(int mediaId, int borrowerId)
        {
            Borrower? borrower = GetBorrowerById(borrowerId);
            if (borrower == null)
            {
                Console.WriteLine($"  ! Borrower #{borrowerId} not found.");
                return;
            }

            IBorrowable? borrowable = FindBorrowable(mediaId);
            if (borrowable == null)
            {
                Console.WriteLine($"  ! Media item #{mediaId} not found.");
                return;
            }

            try
            {
                borrowable.Borrow(borrower);
                Console.WriteLine($"  > Media #{mediaId} successfully borrowed by {borrower.Name}.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"  ! {ex.Message}");
            }
        }

        /// <summary>Returns a borrowed media item back to the library.</summary>
        /// <param name="mediaId">The ID of the media item to return.</param>
        public void ReturnMedia(int mediaId)
        {
            IBorrowable? borrowable = FindBorrowable(mediaId);
            if (borrowable == null)
            {
                Console.WriteLine($"  ! Media item #{mediaId} not found.");
                return;
            }

            try
            {
                borrowable.Return();
                Console.WriteLine($"  > Media #{mediaId} successfully returned.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"  ! {ex.Message}");
            }
        }

        // ── Delegate-based search ─────────────────────────────────────────────
        /// <summary>
        /// Searches all media items using a caller-supplied delegate as the criterion.
        /// This enables fully dynamic, flexible search queries.
        /// </summary>
        /// <param name="criteria">A <see cref="MediaSearchCriteria"/> delegate that returns
        /// <c>true</c> for each item that should be included in the results.</param>
        /// <returns>A list of media items that satisfy the criterion.</returns>
        public List<Media> SearchMedia(MediaSearchCriteria criteria)
        {
            List<Media> results = new();

            foreach (Book book in _books.Items)
                if (criteria(book)) results.Add(book);

            foreach (DVD dvd in _dvds.Items)
                if (criteria(dvd)) results.Add(dvd);

            return results;
        }

        // ── Display helpers ───────────────────────────────────────────────────
        /// <summary>Prints the full media inventory to the console.</summary>
        public void DisplayInventory()
        {
            Console.WriteLine("\n  ── Books " + new string('─', 50));
            if (_books.Items.Count == 0)
                Console.WriteLine("    (no books in inventory)");
            else
                foreach (Book b in _books.Items)
                    Console.WriteLine($"    {b.GetDetails()}");

            Console.WriteLine("  ── DVDs " + new string('─', 51));
            if (_dvds.Items.Count == 0)
                Console.WriteLine("    (no DVDs in inventory)");
            else
                foreach (DVD d in _dvds.Items)
                    Console.WriteLine($"    {d.GetDetails()}");
        }

        /// <summary>Prints all registered borrowers to the console.</summary>
        public void DisplayBorrowers()
        {
            Console.WriteLine("\n  ── Borrowers " + new string('─', 46));
            if (_borrowers.Count == 0)
                Console.WriteLine("    (no borrowers registered)");
            else
                foreach (Borrower b in _borrowers)
                    Console.WriteLine($"    {b}");
        }

        // ── Private helpers ───────────────────────────────────────────────────
        /// <summary>Finds any borrowable item (book or DVD) by media ID.</summary>
        private IBorrowable? FindBorrowable(int mediaId)
        {
            Media? item = (_books.GetMediaById(mediaId) as Media)
                       ?? (_dvds.GetMediaById(mediaId)  as Media);

            return item as IBorrowable;
        }
    }
}