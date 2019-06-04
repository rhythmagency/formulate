namespace formulate.app.Forms.Fields.DropDown
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
    /// A drop down field type.
    /// </summary>
    public class DropDownField : IFormFieldType
    {

        #region Private Properties

        private IDataValuePersistence DataValues { get; set; }
        private DataValueKindCollection DataValueKindCollection { get; set; }

        #endregion


        #region Public Properties

        /// <summary>
        /// The Angular directive for this field type.
        /// </summary>
        public string Directive => "formulate-drop-down-field";


        /// <summary>
        /// The label to show in the UI for this field type.
        /// </summary>
        public string TypeLabel => "Drop Down";


        /// <summary>
        /// The icon to display in the selection screen for this field type.
        /// </summary>
        public string Icon => "icon-formulate-drop-down";


        /// <summary>
        /// The GUID that uniquely identifies this field type (useful for serialization).
        /// </summary>
        public Guid TypeId => new Guid("6D3DF1571BC44FCFB2B70A94FE719B47");

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DropDownField(IDataValuePersistence dataValuePersistence, DataValueKindCollection dataValueKindCollection)
        {
            DataValues = dataValuePersistence;
            DataValueKindCollection = dataValueKindCollection;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Deserialize the configuration for a drop down field.
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
            var items = new List<DropDownItem>();
            var config = new DropDownConfiguration()
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

                        // Create drop down items from values and labels.
                        var pairs = pairCollection.GetValues(dataValue.Data);
                        items.AddRange(pairs.Select(x => new DropDownItem()
                        {
                            Selected = false,
                            Value = x.Value,
                            Label = x.Label
                        }));

                    }
                    else if (stringCollection != null)
                    {

                        // Create drop down items from strings.
                        var strings = stringCollection.GetValues(dataValue.Data);
                        items.AddRange(strings.Select(x => new DropDownItem()
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
        public string FormatValue(IEnumerable<string> values, FieldPresentationFormats format,
            object configuration)
        {
            return string.Join(", ", values);
        }

        #endregion

    }

}