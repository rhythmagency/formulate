using Formulate.Core.Folders;
using System;

namespace Formulate.Core.Persistence
{
    public static class PersistedEntityExtensions
    {
        public static bool IsFolder(this IPersistedEntity entity)
        {
            return entity is PersistedFolder;
        }

        public static Guid? ParentId(this IPersistedEntity entity)
        {
            if (entity is null)
            {
                return default;
            }

            if (entity.Path.Length <= 1)
            {
                return default;
            }

            return entity.Path[^2];         
        }
    }
}
