using System;

namespace Formulate.Core.FormFields
{
    /// <summary>
    /// An abstract class for creating a new <see cref="IFormFieldDefinition"/>.
    /// </summary>
    /// <remarks>It is not necessary to implement from this class but it does provide some helpful defaults for certain optional fields.</remarks>
    public abstract class FormFieldDefinition : IFormFieldDefinition
    {
        /// <inheritdoc />
        public abstract Guid DefinitionId { get; }
        
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
        public abstract IFormField CreateField(IFormFieldSettings settings);
    }
}