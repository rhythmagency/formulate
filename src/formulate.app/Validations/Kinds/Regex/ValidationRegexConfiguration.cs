﻿namespace formulate.app.Validations.Kinds.Regex
{
    using Newtonsoft.Json;

    /// <summary>
    /// Configuration used by <see cref="ValidationRegex"/>.
    /// </summary>
    public class ValidationRegexConfiguration
    {
        #region Properties

        /// <summary>
        /// Gets or sets the regular expression pattern.
        /// </summary>
        [JsonProperty("regex")]
        public string Pattern { get; set; }

        /// <summary>
        /// Gets or sets the error message to show when the validation fails.
        /// </summary>
        public string Message { get; set; }

        #endregion
    }
}
