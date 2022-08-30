using System.Collections.Generic;
using Formulate.Core.Validations;

namespace Formulate.Core.FormFields.RadioButtonList
{
    public sealed class RadioButtonListField : FormField<RadioButtonListFieldConfiguration>
    {
        /// <inheritdoc />
        public RadioButtonListField(IFormFieldSettings settings, IReadOnlyCollection<IValidation> validations, RadioButtonListFieldConfiguration configuration) : base(settings, validations, configuration)
        {
        }

        /// <inheritdoc />
        public RadioButtonListField(IFormFieldSettings settings, RadioButtonListFieldConfiguration config) : base(settings, config)
        {
        }
    }
}