namespace Assignment1.Models;

/// <summary>
/// Abstract base class representing any media item in the library.
/// 
/// OOP Concepts demonstrated:
///   - ABSTRACTION   : Cannot instantiate MediaItem directly; only concrete types (Book, DVD).
///   - ENCAPSULATION : 'IsAvailable' state is controlled through methods, not direct field access.
///   - INHERITANCE   : Book and DVD inherit common Id, Title, and availability behaviour.
///   - POLYMORPHISM  : GetDetails() is abstract → each subclass provides its own display format.
/// </summary>
public abstract class MediaItem
{
    // Thread-safe unique ID counter shared across all media items.
    private static int _nextId = 1;

    /// <summary>Unique identifier assigned automatically at construction.</summary>
    public int Id { get; }

    /// <summary>Title of the media item.</summary>
    public string Title { get; }

    /// <summary>True if the item is on the shelf and can be borrowed.</summary>
    public bool IsAvailable { get; private set; } = true;

    /// <summary>
    /// Base constructor — called by Book and DVD via : base(title).
    /// </summary>
    /// <param name="title">Title of the media item. Must not be empty.</param>
    protected MediaItem(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        Id    = _nextId++;
        Title = title;
    }

    // ── Internal state control ───────────────────────────────────────────────

    /// <summary>Mark item as checked out (borrowed).</summary>
    internal void MarkBorrowed() => IsAvailable = false;

    /// <summary>Mark item as returned (available).</summary>
    internal void MarkReturned() => IsAvailable = true;

    // ── Polymorphic method ───────────────────────────────────────────────────

    /// <summary>
    /// Returns a formatted string with all details for this media type.
    /// Each subclass MUST provide its own implementation.
    /// </summary>
    public abstract string GetDetails();

    /// <summary>Availability as a readable label.</summary>
    protected string AvailabilityLabel => IsAvailable ? "Available" : "Checked Out";

    /// <inheritdoc/>
    public override string ToString() => GetDetails();
}
