namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.Folders;
    using Formulate.Core.Folders;
    using Umbraco.Cms.Core.Mapping;

    internal sealed class FolderEditorModelMapDefinition : EntityEditorModelMapDefinition<PersistedFolder, FolderEditorModel>
    {
        public override FolderEditorModel? MapToEditor(PersistedFolder entity, MapperContext mapperContext)
        {
            return new FolderEditorModel(entity, mapperContext.IsNew());
        }

        public override PersistedFolder? MapToEntity(FolderEditorModel editorModel, MapperContext mapperContext)
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
