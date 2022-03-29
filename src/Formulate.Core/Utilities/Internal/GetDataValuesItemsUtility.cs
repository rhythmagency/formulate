namespace Formulate.Core.Utilities.Internal
{
    // Namespaces.
    using DataValues;
    using System;
    using System.Collections.Generic;

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
        public IReadOnlyCollection<KeyValuePair<string, string>> GetValues(Guid id)
        {
            // TODO: get data value settings for entity id.
            var settings = default(IDataValuesSettings);

            var definition = _dataValuesFactory.Create(settings);

            if (definition is null)
            {
                return Array.Empty<KeyValuePair<string, string>>();
            }

            return definition.Items;
        }
    }
}