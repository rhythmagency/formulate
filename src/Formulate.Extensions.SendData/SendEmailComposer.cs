namespace Formulate.Extensions.SendData
{
    using Umbraco.Cms.Core.Composing;
    using Umbraco.Cms.Core.DependencyInjection;

    public sealed class SendDataComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.AddPackageManifest();
        }
    }
}
