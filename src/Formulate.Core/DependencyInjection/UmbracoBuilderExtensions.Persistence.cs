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
            builder.Services.AddScoped<IFormEntityPersistence, FormEntityPersistence>();
            builder.Services.AddScoped<IFolderEntityPersistence, FolderEntityPersistence>();
            builder.Services.AddScoped<IDataValuesEntityPersistence, DataValuesEntityPersistence>();
            builder.Services.AddScoped<ILayoutEntityPersistence, LayoutEntityPersistence>();
            builder.Services.AddScoped<IValidationEntityPersistence, ValidationEntityPersistence>();

            return builder;
        }
    }
}
