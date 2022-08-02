namespace Formulate.BackOffice.Utilities.Forms
{
    using Formulate.Core.Persistence;

    public  interface ICreateFormsScaffoldingEntity
    {
        IPersistedEntity? Create(CreateFormsScaffoldingEntityInput input);
    }
}
