namespace Formulate.BackOffice.DependencyInjection
{
    using Formulate.BackOffice.ContentApps.Forms;
    using Formulate.BackOffice.ContentApps.Folders;
    using Formulate.BackOffice.ContentApps.Layouts;

    using Umbraco.Cms.Core.DependencyInjection;
    using Formulate.BackOffice.ContentApps.DataValues;
    using Formulate.BackOffice.ContentApps.Validations;

    public partial class UmbracoBuilderExtensions
    {
        private static void AddFormulateContentApps(this IUmbracoBuilder builder)
        {
            builder.AddContentApp<DataValuesEditorContentAppFactory>();

            builder.AddContentApp<FormFieldsContentAppFactory>();
            builder.AddContentApp<FormHandlersContentAppFactory>();

            builder.AddContentApp<FoldersContentAppFactory>();

            builder.AddContentApp<LayoutEditorContentAppFactory>();

            builder.AddContentApp<ValidationEditorContentAppFactory>();
        }
    }
}
