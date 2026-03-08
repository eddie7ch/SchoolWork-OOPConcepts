namespace OOP_CSharp_Assignment3_LibraryManagementSystemV3
{
    /// <summary>
    /// Contract for the media factory.
    /// Follows the Dependency Inversion Principle � high-level modules depend on this abstraction,
    /// not on the concrete <see cref="MediaFactory"/> implementation.
    /// </summary>
    public interface IMediaFactory
    {
        /// <summary>
        /// Creates and returns a new media item of the requested type.
        /// </summary>
        /// <param name="mediaType">Either "book" or "dvd" (case-insensitive).</param>
        /// <param name="id">Unique identifier to assign to the new media item.</param>
        /// <param name="title">Title of the media item.</param>
        /// <param name="secondaryCreator">Author (book) or Director (DVD).</param>
        /// <param name="extraInfo">Genre (book) or runtime in minutes (DVD).</param>
        /// <returns>A fully initialised <see cref="Media"/> instance.</returns>
        Media CreateMedia(string mediaType, int id, string title, string secondaryCreator, string extraInfo);
    }
}
