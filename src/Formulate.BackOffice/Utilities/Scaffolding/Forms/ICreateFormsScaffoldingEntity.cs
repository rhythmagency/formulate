namespace Formulate.BackOffice.Utilities.Scaffolding.Forms
{
    using Formulate.Core.Persistence;

    public interface ICreateFormsScaffoldingEntity
    {
        IPersistedEntity? Create(CreateFormsScaffoldingEntityInput input);
    }
}
