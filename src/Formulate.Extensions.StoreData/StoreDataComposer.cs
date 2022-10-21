namespace Formulate.Extensions.StoreData
{
    using Formulate.Extensions.StoreData.Utilities;
    using Microsoft.Extensions.DependencyInjection;
    using Umbraco.Cms.Core.Composing;
    using Umbraco.Cms.Core.DependencyInjection;

    public sealed class StoreDataComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddScoped<IStoreFields, StoreFields>();
            builder.Services.AddScoped<IStoreFiles, StoreFiles>();
            builder.Services.AddScoped<IStoreData, StoreData>();

            builder.AddPackageManifest();

            builder.RunMigrations();
        }
    }
}
