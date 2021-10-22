using System;
using System.Collections.Generic;
using System.Linq;

namespace Formulate.Core.Types
{
    /// <summary>
    /// Extension methods that augments collections of <see cref="IFormulateType"/>,
    /// </summary>
    public static class FormulateTypeCollectionExtensions
    {
        /// <summary>
        /// Gets the first or default <see cref="IFormulateType"/> which matches the provided <see cref="Guid"/>.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collection">The collection</param>
        /// <param name="id">The id.</param>
        /// <returns>A <typeparamref name="T"/> or default.</returns>
        public static T FirstOrDefault<T>(this IEnumerable<T> collection, Guid id) where T : IFormulateType
        {
            return collection is null ? default : collection.FirstOrDefault(x => x.TypeId == id);
        }
    }
}
