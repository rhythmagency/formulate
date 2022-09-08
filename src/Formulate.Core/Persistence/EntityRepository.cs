using Formulate.Core.Notifications;
using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Scoping;

namespace Formulate.Core.Persistence
{
    /// <summary>
    /// An abstract class for creating a entity repository class.
    /// </summary>
    /// <typeparam name="TPersistedEntity">The type of entity to manage.</typeparam>
    public abstract class EntityRepository<TPersistedEntity> : IEntityRepository<TPersistedEntity> where TPersistedEntity : class, IPersistedEntity
    {
        private readonly ICoreScopeProvider _coreScopeProvider;

        /// <summary>
        /// THe repository utility.
        /// </summary>
        private readonly IRepositoryUtility<TPersistedEntity> _repositoryUtility;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRepository{TPersistedEntity}"/> class.
        /// </summary>
        /// <param name="repositoryHelperFactory">The repository utility helper.</param>
        protected EntityRepository(IRepositoryUtilityFactory repositoryHelperFactory, ICoreScopeProvider coreScopeProvider)
        {
            _repositoryUtility = repositoryHelperFactory.Create<TPersistedEntity>();
            _coreScopeProvider = coreScopeProvider;
        }

        /// <inheritdoc />
        public virtual TPersistedEntity Get(Guid id)
        {
            return _repositoryUtility.Get(id);
        }

        /// <inheritdoc />
        public virtual TPersistedEntity Save(TPersistedEntity entity)
        {
            using (var scope = _coreScopeProvider.CreateCoreScope())
            {
                var savingNotification = new EntitySavingNotification<TPersistedEntity>(entity);
                scope.Notifications.Publish(savingNotification);

                var savedEntity = _repositoryUtility.Save(entity);
                var savedNotification = new EntitySavedNotification<TPersistedEntity>(savedEntity);
                
                scope.Notifications.Publish(savedNotification);
                scope.Complete();

                return savedEntity;
            }
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

        public Guid[] Move(TPersistedEntity entity, Guid[] newPath)
        {
            return _repositoryUtility.Move(entity, newPath);
        }

        /// <inheritdoc />
        public virtual IReadOnlyCollection<TPersistedEntity> GetRootItems()
        {
            return _repositoryUtility.GetRootItems();
        }
    }
}
