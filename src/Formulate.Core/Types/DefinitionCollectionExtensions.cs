using System;
using System.Collections.Generic;
using System.Linq;

namespace Formulate.Core.Types
{
    /// <summary>
    /// Extension methods that augments collections of <see cref="IDefinition"/>.
    /// </summary>
    public static class DefinitionCollectionExtensions
    {
        /// <summary>
        /// Gets the first or default <see cref="IDefinition"/> which matches the provided <see cref="Guid"/>.
        /// </summary>
        /// <definitionparam name="T">The definition of the collection.</definitionparam>
        /// <param name="collection">The collection</param>
        /// <param name="id">The id.</param>
        /// <returns>A <definitionparamref name="T"/> or default.</returns>
        public static T FirstOrDefault<T>(this IEnumerable<T> collection, Guid id) where T : IDefinition
        {
            return collection is null ? default : collection.FirstOrDefault(x => x.KindId == id);
        }

        /// <summary>
        /// Gets the first or default <see cref="IDefinition"/> which matches the provided <see cref="Guid"/>.
        /// </summary>
        /// <definitionparam name="T">The definition of the collection.</definitionparam>
        /// <param name="collection">The collection</param>
        /// <param name="id">The id.</param>
        /// <returns>A <definitionparamref name="T"/> or default.</returns>
        public static T FirstOrDefault<T>(this IEnumerable<T> collection, Guid? id) where T : IDefinition
        {
            return id is null ? default : collection.FirstOrDefault(id.Value);
        }
    }
}
