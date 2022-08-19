namespace Formulate.BackOffice.DependencyInjection
{
    using Formulate.BackOffice.Configuration;
    using Formulate.BackOffice.Mapping.EditorModels;
    using Formulate.BackOffice.NotificationHandlers;
    using Formulate.BackOffice.Persistence;
    using Formulate.BackOffice.Utilities;
    using Formulate.BackOffice.Utilities.CreateOptions.DataValues;
    using Formulate.BackOffice.Utilities.CreateOptions.FormFields;
    using Formulate.BackOffice.Utilities.CreateOptions.FormHandlers;
    using Formulate.BackOffice.Utilities.CreateOptions.Forms;
    using Formulate.BackOffice.Utilities.CreateOptions.Layouts;
    using Formulate.BackOffice.Utilities.CreateOptions.Validations;
    using Formulate.BackOffice.Utilities.DataValues;
    using Formulate.BackOffice.Utilities.EditorModels.Templates;
    using Formulate.BackOffice.Utilities.FormFields;
    using Formulate.BackOffice.Utilities.FormHandlers;
    using Formulate.BackOffice.Utilities.Scaffolding.Forms;
    using Formulate.BackOffice.Utilities.Scaffolding.Layouts;
    using Formulate.BackOffice.Utilities.Trees.DataValues;
    using Formulate.BackOffice.Utilities.Trees.Forms;
    using Formulate.BackOffice.Utilities.Trees.Layouts;
    using Formulate.BackOffice.Utilities.Trees.Validations;
    using Formulate.BackOffice.Utilities.Validations;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Umbraco.Cms.Core.DependencyInjection;
    using Umbraco.Cms.Core.Notifications;

    public static class UmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddFormulateBackOffice(this IUmbracoBuilder builder)
        {
            builder.Sections().Append<FormulateSection>();

            builder.Services.Configure<FormulateBackOfficeOptions>(x => builder.Config.GetSection(FormulateBackOfficeOptions.SectionName).Bind(x));

            builder.Services.AddScoped<IGetFolderIconOrDefault, GetFolderIconOrDefault>();

            builder.Services.AddScoped<IGetTemplateEditorModels, GetTemplateEditorModels>();

            builder.Services.AddScoped<ITreeEntityRepository, TreeEntityRepository>();
            builder.Services.AddScoped<IEditorModelMapper, EditorModelMapper>();

            builder.Services.AddScoped<IDataValuesEntityTreeUtility, DataValuesEntityTreeUtility>();
            builder.Services.AddScoped<IFormsEntityTreeUtility, FormsEntityTreeUtility>();
            builder.Services.AddScoped<ILayoutsEntityTreeUtility, LayoutsEntityTreeUtility>();
            builder.Services.AddScoped<IValidationsEntityTreeUtility, ValidationsEntityTreeUtility>();

            builder.Services.AddScoped<ICreateFormsScaffoldingEntity, CreateFormsScaffoldingEntity>();
            builder.Services.AddScoped<IGetFormsChildEntityOptions, GetFormsChildEntityOptions>();

            builder.Services.AddScoped<ICreateDataValuesScaffoldingEntity, CreateDataValuesScaffoldingEntity>();
            builder.Services.AddScoped<IGetDataValuesChildEntityOptions, GetDataValuesChildEntityOptions>();

            builder.Services.AddScoped<ICreateLayoutsScaffoldingEntity, CreateLayoutsScaffoldingEntity>();
            builder.Services.AddScoped<IGetLayoutsChildEntityOptions, GetLayoutsChildEntityOptions>();

            builder.Services.AddScoped<ICreateValidationsScaffoldingEntity, CreateValidationsScaffoldingEntity>();
            builder.Services.AddScoped<IGetValidationsChildEntityOptions, GetValidationsChildEntityOptions>();

            builder.Services.AddScoped<IGetFormFieldOptions, GetFormFieldOptions>();
            builder.Services.AddScoped<IGetFormFieldScaffolding, GetFormFieldScaffolding>();

            builder.Services.AddScoped<IGetFormHandlerOptions, GetFormHandlerOptions>();
            builder.Services.AddScoped<IGetFormHandlerScaffolding, GetFormHandlerScaffolding>();

            builder.MapDefinitions().Add<DataValuesEditorModelMapDefinition>();
            builder.MapDefinitions().Add<ConfiguredFormEditorModelMapDefinition>();
            builder.MapDefinitions().Add<FolderEditorModelMapDefinition>();
            builder.MapDefinitions().Add<FormFieldEditorModelMapDefinition>();
            builder.MapDefinitions().Add<FormHandlerEditorModelMapDefinition>();
            builder.MapDefinitions().Add<FormEditorModelMapDefinition>();
            builder.MapDefinitions().Add<LayoutEditorModelMapDefinition>();
            builder.MapDefinitions().Add<ValidationEditorModelMapDefinition>();

            builder.AddNotificationHandler<ServerVariablesParsingNotification, ServerVariablesNotificationHandler>();

            return builder;
        }
    }
}
