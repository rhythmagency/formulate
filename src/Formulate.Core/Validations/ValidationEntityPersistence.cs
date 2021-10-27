using Formulate.Core.Persistence;

namespace Formulate.Core.Validations
{
    /// <summary>
    /// The default implementation of <see cref="IValidationEntityPersistence"/>.
    /// </summary>
    internal sealed class ValidationEntityPersistence : EntityPersistence<PersistedValidation>, IValidationEntityPersistence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationEntityPersistence"/> class.
        /// </summary>
        /// <inheritdoc />
        public ValidationEntityPersistence(IPersistenceUtilityFactory persistenceHelperFactory) : base(persistenceHelperFactory)
        {
        }
    }
}
