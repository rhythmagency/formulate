using System.Collections.Generic;
using Formulate.Core.Types;

namespace Formulate.Core.DataValues
{
    /// <summary>
    /// A contract for creating a data values.
    /// </summary>
    public interface IDataValues : IEntity
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        IReadOnlyCollection<KeyValuePair<string, string>> Items { get; }
    }
}
