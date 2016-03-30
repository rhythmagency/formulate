namespace formulate.app.Validations.Kinds.Mandatory
{

    // Namespaces.
    using Helpers;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants = formulate.app.Constants.Validations.ValidationMandatory;


    /// <summary>
    /// A validation kind that makes a field mandatory.
    /// </summary>
    public class ValidationMandatory : IValidationKind
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


        /// <summary>
        /// Deserializes the validation configuration.
        /// </summary>
        /// <param name="configuration">
        /// The serialized validation configuration.
        /// </param>
        /// <returns>
        /// The deserialized configuration.
        /// </returns>
        public object DeserializeConfiguration(string configuration)
        {
            var config = new ValidationMandatoryConfiguration();
            var configData = JsonHelper.Deserialize<JObject>(configuration);
            var dynamicConfig = configData as dynamic;
            var properties = configData.Properties().Select(x => x.Name);
            var propertySet = new HashSet<string>(properties);
            if (propertySet.Contains("message"))
            {
                config.Message = dynamicConfig.message.Value as string;
            }
            return config;
        }

        #endregion

    }

}