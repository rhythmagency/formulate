namespace Formulate.Core.DependencyInjection
{
    // Namespaces.
    using Configuration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Umbraco.Cms.Core.DependencyInjection;

    public partial class UmbracoBuilderExtensions
    {
        /// <summary>
        /// Adds Formulate configuration.
        /// </summary>
        /// <param name="builder">
        /// The Umbraco builder.
        /// </param>
        /// <returns>
        /// The current <see cref="IUmbracoBuilder"/>.
        /// </returns>
        private static IUmbracoBuilder AddFormulateConfiguration(
            this IUmbracoBuilder builder)
        {
            builder.Services.Configure<TemplatesOptions>(x =>
                builder.Config.GetSection(TemplatesOptions.SectionName)
                .Bind(x));

            return builder;
        }
    }
}