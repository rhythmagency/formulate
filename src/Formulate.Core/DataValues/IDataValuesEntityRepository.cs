using Formulate.Core.Persistence;

namespace Formulate.Core.DataValues
{
    /// <summary>
    /// A contract for managing <see cref="PersistedDataValues"/> entities.
    /// </summary>
    public interface IDataValuesEntityRepository : IEntityRepository<PersistedDataValues>
    {
    }
}
