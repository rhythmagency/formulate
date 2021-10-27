using Formulate.Core.Persistence;

namespace Formulate.Core.DataValues
{
    /// <summary>
    /// The default implementation of <see cref="IDataValuesEntityPersistence"/>.
    /// </summary>
    internal sealed class DataValuesEntityPersistence : EntityPersistence<PersistedDataValues>, IDataValuesEntityPersistence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataValuesEntityPersistence"/> class.
        /// </summary>
        /// <inheritdoc />
        public DataValuesEntityPersistence(IPersistenceUtilityFactory persistenceHelperFactory) : base(persistenceHelperFactory)
        {
        }
    }
}
