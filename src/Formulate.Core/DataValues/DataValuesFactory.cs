using System;
using System.Threading;
using System.Threading.Tasks;
using Formulate.Core.Types;

namespace Formulate.Core.DataValues
{
    /// <summary>
    /// The default implementation of <see cref="IDataValuesFactory"/> using the <see cref="DataValuesDefinitionCollection"/>.
    /// </summary>
    internal sealed class DataValuesFactory : IDataValuesFactory
    {
        /// <summary>
        /// The data values.
        /// </summary>
        private readonly DataValuesDefinitionCollection _dataValuesDefinitions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataValuesDefinitions">The data values.</param>
        public DataValuesFactory(DataValuesDefinitionCollection dataValuesDefinitions)
        {
            _dataValuesDefinitions = dataValuesDefinitions;
        }

        /// <inheritdoc />
        public async Task<IDataValues> CreateAsync(IDataValuesSettings settings, CancellationToken cancellationToken = default)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var foundDataValuesDefinition = _dataValuesDefinitions.FirstOrDefault(settings.DefinitionId);

            if (foundDataValuesDefinition is null)
            {
                return default;
            }

            return await foundDataValuesDefinition.CreateDataValuesAsync(settings, cancellationToken);
        }
    }
}
