namespace Formulate.BackOffice.ContentApps.Validations
{
    using Formulate.BackOffice.EditorModels.Validation;
    using System.Collections.Generic;
    using Umbraco.Cms.Core.Models.ContentEditing;
    using Umbraco.Cms.Core.Models.Membership;

    public sealed class ValidationEditorContentAppFactory : IContentAppFactory
    {
        public ContentApp? GetContentAppFor(object source, IEnumerable<IReadOnlyUserGroup> userGroups)
        {
            switch (source)
            {
                case ValidationEditorModel:
                    return new ContentApp()
                    {
                        Name = "Content",
                        Alias = "Content",
                        Icon = "icon-edit",
                        View = "/App_Plugins/Formulate/designers/validations/apps/validation-editor.app.html",
                        Weight = 1,
                    };
            }

            return default;
        }
    }
}
