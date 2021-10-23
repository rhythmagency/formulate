using System.Collections.Generic;
using Formulate.Core.Validations;

namespace Formulate.Core.FormFields.Button
{
    /// <summary>
    /// A button field.
    /// </summary>
    public sealed class ButtonField : FormField<ButtonFieldConfiguration>
    {
        /// <inheritdoc />
        public ButtonField(IFormFieldSettings settings, IReadOnlyCollection<IValidation> validations, ButtonFieldConfiguration configuration) : base(settings, validations, configuration)
        {
        }

        /// <inheritdoc />
        public ButtonField(IFormFieldSettings settings, ButtonFieldConfiguration configuration) : base(settings, configuration)
        {
        }
    }
}
