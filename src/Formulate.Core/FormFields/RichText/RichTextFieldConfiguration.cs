namespace Formulate.Core.FormFields.RichText
{
    using System.Runtime.Serialization;
    
    /// <summary>
    /// Configuation required by <see cref="RichTextField"/>.
    /// </summary>
    [DataContract]
    public sealed class RichTextFieldConfiguration
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }
    }
}
