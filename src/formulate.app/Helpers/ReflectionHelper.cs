namespace formulate.app.Helpers
{

    // Namespace.
    using System;
    using System.Collections.Generic;
    using System.Linq;


    /// <summary>
    /// Helps with reflection operations.
    /// </summary>
    internal class ReflectionHelper
    {

        #region Properties

        private static Dictionary<Type, List<Type>> TypeMap { get; set; }
        private static object TypeMapLock { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Static constructor.
        /// </summary>
        static ReflectionHelper()
        {
            TypeMap = new Dictionary<Type, List<Type>>();
            TypeMapLock = new object();
        }

        #endregion


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

            // Variables.
            var interfaceType = typeof(T);
            var types = default(List<Type>);


            // Attempt to get list of types from cache.
            lock (TypeMapLock)
            {
                if (!TypeMap.TryGetValue(interfaceType, out types))
                {
                    types = null;
                }
            }


            // Add types to cache?
            if (types == null)
            {
                types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(x => x.GetTypes())
                    .Where(x => interfaceType.IsAssignableFrom(x)
                        && !x.IsInterface).ToList();
                lock (TypeMapLock)
                {
                    TypeMap[interfaceType] = types;
                }
            }


            // Return instances.
            var instances = types
                .Select(x => Activator.CreateInstance(x))
                .Where(x => x is T)
                .Where(x => x != null)
                .Select(x => (T)x).ToArray();
            return instances;

        }

        #endregion

    }

}

//TODO: Get rid of static functions.