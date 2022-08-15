﻿namespace Formulate.BackOffice.NotificationHandlers
{
    // Namespaces.
    using Controllers;
    using Controllers.DataValues;
    using Controllers.Folders;
    using Controllers.Forms;
    using Controllers.Layouts;
    using Controllers.Validations;
    using Core.DataValues;
    using Core.Forms;
    using Core.Layouts;
    using Core.Validations;
    using Formulate.BackOffice.Controllers.ConfiguredForms;
    using Formulate.BackOffice.Controllers.Fields;
    using Microsoft.AspNetCore.Routing;
    using System.Collections.Generic;
    using Trees;
    using Umbraco.Cms.Core.Events;
    using Umbraco.Cms.Core.Notifications;
    using Umbraco.Extensions;

    /// <summary>
    /// The server variables notification handler.
    /// </summary>
    internal sealed class ServerVariablesNotificationHandler : INotificationHandler<ServerVariablesParsingNotification>
    {
        private LinkGenerator LinkGenerator { get; set; }

        public ServerVariablesNotificationHandler(LinkGenerator linkGenerator)
        {
            LinkGenerator = linkGenerator;
        }

        public void Handle(ServerVariablesParsingNotification notification)
        {
            // Variables.
            var key = "formulate";// Meta.Constants.PackageNameCamelCase;

            // Add server variables.
            var newEntries = new Dictionary<string, object?>()
            {
                //{ "PersistForm",
                //    LinkGenerator.GetUmbracoApiService<FormsController>(x =>
                //        x.PersistForm(null)) },
                //{ "MoveForm",
                //    LinkGenerator.GetUmbracoApiService<FormsController>(x =>
                //        x.MoveForm(null)) },
                //{ "DeleteConfiguredForm",
                //    LinkGenerator.GetUmbracoApiService<ConfiguredFormsController>(x =>
                //        x.DeleteConfiguredForm(null)) },
                //{ "PersistConfiguredForm",
                //    LinkGenerator.GetUmbracoApiService<ConfiguredFormsController>(x =>
                //        x.PersistConfiguredForm(null)) },
                //{ "GetConfiguredFormInfo",
                //    LinkGenerator.GetUmbracoApiService<ConfiguredFormsContentController>(x =>
                //        x.GetConfiguredFormInfo(null)) },
                //{ "DeleteLayout",
                //    LinkGenerator.GetUmbracoApiService<LayoutsController>(x =>
                //        x.DeleteLayout(null)) },
                //{ "PersistLayout",
                //    LinkGenerator.GetUmbracoApiService<LayoutsController>(x =>
                //        x.PersistLayout(null)) },
                //{ "GetLayoutKinds",
                //    LinkGenerator.GetUmbracoApiService<LayoutsController>(x =>
                //        x.GetLayoutKinds()) },
                //{ "MoveLayout",
                //    LinkGenerator.GetUmbracoApiService<LayoutsController>(x =>
                //        x.MoveLayout(null)) },
                //{ "DeleteValidation",
                //    LinkGenerator.GetUmbracoApiService<ValidationsController>(x =>
                //        x.DeleteValidation(null)) },
                //{ "PersistValidation",
                //    LinkGenerator.GetUmbracoApiService<ValidationsController>(x =>
                //        x.PersistValidation(null)) },
                //{ "GetValidationInfo",
                //    LinkGenerator.GetUmbracoApiService<ValidationsController>(x =>
                //        x.GetValidationInfo(null)) },
                //{ "GetValidationsInfo",
                //    LinkGenerator.GetUmbracoApiService<ValidationsController>(x =>
                //        x.GetValidationsInfo(null)) },
                //{ "MoveValidation",
                //    LinkGenerator.GetUmbracoApiService<ValidationsController>(x =>
                //        x.MoveValidation(null)) },
                //{ "DeleteDataValue",
                //    LinkGenerator.GetUmbracoApiService<DataValuesController>(x =>
                //        x.DeleteDataValue(null)) },
                //{ "PersistDataValue",
                //    LinkGenerator.GetUmbracoApiService<DataValuesController>(x =>
                //        x.PersistDataValue(null)) },
                { "GetDataValueInfo",
                    LinkGenerator.GetUmbracoApiService<DataValuesController>(x =>
                        x.Get()) },
                //{ "GetDataValuesInfo",
                //    LinkGenerator.GetUmbracoApiService<DataValuesController>(x =>
                //        x.Get()) },
                //{ "GetDataValueKinds",
                //    LinkGenerator.GetUmbracoApiService<DataValuesController>(x =>
                //        x.GetDataValueTypes()) },
                //{ "GetDataValueSuppliers",
                //    LinkGenerator.GetUmbracoApiService<DataValuesController>(x =>
                //        x.GetDataValueSuppliers()) },
                //{ "MoveDataValue",
                //    LinkGenerator.GetUmbracoApiService<DataValuesController>(x =>
                //        x.MoveDataValue(null)) },
                //{ "PersistFolder",
                //    LinkGenerator.GetUmbracoApiService<FoldersController>(x =>
                //        x.PersistFolder(null)) },
                //{ "GetFolderInfo",
                //    LinkGenerator.GetUmbracoApiService<FoldersController>(x =>
                //        x.GetFolderInfo(null)) },
                //{ "MoveFolder",
                //    LinkGenerator.GetUmbracoApiService<FoldersController>(x =>
                //        x.MoveFolder(null)) },
                //{ "DeleteFolder",
                //    LinkGenerator.GetUmbracoApiService<FoldersController>(x =>
                //        x.DeleteFolder(null)) },
                { "GetButtonKinds",
                    LinkGenerator.GetUmbracoApiService<FieldsController>(x =>
                        x.GetButtonKinds()) },
                //{ "GetFieldCategories",
                //    LinkGenerator.GetUmbracoApiService<FieldsController>(x =>
                //        x.GetFieldCategories()) },
                //{ "GetResultHandlers",
                //    LinkGenerator.GetUmbracoApiService<HandlersController>(x =>
                //        x.GetResultHandlers()) },
                //{ "GetTemplates",
                //    LinkGenerator.GetUmbracoApiService<TemplatesController>(x =>
                //        x.GetTemplates()) },
                //{ "GetEntityChildren",
                //    LinkGenerator.GetUmbracoApiService<EntitiesContentController>(x =>
                //        x.GetEntityChildren(null)) },
                //{ "GetEntity",
                //    LinkGenerator.GetUmbracoApiService<EntitiesController>(x =>
                //        x.GetEntity(null)) },
                //{ "GetSubmissions",
                //    LinkGenerator.GetUmbracoApiService<StoredDataController>(x =>
                //        x.GetSubmissions(null)) },
                //{ "DeleteSubmission",
                //    LinkGenerator.GetUmbracoApiService<StoredDataController>(x =>
                //        x.DeleteSubmission(null)) },
                //{ "DownloadFile",
                //    LinkGenerator.GetUmbracoApiService<StoredDataDownloadController>(x =>
                //        x.DownloadFile(null)) },
                //{ "DownloadCsvExport",
                //    LinkGenerator.GetUmbracoApiService<StoredDataDownloadController>(x =>
                //        x.DownloadCsvExport(null)) },
                //{ "HasConfiguredRecaptcha",
                //    LinkGenerator.GetUmbracoApiService<ServerConfigurationController>(x =>
                //        x.HasConfiguredRecaptcha()) },
                //{ "EditLayoutBase", "/formulate/formulate/editLayout/" },
                //{ "EditValidationBase",
                //    "/formulate/formulate/editValidation/" },
                //{ "EditDataValueBase",
                //    "/formulate/formulate/editDataValue/" },

                { "Layout.RootId", LayoutConstants.RootId },
                { "Validation.RootId", ValidationConstants.RootId },
                { "DataValues.RootId", DataValuesConstants.RootId },
                { "forms.RootId", FormConstants.RootId },

                { "Folders.Save", LinkGenerator
                    .GetUmbracoApiService<FoldersController>(x => x.Save()) },

                { "GetHandlerDefinitions", LinkGenerator
                    .GetUmbracoApiService<FormsController>(x =>
                        x.GetHandlerDefinitions()) },
                { "GetFieldDefinitions", LinkGenerator
                    .GetUmbracoApiService<FormsController>(x =>
                        x.GetFieldDefinitions()) },
                { "GetTemplateDefinitions", LinkGenerator
                    .GetUmbracoApiService<FormsController>(x =>
                        x.GetTemplateDefinitions()) },
            };

            AddVariables<DataValuesController>(FormulateDataValuesTreeController.Constants.Alias, newEntries);
            AddVariables<FormsController>(FormulateFormsTreeController.Constants.Alias, newEntries);
            AddVariables<LayoutsController>(FormulateLayoutsTreeController.Constants.Alias, newEntries);
            AddVariables<ValidationsController>(FormulateValidationsTreeController.Constants.Alias, newEntries);
            AddVariables<ConfiguredFormsController>("configuredForms", newEntries);

            if (notification.ServerVariables.ContainsKey(key))
            {
                if (notification.ServerVariables[key] is IDictionary<string, object> existing)
                {
                    foreach (var item in newEntries)
                    {
                        existing[item.Key] = item.Value;
                    }
                }
            }
            else
            {
                notification.ServerVariables.Add(key, newEntries);
            }
        }

        private void AddVariables<TApiController>(string sectionAlias, IDictionary<string, object?> newEntries) where TApiController : FormulateBackOfficeEntityApiController 
        {
            newEntries.Add($"{sectionAlias}.Delete", LinkGenerator.GetUmbracoApiService<TApiController>(x => x.Delete()));
            newEntries.Add($"{sectionAlias}.Get", LinkGenerator.GetUmbracoApiService<TApiController>(x => x.Get()));
            newEntries.Add($"{sectionAlias}.GetCreateOptions", LinkGenerator.GetUmbracoApiService<TApiController>(x => x.GetCreateOptions()));
            newEntries.Add($"{sectionAlias}.GetScaffolding", LinkGenerator.GetUmbracoApiService<TApiController>(x => x.GetScaffolding()));
            newEntries.Add($"{sectionAlias}.Move", LinkGenerator.GetUmbracoApiService<TApiController>(x => x.Move()));
            newEntries.Add($"{sectionAlias}.Save", LinkGenerator.GetUmbracoApiService<TApiController>(x => x.Save()));
        }
    }
}