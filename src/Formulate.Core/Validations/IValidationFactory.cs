using Formulate.Core.Types;

namespace Formulate.Core.Validations
{
    /// <summary>
    /// Creates a <see cref="IValidation"/>.
    /// </summary>
    public interface IValidationFactory : IEntityFactory<PersistedValidation, IValidation>
    {
    }
}
