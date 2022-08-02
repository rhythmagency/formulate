namespace Formulate.BackOffice.EditorModels.Folders
{
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.Folders;

    public sealed class FolderEditorModel : EditorModel
    {
        public FolderEditorModel(PersistedFolder entity, bool isNew) : base(entity, isNew)
        {
        }

        public override EntityTypes EntityType => EntityTypes.Folder;
    }
}
