using Formulate.Core.Folders;

namespace Formulate.Core.Persistence
{
    public static class PersistedEntityExtensions
    {
        public static bool IsFolder(this IPersistedEntity entity)
        {
            return entity is PersistedFolder;
        }
    }
}
