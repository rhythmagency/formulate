using System;
using System.Threading;
using System.Threading.Tasks;

namespace Formulate.Core.DataValues
{
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
        public abstract Task<IDataValues> CreateDataValuesAsync(IDataValuesSettings settings, CancellationToken cancellationToken = default);
    }
}
