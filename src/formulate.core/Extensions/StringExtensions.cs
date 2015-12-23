namespace formulate.core.Extensions
{

    /// <summary>
    /// Extension methods for strings.
    /// </summary>
    public static class StringExtensions
    {

        #region Methods

        /// <summary>
        /// Uses a fallback string if the specified source string
        /// is null or whitespace.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="fallback">The fallback string.</param>
        /// <returns>
        /// The original string, or the fallback string.
        /// </returns>
        public static string Fallback(this string source, string fallback)
        {
            return string.IsNullOrWhiteSpace(source)
                ? fallback
                : source;
        }

        #endregion

    }

}