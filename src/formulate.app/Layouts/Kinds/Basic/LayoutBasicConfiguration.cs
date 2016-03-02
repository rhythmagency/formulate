namespace formulate.app.Layouts.Kinds.Basic
{

    // Namespaces.
    using System.Collections.Generic;


    /// <summary>
    /// The configuration for a basic layout.
    /// </summary>
    public class LayoutBasicConfiguration
    {

        #region Properties

        /// <summary>
        /// The rows in this layout.
        /// </summary>
        public IEnumerable<LayoutRow> Rows { get; set; }

        #endregion

    }

}