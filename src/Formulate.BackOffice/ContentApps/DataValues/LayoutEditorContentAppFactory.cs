namespace Formulate.BackOffice.ContentApps.DataValues
{
    using Formulate.BackOffice.EditorModels.DataValues;
    using System.Collections.Generic;
    using Umbraco.Cms.Core.Models.ContentEditing;
    using Umbraco.Cms.Core.Models.Membership;

    public sealed class DataValuesEditorContentAppFactory : IContentAppFactory
    {
        public ContentApp? GetContentAppFor(object source, IEnumerable<IReadOnlyUserGroup> userGroups)
        {
            switch (source)
            {
                case DataValuesEditorModel:
                    return new ContentApp()
                    {
                        Name = "Content",
                        Alias = "Content",
                        Icon = "icon-edit",
                        View = "/App_Plugins/Formulate/designers/dataValues/apps/dataValues-editor.app.html",
                        Weight = 1,
                    };
            }

            return default;
        }
    }
}
