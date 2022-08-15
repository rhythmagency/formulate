namespace Formulate.Core.Layouts.Basic
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// A cell in a basic layout row.
    /// </summary>
    [DataContract]
    public sealed class BasicLayoutCell
    {
        /// <summary>
        /// Gets the number of columns this cell spans.
        /// </summary>
        [DataMember(Name = "columnSpan")]
        public int ColumnSpan { get; set; }

        /// <summary>
        /// Gets the fields in this cell.
        /// </summary>
        [DataMember(Name = "fields")]
        public BasicLayoutField[] Fields { get; set; } = Array.Empty<BasicLayoutField>();
    }
}
