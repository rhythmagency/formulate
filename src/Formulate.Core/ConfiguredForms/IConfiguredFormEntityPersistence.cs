using Formulate.Core.Persistence;

namespace Formulate.Core.ConfiguredForms
{
    /// <summary>
    /// A contract for managing <see cref="PersistedConfiguredForm"/> persistence.
    /// </summary>
    public interface IConfiguredFormEntityPersistence : IEntityPersistence<PersistedConfiguredForm>
    {
    }
}
