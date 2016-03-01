namespace formulate.app.Helpers
{

    // Namespaces.
    using Layouts;


    /// <summary>
    /// Helps with operations related to layouts.
    /// </summary>
    internal class LayoutHelper
    {

        #region Methods

        /// <summary>
        /// Returns the layout kinds.
        /// </summary>
        public static ILayoutKind[] GetAllLayoutKinds()
        {
            var instances = ReflectionHelper
                .InstantiateInterfaceImplementations<ILayoutKind>();
            return instances;
        }

        #endregion

    }

}

//TODO: Get rid of static functions.