namespace formulate.core.Utilities
{

    // Namespaces.
    using System;


    /// <summary>
    /// Helps with dates.
    /// </summary>
    public static class DateUtility
    {

        #region Methods

        /// <summary>
        /// Attempts to parse a string as a date.
        /// </summary>
        /// <param name="strDate">
        /// The string date.
        /// </param>
        /// <returns>
        /// The parsed date, or null.
        /// </returns>
        public static DateTime? AttemptParseDate(string strDate)
        {
            if (string.IsNullOrWhiteSpace(strDate))
            {
                return null;
            }
            var tempDate = default(DateTime);
            return DateTime.TryParse(strDate, out tempDate)
                ? tempDate as DateTime?
                : (DateTime.TryParse(strDate.Substring(0, 24), out tempDate)
                    ? tempDate as DateTime?
                    : null);
        }

        #endregion

    }

}