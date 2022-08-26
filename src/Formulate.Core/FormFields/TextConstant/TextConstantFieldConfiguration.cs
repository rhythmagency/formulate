namespace Formulate.Core.FormFields.TextConstant
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Configuration used by <see cref="TextConstantField"/>.
    /// </summary>
    [DataContract]
    public sealed class TextConstantFieldConfiguration
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }
    }
}