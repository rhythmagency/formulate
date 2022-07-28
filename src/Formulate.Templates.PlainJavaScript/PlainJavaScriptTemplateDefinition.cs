namespace Formulate.Templates.PlainJavaScript
{
    using Formulate.Core.Templates;
    using System;

    /// <summary>
    /// A template definition for Plain JavaScript rendering.
    /// </summary>
    public sealed class PlainJavaScriptTemplateDefinition : ITemplateDefinition
    {
        /// <inheritdoc />
        public Guid Id => Guid.Parse("F3FB1485C1D14806B4190D7ABDE39530");

        /// <inheritdoc />
        public string Name => "Plain JavaScript";

        /// <inheritdoc />
        public string ViewName => "Plain JavaScript";
    }
}
