using System.Collections.Generic;
using Formulate.Core.Validations;

namespace Formulate.Core.FormFields.DropDown
{
    public sealed class DropDownField : FormField<DropDownFieldConfiguration>
    {
        /// <inheritdoc />
        public DropDownField(IFormFieldSettings settings, IReadOnlyCollection<IValidation> validations, DropDownFieldConfiguration configuration) : base(settings, validations, configuration)
        {
        }

        /// <inheritdoc />
        public DropDownField(IFormFieldSettings settings, DropDownFieldConfiguration config) : base(settings, config)
        {
        }
    }
}