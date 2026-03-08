namespace Assignment1.Models;

/// <summary>
/// Records a single borrow/return transaction between a Borrower and a MediaItem.
/// Immutable after creation except for the return action.
/// </summary>
public class Transaction
{
    private static int _nextId = 1;

    /// <summary>Unique transaction ID.</summary>
    public int Id { get; }

    /// <summary>The borrower who checked out the item.</summary>
    public Borrower Borrower { get; }

    /// <summary>The media item that was borrowed.</summary>
    public MediaItem MediaItem { get; }

    /// <summary>Date and time the item was checked out.</summary>
    public DateTime BorrowedOn { get; }

    /// <summary>Date and time the item was returned. Null if still checked out.</summary>
    public DateTime? ReturnedOn { get; private set; }

    /// <summary>True if the item has been returned.</summary>
    public bool IsReturned => ReturnedOn.HasValue;

    /// <summary>
    /// Creates a new transaction, marks the media item as borrowed.
    /// </summary>
    public Transaction(Borrower borrower, MediaItem mediaItem)
    {
        Id         = _nextId++;
        Borrower   = borrower ?? throw new ArgumentNullException(nameof(borrower));
        MediaItem  = mediaItem ?? throw new ArgumentNullException(nameof(mediaItem));
        BorrowedOn = DateTime.Now;

        // Update availability through the internal method (encapsulation).
        MediaItem.MarkBorrowed();
    }

    /// <summary>
    /// Completes the transaction — marks the item as returned.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if already returned.</exception>
    public void CompleteReturn()
    {
        if (IsReturned)
            throw new InvalidOperationException($"Transaction #{Id} has already been returned.");

        ReturnedOn = DateTime.Now;
        MediaItem.MarkReturned();
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        string status = IsReturned
            ? $"Returned: {ReturnedOn:yyyy-MM-dd HH:mm}"
            : "Still checked out";

        return $"[TXN #{Id:D4}] \"{MediaItem.Title}\" → {Borrower.Name}  |  Borrowed: {BorrowedOn:yyyy-MM-dd HH:mm}  |  {status}";
    }
}
