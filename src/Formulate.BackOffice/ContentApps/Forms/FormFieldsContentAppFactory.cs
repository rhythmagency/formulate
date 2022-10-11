namespace Formulate.BackOffice.ContentApps.Forms
{
    using Formulate.BackOffice.EditorModels.Forms;
    using System.Collections.Generic;
    using Umbraco.Cms.Core.Models.ContentEditing;
    using Umbraco.Cms.Core.Models.Membership;

    public sealed class FormFieldsContentAppFactory : IContentAppFactory
    {
        public ContentApp? GetContentAppFor(object source, IEnumerable<IReadOnlyUserGroup> userGroups)
        {
            switch (source)
            {
                case FormEditorModel:
                    return new ContentApp()
                    {
                        Name = "Form",
                        Alias = "Form",
                        Icon = "icon-formulate-form",
                        View = "/App_Plugins/Formulate/designers/forms/apps/form-fields.app.html",
                        Weight = 1,
                    };
            }

            return default;
        }
    }
}
