namespace formulate.app.ExtensionMethods
{

    // Namespaces.
    using System;


    /// <summary>
    /// Extension methods for the Type class.
    /// </summary>
    internal static class TypeExtensions
    {

        #region Extension Methods

        /// <summary>
        /// Returns an assembly-qualified name that doesn't include the extraneous information.
        /// </summary>
        /// <param name="type">
        /// The type to get the name for.
        /// </param>
        /// <returns>
        /// The name.
        /// </returns>
        /// <remarks>
        /// For example, will return "formulate.app.DataValues.Suppliers.Kinds.UsStateSupplier, formulate.app"
        /// rather than "formulate.app.DataValues.Suppliers.Kinds.UsStateSupplier, formulate.app, Version=2.0.2.0, Culture=neutral, PublicKeyToken=null".
        /// Useful if you don't need the version number.
        /// </remarks>
        public static string ShortAssemblyQualifiedName(this Type type)
        {
            if (type == null)
            {
                return null;
            }
            return type.FullName + ", " + type.Assembly.GetName().Name;
        }

        #endregion

    }

}