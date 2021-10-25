using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Formulate.Core.DataValues;

namespace Formulate.Core.Utilities.Internal
{
    /// <summary>
    /// The default implementation of <see cref="IGetDataValuesItemsUtility"/>/
    /// </summary>
    internal sealed class GetDataValuesItemsUtility : IGetDataValuesItemsUtility
    {
        /// <summary>
        /// The data values factory.
        /// </summary>
        private readonly IDataValuesFactory _dataValuesFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDataValuesItemsUtility"/> class.
        /// </summary>
        /// <param name="dataValuesFactory">The data values factory.</param>
        public GetDataValuesItemsUtility(IDataValuesFactory dataValuesFactory)
        {
            _dataValuesFactory = dataValuesFactory;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<KeyValuePair<string, string>>> GetValuesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // TODO: get data value settings for entity id.
            var settings = default(IDataValuesSettings);

            var definition = await _dataValuesFactory.CreateAsync(settings, cancellationToken);

            if (definition is null)
            {
                return Array.Empty<KeyValuePair<string, string>>();
            }

            return definition.Items;
        }
    }
}
