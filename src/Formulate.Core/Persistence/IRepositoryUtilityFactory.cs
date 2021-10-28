namespace Formulate.Core.Persistence
{
    /// <summary>
    /// Creates a <see cref="IRepositoryUtility{TPersistedEntity}"/> of a given type.
    /// </summary>
    public interface IRepositoryUtilityFactory
    {
        /// <summary>
        /// Creates a repository utility of a given type.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity.</typeparam>
        /// <returns>A <see cref="IRepositoryUtility{TEntity}"/>.</returns>
        IRepositoryUtility<TEntity> Create<TEntity>() where TEntity : class, IPersistedEntity;
    }
}
