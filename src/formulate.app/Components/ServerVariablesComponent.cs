namespace formulate.app.Components
{
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using formulate.app.Controllers;

    using Umbraco.Core.Composing;
    using Umbraco.Web;
    using Umbraco.Web.JavaScript;

    using DataValueConstants = Constants.Trees.DataValues;
    using FormConstants = Constants.Trees.Forms;
    using LayoutConstants = Constants.Trees.Layouts;
    using MetaConstants = meta.Constants;
    using ValidationConstants = Constants.Trees.Validations;

    /// <summary>
    /// The server variables component.
    /// </summary>
    internal sealed class ServerVariablesComponent : IComponent
    {
        /// <summary>
        /// Assign Formulate server variables.
        /// </summary>
        public void Initialize()
        {
            ServerVariablesParser.Parsing += AddServerVariables;
        }

        /// <summary>
        /// Run during component termination.
        /// </summary>
        public void Terminate()
        {
        }

        /// <summary>
        /// Adds server variables for use by the JavaScript.
        /// </summary>
        private void AddServerVariables(object sender, Dictionary<string, object> e)
        {
            // Variables.
            var httpContext = new HttpContextWrapper(HttpContext.Current);
            var routeData = new RouteData();
            var requestContext = new RequestContext(httpContext, routeData);
            var helper = new UrlHelper(requestContext);
            var key = MetaConstants.PackageNameCamelCase;


            // Add server variables.
            var newEntries = new Dictionary<string, string>()
            {
                { "DeleteForm",
                    helper.GetUmbracoApiService<FormsController>(x =>
                        x.DeleteForm(null)) },
                { "PersistForm",
                    helper.GetUmbracoApiService<FormsController>(x =>
                        x.PersistForm(null)) },
                { "GetFormInfo",
                    helper.GetUmbracoApiService<FormsController>(x =>
                        x.GetFormInfo(null)) },
                { "MoveForm",
                    helper.GetUmbracoApiService<FormsController>(x =>
                        x.MoveForm(null)) },
                { "DeleteConfiguredForm",
                    helper.GetUmbracoApiService<ConfiguredFormsController>(x =>
                        x.DeleteConfiguredForm(null)) },
                { "PersistConfiguredForm",
                    helper.GetUmbracoApiService<ConfiguredFormsController>(x =>
                        x.PersistConfiguredForm(null)) },
                { "GetConfiguredFormInfo",
                    helper.GetUmbracoApiService<ConfiguredFormsContentController>(x =>
                        x.GetConfiguredFormInfo(null)) },
                { "DeleteLayout",
                    helper.GetUmbracoApiService<LayoutsController>(x =>
                        x.DeleteLayout(null)) },
                { "PersistLayout",
                    helper.GetUmbracoApiService<LayoutsController>(x =>
                        x.PersistLayout(null)) },
                { "GetLayoutInfo",
                    helper.GetUmbracoApiService<LayoutsController>(x =>
                        x.GetLayoutInfo(null)) },
                { "GetLayoutKinds",
                    helper.GetUmbracoApiService<LayoutsController>(x =>
                        x.GetLayoutKinds()) },
                { "MoveLayout",
                    helper.GetUmbracoApiService<LayoutsController>(x =>
                        x.MoveLayout(null)) },
                { "DeleteValidation",
                    helper.GetUmbracoApiService<ValidationsController>(x =>
                        x.DeleteValidation(null)) },
                { "PersistValidation",
                    helper.GetUmbracoApiService<ValidationsController>(x =>
                        x.PersistValidation(null)) },
                { "GetValidationInfo",
                    helper.GetUmbracoApiService<ValidationsController>(x =>
                        x.GetValidationInfo(null)) },
                { "GetValidationsInfo",
                    helper.GetUmbracoApiService<ValidationsController>(x =>
                        x.GetValidationsInfo(null)) },
                { "GetValidationKinds",
                    helper.GetUmbracoApiService<ValidationsController>(x =>
                        x.GetValidationKinds()) },
                { "MoveValidation",
                    helper.GetUmbracoApiService<ValidationsController>(x =>
                        x.MoveValidation(null)) },
                { "DeleteDataValue",
                    helper.GetUmbracoApiService<DataValuesController>(x =>
                        x.DeleteDataValue(null)) },
                { "PersistDataValue",
                    helper.GetUmbracoApiService<DataValuesController>(x =>
                        x.PersistDataValue(null)) },
                { "GetDataValueInfo",
                    helper.GetUmbracoApiService<DataValuesController>(x =>
                        x.GetDataValueInfo(null)) },
                { "GetDataValuesInfo",
                    helper.GetUmbracoApiService<DataValuesController>(x =>
                        x.GetDataValuesInfo(null)) },
                { "GetDataValueKinds",
                    helper.GetUmbracoApiService<DataValuesController>(x =>
                        x.GetDataValueKinds()) },
                { "GetDataValueSuppliers",
                    helper.GetUmbracoApiService<DataValuesController>(x =>
                        x.GetDataValueSuppliers()) },
                { "MoveDataValue",
                    helper.GetUmbracoApiService<DataValuesController>(x =>
                        x.MoveDataValue(null)) },
                { "PersistFolder",
                    helper.GetUmbracoApiService<FoldersController>(x =>
                        x.PersistFolder(null)) },
                { "GetFolderInfo",
                    helper.GetUmbracoApiService<FoldersController>(x =>
                        x.GetFolderInfo(null)) },
                { "MoveFolder",
                    helper.GetUmbracoApiService<FoldersController>(x =>
                        x.MoveFolder(null)) },
                { "DeleteFolder",
                    helper.GetUmbracoApiService<FoldersController>(x =>
                        x.DeleteFolder(null)) },
                { "GetFieldTypes",
                    helper.GetUmbracoApiService<FieldsController>(x =>
                        x.GetFieldTypes()) },
                { "GetButtonKinds",
                    helper.GetUmbracoApiService<FieldsController>(x =>
                        x.GetButtonKinds()) },
                { "GetFieldCategories",
                    helper.GetUmbracoApiService<FieldsController>(x =>
                        x.GetFieldCategories()) },
                { "GetHandlerTypes",
                    helper.GetUmbracoApiService<HandlersController>(x =>
                        x.GetHandlerTypes()) },
                { "GetResultHandlers",
                    helper.GetUmbracoApiService<HandlersController>(x =>
                        x.GetResultHandlers()) },
                { "GetTemplates",
                    helper.GetUmbracoApiService<TemplatesController>(x =>
                        x.GetTemplates()) },
                { "GetEntityChildren",
                    helper.GetUmbracoApiService<EntitiesContentController>(x =>
                        x.GetEntityChildren(null)) },
                { "GetEntity",
                    helper.GetUmbracoApiService<EntitiesController>(x =>
                        x.GetEntity(null)) },
                { "GetSubmissions",
                    helper.GetUmbracoApiService<StoredDataController>(x =>
                        x.GetSubmissions(null)) },
                { "DeleteSubmission",
                    helper.GetUmbracoApiService<StoredDataController>(x =>
                        x.DeleteSubmission(null)) },
                { "DownloadFile",
                    helper.GetUmbracoApiService<StoredDataDownloadController>(x =>
                        x.DownloadFile(null)) },
                { "DownloadCsvExport",
                    helper.GetUmbracoApiService<StoredDataDownloadController>(x =>
                        x.DownloadCsvExport(null)) },
                { "HasConfiguredRecaptcha",
                    helper.GetUmbracoApiService<ServerConfigurationController>(x =>
                        x.HasConfiguredRecaptcha()) },
                { "EditLayoutBase", "/formulate/formulate/editLayout/" },
                { "EditValidationBase",
                    "/formulate/formulate/editValidation/" },
                { "EditDataValueBase",
                    "/formulate/formulate/editDataValue/" },
                { "Layout.RootId", LayoutConstants.Id },
                { "Validation.RootId", ValidationConstants.Id },
                { "DataValue.RootId", DataValueConstants.Id },
                { "Form.RootId", FormConstants.Id }
            };
            if (e.ContainsKey(key))
            {
                var existing = e[key] as Dictionary<string, string>;
                foreach (var item in newEntries)
                {
                    existing[item.Key] = item.Value;
                }
            }
            else
            {
                e.Add(key, newEntries);
            }

        }

    }
}
