using Formulate.Core.Persistence;

namespace Formulate.Core.ConfiguredForms
{
    /// <summary>
    /// A contract for managing <see cref="PersistedConfiguredForm"/> entities.
    /// </summary>
    public interface IConfiguredFormEntityRepository : IEntityRepository<PersistedConfiguredForm>
    {
    }
}
