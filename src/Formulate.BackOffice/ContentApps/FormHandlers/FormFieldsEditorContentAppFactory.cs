namespace Formulate.BackOffice.ContentApps.FormHandlers
{
    using Formulate.BackOffice.EditorModels.Forms;
    using System.Collections.Generic;
    using Umbraco.Cms.Core.Models.ContentEditing;
    using Umbraco.Cms.Core.Models.Membership;

    public sealed class FormHandlersEditorContentAppFactory : IContentAppFactory
    {
        public ContentApp? GetContentAppFor(object source, IEnumerable<IReadOnlyUserGroup> userGroups)
        {
            switch (source)
            {
                case FormHandlerEditorModel:
                    return new ContentApp()
                    {
                        Name = "Content",
                        Alias = "Content",
                        Icon = "icon-edit",
                        View = "/App_Plugins/Formulate/dialogs/form-handler/apps/form-handler-editor.app.html",
                        Weight = 1,
                    };
            }

            return default;
        }
    }
}
