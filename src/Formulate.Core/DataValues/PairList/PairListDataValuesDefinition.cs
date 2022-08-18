namespace Formulate.Core.DataValues.PairList
{
    // Namespaces.
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;

    /// <summary>
    /// A data values definition for providing list of key value pairs.
    /// </summary>
    public sealed class PairListDataValuesDefinition : DataValuesDefinition
    {
        /// <summary>
        /// The json utility.
        /// </summary>
        private readonly IJsonUtility _jsonUtility;

        /// <summary>
        /// Constants related to <see cref="PairListDataValuesDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The kind ID.
            /// </summary>
            public const string KindId = "753A9598804448E39BC6200AC39E1D27";

            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "Pair List";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-data-value-pair-list";

            /// <summary>
            /// The icon.
            /// </summary>
            public const string Icon = "icon-formulate-pair-list";
        }

        /// <inheritdoc />
        public override Guid KindId => Guid.Parse(Constants.KindId);

        /// <inheritdoc />
        public override string Name => Constants.Name;

        /// <inheritdoc />
        public override string Directive => Constants.Directive;

        /// <inheritdoc />
        public override string Icon => Constants.Icon;

        /// <summary>
        /// Initializes a new instance of the <see cref="PairListDataValuesDefinition"/> class.
        /// </summary>
        /// <param name="jsonUtility">The json utility.</param>
        public PairListDataValuesDefinition(IJsonUtility jsonUtility)
        {
            _jsonUtility = jsonUtility;
        }

        /// <inheritdoc />
        public override IDataValues CreateDataValues(IDataValuesSettings settings)
        {
            var items = new List<KeyValuePair<string, string>>();
            var preValues = _jsonUtility.Deserialize<PairListDataValuesPreValues>(settings.Data);

            if (preValues is not null)
            {
                items.AddRange(preValues.Items.Select(x => new KeyValuePair<string, string>(x.Secondary, x.Primary)).ToArray());
            }

            return new DataValues(settings, items);
        }

        public override object GetBackOfficeConfiguration(IDataValuesSettings settings)
        {
            var preValues = _jsonUtility.Deserialize<PairListDataValuesPreValues>(settings.Data);

            if (preValues is not null)
            {
                return preValues;
            }

            return new PairListDataValuesPreValues()
            {
                Items = Array.Empty<PairListDataValuesPreValuesItem>()
            };
        }
    }
}