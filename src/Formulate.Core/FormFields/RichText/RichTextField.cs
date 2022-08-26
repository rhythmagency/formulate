using System.Collections.Generic;
using Formulate.Core.Validations;

namespace Formulate.Core.FormFields.RichText
{
    /// <summary>
    /// A text field.
    /// </summary>
    public sealed class RichTextField : FormField<RichTextFieldConfiguration>
    {
        /// <inheritdoc />
        public RichTextField(IFormFieldSettings settings, RichTextFieldConfiguration configuration) : base(settings, configuration)
        {
        }
    }
}
