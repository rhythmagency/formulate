namespace formulate.app.Helpers
{

    // Namespace.
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using formulate.app.Persistence;

    /// <summary>
    /// Helps with reflection operations.
    /// </summary>
    internal class ReflectionHelper
    {

        #region Read Only Variables

        private static readonly Type[] EmptyTypeArray = new Type[0];

        #endregion


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


        #region Public Methods

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

            // Return instances.
            var instances = GetTypesImplementingInterface<T>()
                .Select(x => Activator.CreateInstance(x))
                .Where(x => x is T)
                .Where(x => x != null)
                .Select(x => (T)x).ToArray();
            return instances;

        }


        /// <summary>
        /// Returns the types that implement an interface.
        /// </summary>
        /// <typeparam name="T">
        /// The interface type.
        /// </typeparam>
        /// <returns>
        /// The types.
        /// </returns>
        public static Type[] GetTypesImplementingInterface<T>()
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
                    .SelectMany(x => SafelyGetTypes(x))
                    .Where(x => interfaceType.IsAssignableFrom(x)
                        && !x.IsInterface).ToList();
                lock (TypeMapLock)
                {
                    TypeMap[interfaceType] = types;
                }
            }


            // Return types.
            return types.ToArray();

        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Safely returns the types in an assembly.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns>
        /// The array of types, or an empty array.
        /// </returns>
        /// <remarks>
        /// This is a workaround for an issue that happens when dependent assemblies
        /// are missing: https://github.com/rhythmagency/formulate/issues/70
        /// </remarks>
        private static Type[] SafelyGetTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes() ?? EmptyTypeArray;
            }
            catch
            {
                return EmptyTypeArray;
            }
        }

        #endregion

    }

}

//TODO: Get rid of static functions.