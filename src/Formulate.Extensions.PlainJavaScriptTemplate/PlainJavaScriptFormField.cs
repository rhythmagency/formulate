namespace Formulate.Extensions.PlainJavaScriptTemplate
{
    using Formulate.Core.FormFields;

    /// <summary>
    /// An object that controls how a <see cref="IFormField"/> is displayed in the Plain JS template.
    /// </summary>
    public sealed class PlainJavaScriptFormField
    {
        public PlainJavaScriptFormField(string fieldType) : this(new {}, fieldType)
        {
        }

        public PlainJavaScriptFormField(object configuration, string fieldType)
        {
            Configuration = configuration;
            FieldType = fieldType;
        }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <remarks>This is optional.</remarks>
        public object Configuration { get; init; }

        /// <summary>
        /// Gets or sets the field type.
        /// </summary>
        /// <remarks>This value determines which registered Plain JS form field to use.</remarks>
        public string FieldType { get; init; }
    }
}
