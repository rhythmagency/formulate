using System;
using System.Collections.Generic;
using Formulate.Core.Persistence;

namespace Formulate.BackOffice.Persistence
{
    public interface ITreeEntityPersistence
    {
        IPersistedEntity Get(Guid id);

        IReadOnlyCollection<IPersistedEntity> GetChildren(Guid parentId);

        bool HasChildren(Guid parentId);

        IReadOnlyCollection<IPersistedEntity> GetRootItems(FormulateEntityTypes type);
    }
}