namespace OOP_CSharp_Assignment3_LibraryManagementSystemV3
{
    /// <summary>
    /// Delegate used for searching media items by a given search term against a collection of titles or creators.
    /// Demonstrates delegation � behaviour (search strategy) is encapsulated in a method reference
    /// and passed around at runtime.
    /// </summary>
    /// <param name="searchTerm">The string to search for.</param>
    /// <param name="mediaItems">The collection of media items to search within.</param>
    /// <returns>An enumerable of <see cref="Media"/> items that match the search term.</returns>
    public delegate IEnumerable<Media> MediaSearchDelegate(string searchTerm, IEnumerable<Media> mediaItems);
}
