namespace Formulate.Extensions.SendEmail
{
    using Umbraco.Cms.Core.Composing;
    using Umbraco.Cms.Core.DependencyInjection;

    public sealed class SendEmailComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.AddPackageManifest();
        }
    }
}
