namespace formulate.core.Utilities
{

    /// <summary>
    /// Helps with numbers.
    /// </summary>
    public static class NumberUtility
    {

        #region Methods

        /// <summary>
        /// Attempts to parse a string as an integer.
        /// </summary>
        /// <param name="strInt">
        /// The string integer.
        /// </param>
        /// <returns>
        /// The parsed integer, or null.
        /// </returns>
        public static int? AttemptParseInt(string strInt)
        {
            if (string.IsNullOrWhiteSpace(strInt))
            {
                return null;
            }
            var tempInt = default(int);
            return int.TryParse(strInt, out tempInt)
                ? tempInt as int?
                : null;
        }

        #endregion

    }

}