namespace Formulate.BackOffice.Utilities.DataValues
{
    using Formulate.Core.Persistence;

    public interface ICreateDataValuesScaffoldingEntity
    {
        IPersistedEntity? Create(CreateDataValuesScaffoldingEntityInput input);
    }
}
