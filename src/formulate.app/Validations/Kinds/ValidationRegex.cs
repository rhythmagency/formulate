namespace formulate.app.Validations.Kinds
{

    // Namespaces.
    using Helpers;
    using System;
    using Constants = formulate.app.Constants.Validations.ValidationRegex;


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
            //TODO: ...
            return null;
        }

        #endregion

    }

}