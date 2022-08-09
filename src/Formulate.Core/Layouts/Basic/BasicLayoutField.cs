namespace Formulate.Core.Layouts.Basic
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// A field in a basic layout row cell.
    /// </summary>
    public sealed class BasicLayoutField
    {
        /// <summary>
        /// Gets the field ID.
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
