namespace Formulate.Core.Packaging
{
    using System;

    /// <summary>
    /// Extension methods that augement the <see cref="Type"/> class.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the version from the current <see cref="Type"/> class's Assembly property.
        /// </summary>
        /// <param name="type">The current type.</param>
        /// <returns>A <see cref="Version"/>.</returns>
        public static Version GetAssemblyVersionOrDefault(this Type type)
        {
            return type.GetAssemblyVersionOrDefault(new Version());
        }

        /// <summary>
        /// Gets the version from the current <see cref="Type"/> class's Assembly property.
        /// </summary>
        /// <param name="type">The current type.</param>
        /// <param name="defaultValue">The default value if no version is found.</param>
        /// <returns>A <see cref="Version"/>.</returns>
        public static Version GetAssemblyVersionOrDefault(this Type type, Version defaultValue)
        {
            return type.Assembly.GetVersionOrDefault(defaultValue);
        }
    }
}
