namespace Formulate.BackOffice.Utilities.Scaffolding.Layouts
{
    using Formulate.Core.Persistence;

    public interface ICreateLayoutsScaffoldingEntity
    {
        IPersistedEntity? Create(CreateLayoutsScaffoldingEntityInput input);
    }
}
