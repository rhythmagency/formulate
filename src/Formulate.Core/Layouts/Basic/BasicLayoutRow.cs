namespace Formulate.Core.Layouts.Basic
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// A row in a <see cref="BasicLayout"/>.
    /// </summary>
    [DataContract]
    public sealed class BasicLayoutRow
    {
        /// <summary>
        /// Gets a value indicating whether this starts a new step in the layout.
        /// </summary>
        [DataMember(Name = "isStep")]
        public bool IsStep { get; set; }

        /// <summary>
        /// Gets the cells.
        /// </summary>
        [DataMember(Name = "cells")]
        public BasicLayoutCell[] Cells { get; set; } = Array.Empty<BasicLayoutCell>();
    }
}
