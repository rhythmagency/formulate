using Formulate.BackOffice.Mapping.EditorModels;
using Formulate.BackOffice.NotificationHandlers;
using Formulate.BackOffice.Persistence;
using Formulate.BackOffice.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace Formulate.BackOffice.DependencyInjection
{
    public static class UmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddFormulateBackOffice(this IUmbracoBuilder builder)
        {
            builder.Sections().Append<FormulateSection>();

            builder.Services.AddScoped<ITreeEntityRepository, TreeEntityRepository>();
            builder.Services.AddScoped<IBuildEditorModel, BuildEditorModel>();
            
            builder.MapDefinitions().Add<DataValuesEditorModelMapDefinition>();
            builder.MapDefinitions().Add<ConfiguredFormEditorModelMapDefinition>();
            builder.MapDefinitions().Add<FolderEditorModelMapDefinition>();
            builder.MapDefinitions().Add<FormEditorModelMapDefinition>();
            builder.MapDefinitions().Add<LayoutEditorModelMapDefinition>();
            builder.MapDefinitions().Add<ValidationEditorModelMapDefinition>();

            builder.AddNotificationHandler<ServerVariablesParsingNotification, ServerVariablesNotificationHandler>();

            return builder;
        }
    }
}
