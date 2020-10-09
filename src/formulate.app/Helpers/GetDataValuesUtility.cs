namespace formulate.app.Helpers
{
    using formulate.app.CollectionBuilders;
    using formulate.app.DataValues.DataInterfaces;
    using formulate.app.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The get data values utility.
    /// </summary>
    internal class GetDataValuesUtility : IGetDataValuesUtility
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDataValuesUtility"/> class.
        /// </summary>
        /// <param name="dataValuePersistence">
        /// The data value persistence.
        /// </param>
        /// <param name="dataValueKindCollection">
        /// The data value kind collection.
        /// </param>
        public GetDataValuesUtility(IDataValuePersistence dataValuePersistence, DataValueKindCollection dataValueKindCollection)
        {
            DataValues = dataValuePersistence;
            DataValueKindCollection = dataValueKindCollection;
        }

        /// <summary>
        /// Gets or sets the data values.
        /// </summary>
        private IDataValuePersistence DataValues { get; set; }

        /// <summary>
        /// Gets or sets the data value kind collection.
        /// </summary>
        private DataValueKindCollection DataValueKindCollection { get; set; }

        /// <inheritdoc />
        public IEnumerable<KeyValuePair<string, string>> GetById(Guid id)
        {
            var items = new List<KeyValuePair<string, string>>();
            var dataValue = DataValues.Retrieve(id);

            if (dataValue == null)
            {
                return items;
            }

            // Extract list items from the data value.
            var kind = DataValueKindCollection.FirstOrDefault(x => x.Id == dataValue.KindId);

            // Check type of collection returned by the data value kind.
            if (kind is IGetValueAndLabelCollection pairCollection)
            {
                // Create drop down items from values and labels.
                var pairs = pairCollection.GetValues(dataValue.Data);
                items.AddRange(pairs.Select(x => new KeyValuePair<string, string>(x.Label, x.Value)));

            }
            else if (kind is IGetStringCollection stringCollection)
            {
                // Create drop down items from strings.
                var strings = stringCollection.GetValues(dataValue.Data);
                items.AddRange(strings.Select(x => new KeyValuePair<string, string>(x, x)));
            }

            return items;
        }
    }
}
