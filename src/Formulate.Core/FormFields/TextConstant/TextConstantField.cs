namespace Formulate.Core.FormFields.TextConstant
{
    /// <summary>
    /// A text field.
    /// </summary>
    public sealed class TextConstantField : FormField<TextConstantFieldConfiguration>
    {
        /// <inheritdoc />
        public TextConstantField(IFormFieldSettings settings, TextConstantFieldConfiguration configuration) : base(settings, configuration)
        {
        }
    }
}
