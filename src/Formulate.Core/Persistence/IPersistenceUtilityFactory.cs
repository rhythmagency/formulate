namespace Formulate.Core.Persistence
{
    /// <summary>
    /// Creates a <see cref="IPersistenceUtility{TPersistedEntity}"/> of a given type.
    /// </summary>
    public interface IPersistenceUtilityFactory
    {
        /// <summary>
        /// Creates a persistence utility of a given type.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity.</typeparam>
        /// <returns>A <see cref="IPersistenceUtility{TEntity}"/>.</returns>
        IPersistenceUtility<TEntity> Create<TEntity>() where TEntity : class, IPersistedEntity;
    }
}
