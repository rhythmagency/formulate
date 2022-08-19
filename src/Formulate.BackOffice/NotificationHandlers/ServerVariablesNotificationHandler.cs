﻿namespace Formulate.BackOffice.NotificationHandlers
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
    using Core.DataValues;
    using Core.Forms;
    using Core.Layouts;
    using Core.Validations;
    using Formulate.BackOffice.Controllers.Templates;
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
                { "ButtonKinds.GetAll",
                    LinkGenerator.GetUmbracoApiService<ButtonKindsController>(x =>
                        x.GetAll()) },

                { "Layout.RootId", LayoutConstants.RootId },
                { "Validation.RootId", ValidationConstants.RootId },
                { "DataValues.RootId", DataValuesConstants.RootId },
                { "forms.RootId", FormConstants.RootId },

                { "Folders.Save", LinkGenerator
                    .GetUmbracoApiService<FoldersController>(x => x.Save()) },

                { "GetHandlerDefinitions", LinkGenerator
                    .GetUmbracoApiService<FormHandlersController>(x =>
                        x.GetDefinitions()) },

                { "FormFields.GetCategories", LinkGenerator
                    .GetUmbracoApiService<FormFieldsController>(x =>
                        x.GetCategories()) },

                { "GetFieldDefinitions", LinkGenerator
                    .GetUmbracoApiService<FormFieldsController>(x =>
                        x.GetDefinitions()) },

                { "FormFields.GetScaffolding", LinkGenerator
                    .GetUmbracoApiService<FormFieldsController>(x =>
                        x.GetScaffolding()) },

                { "FormHandlers.GetScaffolding", LinkGenerator
                    .GetUmbracoApiService<FormHandlersController>(x =>
                        x.GetScaffolding()) },

                { "Templates.GetAll", LinkGenerator
                    .GetUmbracoApiService<TemplatesController>(x =>
                        x.GetAll()) },
            };

            AddVariables<DataValuesController>(Constants.Trees.DataValues, newEntries);
            AddVariables<FormsController>(Constants.Trees.Forms, newEntries);
            AddVariables<LayoutsController>(Constants.Trees.Layouts, newEntries);
            AddVariables<ValidationsController>(Constants.Trees.Validations, newEntries);
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