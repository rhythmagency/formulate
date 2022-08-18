namespace Formulate.Core.FormHandlers.StoreData
{
    // Namespaces.
    using System;

    /// <summary>
    /// The data definition for a form handler that stores data to the database.
    /// </summary>
    public sealed class StoreDataDefinition : FormHandlerDefinition
    {
        /// <summary>
        /// Constants related to <see cref="StoreDataDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The kind ID.
            /// </summary>
            public const string KindId = "238EA92071F44D8C9CC433D7181C9C46";

            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "Store Data";

            /// <summary>
            /// The icon.
            /// </summary>
            public const string Icon = "icon-formulate-store-data";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-store-data-handler";
        }

        /// <inheritdoc />
        public override string Category => FormHandlerConstants.Categories.Persistence;

        /// <inheritdoc />
        public override string Directive => Constants.Directive;

        /// <inheritdoc />
        public override string Name => Constants.Name;

        /// <inheritdoc />
        public override Guid KindId => Guid.Parse(Constants.KindId);

        /// <inheritdoc />
        public override string Icon => Constants.Icon;

        /// <inheritdoc />
        public override FormHandler CreateHandler(IFormHandlerSettings settings)
        {
            var handler = new StoreDataHandler(settings);
            return handler;
        }

        /// <inheritdoc />
        public override object GetBackOfficeConfiguration(IFormHandlerSettings settings)
        {
            //TODO: Implement.
            return null;
        }
    }
}