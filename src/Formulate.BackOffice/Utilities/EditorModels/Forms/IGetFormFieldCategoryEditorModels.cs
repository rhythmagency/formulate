namespace Formulate.BackOffice.Utilities.EditorModels.Forms
{
    using Formulate.BackOffice.EditorModels.Forms;
    using System.Collections.Generic;

    public interface IGetFormFieldCategoryEditorModels
    {
        IReadOnlyCollection<FormFieldCategoryEditorModel> GetAll();
    }
}
