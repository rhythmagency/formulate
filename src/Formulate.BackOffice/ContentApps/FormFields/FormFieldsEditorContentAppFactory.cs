namespace Formulate.BackOffice.ContentApps.FormFields
{
    using Formulate.BackOffice.EditorModels.Forms;
    using System.Collections.Generic;
    using Umbraco.Cms.Core.Models.ContentEditing;
    using Umbraco.Cms.Core.Models.Membership;

    public sealed class FormFieldsEditorContentAppFactory : IContentAppFactory
    {
        public ContentApp? GetContentAppFor(object source, IEnumerable<IReadOnlyUserGroup> userGroups)
        {
            switch (source)
            {
                case FormFieldEditorModel:
                    return new ContentApp()
                    {
                        Name = "Content",
                        Alias = "Content",
                        Icon = "icon-edit",
                        View = "/App_Plugins/Formulate/dialogs/form-field/apps/form-field-editor.app.html",
                        Weight = 1,
                    };
            }

            return default;
        }
    }
}
