namespace Formulate.BackOffice.Utilities.Validations
{
    using Formulate.Core.Persistence;

    public interface ICreateValidationsScaffoldingEntity
    {
        IPersistedEntity? Create(CreateValidationsScaffoldingEntityInput input);
    }
}
