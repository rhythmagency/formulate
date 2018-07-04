namespace formulate.app.Validations.Kinds.Regex
{

    // Namespaces.
    using core.Types;
    using Helpers;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Constants = Constants.Validations.ValidationRegex;


    /// <summary>
    /// A validation kind that validates against a regular expression.
    /// </summary>
    public class ValidationRegex : IValidationKind
    {

        #region Properties

        /// <summary>
        /// The kind ID.
        /// </summary>
        public Guid Id
        {
            get
            {
                return GuidHelper.GetGuid(Constants.Id);
            }
        }


        /// <summary>
        /// The kind name.
        /// </summary>
        public string Name
        {
            get
            {
                return Constants.Name;
            }
        }


        /// <summary>
        /// The kind directive.
        /// </summary>
        public string Directive
        {
            get
            {
                return Constants.Directive;
            }
        }

        #endregion


        #region Methods

        /// <summary>
        /// Deserializes the validation configuration.
        /// </summary>
        /// <param name="configuration">
        /// The serialized validation configuration.
        /// </param>
        /// <param name="context">
        /// The validation configuration deserialization context.
        /// </param>
        /// <returns>
        /// The deserialized configuration.
        /// </returns>
        public object DeserializeConfiguration(string configuration, ValidationContext context)
        {
            var config = new ValidationRegexConfiguration();
            var configData = JsonHelper.Deserialize<JObject>(configuration);
            var dynamicConfig = configData as dynamic;
            var properties = configData.Properties().Select(x => x.Name);
            var propertySet = new HashSet<string>(properties);
            if (propertySet.Contains("regex"))
            {
                config.Pattern = dynamicConfig.regex.Value as string;
            }
            if (propertySet.Contains("message"))
            {
                var message = dynamicConfig.message.Value as string;
                config.Message = ValidationHelper.ReplaceMessageTokens(message, context);
            }
            return config;
        }

        /// <summary>
        /// Is the submitted value valid?
        /// </summary>
        /// <param name="dataValues">
        /// The data values.
        /// </param>
        /// <param name="fileValues">
        /// The file values.
        /// </param>
        /// <param name="configuration">
        /// The validation configuration.
        /// </param>
        /// <returns>
        /// True, if the data is valid; otherwise, false.
        /// </returns>
        public bool IsValueValid(IEnumerable<string> dataValues,
            IEnumerable<FileFieldSubmission> fileValues, object configuration)
        {

            // Variables.
            var castedConfig = configuration as ValidationRegexConfiguration;

            // Validate input.
            if (dataValues == null || fileValues == null || configuration == null)
            {
                return false;
            }

            // All of the values must match the regex pattern.
            var pattern = castedConfig.Pattern;
            var regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.Singleline);
            if (!dataValues.All(x => regex.IsMatch(x ?? string.Empty)))
            {
                return false;
            }

            // All of the filenames must match the regex pattern.
            if (!fileValues.All(x => regex.IsMatch(x.FileName ?? string.Empty)))
            {
                return false;
            }

            // Data is valid.
            return true;

        }

        #endregion

    }

}