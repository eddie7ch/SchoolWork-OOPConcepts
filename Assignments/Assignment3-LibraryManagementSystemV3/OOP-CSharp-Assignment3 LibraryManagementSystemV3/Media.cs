namespace OOP_CSharp_Assignment3_LibraryManagementSystemV3
{
    /// <summary>
    /// Abstract base class representing a media item in the library.
    /// Follows the Open/Closed Principle � open for extension (Book, DVD), closed for modification.
    /// </summary>
    public abstract class Media
    {
        /// <summary>Unique identifier of the media item.</summary>
        public int Id { get; set; }

        /// <summary>Title of the media item.</summary>
        public string Title { get; set; }

        /// <summary>Indicates whether the media item is currently available for borrowing.</summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Media"/> class.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <param name="title">Title of the media.</param>
        protected Media(int id, string title)
        {
            Id = id;
            Title = title;
            IsAvailable = true;
        }

        /// <summary>
        /// Returns a string representation of the media item.
        /// Overridden in derived classes to include type-specific details.
        /// </summary>
        public abstract override string ToString();
    }
}
