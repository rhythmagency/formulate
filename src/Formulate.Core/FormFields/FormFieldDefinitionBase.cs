namespace Formulate.Core.FormFields
{
    // Namespaces.
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// An abstract class for creating a new <see cref="IFormFieldDefinition"/>.
    /// </summary>
    /// <remarks>It is not necessary to implement from this class but it does provide some helpful defaults for certain optional fields.</remarks>
    [DataContract]
    public abstract class FormFieldDefinitionBase<TField> : IFormFieldDefinition
        where TField : IFormField
    {
        /// <inheritdoc />
        [DataMember(Name = "kindId")]
        public abstract Guid KindId { get; }

        /// <inheritdoc />
        [DataMember(Name = "definitionLabel")]
        public abstract string DefinitionLabel { get; }

        /// <inheritdoc />
        [DataMember(Name = "icon")]
        public abstract string Icon { get; }

        /// <inheritdoc />
        [DataMember(Name = "directive")]
        public abstract string Directive { get; }

        /// <inheritdoc />
        /// <remarks>Defaults to false.</remarks>
        [DataMember(Name = "isTransitory")]
        public virtual bool IsTransitory => false;

        /// <inheritdoc />
        /// <remarks>Defaults to false.</remarks>
        [DataMember(Name = "isServerSideOnly")]
        public virtual bool IsServerSideOnly => false;

        /// <inheritdoc />
        /// <remarks>Defaults to false.</remarks>
        [DataMember(Name = "isHidden")]
        public virtual bool IsHidden => false;

        /// <inheritdoc />
        /// <remarks>Defaults to true.</remarks>
        [DataMember(Name = "isStored")]
        public virtual bool IsStored => true;

        /// <inheritdoc />
        /// <remarks>Defaults to true.</remarks>
        [DataMember(Name = "supportsValidation")]
        public virtual bool SupportsValidation => true;

        /// <inheritdoc />
        /// <remarks>Defaults to true.</remarks>
        [DataMember(Name = "supportsFieldLabel")]
        public virtual bool SupportsFieldLabel => true;

        /// <inheritdoc />
        [DataMember(Name = "category")]
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
