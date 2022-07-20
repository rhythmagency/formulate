namespace Formulate.Core.Extensions
{
    // Namespaces.
    using System;

    /// <summary>
    /// Extension methods for the <see cref="Type"/> class.
    /// </summary>
    internal static class TypeExtensionMethods
    {
        /// <summary>
        /// Checks if the specified type is derived from the specified
        /// generic type.
        /// </summary>
        /// <param name="typeToCheck">
        /// The main type to check.
        /// </param>
        /// <param name="genericType">
        /// The generic type to check the main type against.
        /// </param>
        /// <returns>
        /// True, if the main type is derived from the other
        /// generic type.
        /// </returns>
        /// <remarks>
        /// See: https://stackoverflow.com/a/458406/2052963
        /// </remarks>
        public static bool IsTypeDerivedFromGenericType(
            this Type typeToCheck,
            Type genericType)
        {
            // Base case.
            if (typeToCheck == typeof(object) || typeToCheck == null)
            {
                return false;
            }

            // Get type information.
            var isGeneric = typeToCheck.IsGenericType;
            var genericOfType = isGeneric
                ? typeToCheck.GetGenericTypeDefinition()
                : null;

            // Is the type derived from the generic type?
            if (isGeneric && genericOfType == genericType)
            {
                return true;
            }

            // Check ancestor types.
            return IsTypeDerivedFromGenericType(typeToCheck.BaseType, genericType);
        }
    }
}