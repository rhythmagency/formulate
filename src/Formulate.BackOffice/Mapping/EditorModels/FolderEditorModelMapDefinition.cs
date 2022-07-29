namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.Folders;
    using Formulate.Core.Folders;
    using Umbraco.Cms.Core.Mapping;

    internal sealed class FolderEditorModelMapDefinition : EditorModelMapDefinition<PersistedFolder, FolderEditorModel>
    {
        protected override FolderEditorModel Map(PersistedFolder entity, MapperContext mapperContext)
        {
            return new FolderEditorModel(entity);
        }
    }
}
