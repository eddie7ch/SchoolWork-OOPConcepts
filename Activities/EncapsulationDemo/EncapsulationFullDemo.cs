// ============================================================
// ENCAPSULATION DEMO — Data Hiding & Abstraction
// ============================================================
// This file presents a realistic scenario: a BankAccount class
// that stores sensitive financial data and exposes only the
// controlled operations that the outside world should be able
// to perform.
//
// Key encapsulation goals achieved here:
//   • Data hiding    — private fields are completely invisible
//                      to external code.
//   • Abstraction    — callers work with high-level operations
//                      (Deposit, Withdraw) without knowing how
//                      the balance or history is stored.
//   • Invariant enforcement — the class guarantees the balance
//                      can never go negative, regardless of input.
// ============================================================

namespace EncapsulationDemo
{
    public class BankAccount
    {
        // ── Private fields — sensitive data ───────────────────────────────────
        // These fields are completely hidden from external code.
        // No outside class can read or write them without going through
        // the public methods below, preventing accidental or malicious
        // manipulation of financial data.

        private readonly string _accountNumber;   // immutable identity
        private readonly string _ownerName;        // immutable owner
        private decimal _balance;                  // never directly writable from outside
        private string _pin;                       // sensitive — never readable from outside
        private readonly List<string> _transactionHistory; // internal audit log

        // ── Constructor ───────────────────────────────────────────────────────
        // External code can only create an account through this constructor,
        // which enforces that every account starts with a valid state.
        public BankAccount(string accountNumber, string ownerName,
                           decimal initialBalance, string pin)
        {
            if (initialBalance < 0)
                throw new ArgumentException("Initial balance cannot be negative.");
            if (string.IsNullOrWhiteSpace(pin) || pin.Length != 4)
                throw new ArgumentException("PIN must be exactly 4 characters.");

            _accountNumber      = accountNumber;
            _ownerName          = ownerName;
            _balance            = initialBalance;
            _pin                = pin;
            _transactionHistory = new List<string>();

            _transactionHistory.Add($"Account opened with balance: {_balance:C}");
        }

        // ── Public read-only properties ───────────────────────────────────────
        // These expose non-sensitive identity information in a read-only way.
        // Encapsulation benefit: callers know who owns the account without
        // having any ability to rename the owner or change the account number.

        public string AccountNumber => _accountNumber;
        public string OwnerName     => _ownerName;

        // The balance is readable but NEVER directly writable — it can only
        // change through Deposit() and Withdraw(), which enforce business rules.
        public decimal Balance => _balance;

        // ── Public methods — controlled access to sensitive data ──────────────

        // Deposits money into the account.
        // Encapsulation benefit: validation lives here; callers cannot bypass
        // it by setting _balance directly.
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive.");

            _balance += amount;
            _transactionHistory.Add($"Deposit  : +{amount:C}  | New Balance: {_balance:C}");
            Console.WriteLine($"  Deposited {amount:C}. New balance: {_balance:C}");
        }

        // Withdraws money from the account after PIN verification.
        // Encapsulation benefit: the class enforces both PIN authentication
        // and the non-negative balance invariant in one controlled place.
        public bool Withdraw(decimal amount, string pin)
        {
            if (!VerifyPin(pin))
            {
                Console.WriteLine("  Withdrawal denied: incorrect PIN.");
                _transactionHistory.Add($"Failed withdrawal attempt of {amount:C} — bad PIN.");
                return false;
            }

            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive.");

            if (amount > _balance)
            {
                Console.WriteLine("  Withdrawal denied: insufficient funds.");
                _transactionHistory.Add($"Failed withdrawal of {amount:C} — insufficient funds.");
                return false;
            }

            _balance -= amount;
            _transactionHistory.Add($"Withdrawal: -{amount:C}  | New Balance: {_balance:C}");
            Console.WriteLine($"  Withdrew {amount:C}. New balance: {_balance:C}");
            return true;
        }

        // Allows the PIN to be changed, but only if the old PIN is supplied.
        // The new PIN is never stored in a readable property — write-only semantics.
        public bool ChangePin(string oldPin, string newPin)
        {
            if (!VerifyPin(oldPin))
            {
                Console.WriteLine("  PIN change denied: current PIN is incorrect.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(newPin) || newPin.Length != 4)
                throw new ArgumentException("New PIN must be exactly 4 characters.");

            _pin = newPin;
            _transactionHistory.Add("PIN changed successfully.");
            Console.WriteLine("  PIN changed successfully.");
            return true;
        }

        // Returns a copy of the transaction history — the internal list itself
        // is never handed out, so external code cannot mutate the audit log.
        // Encapsulation benefit: the audit trail is protected from tampering.
        public IReadOnlyList<string> GetTransactionHistory()
        {
            return _transactionHistory.AsReadOnly();
        }

        // ── Private helper method ─────────────────────────────────────────────
        // PIN verification is internal logic. Making it private ensures no
        // external class can call it directly or know how it works.
        private bool VerifyPin(string candidate)
        {
            return _pin == candidate;
        }

        public void DisplaySummary()
        {
            Console.WriteLine($"  Account # : {_accountNumber}");
            Console.WriteLine($"  Owner     : {_ownerName}");
            Console.WriteLine($"  Balance   : {_balance:C}");
            Console.WriteLine($"  PIN       : [hidden — never readable]");
        }
    }
}
