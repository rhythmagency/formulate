namespace Formulate.Extensions.StoreData.Utilities
{
    using Formulate.Extensions.StoreData.Models;

    public interface IStoreFields
    {
        IReadOnlyCollection<StoreDataEntry> Execute(StoreFieldsInput input);
    }
}