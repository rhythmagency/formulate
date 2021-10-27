using Formulate.Core.Persistence;

namespace Formulate.Core.ConfiguredForms
{
    /// <summary>
    /// The default implementation of <see cref="IConfiguredFormEntityPersistence"/>.
    /// </summary>
    internal sealed class ConfiguredFormEntityPersistence : EntityPersistence<PersistedConfiguredForm>, IConfiguredFormEntityPersistence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfiguredFormEntityPersistence"/> class.
        /// </summary>
        /// <inheritdoc />
        public ConfiguredFormEntityPersistence(IPersistenceUtilityFactory persistenceHelperFactory) : base(persistenceHelperFactory)
        {
        }
    }
}