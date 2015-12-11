namespace formulate.core.Extensions
{

    // Namespaces.
    using System.Collections.Generic;


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

        #endregion

    }

}