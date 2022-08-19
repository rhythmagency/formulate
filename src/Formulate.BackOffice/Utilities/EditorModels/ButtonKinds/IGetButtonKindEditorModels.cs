namespace Formulate.BackOffice.Utilities.EditorModels.Buttons
{
    using Formulate.BackOffice.EditorModels.ButtonKinds;
    using System.Collections.Generic;

    public interface IGetButtonKindEditorModels
    {
        IReadOnlyCollection<ButtonKindEditorModel> GetAll();
    }
}
