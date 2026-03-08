namespace Assignment1.Models;

/// <summary>
/// Represents a DVD in the library inventory.
/// Inherits common media behaviour from <see cref="MediaItem"/>.
/// </summary>
public class DVD : MediaItem
{
    /// <summary>Name of the director.</summary>
    public string Director { get; }

    /// <summary>Total running time in minutes.</summary>
    public int RuntimeMinutes { get; }

    /// <summary>
    /// Creates a new DVD and adds it to the shared ID sequence.
    /// </summary>
    /// <param name="title">DVD title.</param>
    /// <param name="director">Director's full name.</param>
    /// <param name="runtimeMinutes">Running time in minutes (must be > 0).</param>
    public DVD(string title, string director, int runtimeMinutes) : base(title)
    {
        if (string.IsNullOrWhiteSpace(director))
            throw new ArgumentException("Director cannot be empty.", nameof(director));
        if (runtimeMinutes <= 0)
            throw new ArgumentOutOfRangeException(nameof(runtimeMinutes), "Runtime must be positive.");

        Director       = director;
        RuntimeMinutes = runtimeMinutes;
    }

    /// <summary>
    /// POLYMORPHISM: overrides the abstract GetDetails() from MediaItem.
    /// Returns a DVD-specific formatted string.
    /// </summary>
    public override string GetDetails() =>
        $"[DVD   #{Id:D4}] \"{Title}\"  |  Director: {Director}  |  Runtime: {RuntimeMinutes} min  |  Status: {AvailabilityLabel}";
}
