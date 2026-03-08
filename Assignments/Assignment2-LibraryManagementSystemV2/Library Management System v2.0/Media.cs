namespace Library_Management_System_v2._0
{
    /// <summary>
    /// Abstract base class representing a media item in the library.
    /// All concrete media types (Book, DVD, etc.) inherit from this class.
    /// </summary>
    public abstract class Media
    {
        // ?? Auto-increment ID counter ?????????????????????????????????????
        private static int _nextId = 1;

        // ?? Properties ???????????????????????????????????????????????????
        /// <summary>Unique identifier for the media item.</summary>
        public int MediaId { get; private set; }

        /// <summary>Title of the media item.</summary>
        public string Title { get; set; }

        /// <summary>Indicates whether the item is currently available for borrowing.</summary>
        public bool IsAvailable { get; protected set; }

        /// <summary>The borrower who currently holds this item; null when available.</summary>
        public Borrower? CurrentBorrower { get; protected set; }

        // ?? Constructor ??????????????????????????????????????????????????
        /// <summary>
        /// Initialises a new <see cref="Media"/> item and assigns a unique ID.
        /// </summary>
        /// <param name="title">Title of the media item.</param>
        protected Media(string title)
        {
            MediaId         = _nextId++;
            Title           = title;
            IsAvailable     = true;
            CurrentBorrower = null;
        }

        // ?? Abstract / Virtual members ???????????????????????????????????
        /// <summary>
        /// Returns a human-readable description of the media item.
        /// Each derived class must provide its own implementation.
        /// </summary>
        public abstract string GetDetails();

        /// <summary>Returns a brief string summary of the media item.</summary>
        public override string ToString() => GetDetails();
    }
}
