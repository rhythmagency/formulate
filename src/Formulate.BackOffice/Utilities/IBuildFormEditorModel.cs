namespace Formulate.BackOffice.Utilities
{
    using Formulate.BackOffice.EditorModels;
    using Formulate.Core.Persistence;

    public interface IBuildEditorModel
    {
        IEditorModel? Build(BuildEditorModelInput input);
    }
}
