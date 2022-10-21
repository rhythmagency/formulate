namespace Formulate.Extensions.StoreData.Utilities
{
    using Formulate.Extensions.StoreData.Models;

    public interface IStoreFiles
    {
        IReadOnlyCollection<StoreDataEntry> Execute(StoreFilesInput input);
    }
}