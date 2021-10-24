using System;
using System.Collections.Generic;
using System.Linq;

namespace Formulate.Core.Types
{
    /// <summary>
    /// Extension methods that augments collections of <see cref="IType"/>.
    /// </summary>
    public static class TypeCollectionExtensions
    {
        /// <summary>
        /// Gets the first or default <see cref="IType"/> which matches the provided <see cref="Guid"/>.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="id">The id.</param>
        /// <returns>A <typeparamref name="T"/> or default.</returns>
        public static T FirstOrDefault<T>(this IEnumerable<T> collection, Guid id) where T : IType
        {
            return collection is null ? default : collection.FirstOrDefault(x => x.TypeId == id);
        }

        /// <summary>
        /// Gets the first or default <see cref="IType"/> which matches the provided <see cref="Guid"/>.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="id">The id.</param>
        /// <returns>A <typeparamref name="T"/> or default.</returns>
        public static T FirstOrDefault<T>(this IEnumerable<T> collection, Guid? id) where T : IType
        {
            return id is null ? default : collection.FirstOrDefault(id.Value);
        }
    }
}
