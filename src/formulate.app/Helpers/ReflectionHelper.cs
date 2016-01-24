namespace formulate.app.Helpers
{

    // Namespace.
    using System;
    using System.Linq;


    /// <summary>
    /// Helps with reflection operations.
    /// </summary>
    internal class ReflectionHelper
    {

        #region Methods

        /// <summary>
        /// Instantiates all of the classes that implement the specified
        /// interface.
        /// </summary>
        /// <typeparam name="T">
        /// The interface type.
        /// </typeparam>
        /// <returns>
        /// An array of instances.
        /// </returns>
        public static T[] InstantiateInterfaceImplementations<T>()
        {
            var interfaceType = typeof(T);
            var instances = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => interfaceType.IsAssignableFrom(x)
                    && !x.IsInterface)
                .Select(x =>
                    x is T ? (T)Activator.CreateInstance(x) : default(T))
                .Where(x => x != null).ToArray();
            return instances;
        }

        #endregion

    }

}

//TODO: Get rid of static functions.