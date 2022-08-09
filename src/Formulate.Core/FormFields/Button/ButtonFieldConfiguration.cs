namespace Formulate.Core.FormFields.Button
{
    using Newtonsoft.Json;

    /// <summary>
    /// Configuration required by the <see cref="ButtonField"/>.
    /// </summary>
    public sealed class ButtonFieldConfiguration
    {
        /// <summary>
        /// Gets or sets the button kind.
        /// </summary>
        [JsonProperty("buttonKind")]
        public string ButtonKind { get; set; }
    }
}