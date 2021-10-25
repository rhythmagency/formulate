using System.Collections.Generic;

namespace Formulate.Core.DataValues
{
    /// <summary>
    /// A data values.
    /// </summary>
    public sealed class DataValues : DataValuesBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="items">The items.</param>
        public DataValues(IDataValuesSettings settings, IReadOnlyCollection<KeyValuePair<string, string>> items) : base(settings, items)
        {
        }
    }
}
