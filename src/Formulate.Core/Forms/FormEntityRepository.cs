using Formulate.Core.Persistence;

namespace Formulate.Core.Forms
{
    /// <summary>
    /// The default implementation of <see cref="IFormEntityRepository"/>.
    /// </summary>
    internal sealed class FormEntityRepository : EntityRepository<PersistedForm>, IFormEntityRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormEntityRepository"/> class.
        /// </summary>
        /// <inheritdoc />
        public FormEntityRepository(IRepositoryUtilityFactory repositoryHelperFactory) : base(repositoryHelperFactory)
        {
        }
    }
}
