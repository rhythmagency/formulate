using Formulate.Core.Persistence;

namespace Formulate.Core.Layouts
{
    /// <summary>
    /// A contract for managing <see cref="PersistedLayout"/> entities.
    /// </summary>
    public interface ILayoutEntityRepository : IEntityRepository<PersistedLayout>
    {
    }
}