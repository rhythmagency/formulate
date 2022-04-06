namespace Formulate.Core.Utilities
{
    // Namespaces.
    using DataValues;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A contract for creating a utility that gets the items from
    /// a <see cref="IDataValues"/>.
    /// </summary>
    public interface IGetDataValuesItemsUtility
    {
        /// <summary>
        /// Gets the values from a <see cref="IDataValues"/> for a given ID.
        /// </summary>
        /// <param name="id">
        /// The data values ID.
        /// </param>
        /// <returns>
        /// The data values.
        /// </returns>
        IReadOnlyCollection<KeyValuePair<string, string>> GetValues(Guid id);
    }
}