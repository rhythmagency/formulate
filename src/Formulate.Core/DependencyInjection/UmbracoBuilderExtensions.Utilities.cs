namespace Formulate.Core.DependencyInjection
{
    // Namespaces.
    using Microsoft.Extensions.DependencyInjection;
    using Umbraco.Cms.Core.DependencyInjection;
    using Utilities;
    using Utilities.Internal;

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
            builder.Services.AddSingleton<IJsonUtility, JsonUtility>();
            builder.Services.AddSingleton<IJsonSerializer, NewtonsoftJsonSerializer>();

            return builder;
        }
    }
}