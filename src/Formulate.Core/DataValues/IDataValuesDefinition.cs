namespace Formulate.Core.DataValues
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
        /// Creates a <see cref="IDataValues"/>.
        /// </summary>
        /// <param name="entity">
        /// The current entity.
        /// </param>
        /// <returns>
        /// A <see cref="IDataValues"/>.
        /// </returns>
        IDataValues CreateDataValues(PersistedDataValues entity);

        /// <inheritdoc />
        public object GetBackOfficeConfiguration(PersistedDataValues entity);
    }
}