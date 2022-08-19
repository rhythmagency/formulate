namespace Formulate.BackOffice.EditorModels.Folders
{
    using Formulate.Core.Folders;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class FolderEditorModel : EntityEditorModel
    {
        public FolderEditorModel() : base()
        {
        }

        public FolderEditorModel(PersistedFolder entity, bool isNew) : base(entity, isNew, false)
        {
        }

        public override EntityTypes EntityType => EntityTypes.Folder;
    }
}
