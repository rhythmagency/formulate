namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.Layouts;
    using Formulate.Core.Layouts;

    internal sealed class LayoutEditorModelMapDefinition : EditorModelMapDefinition<PersistedLayout, LayoutEditorModel>
    {
        protected override LayoutEditorModel Map(PersistedLayout entity, bool isNew)
        {
            return new LayoutEditorModel(entity, isNew);
        }
    }
}
