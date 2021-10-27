using Formulate.Core.Persistence;

namespace Formulate.Core.Layouts
{
    /// <summary>
    /// The default implementation of <see cref="ILayoutEntityPersistence"/>.
    /// </summary>
    internal sealed class LayoutEntityPersistence : EntityPersistence<PersistedLayout>, ILayoutEntityPersistence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutEntityPersistence"/> class.
        /// </summary>
        /// <inheritdoc />
        public LayoutEntityPersistence(IPersistenceUtilityFactory persistenceHelperFactory) : base(persistenceHelperFactory)
        {
        }
    }
}
