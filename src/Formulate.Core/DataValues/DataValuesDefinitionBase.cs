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
        public abstract string DefinitionLabel { get; }

        /// <inheritdoc />
        public abstract string Directive { get; }

        /// <inheritdoc />
        public abstract string Icon { get; }

        /// <inheritdoc />
        public virtual bool IsLegacy => false;

        /// <inheritdoc />
        public abstract IDataValues CreateDataValues(IDataValuesSettings settings);

        /// <inheritdoc />
        public abstract object GetBackOfficeConfiguration(IDataValuesSettings settings);
    }
}