using Formulate.Core.Persistence;

namespace Formulate.Core.Folders
{
    /// <summary>
    /// A contract for managing <see cref="PersistedFolder"/> repository.
    /// </summary>
    public interface IFolderEntityRepository : IEntityRepository<PersistedFolder>
    {
    }
}
