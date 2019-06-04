namespace formulate.app.Configuration
{

    // Namespaces.
    using formulate.app.Serialization;

    using Newtonsoft.Json;

    /// <summary>
    /// The configuration for an email header.
    /// </summary>
    public sealed class EmailHeaderConfig
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the email header.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the email header.
        /// </summary>
        [JsonConverter(typeof(NewGuidTokenReplacementJsonConverter))]
        public string Value { get; set; }

        #endregion
    }
}
