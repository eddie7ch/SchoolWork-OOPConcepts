namespace Assignment1.Models;

/// <summary>
/// Represents a book in the library inventory.
/// Inherits common media behaviour from <see cref="MediaItem"/>.
/// </summary>
public class Book : MediaItem
{
    /// <summary>Name of the author.</summary>
    public string Author { get; }

    /// <summary>Literary genre (e.g., Fiction, Science, History).</summary>
    public string Genre { get; }

    /// <summary>
    /// Creates a new Book and adds it to the shared ID sequence.
    /// </summary>
    /// <param name="title">Book title.</param>
    /// <param name="author">Author's full name.</param>
    /// <param name="genre">Genre of the book.</param>
    public Book(string title, string author, string genre) : base(title)
    {
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author cannot be empty.", nameof(author));
        if (string.IsNullOrWhiteSpace(genre))
            throw new ArgumentException("Genre cannot be empty.", nameof(genre));

        Author = author;
        Genre  = genre;
    }

    /// <summary>
    /// POLYMORPHISM: overrides the abstract GetDetails() from MediaItem.
    /// Returns a book-specific formatted string.
    /// </summary>
    public override string GetDetails() =>
        $"[BOOK  #{Id:D4}] \"{Title}\"  |  Author: {Author}  |  Genre: {Genre}  |  Status: {AvailabilityLabel}";
}
