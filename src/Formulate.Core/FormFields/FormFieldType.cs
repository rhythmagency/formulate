using System;

namespace Formulate.Core.FormFields
{
    /// <summary>
    /// An abstract class for creating a new <see cref="IFormFieldType"/>.
    /// </summary>
    /// <remarks>It is not necessary to implement from this class but it does provide some helpful defaults for certain optional fields.</remarks>
    public abstract class FormFieldType : IFormFieldType
    {
        /// <inheritdoc />
        public abstract Guid TypeId { get; }
        
        /// <inheritdoc />
        public abstract string TypeLabel { get; }

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