using System;
using System.Collections.Generic;

namespace Formulate.Core.Persistence
{
    /// <summary>
    /// An abstract class for creating a entity persistence class.
    /// </summary>
    /// <typeparam name="TPersistedEntity">The type of entity to manage.</typeparam>
    public abstract class EntityPersistence<TPersistedEntity> : IEntityPersistence<TPersistedEntity> where TPersistedEntity : class, IPersistedEntity
    {
        /// <summary>
        /// THe persistence utility.
        /// </summary>
        private readonly IPersistenceUtility<TPersistedEntity> _persistenceUtility;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityPersistence{TPersistedEntity}"/> class.
        /// </summary>
        /// <param name="persistenceHelperFactory">The persistence utility helper.</param>
        protected EntityPersistence(IPersistenceUtilityFactory persistenceHelperFactory)
        {
            _persistenceUtility = persistenceHelperFactory.Create<TPersistedEntity>();
        }

        /// <inheritdoc />
        public virtual TPersistedEntity Get(Guid id)
        {
            return _persistenceUtility.Get(id);
        }

        /// <inheritdoc />
        public virtual TPersistedEntity Save(TPersistedEntity entity)
        {
            return _persistenceUtility.Save(entity);
        }

        /// <inheritdoc />
        public virtual void Delete(Guid id)
        {
            _persistenceUtility.Delete(id);
        }

        /// <inheritdoc />
        public IReadOnlyCollection<TPersistedEntity> GetChildren(Guid parentId)
        {
            return _persistenceUtility.GetChildren(parentId);
        }

        /// <inheritdoc />
        public virtual IReadOnlyCollection<TPersistedEntity> GetRootItems()
        {
            return _persistenceUtility.GetRootItems();
        }
    }
}
