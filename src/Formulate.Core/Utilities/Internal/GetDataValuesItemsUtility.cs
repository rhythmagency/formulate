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
        /// The data values entity repository.
        /// </summary>
        private readonly IDataValuesEntityRepository _dataValuesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDataValuesItemsUtility"/>
        /// class.
        /// </summary>
        /// <param name="dataValuesFactory">
        /// The data values factory.
        /// </param>
        /// <param name="dataValuesRepository">
        /// The data values entity repo.
        /// </param>
        public GetDataValuesItemsUtility(IDataValuesFactory dataValuesFactory,
            IDataValuesEntityRepository dataValuesRepository)
        {
            _dataValuesFactory = dataValuesFactory;
            _dataValuesRepository = dataValuesRepository;
        }

        /// <inheritdoc />
        public IReadOnlyCollection<KeyValuePair<string, string>> GetValues(Guid id)
        {
            var settings = _dataValuesRepository.Get(id);

            var definition = settings == null
                ? null 
                : _dataValuesFactory.Create(settings);

            if (definition is null)
            {
                return Array.Empty<KeyValuePair<string, string>>();
            }

            return definition.Items;
        }
    }
}