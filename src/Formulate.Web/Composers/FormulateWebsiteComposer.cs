namespace Formulate.Web.Composers
{
    using Formulate.Web.DependencyInjection;
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
