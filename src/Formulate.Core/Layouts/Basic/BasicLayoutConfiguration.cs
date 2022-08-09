namespace Formulate.Core.Layouts.Basic
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The configuration for a <see cref="BasicLayout"/>.
    /// </summary>
    public sealed class BasicLayoutConfiguration
    {
        /// <summary>
        /// Gets a value indicating whether to automatically populate this layout based on changes to the form.
        /// </summary>
        [JsonProperty("autoPopulate")]
        public bool AutoPopulate { get; set; }

        /// <summary>
        /// Gets the form ID.
        /// </summary>
        [JsonProperty("formId")]
        public Guid? FormId { get; set; }

        /// <summary>
        /// Gets the rows.
        /// </summary>
        [JsonProperty("rows")]
        public IEnumerable<BasicLayoutRow> Rows { get; set; }
    }
}
