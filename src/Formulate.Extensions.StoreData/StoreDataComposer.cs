namespace Formulate.Extensions.StoreData
{
    using Umbraco.Cms.Core.Composing;
    using Umbraco.Cms.Core.DependencyInjection;

    public sealed class StoreDataComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.AddPackageManifest();
        }
    }
}
