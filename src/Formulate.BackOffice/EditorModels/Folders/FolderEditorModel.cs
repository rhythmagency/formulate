namespace Formulate.BackOffice.EditorModels.Folders
{
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.Folders;

    public sealed class FolderEditorModel : EditorModel
    {
        public FolderEditorModel(PersistedFolder entity) : base(entity)
        {
        }

        public override EntityTypes EntityType => EntityTypes.Folder;
    }
}
