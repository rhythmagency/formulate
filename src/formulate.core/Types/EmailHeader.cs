namespace formulate.core.Types
{

    /// <summary>
    /// An email header.
    /// </summary>
    public class EmailHeader
    {
        #region Properties

        /// <summary>
        /// The name of the header.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The value of the header.
        /// </summary>
        public string Value { get; set; }

        #endregion
    }
}