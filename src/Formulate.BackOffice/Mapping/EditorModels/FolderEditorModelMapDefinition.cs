namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.Folders;
    using Formulate.Core.Folders;
    using Umbraco.Cms.Core.ContentApps;
    using Umbraco.Cms.Core.Mapping;

    internal sealed class FolderEditorModelMapDefinition : EntityEditorModelMapDefinition<PersistedFolder, FolderEditorModel>
    {
        public FolderEditorModelMapDefinition(ContentAppFactoryCollection contentAppDefinitions) : base(contentAppDefinitions)
        {
        }

        public override FolderEditorModel? MapToEditor(PersistedFolder entity, MapperContext mapperContext)
        {
            var editorModel = new FolderEditorModel(entity, mapperContext.IsNew());
            editorModel.Apps = MapApps(editorModel);

            return editorModel;
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
