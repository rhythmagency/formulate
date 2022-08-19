namespace Formulate.BackOffice.DependencyInjection
{
    using Umbraco.Cms.Core.DependencyInjection;

    public partial class UmbracoBuilderExtensions
    {
        /// <summary>
        /// Adds Formulate Section.
        /// </summary>
        /// <param name="builder">
        /// The Umbraco builder.
        /// </param>
        /// <returns>
        /// The current <see cref="IUmbracoBuilder"/>.
        /// </returns>
        private static IUmbracoBuilder AddFormulateSection(
            this IUmbracoBuilder builder)
        {
            builder.Sections().Append<FormulateSection>();

            return builder;
        }
    }
}
