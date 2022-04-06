namespace Formulate.Core.DataValues
{
    // Namespaces.
    using Persistence;

    /// <summary>
    /// A contract for managing <see cref="PersistedDataValues"/> entities.
    /// </summary>
    public interface IDataValuesEntityRepository : IEntityRepository<PersistedDataValues>
    {
    }
}