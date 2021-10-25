using System.Threading;
using System.Threading.Tasks;

namespace Formulate.Core.Types
{
    /// <summary>
    /// A contract for creating a factory which asynchronously creates a <typeparamref name="TEntity"/> from a <typeparamref name="TSettings"/>.
    /// </summary>
    /// <typeparam name="TSettings">The type of the settings.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <remarks>This should only be used for readonly operations not persistence.</remarks>
    public interface IAsyncEntityFactory<in TSettings, TEntity> where TSettings : IEntitySettings where TEntity : IEntity
    {
        /// <summary>
        /// Asynchronously creates a new instance which implements <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="settings">The current settings.</param>
        /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
        /// <returns>A <typeparamref name="TEntity"/>.</returns>
        Task<TEntity> CreateAsync(TSettings settings, CancellationToken cancellationToken = default);
    }
}
