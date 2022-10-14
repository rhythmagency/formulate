namespace Formulate.Core.Packaging
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Extension methods that augement the <see cref="Assembly"/> class.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets the <see cref="Version"/> from the current <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly">The current assembly</param>
        /// <returns>A <see cref="Version"/>.</returns>
        public static Version GetVersionOrDefault(this Assembly assembly)
        {
            return assembly.GetVersionOrDefault(new Version());
        }

        /// <summary>
        /// Gets the <see cref="Version"/> from the current <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly">The current assembly</param>
        /// <param name="defaultValue">The default value if no version is found.</param>
        /// <returns>A <see cref="Version"/>.</returns>
        public static Version GetVersionOrDefault(this Assembly assembly, Version defaultValue)
        {
            var name = assembly.GetName();
            var version = name.Version ?? defaultValue;

            return version;
        }
    }
}
