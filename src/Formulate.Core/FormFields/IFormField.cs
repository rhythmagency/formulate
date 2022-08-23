using System.Collections.Generic;
using Formulate.Core.Submissions.Requests;
using Formulate.Core.Types;
using Formulate.Core.Validations;

namespace Formulate.Core.FormFields
{
    /// <summary>
    /// A contract for creating a form field.
    /// </summary>
    public interface IFormField : IEntity
    {
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
        /// Gets the category.
        /// </summary>
        string Category { get; }

        /// <summary>
        /// Gets the validations.
        /// </summary>
        IReadOnlyCollection<IValidation> Validations { get; }

        FormFieldValidationResult Validate(IFormFieldValues values);
    }
}