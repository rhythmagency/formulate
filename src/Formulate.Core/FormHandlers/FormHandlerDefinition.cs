namespace Formulate.Core.FormHandlers
{
    // Namespaces.
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// An abstract class for creating a form handler definition.
    /// </summary>
    [DataContract]
    public abstract class FormHandlerDefinition : IFormHandlerDefinition
    {
        /// <inheritdoc />
        [DataMember(Name = "icon")]
        public abstract string Icon { get; }

        /// <inheritdoc />
        [DataMember(Name = "kindId")]
        public abstract Guid KindId { get; }

        /// <inheritdoc />
        [DataMember(Name = "definitionLabel")]
        public abstract string DefinitionLabel { get; }

        /// <inheritdoc />
        [DataMember(Name = "directive")]
        public abstract string Directive { get; }

        /// <inheritdoc />
        [DataMember(Name = "category")]
        public virtual string Category => FormHandlerConstants.Categories.Uncategorized;

        /// <inheritdoc />
        public abstract FormHandler CreateHandler(IFormHandlerSettings settings);

        /// <inheritdoc />
        public abstract object GetBackOfficeConfiguration(IFormHandlerSettings settings);
    }
}