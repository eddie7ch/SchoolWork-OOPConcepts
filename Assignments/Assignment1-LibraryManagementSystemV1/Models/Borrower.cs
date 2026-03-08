namespace Assignment1.Models;

/// <summary>
/// Represents a library borrower (patron).
/// 
/// ENCAPSULATION: all personal data is read-only after construction.
/// Updating contact info is done through dedicated methods that validate input.
/// </summary>
public class Borrower
{
    private static int _nextId = 1;

    /// <summary>Unique borrower ID assigned automatically.</summary>
    public int Id { get; }

    /// <summary>Full name of the borrower.</summary>
    public string Name { get; private set; }

    /// <summary>Mailing address.</summary>
    public string Address { get; private set; }

    /// <summary>Phone or contact number.</summary>
    public string ContactNumber { get; private set; }

    /// <summary>Email address.</summary>
    public string Email { get; private set; }

    /// <summary>
    /// Creates a new Borrower with a unique ID.
    /// </summary>
    public Borrower(string name, string address, string contactNumber, string email)
    {
        if (string.IsNullOrWhiteSpace(name))          throw new ArgumentException("Name is required.",          nameof(name));
        if (string.IsNullOrWhiteSpace(address))        throw new ArgumentException("Address is required.",        nameof(address));
        if (string.IsNullOrWhiteSpace(contactNumber))  throw new ArgumentException("Contact number is required.", nameof(contactNumber));
        if (string.IsNullOrWhiteSpace(email))          throw new ArgumentException("Email is required.",          nameof(email));

        Id            = _nextId++;
        Name          = name;
        Address       = address;
        ContactNumber = contactNumber;
        Email         = email;
    }

    // ── Controlled mutation (encapsulation) ──────────────────────────────────

    /// <summary>Update contact number after validation.</summary>
    public void UpdateContactNumber(string contactNumber)
    {
        if (string.IsNullOrWhiteSpace(contactNumber))
            throw new ArgumentException("Contact number cannot be empty.");
        ContactNumber = contactNumber;
    }

    /// <summary>Update email after validation.</summary>
    public void UpdateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.");
        Email = email;
    }

    /// <inheritdoc/>
    public override string ToString() =>
        $"[BORROWER #{Id:D4}] {Name}  |  Email: {Email}  |  Phone: {ContactNumber}  |  Address: {Address}";
}
