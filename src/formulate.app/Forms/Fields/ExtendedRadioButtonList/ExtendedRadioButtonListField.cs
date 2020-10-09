namespace formulate.app.Forms.Fields.ExtendedRadioButtonList
{

    // Namespaces.
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// An extended radio button list form field type.
    /// </summary>
    /// <remarks>
    /// The extended radio button list differs from the plain radio button list in that each item
    /// has two text fields associated with it. You can use this, for example, to add some clarifying
    /// text under each radio button selection.
    /// </remarks>
    public class ExtendedRadioButtonListField : IFormFieldType
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedRadioButtonListField"/> class. 
        /// </summary>
        /// <param name="getDataValuesHelper">
        /// The get Data Values Helper.
        /// </param>
        /// <remarks>
        /// Default constructor.
        /// </remarks>
        public ExtendedRadioButtonListField(IGetDataValuesHelper getDataValuesHelper)
        {
            this.GetDataValuesHelper = getDataValuesHelper;
        }

        #endregion

        #region Public Properties

        /// <inheritdoc />
        public string Directive => "formulate-extended-radio-button-list-field";

        /// <inheritdoc />
        public string TypeLabel => "Extended Radio Button List";

        /// <inheritdoc />
        public string Icon => "icon-formulate-extended-radio-button-list";

        /// <inheritdoc />
        public Guid TypeId => new Guid("8B277AF31B1F469897D23900C6621C7D");

        #endregion
        
        #region Private Properties

        /// <summary>
        /// Gets or sets the get data values helper.
        /// </summary>
        private IGetDataValuesHelper GetDataValuesHelper { get; set; }

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
            var configPrevalues = JsonHelper.Deserialize<ExtendedRadioButtonListConfigurationPrevalues>(configuration);

            // A data value is selected?
            if (string.IsNullOrWhiteSpace(configPrevalues.DataValue) == false)
            {
                // Get info about the data value.
                var dataValueId = GuidHelper.GetGuid(configPrevalues.DataValue);
                var dataValues = this.GetDataValuesHelper.GetById(dataValueId);

                items.AddRange(dataValues.Select(x => new ExtendedRadioButtonListItem()
                {
                    Selected = false,
                    Primary = x.Value,
                    Secondary = x.Key == x.Value ? string.Empty : x.Key
                }));
            }

            // Return the data value configuration.
            return new ExtendedRadioButtonListConfiguration()
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
