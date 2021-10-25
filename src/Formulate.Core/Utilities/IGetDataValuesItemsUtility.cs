using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Formulate.Core.DataValues;

namespace Formulate.Core.Utilities
{
    /// <summary>
    /// A contract for creating a utility that gets the items from a <see cref="IDataValues"/>.
    /// </summary>
    public interface IGetDataValuesItemsUtility
    {
        /// <summary>
        /// Gets the values from a <see cref="IDataValues"/> for a given id.
        /// </summary>
        /// <param name="id">The data values id.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<KeyValuePair<string, string>>> GetValuesAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
