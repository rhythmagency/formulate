namespace formulate.app.Helpers
{

    // Namespace.
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;


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


        /// <summary>
        /// Indicates whether or not the specified object has the member with the specified name.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <param name="memberName">
        /// The member name.
        /// </param>
        /// <returns>
        /// True, if the specified member exists; otherwise, false.
        /// </returns>
        public static bool HasMember(object obj, string memberName)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var objectProp = obj.GetType().GetMember(memberName, flags);
            return objectProp != null;
        }

        #endregion

    }

}

//TODO: Get rid of static functions.