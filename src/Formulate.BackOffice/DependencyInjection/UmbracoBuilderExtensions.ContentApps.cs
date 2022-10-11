namespace Formulate.BackOffice.DependencyInjection
{
    using Formulate.BackOffice.ContentApps.Forms;
    using Formulate.BackOffice.ContentApps.Folders;
    using Formulate.BackOffice.ContentApps.Layouts;
    using Formulate.BackOffice.ContentApps.DataValues;
    using Formulate.BackOffice.ContentApps.Validations;
    using Formulate.BackOffice.ContentApps.FormFields;
    using Formulate.BackOffice.ContentApps.FormHandlers;

    using Umbraco.Cms.Core.DependencyInjection;

    public partial class UmbracoBuilderExtensions
    {
        private static void AddFormulateContentApps(this IUmbracoBuilder builder)
        {
            builder.AddContentApp<DataValuesEditorContentAppFactory>();

            builder.AddContentApp<FormFieldsEditorContentAppFactory>();
            builder.AddContentApp<FormHandlersEditorContentAppFactory>();


            builder.AddContentApp<FormFieldsContentAppFactory>();
            builder.AddContentApp<FormHandlersContentAppFactory>();

            builder.AddContentApp<FoldersContentAppFactory>();

            builder.AddContentApp<LayoutEditorContentAppFactory>();

            builder.AddContentApp<ValidationEditorContentAppFactory>();
        }
    }
}
