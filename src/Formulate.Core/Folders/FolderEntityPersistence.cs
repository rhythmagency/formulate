using Formulate.Core.Persistence;

namespace Formulate.Core.Folders
{
    internal sealed class FolderEntityPersistence : EntityPersistence<PersistedFolder>, IFolderEntityPersistence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FolderEntityPersistence"/> class.
        /// </summary>
        /// <inheritdoc />
        public FolderEntityPersistence(IPersistenceUtilityFactory persistenceHelperFactory) : base(persistenceHelperFactory)
        {
        }
    }
}