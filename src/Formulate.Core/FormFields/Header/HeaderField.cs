using System.Collections.Generic;
using Formulate.Core.Validations;

namespace Formulate.Core.FormFields.Header
{
    /// <summary>
    /// A text field.
    /// </summary>
    public sealed class HeaderField : FormField<HeaderFieldConfiguration>
    {
        /// <inheritdoc />
        public HeaderField(IFormFieldSettings settings, IReadOnlyCollection<IValidation> validations, HeaderFieldConfiguration configuration) : base(settings, validations, configuration)
        {
        }

        /// <inheritdoc />
        public HeaderField(IFormFieldSettings settings, HeaderFieldConfiguration configuration) : base(settings, configuration)
        {
        }
    }
}
