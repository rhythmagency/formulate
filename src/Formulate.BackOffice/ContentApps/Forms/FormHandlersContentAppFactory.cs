namespace Formulate.BackOffice.ContentApps.Forms
{
    using Formulate.BackOffice.EditorModels.Forms;
    using System.Collections.Generic;
    using Umbraco.Cms.Core.Models.ContentEditing;
    using Umbraco.Cms.Core.Models.Membership;

    public sealed class FormHandlersContentAppFactory : IContentAppFactory
    {
        public ContentApp? GetContentAppFor(object source, IEnumerable<IReadOnlyUserGroup> userGroups)
        {
            switch (source)
            {
                case FormEditorModel _:
                    return new ContentApp()
                    {
                        Name = "Handlers",
                        Alias = "Handlers",
                        Icon = "icon-handshake",
                        View = "/App_Plugins/Formulate/designers/forms/apps/form-handlers.app.html",
                        Weight = 2
                    };
            }

            return default;
        }
    }
}
