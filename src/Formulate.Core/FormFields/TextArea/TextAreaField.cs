using System.Collections.Generic;
using Formulate.Core.Validations;

namespace Formulate.Core.FormFields.TextArea
{
    /// <summary>
    /// A text area field.
    /// </summary>
    public sealed class TextAreaField : FormField
    {
        /// <inheritdoc />
        public TextAreaField(IFormFieldSettings settings) : base(settings)
        {
        }

        /// <inheritdoc />
        public TextAreaField(IFormFieldSettings settings, IReadOnlyCollection<IValidation> validations) : base(settings, validations)
        {
        }
    }
}
