using Assignment1.Models;
using Assignment1.Services;

// ─────────────────────────────────────────────────────────────────────────────
// SODV1254 — Assignment 1: Library Management System
// Demonstrates: encapsulation, inheritance, polymorphism, classes & objects
// ─────────────────────────────────────────────────────────────────────────────

Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
Console.WriteLine("║           SODV1254 — Assignment 1: Library Management System         ║");
Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");

var library = new Library("Bow Valley College Library");

// ── STEP 1: Add Media Items ───────────────────────────────────────────────────
Console.WriteLine("\n▶ Adding media items to inventory...");

// INHERITANCE + POLYMORPHISM: Both Book and DVD are stored as MediaItem references.
library.AddMedia(new Book("Clean Code",                  "Robert C. Martin",   "Technology"));
library.AddMedia(new Book("The Pragmatic Programmer",    "Andrew Hunt",        "Technology"));
library.AddMedia(new Book("Design Patterns",             "Gang of Four",       "Technology"));
library.AddMedia(new Book("To Kill a Mockingbird",       "Harper Lee",         "Fiction"));
library.AddMedia(new Book("1984",                        "George Orwell",      "Dystopian Fiction"));
library.AddMedia(new DVD("Inception",                    "Christopher Nolan",  148));
library.AddMedia(new DVD("The Social Network",           "David Fincher",      120));
library.AddMedia(new DVD("A Beautiful Mind",             "Ron Howard",         135));

// Display full inventory (polymorphic GetDetails() called for each item)
library.DisplayAllMedia();

// ── STEP 2: Add Borrowers ─────────────────────────────────────────────────────
Console.WriteLine("\n▶ Registering borrowers...");

library.AddBorrower(new Borrower("Alice Johnson", "123 Main St, Calgary",    "403-555-0101", "alice@email.com"));
library.AddBorrower(new Borrower("Bob Smith",     "456 Oak Ave, Calgary",    "403-555-0202", "bob@email.com"));
library.AddBorrower(new Borrower("Carol White",   "789 Pine Rd, Calgary",    "403-555-0303", "carol@email.com"));

library.DisplayAllBorrowers();

// ── STEP 3: Check Out Items ───────────────────────────────────────────────────
Console.WriteLine("\n▶ Borrowing items...");

// Media IDs were auto-assigned starting at 1 in order of creation.
library.CheckOut(mediaId: 1, borrowerId: 1);  // Alice borrows "Clean Code"
library.CheckOut(mediaId: 6, borrowerId: 1);  // Alice borrows "Inception"
library.CheckOut(mediaId: 3, borrowerId: 2);  // Bob borrows "Design Patterns"
library.CheckOut(mediaId: 7, borrowerId: 3);  // Carol borrows "The Social Network"

// Show inventory — borrowed items appear as "Checked Out"
library.DisplayAllMedia();

// Show only what's out
library.DisplayCheckedOutItems();

// ── STEP 4: Attempt Invalid Operations (demonstrates defensive coding) ────────
Console.WriteLine("\n▶ Testing invalid operations...\n");

try
{
    library.CheckOut(mediaId: 1, borrowerId: 2); // Item already checked out
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"  [Expected error] {ex.Message}");
}

try
{
    library.RemoveMedia(1); // Cannot remove a checked-out item
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"  [Expected error] {ex.Message}");
}

// ── STEP 5: Return Items ──────────────────────────────────────────────────────
Console.WriteLine("\n▶ Returning items...");

library.ReturnMedia(1);  // Alice returns "Clean Code"
library.ReturnMedia(6);  // Alice returns "Inception"

// ── STEP 6: Remove an Item and a Borrower ────────────────────────────────────
Console.WriteLine("\n▶ Removing media and borrower...");

library.RemoveMedia(1);        // "Clean Code" is now available, so removal works
library.RemoveBorrower(1);     // Alice has no active loans, removal works

// ── STEP 7: Final State ───────────────────────────────────────────────────────
library.DisplayAllMedia();
library.DisplayAllBorrowers();
library.DisplayAllTransactions();

Console.WriteLine("\n✔ Program complete.");

