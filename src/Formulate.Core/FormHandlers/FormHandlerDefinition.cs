namespace Formulate.Core.FormHandlers
{
    // Namespaces.
    using System;

    /// <summary>
    /// An abstract class for creating a form handler definition.
    /// </summary>
    public abstract class FormHandlerDefinition : IFormHandlerDefinition
    {
        /// <inheritdoc />
        public abstract string Icon { get; }

        /// <inheritdoc />
        public abstract Guid KindId { get; }

        /// <inheritdoc />
        public abstract string DefinitionLabel { get; }

        /// <inheritdoc />
        public abstract string Directive { get; }

        /// <inheritdoc />
        public virtual string Category => FormHandlerConstants.Categories.Uncategorized;

        /// <inheritdoc />
        public abstract FormHandler CreateHandler(IFormHandlerSettings settings);

        /// <inheritdoc />
        public abstract object GetBackOfficeConfiguration(IFormHandlerSettings settings);
    }
}