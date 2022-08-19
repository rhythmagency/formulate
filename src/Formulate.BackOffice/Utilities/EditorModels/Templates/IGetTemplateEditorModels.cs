namespace Formulate.BackOffice.Utilities.EditorModels.Templates
{
    using Formulate.BackOffice.EditorModels.Templates;
    using System.Collections.Generic;

    public interface IGetTemplateEditorModels
    {
        IReadOnlyCollection<TemplateEditorModel> GetAll();
    }
}
