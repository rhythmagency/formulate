namespace Formulate.Core.DataValues.Legacy
{
    using System;
    
    public sealed class LegacyDataValuesDefinition : DataValuesDefinition
    {
        /// <summary>
        /// Constants related to <see cref="ListDataValuesDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The kind ID.
            /// </summary>
            public const string KindId = "BBF66F6A8F7D4ABA9D5B194A46084EC2";

            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "Legacy";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-legacy-data-value";

            /// <summary>
            /// The icon.
            /// </summary>
            public const string Icon = "icon-block";
        }

        /// <inheritdoc />
        public override bool IsLegacy => true;

        /// <inheritdoc />
        public override Guid KindId => Guid.Parse(Constants.KindId);

        /// <inheritdoc />
        public override string Name => Constants.Name;

        /// <inheritdoc />
        public override string Directive => Constants.Directive;

        /// <inheritdoc />
        public override string Icon => Constants.Icon;

        public override IDataValues CreateDataValues(IDataValuesSettings settings)
        {
            return default;
        }

        public override object GetBackOfficeConfiguration(IDataValuesSettings settings)
        {
            return default;
        }
    }
}
