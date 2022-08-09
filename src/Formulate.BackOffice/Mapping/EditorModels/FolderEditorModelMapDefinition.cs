namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.Folders;
    using Formulate.Core.Folders;

    internal sealed class FolderEditorModelMapDefinition : EditorModelMapDefinition<PersistedFolder, FolderEditorModel>
    {
        protected override FolderEditorModel? Map(PersistedFolder entity, bool isNew)
        {
            return new FolderEditorModel(entity, isNew);
        }
    }
}
