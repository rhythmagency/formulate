using System.Collections.Generic;
using Formulate.Core.Validations;

namespace Formulate.Core.FormFields.CheckboxList
{
    public sealed class CheckboxListField : FormField<CheckboxListFieldConfiguration>
    {
        /// <inheritdoc />
        public CheckboxListField(IFormFieldSettings settings, IReadOnlyCollection<IValidation> validations, CheckboxListFieldConfiguration configuration) : base(settings, validations, configuration)
        {
        }

        /// <inheritdoc />
        public CheckboxListField(IFormFieldSettings settings, CheckboxListFieldConfiguration config) : base(settings, config)
        {
        }
    }
}