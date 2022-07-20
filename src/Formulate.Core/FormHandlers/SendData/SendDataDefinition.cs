namespace Formulate.Core.FormHandlers.SendData
{
    // Namespaces.
    using System;

    /// <summary>
    /// The data definition for a form handler that sends data to a URL.
    /// </summary>
    public sealed class SendDataDefinition : FormHandlerDefinition
    {
        /// <summary>
        /// Constants related to <see cref="SendDataDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The kind ID.
            /// </summary>
            public const string KindId = "C76E8D1D5DF244CB8FA285C32312D688";

            /// <summary>
            /// The definition label.
            /// </summary>
            public const string DefinitionLabel = "Send Data";

            /// <summary>
            /// The icon.
            /// </summary>
            public const string Icon = "icon-formulate-send-data";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-send-data-handler";
        }

        /// <inheritdoc />
        public override string Directive => Constants.Directive;

        /// <inheritdoc />
        public override string DefinitionLabel => Constants.DefinitionLabel;

        /// <inheritdoc />
        public override Guid KindId => Guid.Parse(Constants.KindId);

        /// <inheritdoc />
        public override string Icon => Constants.Icon;

        /// <inheritdoc />
        public override FormHandler CreateHandler(IFormHandlerSettings settings)
        {
            var handler = new SendDataHandler(settings);
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