namespace Formulate.Core.FormFields.Button
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Configuration required by the <see cref="ButtonField"/>.
    /// </summary>

    [DataContract]
    public sealed class ButtonFieldConfiguration
    {
        /// <summary>
        /// Gets or sets the button kind.
        /// </summary>
        [DataMember(Name = "buttonKind")]
        public string ButtonKind { get; set; }
    }
}