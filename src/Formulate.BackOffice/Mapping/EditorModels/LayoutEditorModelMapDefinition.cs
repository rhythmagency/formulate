namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels.Layouts;
    using Formulate.Core.Layouts;
    using Formulate.Core.Types;

    internal sealed class LayoutEditorModelMapDefinition : EditorModelMapDefinition<PersistedLayout, LayoutEditorModel>
    {
        private readonly LayoutDefinitionCollection _layoutDefinitions;

        public LayoutEditorModelMapDefinition(LayoutDefinitionCollection layoutDefinitions)
        {
            _layoutDefinitions = layoutDefinitions;
        }

        protected override LayoutEditorModel? Map(PersistedLayout entity, bool isNew)
        {
            var definition = _layoutDefinitions.FirstOrDefault(entity.KindId);

            if (definition is null)
            {
                return default;
            }

            return new LayoutEditorModel(entity, isNew)
            {
                Directive = definition.Directive,
                Data = definition.GetBackOfficeConfiguration(entity)
            };
        }
    }
}
