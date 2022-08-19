namespace Formulate.BackOffice.NotificationHandlers
{
    // Namespaces.
    using Controllers;
    using Controllers.ConfiguredForms;
    using Controllers.DataValues;
    using Controllers.Folders;
    using Controllers.FormFields;
    using Controllers.FormHandlers;
    using Controllers.Forms;
    using Controllers.Layouts;
    using Controllers.Validations;
    using Formulate.BackOffice.Controllers.Templates;
    using Microsoft.AspNetCore.Routing;
    using System.Collections.Generic;
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
                {
                    "ButtonKinds", new {
                        GetAll = LinkGenerator.GetUmbracoApiService<ButtonKindsController>(x =>
                            x.GetAll())
                    }
                },
                {
                    "Folders", new {
                        Save = LinkGenerator.GetUmbracoApiService<FoldersController>(x => x.Save())
                    }
                },
                {
                    "FormHandlers", new {
                        GetDefinitions = LinkGenerator.GetUmbracoApiService<FormHandlersController>(x => x.GetDefinitions()),
                        GetScaffolding = LinkGenerator.GetUmbracoApiService<FormHandlersController>(x => x.GetScaffolding()),
                    }
                },
                {
                    "FormFields", new {
                        GetCategories = LinkGenerator.GetUmbracoApiService<FormFieldsController>(x => x.GetCategories()),
                        GetDefinitions = LinkGenerator.GetUmbracoApiService<FormFieldsController>(x => x.GetDefinitions()),
                        GetScaffolding = LinkGenerator.GetUmbracoApiService<FormFieldsController>(x => x.GetScaffolding())
                    }
                },
                {
                    "Templates", new {
                        GetAll = LinkGenerator.GetUmbracoApiService<TemplatesController>(x => x.GetAll())
                    }
                },
                {
                    "ConfiguredForms", GetApiControllerVariables<ConfiguredFormsController>()
                },
                {
                    "DataValues", GetApiControllerVariables<DataValuesController>()
                },
                {
                    "Forms", GetApiControllerVariables<FormsController>()
                },
                {
                    "Layouts", GetApiControllerVariables<LayoutsController>()
                },
                {
                    "Validations", GetApiControllerVariables<ValidationsController>()
                }
            };

            if (notification.ServerVariables.ContainsKey(key))
            {
                if (notification.ServerVariables[key] is IDictionary<string, object> existing)
                {
                    foreach (var item in newEntries)
                    {
                        if (item.Value is not null)
                        {
                            existing[item.Key] = item.Value;
                        }
                    }
                }
            }
            else
            {
                notification.ServerVariables.Add(key, newEntries);
            }
        }

        /// <summary>
        /// Gets the standard variables for a controller inheriting from <typeparamref name="TApiController"/>.
        /// </summary>
        /// <typeparam name="TApiController">The type of the API controller.</typeparam>
        /// <returns>An anonymous object containing all expected API endpoints.</returns>
        private object GetApiControllerVariables<TApiController>() where TApiController : FormulateBackOfficeEntityApiController
        {
            return new {
                Delete = LinkGenerator.GetUmbracoApiService<TApiController>(x => x.Delete()),
                Get = LinkGenerator.GetUmbracoApiService<TApiController>(x => x.Get()),
                GetCreateOptions = LinkGenerator.GetUmbracoApiService<TApiController>(x => x.GetCreateOptions()),
                GetScaffolding = LinkGenerator.GetUmbracoApiService<TApiController>(x => x.GetScaffolding()),
                Move = LinkGenerator.GetUmbracoApiService<TApiController>(x => x.Move()),
                Save = LinkGenerator.GetUmbracoApiService<TApiController>(x => x.Save())
            };
        }
    }
}
