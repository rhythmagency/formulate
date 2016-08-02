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


        /// <summary>
        /// Converts a field type (e.g., "TextField") to an Angular-friendly type (e.g., "text").
        /// </summary>
        /// <param name="fieldType">
        /// The field type (e.g., "TextField").
        /// </param>
        /// <returns>
        /// The Angular-friendly type (e.g., "text").
        /// </returns>
        public static string ConvertFieldTypeToAngularType(this string fieldType)
        {
            var angularType = default(string);
            switch ((fieldType ?? string.Empty).ToLower())
            {
                case "textfield":
                    angularType = "text";
                    break;
                case "checkboxfield":
                    angularType = "checkbox";
                    break;
                case "dropdownfield":
                    angularType = "select";
                    break;
                case "buttonfield":
                    angularType = "submit";
                    break;
                case "textareafield":
                    angularType = "textarea";
                    break;
                case "uploadfield":
                    angularType = "upload";
                    break;
            }
            return angularType ?? fieldType;
        }


        /// <summary>
        /// Converts a validation type (e.g., "ValidationRegex") to an
        /// Angular-friendly type (e.g., "regex").
        /// </summary>
        /// <param name="validationType">
        /// The validation type (e.g., "ValidationRegex").
        /// </param>
        /// <returns>
        /// The Angular-friendly type (e.g., "regex").
        /// </returns>
        public static string ConvertValidationTypeToAngularType(this string validationType)
        {
            var angularType = default(string);
            switch ((validationType ?? string.Empty).ToLower())
            {
                case "validationregex":
                    angularType = "regex";
                    break;
            }
            return angularType ?? validationType;
        }

        #endregion

    }

}