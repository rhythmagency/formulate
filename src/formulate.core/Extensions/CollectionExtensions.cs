namespace formulate.core.Extensions
{

    // Namespaces.
    using System.Collections.Generic;
    using System.Linq;


    /// <summary>
    /// Extension methods for collections.
    /// </summary>
    public static class CollectionExtensions
    {

        #region Extension Methods

        /// <summary>
        /// Converts a null collection into an empty collection.
        /// </summary>
        /// <typeparam name="T">The type of item stored by the collection.</typeparam>
        /// <param name="items">The collection of items.</param>
        /// <returns>
        /// An empty list, if the supplied collection is null; otherwise, the supplied collection.
        /// </returns>
        public static IEnumerable<T> MakeSafe<T>(this IEnumerable<T> items)
        {
            return items == null
                ? new List<T>()
                : items;
        }


        /// <summary>
        /// Returns the collection of items without nulls.
        /// </summary>
        /// <typeparam name="T">The type of item stored by the collection.</typeparam>
        /// <param name="items">The collection of items.</param>
        /// <returns>
        /// The collection without any empty items.
        /// </returns>
        public static IEnumerable<T> WithoutNulls<T>(this IEnumerable<T> items)
        {
            return items == null ? null : items.Where(x => x != null);
        }

        /// <summary>
        /// Sorts a collection by the order of the items in another collection.
        /// </summary>
        /// <typeparam name="T">
        /// The type of item in each ceollection.
        /// </typeparam>
        /// <param name="items">
        /// The items to sort.
        /// </param>
        /// <param name="orderCollection">
        /// The collection to order by.
        /// </param>
        /// <returns>
        /// The sorted collection.
        /// </returns>
        /// <remarks>
        /// If the collection to order by is missing an item, the original order will be used as
        /// a fallback.
        /// </remarks>
        public static IEnumerable<T> OrderByCollection<T>(this IEnumerable<T> items,
            IEnumerable<T> orderCollection)
        {

            // Store the sort order in a dictionary for faster lookup.
            var orderIndex = default(int);
            var byId = orderCollection.Select((item, index) => new
            {
                Item = item,
                Index = index
            })
            .ToDictionary(x => x.Item, x => x.Index);

            // Return the sorted items.
            return items

                // Store information about the sort order.
                .Select((item, index) => new
                {
                    Item = item,
                    OriginalIndex = index,
                    HasOrderIndex = byId.TryGetValue(item, out orderIndex),
                    OrderIndex = orderIndex
                })

                // First, order by whether or not the item exists in the ordered collection.
                .OrderBy(x => x.HasOrderIndex ? 0 : 1)

                // Then, order by the order index (or, fallback to the original index).
                .ThenBy(x => x.HasOrderIndex ? x.OrderIndex : x.OriginalIndex)

                // Return the items.
                .Select(x => x.Item)
                .ToArray();

        }

        #endregion

    }

}