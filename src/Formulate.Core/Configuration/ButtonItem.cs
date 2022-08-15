namespace Formulate.Core.Configuration
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Details about a button item from configuration.
    /// </summary>
    [DataContract]
    public sealed class ButtonItem
    {
        /// <summary>
        /// The kind of button.
        /// </summary>
        [DataMember(Name = "kind")]
        public string Kind { get; set; }
    }
}