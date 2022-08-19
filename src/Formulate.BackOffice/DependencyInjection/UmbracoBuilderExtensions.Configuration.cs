namespace Formulate.BackOffice.DependencyInjection
{
    // Namespaces.
    using Configuration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
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
            builder.Services.Configure<FormulateBackOfficeOptions>(x => builder.Config.GetSection(FormulateBackOfficeOptions.SectionName).Bind(x));

            builder.Services.Configure<FormFieldOptions>(x =>
               builder.Config.GetSection(FormFieldOptions.SectionName)
               .Bind(x));

            builder.Services.Configure<ButtonsOptions>(x =>
                builder.Config.GetSection(ButtonsOptions.SectionName)
                .Bind(x));;

            builder.Services.PostConfigure<ButtonsOptions>(options =>
            {
                if (options.Any() == false)
                {
                    options.AddRange(ButtonsOptions.FallbackOptions);
                }
            });

            return builder;
        }
    }
}