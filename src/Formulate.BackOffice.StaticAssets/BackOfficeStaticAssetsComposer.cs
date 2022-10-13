namespace Formulate.BackOffice.StaticAssets
{
    using Umbraco.Cms.Core.Composing;
    using Umbraco.Cms.Core.DependencyInjection;

    public sealed class BackOfficeStaticAssetsComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.AddFormulateBackOfficeStaticAssets();
        }
    }
}
