using Formulate.Core.Persistence;

namespace Formulate.Core.Forms
{
    /// <summary>
    /// A contract for managing <see cref="PersistedForm"/> entities.
    /// </summary>
    public interface IFormEntityRepository : IEntityRepository<PersistedForm>
    {
    }
}
