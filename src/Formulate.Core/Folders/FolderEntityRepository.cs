using Formulate.Core.Persistence;

namespace Formulate.Core.Folders
{
    /// <summary>
    /// The default implementation of <see cref="IFolderEntityRepository"/>.
    /// </summary>
    internal sealed class FolderEntityRepository : EntityRepository<PersistedFolder>, IFolderEntityRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FolderEntityRepository"/> class.
        /// </summary>
        /// <inheritdoc />
        public FolderEntityRepository(IRepositoryUtilityFactory repositoryHelperFactory) : base(repositoryHelperFactory)
        {
        }
    }
}
