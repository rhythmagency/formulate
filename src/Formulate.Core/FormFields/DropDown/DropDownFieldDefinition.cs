﻿namespace Formulate.Core.FormFields.DropDown
{
    using Formulate.Core.DataValues;
    // Namesapces.
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;

    /// <summary>
    /// A drop down form field definition.
    /// </summary>
    public sealed class DropDownFieldDefinition : FormFieldDefinitionBase<DropDownField>
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
        /// The factory to create data value instances.
        /// </summary>
        private readonly IDataValuesFactory _dataValuesFactory;

        /// <summary>
        /// The repo to fetch data values.
        /// </summary>
        private readonly IDataValuesEntityRepository _dataValuesRepository;

        /// <summary>
        /// Constants related to <see cref="DropDownFieldDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The kind ID.
            /// </summary>
            public const string KindId = "6D3DF1571BC44FCFB2B70A94FE719B47";

            /// <summary>
            /// The definition label.
            /// </summary>
            public const string DefinitionLabel = "Drop Down";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-drop-down-field";

            /// <summary>
            /// The icon.
            /// </summary>
            public const string Icon = "icon-formulate-drop-down";
        }

        /// <inheritdoc />
        public override Guid KindId => Guid.Parse(Constants.KindId);

        /// <inheritdoc />
        public override string DefinitionLabel => Constants.DefinitionLabel;

        /// <inheritdoc />
        public override string Icon => Constants.Icon;

        /// <inheritdoc />
        public override string Directive => Constants.Directive;

        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownFieldDefinition"/> class. 
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
        public DropDownFieldDefinition(
            IJsonUtility jsonUtility,
            IGetDataValuesItemsUtility getDataValuesItemsUtility,
            IDataValuesFactory dataValuesFactory,
            IDataValuesEntityRepository dataValuesRepository)
        {
            _jsonUtility = jsonUtility;
            _getDataValuesItemsUtility = getDataValuesItemsUtility;
            _dataValuesFactory = dataValuesFactory;
            _dataValuesRepository = dataValuesRepository;
        }

        /// <inheritdoc />
        public override FormField CreateField(IFormFieldSettings settings)
        {
            var items = new List<DropDownFieldItem>();
            var preValues = _jsonUtility.Deserialize<DropDownFieldPreValues>(settings.Data);

            if (preValues is not null)
            {
                var values = _getDataValuesItemsUtility.GetValues(preValues.DataValue);

                if (values is not null)
                {
                    items.AddRange(values.Select(x => new DropDownFieldItem()
                    {
                        Selected = false,
                        Value = x.Value,
                        Label = x.Key
                    }).ToArray());
                }
            }

            var config = new DropDownFieldConfiguration()
            {
                Items = items
            };

            return new DropDownField(settings, config);
        }

        /// <inheritdoc />
        public override object GetBackOfficeConfiguration(IFormFieldSettings settings)
        {
            var preValues = settings.Data == null
                ? null
                : _jsonUtility.Deserialize<DropDownFieldPreValues>(settings.Data);
            var guid = preValues?.DataValue;
            if (guid.HasValue)
            {
                var valueSettings = _dataValuesRepository.Get(guid.Value);
                var definition = valueSettings == null
                    ? null
                    : _dataValuesFactory.Create(valueSettings);
                return definition == null
                    ? null
                    : new
                    {
                        definition.Id,
                        definition.Name,
                    };
            }
            else
            {
                return null;
            }
        }
    }
}