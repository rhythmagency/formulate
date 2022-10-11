namespace Formulate.BackOffice.DependencyInjection
{
    using Formulate.BackOffice.ContentApps.Forms;
    using Formulate.BackOffice.Definitions.Forms;
    using Umbraco.Cms.Core.DependencyInjection;

    public partial class UmbracoBuilderExtensions
    {
        private static void AddFormulateContentApps(this IUmbracoBuilder builder)
        {
            builder.AddContentApp<FormFieldsContentAppFactory>();
            builder.AddContentApp<FormHandlersContentAppFactory>();
        }
    }
}
