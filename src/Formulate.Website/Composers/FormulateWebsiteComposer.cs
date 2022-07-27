namespace Formulate.Website.Composers
{
    using Formulate.Website.DependencyInjection;
    using Umbraco.Cms.Core.Composing;
    using Umbraco.Cms.Core.DependencyInjection;

    public sealed class FormulateWebsiteComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.AddFormulateWebsite();
        }
    }
}
