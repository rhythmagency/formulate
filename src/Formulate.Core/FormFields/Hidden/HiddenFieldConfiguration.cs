namespace Formulate.Core.FormFields.Hidden
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Configuration used by <see cref="HiddenField"/>.
    /// </summary>
    [DataContract]
    public sealed class HiddenFieldConfiguration
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }
    }
}