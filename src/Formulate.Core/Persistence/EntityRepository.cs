using System;
using System.Collections.Generic;

namespace Formulate.Core.Persistence
{
    /// <summary>
    /// An abstract class for creating a entity repository class.
    /// </summary>
    /// <typeparam name="TPersistedEntity">The type of entity to manage.</typeparam>
    public abstract class EntityRepository<TPersistedEntity> : IEntityRepository<TPersistedEntity> where TPersistedEntity : class, IPersistedEntity
    {
        /// <summary>
        /// THe repository utility.
        /// </summary>
        private readonly IRepositoryUtility<TPersistedEntity> _repositoryUtility;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRepository{TPersistedEntity}"/> class.
        /// </summary>
        /// <param name="repositoryHelperFactory">The repository utility helper.</param>
        protected EntityRepository(IRepositoryUtilityFactory repositoryHelperFactory)
        {
            _repositoryUtility = repositoryHelperFactory.Create<TPersistedEntity>();
        }

        /// <inheritdoc />
        public virtual TPersistedEntity Get(Guid id)
        {
            return _repositoryUtility.Get(id);
        }

        /// <inheritdoc />
        public virtual TPersistedEntity Save(TPersistedEntity entity)
        {
            return _repositoryUtility.Save(entity);
        }

        /// <inheritdoc />
        public virtual void Delete(Guid id)
        {
            _repositoryUtility.Delete(id);
        }

        /// <inheritdoc />
        public IReadOnlyCollection<TPersistedEntity> GetChildren(Guid parentId)
        {
            return _repositoryUtility.GetChildren(parentId);
        }

        /// <inheritdoc />
        public virtual IReadOnlyCollection<TPersistedEntity> GetRootItems()
        {
            return _repositoryUtility.GetRootItems();
        }
    }
}
