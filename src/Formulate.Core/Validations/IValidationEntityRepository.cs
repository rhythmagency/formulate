using Formulate.Core.Persistence;

namespace Formulate.Core.Validations
{
    /// <summary>
    /// A contract for managing <see cref="PersistedValidation"/> entities.
    /// </summary>
    public interface IValidationEntityRepository : IEntityRepository<PersistedValidation>
    {
    }
}