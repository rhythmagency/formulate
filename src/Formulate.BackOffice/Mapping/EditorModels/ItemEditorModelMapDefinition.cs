﻿namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels;
    using Formulate.Core.Persistence;
    using Umbraco.Cms.Core.Mapping;

    public abstract class ItemEditorModelMapDefinition<TPersistedItem, TEditorModel> : IMapDefinition
        where TPersistedItem : class, IPersistedItem
        where TEditorModel : class, IEditorModel
    {
        public void DefineMaps(IUmbracoMapper mapper)
        {
            mapper.Define<TPersistedItem, IEditorModel?>((entity, mapperContext) => MapToEditor(entity, mapperContext));
            mapper.Define<TPersistedItem, TEditorModel?>((entity, mapperContext) => MapToEditor(entity, mapperContext));

            mapper.Define<TEditorModel, TPersistedItem?>((editorModel, mapperContext) => MapToItem(editorModel, mapperContext));
        }

        public abstract TEditorModel? MapToEditor(TPersistedItem entity, MapperContext mapperContext);

        public abstract TPersistedItem? MapToItem(TEditorModel editorModel, MapperContext mapperContext);
    }
}