namespace formulate.app.Forms.Fields.ExtendedRadioButtonList
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
    /// An extended radio button list field type.
    /// </summary>
    /// <remarks>
    /// The extended radio button list differs from the plain radio button list in that each item
    /// has two text fields associated with it. You can use this, for example, to add some clarifying
    /// text under each radio button selection.
    /// </remarks>
    public class ExtendedRadioButtonListField : IFormFieldType
    {

        #region Private Properties

        private IDataValuePersistence DataValues { get; set; }
        private DataValueKindCollection DataValueKindCollection { get; set; }

        #endregion


        #region Public Properties

        /// <summary>
        /// The Angular directive for this field type.
        /// </summary>
        public string Directive => "formulate-extended-radio-button-list-field";


        /// <summary>
        /// The label to show in the UI for this field type.
        /// </summary>
        public string TypeLabel => "Extended Radio Button List";


        /// <summary>
        /// The icon to display in the selection screen for this field type.
        /// </summary>
        public string Icon => "icon-formulate-extended-radio-button-list";


        /// <summary>
        /// The GUID that uniquely identifies this field type (useful for serialization).
        /// </summary>
        public Guid TypeId => new Guid("8B277AF31B1F469897D23900C6621C7D");

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ExtendedRadioButtonListField(IDataValuePersistence dataValuePersistence, DataValueKindCollection dataValueKindCollection)
        {
            DataValues = dataValuePersistence;
            DataValueKindCollection = dataValueKindCollection;
        }

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
            var items = new List<ExtendedRadioButtonListItem>();
            var config = new ExtendedRadioButtonListConfiguration()
            {
                Items = items
            };
            var configData = JsonHelper.Deserialize<JObject>(configuration);
            var dynamicConfig = configData as dynamic;
            var properties = configData.Properties().Select(x => x.Name);
            var propertySet = new HashSet<string>(properties);


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
                        items.AddRange(pairs.Select(x => new ExtendedRadioButtonListItem()
                        {
                            Selected = false,
                            Primary = x.Value,
                            Secondary = x.Label
                        }));

                    }
                    else if (stringCollection != null)
                    {

                        // Create radio buttons from strings.
                        var strings = stringCollection.GetValues(dataValue.Data);
                        items.AddRange(strings.Select(x => new ExtendedRadioButtonListItem()
                        {
                            Selected = false,
                            Primary = x,
                            Secondary = null
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
        public string FormatValue(IEnumerable<string> values, FieldPresentationFormats format,
            object configuration)
        {
            return string.Join(", ", values);
        }

        #endregion

    }

}