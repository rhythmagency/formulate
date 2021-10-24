using System.Collections.Generic;
using Formulate.Core.Validations;

namespace Formulate.Core.FormFields.Text
{
    /// <summary>
    /// A text field.
    /// </summary>
    public sealed class TextField : FormField
    {
        /// <inheritdoc />
        public TextField(IFormFieldSettings settings) : base(settings)
        {
        }

        /// <inheritdoc />
        public TextField(IFormFieldSettings settings, IReadOnlyCollection<IValidation> validations) : base(settings, validations)
        {
        }
    }
}
