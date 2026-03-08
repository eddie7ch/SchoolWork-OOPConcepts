namespace OOP_CSharp_Assignment3_LibraryManagementSystemV3
{
    /// <summary>
    /// Concrete factory that creates <see cref="Book"/> and <see cref="DVD"/> instances.
    /// Implements the Factory design pattern, decoupling object creation from client code.
    /// Follows the Single Responsibility Principle � sole responsibility is media creation.
    /// </summary>
    public class MediaFactory : IMediaFactory
    {
        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when an unsupported media type is supplied.</exception>
        public Media CreateMedia(string mediaType, int id, string title, string secondaryCreator, string extraInfo)
        {
            switch (mediaType.ToLower())
            {
                case "book":
                    return new Book(id, title, secondaryCreator, extraInfo);

                case "dvd":
                    if (!int.TryParse(extraInfo, out int runTime))
                        throw new ArgumentException($"Runtime must be an integer. Received: '{extraInfo}'", nameof(extraInfo));
                    return new DVD(id, title, secondaryCreator, runTime);

                default:
                    throw new ArgumentException($"Unknown media type: '{mediaType}'. Supported types are 'book' and 'dvd'.", nameof(mediaType));
            }
        }
    }
}
