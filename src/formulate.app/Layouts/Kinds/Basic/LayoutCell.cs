namespace formulate.app.Layouts.Kinds.Basic
{

    // Namespaces.
    using System.Collections.Generic;


    /// <summary>
    /// A cell in a layout row.
    /// </summary>
    public class LayoutCell
    {

        #region Properties

        /// <summary>
        /// The number of columns this cell spans.
        /// </summary>
        public int ColumnSpan { get; set; }


        /// <summary>
        /// The fields in this cell.
        /// </summary>
        public IEnumerable<LayoutField> Fields { get; set; }

        #endregion

    }

}