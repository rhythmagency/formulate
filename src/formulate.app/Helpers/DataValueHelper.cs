namespace formulate.app.Helpers
{

    // Namespaces.
    using DataValues;


    /// <summary>
    /// Helps with operations related to data values.
    /// </summary>
    internal class DataValueHelper
    {

        #region Methods

        /// <summary>
        /// Returns the data value kinds.
        /// </summary>
        public static IDataValueKind[] GetAllDataValueKinds()
        {
            var instances = ReflectionHelper
                .InstantiateInterfaceImplementations<IDataValueKind>();
            return instances;
        }

        #endregion

    }

}

//TODO: Get rid of static functions.