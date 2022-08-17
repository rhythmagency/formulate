namespace Formulate.Core.FormFields.Header
{
    using System.Runtime.Serialization;
    
    /// <summary>
    /// Configuation required by <see cref="HeaderField"/>.
    /// </summary>
    [DataContract]
    public sealed class HeaderFieldConfiguration
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }
    }
}
