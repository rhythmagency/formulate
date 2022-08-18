namespace Formulate.Core.FormFields
{
    // Namespaces.
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// An abstract class for creating a new <see cref="IFormFieldDefinition"/>.
    /// </summary>
    /// <remarks>It is not necessary to implement from this class but it does provide some helpful defaults for certain optional fields.</remarks>
    public abstract class FormFieldDefinitionBase<TField> : IFormFieldDefinition
        where TField : IFormField
    {
        /// <inheritdoc />
        public abstract Guid KindId { get; }

        /// <inheritdoc />
        public abstract string DefinitionLabel { get; }

        /// <inheritdoc />
        public abstract string Icon { get; }

        /// <inheritdoc />
        public abstract string Directive { get; }

        /// <inheritdoc />
        /// <remarks>Defaults to false.</remarks>
        public virtual bool IsTransitory => false;

        /// <inheritdoc />
        /// <remarks>Defaults to false.</remarks>
        public virtual bool IsServerSideOnly => false;

        /// <inheritdoc />
        /// <remarks>Defaults to false.</remarks>
        public virtual bool IsHidden => false;

        /// <inheritdoc />
        /// <remarks>Defaults to true.</remarks>
        public virtual bool IsStored => true;

        /// <inheritdoc />
        /// <remarks>Defaults to true.</remarks>
        public virtual bool SupportsValidation => true;

        /// <inheritdoc />
        /// <remarks>Defaults to true.</remarks>
        public virtual bool SupportsLabel => true;

        /// <inheritdoc />
        public virtual string Category => FormFieldConstants.Categories.Uncategorized;

        /// <inheritdoc />
        public abstract FormField CreateField(IFormFieldSettings settings);

        /// <inheritdoc />
        public virtual object GetBackOfficeConfiguration(IFormFieldSettings settings)
        {
            return default;
        }
    }
}
