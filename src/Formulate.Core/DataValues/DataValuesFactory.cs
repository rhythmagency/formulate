namespace Formulate.Core.DataValues
{
    // Namespaces.
    using System;
    using Types;

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
        public IDataValues Create(IDataValuesSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var foundDataValuesDefinition = _dataValuesDefinitions.FirstOrDefault(settings.KindId);

            if (foundDataValuesDefinition is null)
            {
                return default;
            }

            return foundDataValuesDefinition.CreateDataValues(settings);
        }
    }
}