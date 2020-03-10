namespace formulate.app.Layouts.Kinds.Basic
{

    // Namespaces.
    using System;
    using System.Collections.Generic;


    /// <summary>
    /// The configuration for a basic layout.
    /// </summary>
    public class LayoutBasicConfiguration
    {

        #region Properties

        /// <summary>
        /// Automatically populate this layout based on changes to the form?
        /// </summary>
        public bool Autopopulate { get; set; }


        /// <summary>
        /// The ID of the form this layout applies to.
        /// </summary>
        public Guid? FormId { get; set; }


        /// <summary>
        /// The rows in this layout.
        /// </summary>
        public IEnumerable<LayoutRow> Rows { get; set; }

        #endregion

    }

}