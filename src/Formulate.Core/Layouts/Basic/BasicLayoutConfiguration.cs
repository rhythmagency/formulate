using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Formulate.Core.Layouts.Basic
{
    /// <summary>
    /// The configuration for a <see cref="BasicLayout"/>.
    /// </summary>
    public sealed class BasicLayoutConfiguration
    {
        /// <summary>
        /// Gets a value indicating whether to automatically populate this layout based on changes to the form.
        /// </summary>
        [JsonPropertyName("autopopulate")]
        public bool AutoPopulate { get; set; }

        /// <summary>
        /// Gets the form ID.
        /// </summary>
        [JsonPropertyName("formId")]
        public Guid? FormId { get; set; }

        /// <summary>
        /// Gets the rows.
        /// </summary>
        [JsonPropertyName("rows")]
        public IEnumerable<BasicLayoutRow> Rows { get; set; }
    }
}
