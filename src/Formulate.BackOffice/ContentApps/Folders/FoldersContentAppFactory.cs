namespace Formulate.BackOffice.ContentApps.Folders
{
    using Formulate.BackOffice.EditorModels.Folders;
    using System.Collections.Generic;
    using Umbraco.Cms.Core.Models.ContentEditing;
    using Umbraco.Cms.Core.Models.Membership;

    public sealed class FoldersContentAppFactory : IContentAppFactory
    {
        public ContentApp? GetContentAppFor(object source, IEnumerable<IReadOnlyUserGroup> userGroups)
        {
            switch (source)
            {
                case FolderEditorModel:
                    return new ContentApp()
                    {
                        Name = "Content",
                        Alias = "Content",
                        Icon = "icon-folder",
                        View = "/App_Plugins/Formulate/designers/folders/apps/folder-content.app.html",
                        Weight = 1,
                    };
            }

            return default;
        }
    }
}
