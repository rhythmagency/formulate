namespace Formulate.Core.FormFields.Hidden
{
    /// <summary>
    /// A hidden field.
    /// </summary>
    public sealed class HiddenField : FormField<HiddenFieldConfiguration>
    {
        /// <inheritdoc />
        public HiddenField(IFormFieldSettings settings, HiddenFieldConfiguration configuration) : base(settings, configuration)
        {
        }
    }
}
