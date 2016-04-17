namespace formulate.app.Helpers
{

    // Namespaces.
    using System.Text.RegularExpressions;
    using Validations;


    /// <summary>
    /// Helps with operations related to validations.
    /// </summary>
    internal class ValidationHelper
    {

        #region Properties

        private static Regex FieldTokensRegex = new Regex("{(name|alias|label|{)}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        #endregion


        #region Constructors

        /// <summary>
        /// Static constructor.
        /// </summary>
        static ValidationHelper()
        {
            var options = RegexOptions.IgnoreCase | RegexOptions.Compiled;
            FieldTokensRegex = new Regex("{(name|alias|label|{)}", options);
        }

        #endregion


        #region Methods

        /// <summary>
        /// Returns the validation kinds.
        /// </summary>
        public static IValidationKind[] GetAllValidationKinds()
        {
            var instances = ReflectionHelper
                .InstantiateInterfaceImplementations<IValidationKind>();
            return instances;
        }


        /// <summary>
        /// Replaces the tokens in a validation message.
        /// </summary>
        /// <param name="message">
        /// The validation message.
        /// </param>
        /// <param name="context">
        /// The validation configuration deserialization context.
        /// </param>
        /// <returns>
        /// The transformed message.
        /// </returns>
        public static string ReplaceMessageTokens(string message, ValidationContext context)
        {

            // Validate input.
            if (string.IsNullOrWhiteSpace(message))
            {
                return message;
            }


            // Variables.
            var field = context.Field;


            // Replace tokens.
            message = FieldTokensRegex.Replace(message, m =>
            {
                switch (m.Value.ToLower())
                {
                    case "{name}":
                        return field.Name;
                    case "{alias}":
                        return field.Alias;
                    case "{label}":
                        return field.Label;
                    case "{{}":
                        return "{";
                    default:
                        return m.Value;
                }
            });


            // Return message.
            return message;

        }

        #endregion

    }

}

//TODO: Get rid of static functions.