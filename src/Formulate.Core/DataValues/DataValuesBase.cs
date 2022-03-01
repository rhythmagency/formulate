using System;
using System.Collections.Generic;

namespace Formulate.Core.DataValues
{
    /// <summary>
    /// The base class for all data values.
    /// </summary>
    public abstract class DataValuesBase : IDataValues
    {
        /// <inheritdoc />
        public Guid Id { get; }

        /// <inheritdoc />
        public Guid KindId { get; }

        /// <inheritdoc />
        public IReadOnlyCollection<KeyValuePair<string, string>> Items { get; }

        /// <summary>
        /// The raw configuration.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is for reference only.
        /// </para>
        /// <para>
        /// Deserialization should typically happen in the overridden <see cref="IDataValuesDefinition"/>, <see cref="AsyncDataValuesDefinition"/> CreateDataValuesAsync or <see cref="DataValuesDefinition"/> CreateDataValues method.
        /// </para>
        /// </remarks>
        protected readonly string RawConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataValuesBase"/> class.
        /// </summary>
        /// <param name="settings">The data values settings.</param>
        /// <param name="items">The items.</param>
        /// <exception cref="ArgumentNullException">The settings parameter is null.</exception>
        protected DataValuesBase(IDataValuesSettings settings, IReadOnlyCollection<KeyValuePair<string, string>> items)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            Id = settings.Id;
            KindId = settings.KindId;
            RawConfiguration = settings.Configuration;
            Items = items ?? Array.Empty<KeyValuePair<string, string>>();
        }
    }
}
