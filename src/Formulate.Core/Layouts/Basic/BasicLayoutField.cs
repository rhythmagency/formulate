using System;
using System.Text.Json.Serialization;

namespace Formulate.Core.Layouts.Basic
{
    /// <summary>
    /// A field in a basic layout row cell.
    /// </summary>
    public sealed class BasicLayoutField
    {
        /// <summary>
        /// Gets the field ID.
        /// </summary>
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
