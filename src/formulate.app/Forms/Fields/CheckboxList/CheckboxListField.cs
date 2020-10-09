namespace formulate.app.Forms.Fields.CheckboxList
{

    // Namespaces.
    using Helpers;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A checkbox list form field type.
    /// </summary>
    public class CheckboxListField : IFormFieldType
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckboxListField"/> class.
        /// </summary>
        /// <param name="getDataValuesHelper">
        /// The get data values helper.
        /// </param>
        /// <remarks>
        /// Default constructor.
        /// </remarks>
        public CheckboxListField(IGetDataValuesHelper getDataValuesHelper)
        {
            this.GetDataValuesHelper = getDataValuesHelper;
        }

        #endregion


        #region Public Properties

        /// <inheritdoc />
        public string Directive => "formulate-checkbox-list-field";

        /// <inheritdoc />
        public string TypeLabel => "Checkbox List";

        /// <inheritdoc />
        public string Icon => "icon-formulate-checkbox-list";

        /// <inheritdoc />
        public Guid TypeId => new Guid("02DBA5DA4B93439EB63F43392E443DCF");

        #endregion

        #region Private Properties

        /// <summary>
        /// Gets or sets the get data values helper.
        /// </summary>
        private IGetDataValuesHelper GetDataValuesHelper { get; set; }

        #endregion


        #region Public Methods

        /// <summary>
        /// Deserialize the configuration for a checkbox list field.
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
            var items = new List<CheckboxListItem>();
            var configData = JsonHelper.Deserialize<JObject>(configuration);
            var dynamicConfig = configData as dynamic;
            var properties = configData.Properties().Select(x => x.Name);
            var propertySet = new HashSet<string>(properties);


            // A data value is selected?
            if (propertySet.Contains("dataValue"))
            {
                // Get info about the data value.
                var dataValueId = GuidHelper.GetGuid(dynamicConfig.dataValue.Value as string);
                var dataValues = this.GetDataValuesHelper.GetById(dataValueId);

                items.AddRange(dataValues.Select(x => new CheckboxListItem()
                {
                    Selected = false,
                    Value = x.Value,
                    Label = x.Key
                }));
            }

            // Return the data value configuration.
            return new CheckboxListConfiguration()
            {
                Items = items
            };
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
