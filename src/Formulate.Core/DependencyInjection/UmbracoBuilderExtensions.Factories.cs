using Formulate.Core.FormFields;
using Formulate.Core.FormHandlers;
using Formulate.Core.Validations;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;

namespace Formulate.Core.DependencyInjection
{
    public partial class UmbracoBuilderExtensions
    {
        /// <summary>
        /// Adds Formulates factories.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>The current <see cref="IUmbracoBuilder"/>.</returns>
        private static IUmbracoBuilder AddFormulateFactories(this IUmbracoBuilder builder)
        {
            builder.Services.AddScoped<IFormHandlerFactory, FormHandlerFactory>();
            builder.Services.AddScoped<IFormFieldFactory, FormFieldFactory>();
            builder.Services.AddScoped<IValidationFactory, ValidationFactory>();

            return builder;
        }
    }
}
