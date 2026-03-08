namespace Library_Management_System_v2._0
{
    /// <summary>
    /// Generic collection that manages any type of <see cref="Media"/> item.
    /// </summary>
    /// <typeparam name="T">A type that derives from <see cref="Media"/>.</typeparam>
    public class GenericLibrary<T> where T : Media
    {
        // ?? Internal storage ?????????????????????????????????????????????
        private readonly List<T> _items = new();

        // ?? Public API ???????????????????????????????????????????????????
        /// <summary>Read-only view of all items in this collection.</summary>
        public IReadOnlyList<T> Items => _items.AsReadOnly();

        /// <summary>Adds a media item to the inventory.</summary>
        /// <param name="media">The item to add.</param>
        public void AddMedia(T media)
        {
            if (media == null) throw new ArgumentNullException(nameof(media));
            _items.Add(media);
        }

        /// <summary>
        /// Removes a media item from the inventory by its ID.
        /// </summary>
        /// <param name="mediaId">The unique ID of the item to remove.</param>
        /// <returns><c>true</c> if the item was found and removed; otherwise <c>false</c>.</returns>
        public bool RemoveMedia(int mediaId)
        {
            T? item = _items.FirstOrDefault(m => m.MediaId == mediaId);
            if (item == null) return false;
            _items.Remove(item);
            return true;
        }

        /// <summary>
        /// Retrieves a media item by its ID.
        /// </summary>
        /// <param name="mediaId">The unique ID of the item to find.</param>
        /// <returns>The matching item, or <c>null</c> if not found.</returns>
        public T? GetMediaById(int mediaId)
        {
            return _items.FirstOrDefault(m => m.MediaId == mediaId);
        }
    }
}
