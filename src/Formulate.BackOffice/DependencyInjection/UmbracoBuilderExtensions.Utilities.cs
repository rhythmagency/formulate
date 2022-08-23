namespace Formulate.BackOffice.DependencyInjection
{
    // Namespaces.
    using Formulate.BackOffice.Persistence;
    using Formulate.BackOffice.Utilities;
    using Formulate.BackOffice.Utilities.CreateOptions.DataValues;
    using Formulate.BackOffice.Utilities.CreateOptions.FormFields;
    using Formulate.BackOffice.Utilities.CreateOptions.FormHandlers;
    using Formulate.BackOffice.Utilities.CreateOptions.Forms;
    using Formulate.BackOffice.Utilities.CreateOptions.Layouts;
    using Formulate.BackOffice.Utilities.CreateOptions.Validations;
    using Formulate.BackOffice.Utilities.DataValues;
    using Formulate.BackOffice.Utilities.EditorModels.Buttons;
    using Formulate.BackOffice.Utilities.EditorModels.Forms;
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
    using Formulate.Core.Utilities.Submissions;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using Umbraco.Cms.Core.DependencyInjection;

    public partial class UmbracoBuilderExtensions
    {
        /// <summary>
        /// Adds Formulate Utilities.
        /// </summary>
        /// <param name="builder">
        /// The Umbraco builder.
        /// </param>
        /// <returns>
        /// The current <see cref="IUmbracoBuilder"/>.
        /// </returns>
        private static IUmbracoBuilder AddFormulateUtilities(
            this IUmbracoBuilder builder)
        {
            builder.Services.AddScoped<IGetFolderIconOrDefault, GetFolderIconOrDefault>();

            builder.Services.AddScoped<IGetButtonKindEditorModels, GetButtonKindEditorModels>();
            builder.Services.AddScoped<IGetFormFieldCategoryEditorModels, GetFormFieldCategoryEditorModels>();
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

            return builder;
        }
    }
}
