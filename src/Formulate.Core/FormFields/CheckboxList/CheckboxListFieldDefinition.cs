namespace Formulate.Core.FormFields.CheckboxList
{
    // Namesapces.
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;

    /// <summary>
    /// A drop down form field definition.
    /// </summary>
    public sealed class CheckboxListFieldDefinition : FormFieldDefinitionBase<CheckboxListField>
    {
        /// <summary>
        /// The json utility.
        /// </summary>
        private readonly IJsonUtility _jsonUtility;

        /// <summary>
        /// The get data values items utility.
        /// </summary>
        private readonly IGetDataValuesItemsUtility _getDataValuesItemsUtility;

        /// <summary>
        /// Constants related to <see cref="CheckboxListFieldDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The kind ID.
            /// </summary>
            public const string KindId = "02DBA5DA4B93439EB63F43392E443DCF";

            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "Checkbox List";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-checkbox-list-field";

            /// <summary>
            /// The icon.
            /// </summary>
            public const string Icon = "icon-formulate-checkbox-list";
        }

        /// <inheritdoc />
        public override Guid KindId => Guid.Parse(Constants.KindId);

        /// <inheritdoc />
        public override string Name => Constants.Name;

        /// <inheritdoc />
        public override string Icon => Constants.Icon;

        /// <inheritdoc />
        public override string Directive => Constants.Directive;

        /// <inheritdoc />
        public override string Category => FormFieldConstants.Categories.Inputs;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckboxListFieldDefinition"/> class. 
        /// </summary>
        /// <param name="jsonUtility">
        /// The json utility.
        /// </param>
        /// <param name="getDataValuesItemsUtility">
        /// The get data values items utility.
        /// </param>
        /// <remarks>
        /// Default constructor.
        /// </remarks>
        public CheckboxListFieldDefinition(
            IJsonUtility jsonUtility,
            IGetDataValuesItemsUtility getDataValuesItemsUtility)
        {
            _jsonUtility = jsonUtility;
            _getDataValuesItemsUtility = getDataValuesItemsUtility;
        }

        /// <inheritdoc />
        public override FormField CreateField(IFormFieldSettings settings)
        {
            var items = new List<CheckboxListFieldItem>();
            var preValues = _jsonUtility.Deserialize<CheckboxListFieldPreValues>(settings.Data);

            if (preValues is not null)
            {
                var values = _getDataValuesItemsUtility.GetValues(preValues.DataValue);

                if (values is not null)
                {
                    items.AddRange(values.Select(x => new CheckboxListFieldItem()
                    {
                        Selected = false,
                        Value = x.Value,
                        Label = x.Key
                    }).ToArray());
                }
            }

            var config = new CheckboxListFieldConfiguration()
            {
                Items = items
            };

            return new CheckboxListField(settings, config);
        }

        /// <inheritdoc />
        public override object GetBackOfficeConfiguration(IFormFieldSettings settings)
        {
            if (settings is null)
            {
                return default;
            }

            var savedSettings = _jsonUtility.Deserialize<CheckboxListFieldPreValues>(settings.Data);

            if (savedSettings is not null)
            {
                return savedSettings;
            }

            return new CheckboxListFieldPreValues();
        }
    }
}