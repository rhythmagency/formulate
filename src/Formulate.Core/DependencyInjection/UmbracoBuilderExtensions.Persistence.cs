using Formulate.Core.ConfiguredForms;
using Formulate.Core.DataValues;
using Formulate.Core.Folders;
using Formulate.Core.Forms;
using Formulate.Core.Layouts;
using Formulate.Core.Persistence;
using Formulate.Core.Validations;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;

namespace Formulate.Core.DependencyInjection
{
    public partial class UmbracoBuilderExtensions
    {
        /// <summary>
        /// Adds Formulate persistence.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>The current <see cref="IUmbracoBuilder"/>.</returns>
        private static IUmbracoBuilder AddFormulatePersistence(this IUmbracoBuilder builder)
        {
            builder.Services.AddSingleton<IPersistedEntityCache, FileSystemPersistedEntityCache>();
            builder.Services.AddScoped<IDataValuesEntityRepository, DataValuesEntityRepository>();
            builder.Services.AddScoped<IConfiguredFormEntityRepository, ConfiguredFormEntityRepository>();
            builder.Services.AddScoped<IFormEntityRepository, FormEntityRepository>();
            builder.Services.AddScoped<IFolderEntityRepository, FolderEntityRepository>();
            builder.Services.AddScoped<ILayoutEntityRepository, LayoutEntityRepository>();
            builder.Services.AddScoped<IValidationEntityRepository, ValidationEntityRepository>();

            return builder;
        }
    }
}
