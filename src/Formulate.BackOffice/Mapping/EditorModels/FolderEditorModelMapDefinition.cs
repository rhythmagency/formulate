namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.Folders;
    using Formulate.Core.Folders;
    using Umbraco.Cms.Core.Mapping;

    internal sealed class FolderEditorModelMapDefinition : EditorModelMapDefinition<PersistedFolder, FolderEditorModel>
    {
        protected override FolderEditorModel? MapToEditor(PersistedFolder entity, bool isNew)
        {
            return new FolderEditorModel(entity, isNew);
        }

        protected override PersistedFolder? MapToEntity(FolderEditorModel editorModel, MapperContext mapperContext)
        {
            return new PersistedFolder()
            {
                Id = editorModel.Id,
                Name = editorModel.Name,
                Path = editorModel.Path
            };
        }
    }
}
