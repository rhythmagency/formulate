using System;
using System.Collections.Generic;
using Formulate.BackOffice.Controllers;
using Formulate.BackOffice.Controllers.DataValues;
using Formulate.BackOffice.Controllers.Folders;
using Formulate.BackOffice.Controllers.Forms;
using Formulate.BackOffice.Controllers.Validations;
using Formulate.BackOffice.Trees;
using Formulate.Core.DataValues;
using Formulate.Core.Forms;
using Formulate.Core.Layouts;
using Formulate.Core.Validations;
using Microsoft.AspNetCore.Routing;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Extensions;

namespace Formulate.BackOffice.NotificationHandlers
{
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
            var newEntries = new Dictionary<string, object>()
            {
                //{ "DeleteForm", LinkGenerator.GetUmbracoApiService<FormsController>(x => x.DeleteForm(null)) },
                //{ "PersistForm",
                //    LinkGenerator.GetUmbracoApiService<FormsController>(x =>
                //        x.PersistForm(null)) },
                //{ "GetFormInfo",
                //    LinkGenerator.GetUmbracoApiService<FormsController>(x =>
                //        x.GetFormInfo(null)) },
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
                //{ "GetLayoutInfo",
                //    LinkGenerator.GetUmbracoApiService<LayoutsController>(x =>
                //        x.GetLayoutInfo(null)) },
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
                { "GetValidationKinds",
                    LinkGenerator.GetUmbracoApiService<ValidationDefinitionsController>(x =>
                        x.GetAll()) },
                //{ "MoveValidation",
                //    LinkGenerator.GetUmbracoApiService<ValidationsController>(x =>
                //        x.MoveValidation(null)) },
                //{ "DeleteDataValue",
                //    LinkGenerator.GetUmbracoApiService<DataValuesController>(x =>
                //        x.DeleteDataValue(null)) },
                //{ "PersistDataValue",
                //    LinkGenerator.GetUmbracoApiService<DataValuesController>(x =>
                //        x.PersistDataValue(null)) },
                //{ "GetDataValueInfo",
                //    LinkGenerator.GetUmbracoApiService<DataValuesController>(x =>
                //        x.GetDataValueInfo(null)) },
                //{ "GetDataValuesInfo",
                //    LinkGenerator.GetUmbracoApiService<DataValuesController>(x =>
                //        x.GetDataValuesInfo(null)) },
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
                //{ "GetFieldTypes",
                //    LinkGenerator.GetUmbracoApiService<FieldsController>(x =>
                //        x.GetFieldTypes()) },
                //{ "GetButtonKinds",
                //    LinkGenerator.GetUmbracoApiService<FieldsController>(x =>
                //        x.GetButtonKinds()) },
                //{ "GetFieldCategories",
                //    LinkGenerator.GetUmbracoApiService<FieldsController>(x =>
                //        x.GetFieldCategories()) },
                //{ "GetHandlerTypes",
                //    LinkGenerator.GetUmbracoApiService<HandlersController>(x =>
                //        x.GetHandlerTypes()) },
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
                { "Validation.RootId", ValidationConstant.RootId },
                { "DataValues.RootId", DataValuesConstants.RootId },
                { "forms.RootId", FormConstants.RootId },

                { "Folders.Save", LinkGenerator.GetUmbracoApiService<FoldersController>(x => x.Save()) },

                { "forms.Delete", LinkGenerator.GetUmbracoApiService<FormsController>(x => x.Delete()) },
                { "forms.Get", LinkGenerator.GetUmbracoApiService<FormsController>(x => x.Get()) },
                { "forms.GetCreateOptions", LinkGenerator.GetUmbracoApiService<FormsController>(x => x.GetCreateOptions()) },
                { "forms.GetScaffolding", LinkGenerator.GetUmbracoApiService<FormsController>(x => x.GetScaffolding()) },
                { "forms.Move", LinkGenerator.GetUmbracoApiService<FormsController>(x => x.Move()) },

                { $"{FormulateDataValuesTreeController.Constants.Alias}.Delete", LinkGenerator.GetUmbracoApiService<DataValuesController>(x => x.Delete()) },
                { $"{FormulateDataValuesTreeController.Constants.Alias}.Get", LinkGenerator.GetUmbracoApiService<DataValuesController>(x => x.Get()) },
                { $"{FormulateDataValuesTreeController.Constants.Alias}.GetCreateOptions", LinkGenerator.GetUmbracoApiService<DataValuesController>(x => x.GetCreateOptions()) },
                { $"{FormulateDataValuesTreeController.Constants.Alias}.GetScaffolding", LinkGenerator.GetUmbracoApiService<DataValuesController>(x => x.GetScaffolding()) },
                { $"{FormulateDataValuesTreeController.Constants.Alias}.Move", LinkGenerator.GetUmbracoApiService<DataValuesController>(x => x.Move()) },

                //{ "DuplicateForm",
                //    LinkGenerator.GetUmbracoApiService<FormsController>(x => x.DuplicateForm(null)) },
            };


            if (notification.ServerVariables.ContainsKey(key))
            {
                var existing = notification.ServerVariables[key] as Dictionary<string, object>;
                foreach (var item in newEntries)
                {
                    existing[item.Key] = item.Value;
                }
            }
            else
            {
                notification.ServerVariables.Add(key, newEntries);
            }
        }
    }
}
