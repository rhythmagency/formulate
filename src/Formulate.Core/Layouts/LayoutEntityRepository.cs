using Formulate.Core.Persistence;
using Umbraco.Cms.Core.Scoping;

namespace Formulate.Core.Layouts
{
    /// <summary>
    /// The default implementation of <see cref="ILayoutEntityRepository"/>.
    /// </summary>
    internal sealed class LayoutEntityRepository : EntityRepository<PersistedLayout>, ILayoutEntityRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutEntityRepository"/> class.
        /// </summary>
        /// <inheritdoc />
        public LayoutEntityRepository(IRepositoryUtilityFactory repositoryHelperFactory, ICoreScopeProvider coreScopeProvider) : base(repositoryHelperFactory, coreScopeProvider)
        {
        }
    }
}
