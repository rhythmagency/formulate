namespace Formulate.Core.DataValues
{
    // Namespaces.
    using System.Collections.Generic;
    using Types;

    /// <summary>
    /// A contract for creating a data values.
    /// </summary>
    public interface IDataValues : IEntity
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        IReadOnlyCollection<KeyValuePair<string, string>> Items { get; }

        /// <summary>
        /// The name of this data value.
        /// </summary>
        string Name { get; }
    }
}