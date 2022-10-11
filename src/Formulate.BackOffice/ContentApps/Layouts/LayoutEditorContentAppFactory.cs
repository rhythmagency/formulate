namespace Formulate.BackOffice.ContentApps.Layouts
{
    using Formulate.BackOffice.EditorModels.Layouts;
    using System.Collections.Generic;
    using Umbraco.Cms.Core.Models.ContentEditing;
    using Umbraco.Cms.Core.Models.Membership;

    public sealed class LayoutEditorContentAppFactory : IContentAppFactory
    {
        public ContentApp? GetContentAppFor(object source, IEnumerable<IReadOnlyUserGroup> userGroups)
        {
            switch (source)
            {
                case LayoutEditorModel:
                    return new ContentApp()
                    {
                        Name = "Content",
                        Alias = "Content",
                        Icon = "icon-edit",
                        View = "/App_Plugins/Formulate/designers/layouts/apps/layout-editor.app.html",
                        Weight = 1,
                    };
            }

            return default;
        }
    }
}
