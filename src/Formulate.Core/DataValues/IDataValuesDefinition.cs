﻿namespace Formulate.Core.DataValues
{
    // Namespaces.
    using Types;

    /// <summary>
    /// A contract for implementing a data values definition.
    /// </summary>
    public interface IDataValuesDefinition : IDefinition
    {
        /// <summary>
        /// Gets the icon.
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// Gets if this is a legacy definition.
        /// </summary>
        /// <remarks>Legacy definitions can not have new entities created from them and exist for older data.</remarks>
        bool IsLegacy { get; }

        /// <summary>
        /// Creates a <see cref="IDataValues"/>.
        /// </summary>
        /// <param name="settings">
        /// The current settings.
        /// </param>
        /// <returns>
        /// A <see cref="IDataValues"/>.
        /// </returns>
        IDataValues CreateDataValues(IDataValuesSettings settings);

        /// <inheritdoc />
        public object GetBackOfficeConfiguration(IDataValuesSettings settings);
    }
}