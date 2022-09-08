using Formulate.Core.Persistence;
using Umbraco.Cms.Core.Scoping;

namespace Formulate.Core.DataValues
{
    /// <summary>
    /// The default implementation of <see cref="IDataValuesEntityRepository"/>.
    /// </summary>
    internal sealed class DataValuesEntityRepository : EntityRepository<PersistedDataValues>, IDataValuesEntityRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataValuesEntityRepository"/> class.
        /// </summary>
        /// <inheritdoc />
        public DataValuesEntityRepository(IRepositoryUtilityFactory repositoryHelperFactory, ICoreScopeProvider coreScopeProvider) : base(repositoryHelperFactory, coreScopeProvider)
        {
        }
    }
}
