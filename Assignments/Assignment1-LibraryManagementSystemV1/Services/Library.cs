using Assignment1.Models;

namespace Assignment1.Services;

/// <summary>
/// Central service class that manages all library operations.
/// 
/// Responsibilities:
///   - Media inventory (add, remove, display)
///   - Borrower registry (add, remove, display)
///   - Borrow / return transactions
/// 
/// All collections are private — external code interacts only through
/// well-defined public methods (ENCAPSULATION).
/// </summary>
public class Library
{
    // ── Private state ────────────────────────────────────────────────────────

    private readonly List<MediaItem>   _inventory    = [];
    private readonly List<Borrower>    _borrowers    = [];
    private readonly List<Transaction> _transactions = [];

    // ── Properties ───────────────────────────────────────────────────────────

    /// <summary>Display name of this library branch.</summary>
    public string Name { get; }

    // ── Constructor ──────────────────────────────────────────────────────────

    /// <param name="name">Name of the library.</param>
    public Library(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Library name cannot be empty.", nameof(name));
        Name = name;
    }

    // ═══════════════════════════════════════════════════════════════════════
    // MEDIA MANAGEMENT
    // ═══════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Adds a new media item (Book or DVD) to the inventory.
    /// POLYMORPHISM: accepts any MediaItem subtype.
    /// </summary>
    /// <param name="item">The media item to add.</param>
    public void AddMedia(MediaItem item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        _inventory.Add(item);
        Console.WriteLine($"  + Added: {item.GetDetails()}");
    }

    /// <summary>
    /// Removes a media item by its ID, only if it is currently available.
    /// </summary>
    /// <param name="mediaId">The ID of the item to remove.</param>
    /// <returns>True if removed; false if not found.</returns>
    /// <exception cref="InvalidOperationException">Thrown if item is currently checked out.</exception>
    public bool RemoveMedia(int mediaId)
    {
        var item = FindMedia(mediaId);
        if (item is null) return false;

        if (!item.IsAvailable)
            throw new InvalidOperationException(
                $"Cannot remove \"{item.Title}\" — it is currently checked out.");

        _inventory.Remove(item);
        Console.WriteLine($"  - Removed: {item.Title}");
        return true;
    }

    /// <summary>Finds a media item by ID. Returns null if not found.</summary>
    public MediaItem? FindMedia(int mediaId) =>
        _inventory.FirstOrDefault(m => m.Id == mediaId);

    // ═══════════════════════════════════════════════════════════════════════
    // BORROWER MANAGEMENT
    // ═══════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Registers a new borrower in the system.
    /// </summary>
    public void AddBorrower(Borrower borrower)
    {
        if (borrower is null) throw new ArgumentNullException(nameof(borrower));
        _borrowers.Add(borrower);
        Console.WriteLine($"  + Registered: {borrower}");
    }

    /// <summary>
    /// Removes a borrower by ID, only if they have no outstanding loans.
    /// </summary>
    /// <returns>True if removed; false if not found.</returns>
    /// <exception cref="InvalidOperationException">Thrown if borrower has active loans.</exception>
    public bool RemoveBorrower(int borrowerId)
    {
        var borrower = FindBorrower(borrowerId);
        if (borrower is null) return false;

        bool hasActiveLoans = _transactions.Any(t => t.Borrower.Id == borrowerId && !t.IsReturned);
        if (hasActiveLoans)
            throw new InvalidOperationException(
                $"Cannot remove borrower \"{borrower.Name}\" — they have items still checked out.");

        _borrowers.Remove(borrower);
        Console.WriteLine($"  - Removed borrower: {borrower.Name}");
        return true;
    }

    /// <summary>Finds a borrower by ID. Returns null if not found.</summary>
    public Borrower? FindBorrower(int borrowerId) =>
        _borrowers.FirstOrDefault(b => b.Id == borrowerId);

    // ═══════════════════════════════════════════════════════════════════════
    // BORROW & RETURN
    // ═══════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Checks out a media item to a borrower.
    /// </summary>
    /// <param name="mediaId">ID of the item to borrow.</param>
    /// <param name="borrowerId">ID of the borrower.</param>
    /// <returns>The created <see cref="Transaction"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown if item unavailable.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if IDs are not found.</exception>
    public Transaction CheckOut(int mediaId, int borrowerId)
    {
        var item     = FindMedia(mediaId)     ?? throw new KeyNotFoundException($"Media ID {mediaId} not found.");
        var borrower = FindBorrower(borrowerId) ?? throw new KeyNotFoundException($"Borrower ID {borrowerId} not found.");

        if (!item.IsAvailable)
            throw new InvalidOperationException($"\"{item.Title}\" is currently checked out.");

        var transaction = new Transaction(borrower, item);
        _transactions.Add(transaction);

        Console.WriteLine($"  ✔ Checked out: \"{item.Title}\" → {borrower.Name}  [TXN #{transaction.Id:D4}]");
        return transaction;
    }

    /// <summary>
    /// Returns a media item using its ID.
    /// </summary>
    /// <param name="mediaId">ID of the item being returned.</param>
    /// <returns>The completed <see cref="Transaction"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown if item is already available (not checked out).</exception>
    public Transaction ReturnMedia(int mediaId)
    {
        var item = FindMedia(mediaId) ?? throw new KeyNotFoundException($"Media ID {mediaId} not found.");

        if (item.IsAvailable)
            throw new InvalidOperationException($"\"{item.Title}\" is not currently checked out.");

        // Find the open transaction for this item.
        var txn = _transactions.Last(t => t.MediaItem.Id == mediaId && !t.IsReturned);
        txn.CompleteReturn();

        Console.WriteLine($"  ✔ Returned: \"{item.Title}\" by {txn.Borrower.Name}  [TXN #{txn.Id:D4}]");
        return txn;
    }

    // ═══════════════════════════════════════════════════════════════════════
    // DISPLAY
    // ═══════════════════════════════════════════════════════════════════════

    /// <summary>
    /// Prints all media items in the inventory.
    /// POLYMORPHISM: calls GetDetails() — the correct override runs for Book or DVD.
    /// </summary>
    public void DisplayAllMedia()
    {
        PrintHeader($"INVENTORY — {Name}  ({_inventory.Count} items)");
        if (_inventory.Count == 0)
        {
            Console.WriteLine("  (no items in inventory)");
            return;
        }

        foreach (var item in _inventory)
            Console.WriteLine($"  {item.GetDetails()}");   // polymorphic call
    }

    /// <summary>Prints all registered borrowers.</summary>
    public void DisplayAllBorrowers()
    {
        PrintHeader($"BORROWERS — {Name}  ({_borrowers.Count} registered)");
        if (_borrowers.Count == 0)
        {
            Console.WriteLine("  (no borrowers registered)");
            return;
        }

        foreach (var b in _borrowers)
            Console.WriteLine($"  {b}");
    }

    /// <summary>Prints all transactions (active and completed).</summary>
    public void DisplayAllTransactions()
    {
        PrintHeader($"TRANSACTIONS — {Name}  ({_transactions.Count} total)");
        if (_transactions.Count == 0)
        {
            Console.WriteLine("  (no transactions recorded)");
            return;
        }

        foreach (var t in _transactions)
            Console.WriteLine($"  {t}");
    }

    /// <summary>Prints only items that are currently checked out.</summary>
    public void DisplayCheckedOutItems()
    {
        var checkedOut = _inventory.Where(m => !m.IsAvailable).ToList();
        PrintHeader($"CHECKED OUT — {checkedOut.Count} item(s)");
        if (checkedOut.Count == 0)
        {
            Console.WriteLine("  (all items are available)");
            return;
        }

        foreach (var item in checkedOut)
            Console.WriteLine($"  {item.GetDetails()}");
    }

    // ── Helper ───────────────────────────────────────────────────────────────
    private static void PrintHeader(string text)
    {
        Console.WriteLine();
        Console.WriteLine(new string('═', 70));
        Console.WriteLine($"  {text}");
        Console.WriteLine(new string('═', 70));
    }
}
