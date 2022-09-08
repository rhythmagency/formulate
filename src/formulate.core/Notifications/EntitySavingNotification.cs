using Formulate.Core.Persistence;
using Umbraco.Cms.Core.Notifications;

namespace Formulate.Core.Notifications
{
    public sealed class EntitySavingNotification<TPersistedEntity> : INotification where TPersistedEntity : class, IPersistedEntity
    {
        public EntitySavingNotification(TPersistedEntity entity)
        {
            Entity = entity;
        }

        public TPersistedEntity Entity { get; init; }
    }
}
