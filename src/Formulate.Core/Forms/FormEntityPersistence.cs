using Formulate.Core.Persistence;

namespace Formulate.Core.Forms
{
    /// <summary>
    /// The default implementation of <see cref="IFormEntityPersistence"/>.
    /// </summary>
    internal sealed class FormEntityPersistence : EntityPersistence<PersistedForm>, IFormEntityPersistence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormEntityPersistence"/> class.
        /// </summary>
        /// <inheritdoc />
        public FormEntityPersistence(IPersistenceUtilityFactory persistenceHelperFactory) : base(persistenceHelperFactory)
        {
        }
    }
}
