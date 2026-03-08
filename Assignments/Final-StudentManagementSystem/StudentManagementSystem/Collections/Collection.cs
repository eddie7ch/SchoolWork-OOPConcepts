using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Collections
{
    // ============================================================
    //  Part 3 – Generics & Delegates
    //  A reusable generic collection that works for ANY type T
    //  that has a string ID property (enforced via IIdentifiable).
    // ============================================================

    /// <summary>
    /// Contract that every entity stored in the collection must fulfil.
    /// Enforces the presence of a unique string identifier.
    /// </summary>
    public interface IIdentifiable
    {
        string ID { get; }
    }

    /// <summary>
    /// Generic collection class that stores, removes, and searches
    /// any IIdentifiable objects.  Raises delegate-based events when
    /// items are added or removed.
    /// </summary>
    /// <typeparam name="T">Any type that implements IIdentifiable.</typeparam>
    public class Collection<T> where T : IIdentifiable
    {
        // ── Internal storage ─────────────────────────────────────────────────
        private readonly List<T> _items = new();

        // ── Delegates & Events (Part 3) ───────────────────────────────────────

        /// <summary>Delegate signature for collection-change notifications.</summary>
        public delegate void CollectionChangedHandler(string action, string itemID);

        /// <summary>Fired whenever an item is added or removed.</summary>
        public event CollectionChangedHandler? OnCollectionChanged;

        // ── Properties ────────────────────────────────────────────────────────
        public int Count => _items.Count;

        // ── Methods ───────────────────────────────────────────────────────────

        /// <summary>Add an item to the collection.</summary>
        public void Add(T item)
        {
            if (FindByID(item.ID) != null)
            {
                Console.WriteLine($"  [Collection] Item with ID '{item.ID}' already exists.");
                return;
            }
            _items.Add(item);
            OnCollectionChanged?.Invoke("Added", item.ID);
        }

        /// <summary>Remove an item by its ID.</summary>
        public bool Remove(string id)
        {
            var item = FindByID(id);
            if (item == null) return false;
            _items.Remove(item);
            OnCollectionChanged?.Invoke("Removed", id);
            return true;
        }

        /// <summary>Find a single item by its exact ID.</summary>
        public T? FindByID(string id)
        {
            foreach (var item in _items)
                if (item.ID.Equals(id, StringComparison.OrdinalIgnoreCase))
                    return item;
            return default;
        }

        /// <summary>Search items using a predicate (supports any criteria).</summary>
        public List<T> Search(Func<T, bool> predicate)
        {
            var results = new List<T>();
            foreach (var item in _items)
                if (predicate(item))
                    results.Add(item);
            return results;
        }

        /// <summary>Return a read-only snapshot of all items.</summary>
        public IReadOnlyList<T> GetAll() => _items.AsReadOnly();
    }
}
