namespace Formulate.Core.Persistence
{
    /// <summary>
    /// A contract for creating a cache for persisted entities.
    /// </summary>
    internal interface IPersistedEntityCache
    {
        /// <summary>
        /// Gets a persisted entity from the cache.
        /// </summary>
        /// <typeparam name="TPersistedEntity">The type of the entity</typeparam>
        /// <param name="key">The key associated with the cached entity.</param>
        /// <returns>A <typeparamref name="TPersistedEntity"/>.</returns>
        TPersistedEntity Get<TPersistedEntity>(string key) where TPersistedEntity : class, IPersistedEntity;

        /// <summary>
        /// Invalidates a cache entry.
        /// </summary>
        /// <param name="key">The key associated with the cached entity.</param>
        void Invalidate(string key);
    }
}
