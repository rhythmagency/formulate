namespace Formulate.Core.FormHandlers
{
    // Namespaces.
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A persisted form handler.
    /// </summary>
    public sealed class PersistedFormHandler : IFormHandlerSettings
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the kind ID.
        /// </summary>
        [JsonPropertyName("TypeId")]
        public Guid KindId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets value indicating whether this is enabled.
        /// </summary>
        public bool Enabled { get; set; }
    }
}