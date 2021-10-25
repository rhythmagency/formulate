using System.Threading;
using System.Threading.Tasks;

namespace Formulate.Core.DataValues
{
    /// <summary>
    /// The abstract class for creating synchronous data values definitions.
    /// </summary>
    /// <remarks>
    /// <para>Acts as wrapper for synchronous data value creation.</para>
    /// <para>All calls are made asynchronously to maintain consistency with asynchronous definitions.</para>
    /// </remarks>
    public abstract class DataValuesDefinition : DataValuesDefinitionBase
    {
        /// <summary>
        /// Creates a <see cref="IDataValues"/>.
        /// </summary>
        /// <param name="settings">The current settings.</param>
        /// <returns>A <see cref="IDataValues"/>.</returns>
        protected abstract IDataValues CreateDataValues(IDataValuesSettings settings);

        /// <inheritdoc />
        public override async Task<IDataValues> CreateDataValuesAsync(IDataValuesSettings settings, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => CreateDataValues(settings), cancellationToken);
        }
    }
}
