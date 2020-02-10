namespace formulate.app.Layouts.Kinds.Basic
{

    // Namespaces.
    using System.Collections.Generic;


    /// <summary>
    /// A row in a layout.
    /// </summary>
    public class LayoutRow
    {

        #region Properties

        /// <summary>
        /// Is this a row that starts a new step in the layout?
        /// </summary>
        public bool IsStep { get; set; }


        /// <summary>
        /// The cells in this row.
        /// </summary>
        public IEnumerable<LayoutCell> Cells { get; set; }

        #endregion

    }

}