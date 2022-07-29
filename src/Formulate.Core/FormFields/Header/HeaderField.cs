using System.Collections.Generic;
using Formulate.Core.Validations;

namespace Formulate.Core.FormFields.Header
{
    /// <summary>
    /// A text field.
    /// </summary>
    public sealed class HeaderField : FormField
    {
        /// <inheritdoc />
        public HeaderField(IFormFieldSettings settings) : base(settings)
        {
        }

        /// <inheritdoc />
        public HeaderField(IFormFieldSettings settings, IReadOnlyCollection<IValidation> validations) : base(settings, validations)
        {
        }
    }
}
