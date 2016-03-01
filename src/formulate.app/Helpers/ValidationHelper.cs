namespace formulate.app.Helpers
{

    // Namespaces.
    using Validations;


    /// <summary>
    /// Helps with operations related to validations.
    /// </summary>
    internal class ValidationHelper
    {

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

        #endregion

    }

}

//TODO: Get rid of static functions.