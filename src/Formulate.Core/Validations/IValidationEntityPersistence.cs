using Formulate.Core.Persistence;

namespace Formulate.Core.Validations
{
    /// <summary>
    /// A contract for managing <see cref="PersistedValidation"/> persistence.
    /// </summary>
    public interface IValidationEntityPersistence : IEntityPersistence<PersistedValidation>
    {
    }
}