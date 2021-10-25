using System.Threading;
using System.Threading.Tasks;
using Formulate.Core.Types;
using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.DataValues
{
    /// <summary>
    /// A contract for implementing a data values definition.
    /// </summary>
    public interface IDataValuesDefinition : IDefinition, IDiscoverable
    {
        /// <summary>
        /// Gets the definition label.
        /// </summary>
        string DefinitionLabel { get; }

        /// <summary>
        /// Asynchronously creates a <see cref="IDataValues"/>.
        /// </summary>
        /// <param name="settings">The current settings.</param>
        /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
        /// <returns>A <see cref="IDataValues"/>.</returns>
        Task<IDataValues> CreateDataValuesAsync(IDataValuesSettings settings, CancellationToken cancellationToken = default);
    }
}
