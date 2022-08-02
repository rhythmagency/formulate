namespace Formulate.BackOffice.Utilities.Layouts
{
    using Formulate.Core.Persistence;

    public interface ICreateLayoutsScaffoldingEntity
    {
        IPersistedEntity? Create(CreateLayoutsScaffoldingEntityInput input);
    }
}
