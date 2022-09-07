namespace Formulate.Core.Layouts.Basic
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The configuration for a <see cref="BasicLayout"/>.
    /// </summary>
    [DataContract]
    public sealed class BasicLayoutConfiguration
    {
        /// <summary>
        /// Gets a value indicating whether to automatically populate this layout based on changes to the form.
        /// </summary>
        [DataMember(Name = "autoPopulate")]
        public bool AutoPopulate { get; set; }

        /// <summary>
        /// Gets the rows.
        /// </summary>
        [DataMember(Name = "rows")]
        public BasicLayoutRow[] Rows { get; set; } = Array.Empty<BasicLayoutRow>();
    }
}
