using Formulate.Core.Persistence;

namespace Formulate.Core.ConfiguredForms
{
    /// <summary>
    /// The default implementation of <see cref="IConfiguredFormEntityRepository"/>.
    /// </summary>
    internal sealed class ConfiguredFormEntityRepository : EntityRepository<PersistedConfiguredForm>, IConfiguredFormEntityRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfiguredFormEntityRepository"/> class.
        /// </summary>
        /// <inheritdoc />
        public ConfiguredFormEntityRepository(IRepositoryUtilityFactory repositoryHelperFactory) : base(repositoryHelperFactory)
        {
        }
    }
}