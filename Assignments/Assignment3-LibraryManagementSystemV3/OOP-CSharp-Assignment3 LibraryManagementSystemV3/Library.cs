namespace OOP_CSharp_Assignment3_LibraryManagementSystemV3
{
    /// <summary>
    /// Manages the library's media inventory and borrower records.
    /// Follows the Single Responsibility Principle � handles library operations only.
    /// Follows the Open/Closed Principle � new media types can be added via <see cref="IMediaFactory"/>
    /// without modifying this class.
    /// Follows the Dependency Inversion Principle � depends on <see cref="IMediaFactory"/> abstraction.
    /// Uses LINQ extensively for querying and manipulating collections.
    /// </summary>
    public class Library
    {
        private readonly List<Media> _mediaItems;
        private readonly List<Borrower> _borrowers;
        private readonly IMediaFactory _mediaFactory;
        private int _nextMediaId = 1;
        private int _nextBorrowerId = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Library"/> class.
        /// </summary>
        /// <param name="mediaFactory">Factory used to create media items (Dependency Injection).</param>
        public Library(IMediaFactory mediaFactory)
        {
            _mediaFactory = mediaFactory ?? throw new ArgumentNullException(nameof(mediaFactory));
            _mediaItems = new List<Media>();
            _borrowers = new List<Borrower>();
        }

        // ?? Media management ????????????????????????????????????????????????????

        /// <summary>
        /// Creates a new media item via the factory and adds it to the inventory.
        /// </summary>
        /// <param name="mediaType">"book" or "dvd".</param>
        /// <param name="title">Title of the item.</param>
        /// <param name="secondaryCreator">Author (book) or Director (DVD).</param>
        /// <param name="extraInfo">Genre (book) or runtime string (DVD).</param>
        /// <returns>The newly created <see cref="Media"/> item.</returns>
        public Media AddMedia(string mediaType, string title, string secondaryCreator, string extraInfo)
        {
            Media item = _mediaFactory.CreateMedia(mediaType, _nextMediaId++, title, secondaryCreator, extraInfo);
            _mediaItems.Add(item);
            return item;
        }

        /// <summary>
        /// Removes a media item from the library by its identifier.
        /// </summary>
        /// <param name="mediaId">The id of the item to remove.</param>
        /// <exception cref="InvalidOperationException">Thrown when the item is currently borrowed.</exception>
        /// <exception cref="KeyNotFoundException">Thrown when no item with the given id exists.</exception>
        public void RemoveMedia(int mediaId)
        {
            // LINQ: FirstOrDefault to locate the item
            Media? item = _mediaItems.FirstOrDefault(m => m.Id == mediaId);

            if (item == null)
                throw new KeyNotFoundException($"No media item found with id {mediaId}.");

            if (!item.IsAvailable)
                throw new InvalidOperationException($"Cannot remove \"{item.Title}\" � it is currently borrowed.");

            _mediaItems.Remove(item);
        }

        /// <summary>
        /// Retrieves a media item by its unique identifier.
        /// </summary>
        /// <param name="mediaId">The id to look up.</param>
        /// <returns>The matching <see cref="Media"/> item.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when no item with the given id exists.</exception>
        public Media GetMediaById(int mediaId)
        {
            // LINQ: FirstOrDefault with null-check pattern
            return _mediaItems.FirstOrDefault(m => m.Id == mediaId)
                   ?? throw new KeyNotFoundException($"No media item found with id {mediaId}.");
        }

        // ?? Borrower management ??????????????????????????????????????????????????

        /// <summary>
        /// Adds a new borrower to the system.
        /// </summary>
        public Borrower AddBorrower(string name, string address, string contactNumber, string email)
        {
            var borrower = new Borrower(_nextBorrowerId++, name, address, contactNumber, email);
            _borrowers.Add(borrower);
            return borrower;
        }

        /// <summary>
        /// Removes a borrower from the system by their identifier.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the borrower still has items checked out.</exception>
        /// <exception cref="KeyNotFoundException">Thrown when no borrower with the given id exists.</exception>
        public void RemoveBorrower(int borrowerId)
        {
            Borrower? borrower = _borrowers.FirstOrDefault(b => b.Id == borrowerId);

            if (borrower == null)
                throw new KeyNotFoundException($"No borrower found with id {borrowerId}.");

            // LINQ: Any to check outstanding borrows
            if (borrower.BorrowedMedia.Any())
                throw new InvalidOperationException(
                    $"Cannot remove borrower \"{borrower.Name}\" � they still have {borrower.BorrowedMedia.Count} item(s) checked out.");

            _borrowers.Remove(borrower);
        }

        /// <summary>Retrieves a borrower by their unique identifier.</summary>
        /// <exception cref="KeyNotFoundException">Thrown when no borrower with the given id exists.</exception>
        public Borrower GetBorrowerById(int borrowerId)
        {
            return _borrowers.FirstOrDefault(b => b.Id == borrowerId)
                   ?? throw new KeyNotFoundException($"No borrower found with id {borrowerId}.");
        }

        // ?? Borrow / Return ??????????????????????????????????????????????????????

        /// <summary>
        /// Checks out a media item to a borrower.
        /// </summary>
        /// <param name="mediaId">The id of the media item to borrow.</param>
        /// <param name="borrowerId">The id of the borrower.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the item is already borrowed or when the borrower has reached their limit.
        /// </exception>
        public void BorrowMedia(int mediaId, int borrowerId)
        {
            Media item = GetMediaById(mediaId);
            Borrower borrower = GetBorrowerById(borrowerId);

            if (!item.IsAvailable)
                throw new InvalidOperationException($"\"{item.Title}\" is already borrowed and not available.");

            if (borrower.HasReachedBorrowLimit())
                throw new InvalidOperationException(
                    $"{borrower.Name} has reached the maximum borrow limit of {Borrower.MaxBorrowLimit} items.");

            if (item is IBorrowable borrowable)
            {
                borrowable.Borrow(borrower);
                borrower.BorrowedMedia.Add(item);
            }
        }

        /// <summary>
        /// Returns a media item from a borrower back to the library.
        /// </summary>
        /// <param name="mediaId">The id of the media item to return.</param>
        /// <param name="borrowerId">The id of the borrower returning the item.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the item is not checked out to the specified borrower.
        /// </exception>
        public void ReturnMedia(int mediaId, int borrowerId)
        {
            Media item = GetMediaById(mediaId);
            Borrower borrower = GetBorrowerById(borrowerId);

            // LINQ: Any to verify the borrower actually has this item
            if (!borrower.BorrowedMedia.Any(m => m.Id == mediaId))
                throw new InvalidOperationException(
                    $"\"{item.Title}\" is not currently checked out by {borrower.Name}.");

            if (item is IBorrowable borrowable)
            {
                borrowable.Return();
                // LINQ: RemoveAll to clean up the borrower's list
                borrower.BorrowedMedia.RemoveAll(m => m.Id == mediaId);
            }
        }

        // ?? Search ???????????????????????????????????????????????????????????????

        /// <summary>
        /// Searches the media inventory using the supplied <see cref="MediaSearchDelegate"/>.
        /// The delegate pattern allows the caller to inject any search strategy at runtime.
        /// </summary>
        /// <param name="searchTerm">Text to search for.</param>
        /// <param name="searchDelegate">The delegate that defines the search behaviour.</param>
        /// <returns>Media items matching the search.</returns>
        public IEnumerable<Media> SearchMedia(string searchTerm, MediaSearchDelegate searchDelegate)
        {
            return searchDelegate(searchTerm, _mediaItems);
        }

        // ?? Display ??????????????????????????????????????????????????????????????

        /// <summary>Displays all media items in the library to the console.</summary>
        public void DisplayAllMedia()
        {
            Console.WriteLine("\n==========================================================");
            Console.WriteLine(" ALL MEDIA ITEMS IN LIBRARY");
            Console.WriteLine("==========================================================");

            if (!_mediaItems.Any())
            {
                Console.WriteLine(" (no media items)");
                return;
            }

            foreach (var item in _mediaItems.OrderBy(m => m.Id))
                Console.WriteLine($"  {item}");
        }

        /// <summary>Displays all borrowers registered in the library to the console.</summary>
        public void DisplayAllBorrowers()
        {
            Console.WriteLine("\n==========================================================");
            Console.WriteLine(" ALL BORROWERS");
            Console.WriteLine("==========================================================");

            if (!_borrowers.Any())
            {
                Console.WriteLine(" (no borrowers)");
                return;
            }

            foreach (var borrower in _borrowers.OrderBy(b => b.Id))
                Console.WriteLine($"  {borrower}");
        }

        /// <summary>
        /// Displays all media items currently borrowed by the specified borrower.
        /// </summary>
        /// <param name="borrowerId">The id of the borrower.</param>
        public void DisplayBorrowedByBorrower(int borrowerId)
        {
            Borrower borrower = GetBorrowerById(borrowerId);

            Console.WriteLine($"\n----------------------------------------------------------");
            Console.WriteLine($" Items borrowed by: {borrower.Name} (Id: {borrower.Id})");
            Console.WriteLine($"----------------------------------------------------------");

            if (!borrower.BorrowedMedia.Any())
            {
                Console.WriteLine("  (no items currently borrowed)");
                return;
            }

            foreach (var item in borrower.BorrowedMedia)
                Console.WriteLine($"  {item}");
        }

        /// <summary>
        /// Returns a read-only view of all media items (useful for external LINQ queries).
        /// </summary>
        public IReadOnlyList<Media> GetAllMedia() => _mediaItems.AsReadOnly();

        /// <summary>
        /// Returns a read-only view of all borrowers.
        /// </summary>
        public IReadOnlyList<Borrower> GetAllBorrowers() => _borrowers.AsReadOnly();
    }
}
