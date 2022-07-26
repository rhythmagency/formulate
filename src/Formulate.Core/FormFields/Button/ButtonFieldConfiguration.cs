namespace Formulate.Core.FormFields.Button
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Configuration required by the <see cref="ButtonField"/>.
    /// </summary>
    public sealed class ButtonFieldConfiguration
    {
        /// <summary>
        /// Gets or sets the button kind.
        /// </summary>
        [JsonPropertyName("buttonKind")]
        public string ButtonKind { get; set; }
    }
}