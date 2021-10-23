using System;
using Formulate.Core.Types;

namespace Formulate.Core.FormFields
{
    /// <summary>
    /// Settings for creating a form field.
    /// </summary>
    public interface IFormFieldSettings : IFormulateType
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the alias.
        /// </summary>
        string Alias { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the label.
        /// </summary>
        string Label { get; }

        /// <summary>
        /// Gets the field configuration.
        /// </summary>
        string Configuration { get; }

        /// <summary>
        /// Gets the category.
        /// </summary>
        string Category { get; }

        /// <summary>
        /// Gets the validations.
        /// </summary>
        Guid[] Validations { get; }
    }
}