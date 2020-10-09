namespace formulate.app.Forms.Fields.RadioButtonList
{

    // Namespaces.
    using DataValues.DataInterfaces;
    using Helpers;
    using Newtonsoft.Json.Linq;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using formulate.app.CollectionBuilders;

    /// <summary>
    /// A radio button list form field type.
    /// </summary>
    public class RadioButtonListField : IFormFieldType
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RadioButtonListField"/> class. 
        /// The radio button list field.
        /// </summary>
        /// <param name="dataValuePersistence">
        /// The data Value Persistence.
        /// </param>
        /// <param name="dataValueKindCollection">
        /// The data Value Kind Collection.
        /// </param>
        /// <remarks>
        /// Default constructor.
        /// </remarks>
        public RadioButtonListField(IDataValuePersistence dataValuePersistence, DataValueKindCollection dataValueKindCollection)
        {
            DataValues = dataValuePersistence;
            DataValueKindCollection = dataValueKindCollection;
        }

        #endregion

        #region Public Properties

        /// <inheritdoc />
        public string Directive => "formulate-radio-button-list-field";

        /// <inheritdoc />
        public string TypeLabel => "Radio Button List";

        /// <inheritdoc />
        public string Icon => "icon-formulate-radio-button-list";

        /// <inheritdoc />
        public Guid TypeId => new Guid("E5F42754D82D468DBCBFCEE115E9563D");

        #endregion


        #region Private Properties

        /// <summary>
        /// Gets or sets the data values.
        /// </summary>
        private IDataValuePersistence DataValues { get; set; }

        /// <summary>
        /// Gets or sets the data value kind collection.
        /// </summary>
        private DataValueKindCollection DataValueKindCollection { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Deserialize the configuration for a radio button list field.
        /// </summary>
        /// <param name="configuration">
        /// The serialized configuration.
        /// </param>
        /// <returns>
        /// The deserialized configuration.
        /// </returns>
        public object DeserializeConfiguration(string configuration)
        {

            // Variables.
            var defaultOrientation = "Horizontal";
            var items = new List<RadioButtonListItem>();
            var config = new RadioButtonListConfiguration()
            {
                Items = items,
                Orientation = defaultOrientation
            };
            var configData = JsonHelper.Deserialize<JObject>(configuration);
            var dynamicConfig = configData as dynamic;
            var properties = configData.Properties().Select(x => x.Name);
            var propertySet = new HashSet<string>(properties);


            // An orientation is set?
            if (propertySet.Contains("orientation"))
            {
                config.Orientation = dynamicConfig.orientation.Value as string;
                if (string.IsNullOrWhiteSpace(config.Orientation))
                {
                    config.Orientation = defaultOrientation;
                }
            }


            // A data value is selected?
            if (propertySet.Contains("dataValue"))
            {

                // Get info about the data value.
                var dataValueId = GuidHelper.GetGuid(dynamicConfig.dataValue.Value as string);
                var dataValue = DataValues.Retrieve(dataValueId);
                if (dataValue != null)
                {

                    // Extract list items from the data value.
                    var kind = DataValueKindCollection.FirstOrDefault(x => x.Id == dataValue.KindId);
                    var pairCollection = kind as IGetValueAndLabelCollection;
                    var stringCollection = kind as IGetStringCollection;


                    // Check type of collection returned by the data value kind.
                    if (pairCollection != null)
                    {

                        // Create radio buttons from values and labels.
                        var pairs = pairCollection.GetValues(dataValue.Data);
                        items.AddRange(pairs.Select(x => new RadioButtonListItem()
                        {
                            Selected = false,
                            Value = x.Value,
                            Label = x.Label
                        }));

                    }
                    else if (stringCollection != null)
                    {

                        // Create radio buttons from strings.
                        var strings = stringCollection.GetValues(dataValue.Data);
                        items.AddRange(strings.Select(x => new RadioButtonListItem()
                        {
                            Selected = false,
                            Value = x,
                            Label = x
                        }));
                    }
                }
            }

            // Return the data value configuration.
            return config;
        }


        /// <summary>
        /// Formats a value in the specified field presentation format.
        /// </summary>
        /// <param name="values">
        /// The values to format.
        /// </param>
        /// <param name="format">
        /// The format to present the value in.
        /// </param>
        /// <param name="configuration">
        /// The configuration for this field.
        /// </param>
        /// <returns>
        /// The formatted value.
        /// </returns>
        public string FormatValue(IEnumerable<string> values, FieldPresentationFormats format, object configuration)
        {
            return string.Join(", ", values);
        }

        #endregion
    }
}
