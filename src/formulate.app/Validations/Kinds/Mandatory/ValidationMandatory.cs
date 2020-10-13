namespace formulate.app.Validations.Kinds.Mandatory
{

    // Namespaces.
    using core.Types;
    using Helpers;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A validation kind that makes a field mandatory.
    /// </summary>
    public class ValidationMandatory : IValidationKind
    {
        #region Properties

        /// <summary>
        /// Gets the kind ID.
        /// </summary>
        public Guid Id => GuidHelper.GetGuid("93957A02633944A193238E8CD754680B");

        /// <summary>
        /// Gets the kind name.
        /// </summary>
        public string Name => "Mandatory";

        /// <summary>
        /// Gets the kind directive.
        /// </summary>
        public string Directive => "formulate-validation-mandatory";

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
            var config = new ValidationMandatoryConfiguration();
            var configData = JsonHelper.Deserialize<JObject>(configuration);
            var dynamicConfig = configData as dynamic;
            var properties = configData.Properties().Select(x => x.Name);
            var propertySet = new HashSet<string>(properties);
            if (propertySet.Contains("message"))
            {
                var message = dynamicConfig.message.Value as string;
                config.Message = ValidationHelper.ReplaceMessageTokens(message, context);
            }
            if (propertySet.Contains("clientSideOnly"))
            {
                var clientSideOnly = dynamicConfig.clientSideOnly.Value as bool?;
                config.ClientSideOnly = clientSideOnly.GetValueOrDefault();
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
        public bool IsValueValid(
            IEnumerable<string> dataValues,
            IEnumerable<FileFieldSubmission> fileValues,
            object configuration)
        {

            // Check configuration.
            var config = configuration as ValidationMandatoryConfiguration;
            if (config.ClientSideOnly)
            {
                return true;
            }

            // Validate input.
            if (dataValues == null || fileValues == null)
            {
                return false;
            }

            // Is at least one of the values non-null and non-whitespace?
            if (dataValues.Any(x => !string.IsNullOrWhiteSpace(x)))
            {
                return true;
            }

            // Is there at least one file?
            if (fileValues.Any())
            {
                return true;
            }

            // Data is invalid.
            return false;
        }

        #endregion
    }
}
