using Formulate.Core.Persistence;
using System;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Scoping;

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
        public FormEntityRepository(IRepositoryUtilityFactory repositoryHelperFactory, ICoreScopeProvider coreScopeProvider) : base(repositoryHelperFactory, coreScopeProvider)
        {
        }
    }
}
