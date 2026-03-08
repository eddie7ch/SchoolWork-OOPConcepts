namespace OOP_CSharp_Assignment3_LibraryManagementSystemV3
{
    /// <summary>
    /// Provides static search methods compatible with <see cref="MediaSearchDelegate"/>.
    /// Each method is a concrete strategy that can be assigned to the delegate at runtime,
    /// demonstrating the delegation and Strategy pattern in combination.
    /// LINQ is used inside each method for concise, readable querying.
    /// </summary>
    public static class MediaSearchStrategies
    {
        /// <summary>
        /// Searches media items whose <see cref="Media.Title"/> contains the search term
        /// (case-insensitive).
        /// </summary>
        public static IEnumerable<Media> SearchByTitle(string searchTerm, IEnumerable<Media> mediaItems)
        {
            // LINQ: Where with string.Contains and StringComparison for case-insensitive match
            return mediaItems.Where(m =>
                m.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Searches media items by creator (Author for <see cref="Book"/>, Director for <see cref="DVD"/>)
        /// where the creator field contains the search term (case-insensitive).
        /// </summary>
        public static IEnumerable<Media> SearchByCreator(string searchTerm, IEnumerable<Media> mediaItems)
        {
            // LINQ: OfType<T> to filter by concrete type, then Union to combine results
            IEnumerable<Media> bookMatches = mediaItems
                .OfType<Book>()
                .Where(b => b.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

            IEnumerable<Media> dvdMatches = mediaItems
                .OfType<DVD>()
                .Where(d => d.Director.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

            return bookMatches.Union(dvdMatches);
        }
    }
}
