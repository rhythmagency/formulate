using System;
using System.Collections.Generic;
using System.Linq;
using Formulate.Core.Utilities;

namespace Formulate.Core.DataValues.PairList
{
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
            /// The definition label.
            /// </summary>
            public const string DefinitionLabel = "Pair List";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-pair-list-data-values";

            /// <summary>
            /// The icon.
            /// </summary>
            public const string Icon = "icon-formulate-pair-list";
        }

        /// <inheritdoc />
        public override Guid KindId => Guid.Parse(Constants.KindId);

        /// <inheritdoc />
        public override string DefinitionLabel => Constants.DefinitionLabel;

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
        protected override IDataValues CreateDataValues(IDataValuesSettings settings)
        {
            var items = new List<KeyValuePair<string, string>>();
            var preValues = _jsonUtility.Deserialize<PairListDataValuesPreValues>(settings.Data);

            if (preValues is not null)
            {
                items.AddRange(preValues.Items.Select(x => new KeyValuePair<string, string>(x.Label, x.Value)).ToArray());
            }

            return new DataValues(settings, items);
        }
    }
}
