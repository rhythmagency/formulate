namespace Formulate.Core.DataValues
{
    // Namespaces.
    using System;

    /// <summary>
    /// The base class for creating data values definitions.
    /// </summary>
    public abstract class DataValuesDefinitionBase : IDataValuesDefinition
    {
        /// <inheritdoc />
        public abstract Guid KindId { get; }

        /// <inheritdoc />
        public abstract string Name { get; }

        /// <inheritdoc />
        public abstract string Directive { get; }

        /// <inheritdoc />
        public abstract string Icon { get; }

        /// <inheritdoc />
        public virtual bool IsLegacy => false;

        /// <inheritdoc />
        public abstract IDataValues CreateDataValues(PersistedDataValues entity);

        /// <inheritdoc />
        public abstract object GetBackOfficeConfiguration(PersistedDataValues entity);
    }
}