using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Formulate.Core.Utilities;

namespace Formulate.Core.FormFields.DropDown
{
    /// <summary>
    /// A drop down form field definition.
    /// </summary>
    public sealed class DropDownFieldDefinition : FormFieldDefinitionBase
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
        /// Constants related to <see cref="DropDownFieldDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The definition ID.
            /// </summary>
            public const string DefinitionId = "6D3DF1571BC44FCFB2B70A94FE719B47";

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
        public override Guid DefinitionId => Guid.Parse(Constants.DefinitionId);

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
        public DropDownFieldDefinition(IJsonUtility jsonUtility, IGetDataValuesItemsUtility getDataValuesItemsUtility)
        {
            _jsonUtility = jsonUtility;
            _getDataValuesItemsUtility = getDataValuesItemsUtility;
        }

        /// <inheritdoc />
        public override async Task<IFormField> CreateFieldAsync(IFormFieldSettings settings, CancellationToken cancellationToken = default)
        {
            var items = new List<DropDownFieldItem>();
            var preValues = _jsonUtility.Deserialize<DropDownFieldPreValues>(settings.Configuration);

            if (preValues is not null)
            {
                var values = await _getDataValuesItemsUtility.GetValuesAsync(preValues.DataValue, cancellationToken);

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
    }
}
