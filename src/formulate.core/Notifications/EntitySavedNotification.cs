using Formulate.Core.Persistence;
using Umbraco.Cms.Core.Notifications;

namespace Formulate.Core.Notifications
{
    public sealed class EntitySavedNotification<TPersistedEntity> : INotification where TPersistedEntity : class, IPersistedEntity
    {
        public EntitySavedNotification(TPersistedEntity entity)
        {
            Entity = entity;
        }

        public TPersistedEntity Entity { get; init; }
    }
}
