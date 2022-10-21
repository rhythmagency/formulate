namespace Formulate.Extensions.StoreData.Utilities
{
    using Formulate.Extensions.StoreData.Models;
    using Lucene.Net.Index;

    public interface IStoreFields
    {
        IReadOnlyCollection<StoreDataEntry> Execute(StoreFieldsInput input);
    }
}