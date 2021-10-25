using Formulate.Core.Utilities;
using Formulate.Core.Utilities.Internal;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;

namespace Formulate.Core.DependencyInjection
{
    public partial class UmbracoBuilderExtensions
    {
        /// <summary>
        /// Adds Formulates utilities.
        /// </summary>
        /// <param name="builder">The Umbraco builder.</param>
        /// <returns>The current <see cref="IUmbracoBuilder"/>.</returns>
        private static IUmbracoBuilder AddFormulateUtilities(this IUmbracoBuilder builder)
        {
            builder.Services.AddSingleton<IGetDataValuesItemsUtility, GetDataValuesItemsUtility>();
            builder.Services.AddSingleton<IJsonUtility, SystemTextJsonUtility>();

            return builder;
        }
    }
}
