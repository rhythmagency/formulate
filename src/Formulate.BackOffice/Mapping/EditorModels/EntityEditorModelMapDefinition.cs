namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Formulate.BackOffice.EditorModels;
    using Formulate.Core.Persistence;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Cms.Core.ContentApps;
    using Umbraco.Cms.Core.Mapping;
    using Umbraco.Cms.Core.Models.ContentEditing;

    public abstract class EntityEditorModelMapDefinition<TPersistedEntity, TEditorModel> : IMapDefinition
        where TPersistedEntity : IPersistedEntity
        where TEditorModel : IEditorModel
    {
        private readonly ContentAppFactoryCollection _contentAppDefinitions;

        public EntityEditorModelMapDefinition(ContentAppFactoryCollection contentAppDefinitions)
        {
            _contentAppDefinitions = contentAppDefinitions;
        }

        public void DefineMaps(IUmbracoMapper mapper)
        {
            mapper.Define<TPersistedEntity, IEditorModel?>((entity, mapperContext) => MapToEditor(entity, mapperContext));

            mapper.Define<TEditorModel, TPersistedEntity?>((editorModel, mapperContext) => MapToEntity(editorModel, mapperContext));
        }

        public abstract TPersistedEntity? MapToEntity(TEditorModel editorModel, MapperContext mapperContext);

        public abstract TEditorModel? MapToEditor(TPersistedEntity entity, MapperContext mapperContext);

        protected virtual IReadOnlyCollection<ContentApp> MapApps(TEditorModel editorModel)
        {
            var apps = _contentAppDefinitions.GetContentAppsFor(editorModel).OrderBy(x => x.Weight).ToArray();
            var processedApps = new List<ContentApp>();
            var hasProcessedFirstApp = false;

            foreach (var app in apps)
            {
                if (hasProcessedFirstApp)
                {
                    app.Active = false;
                }
                else
                {
                    app.Active = true;
                    hasProcessedFirstApp = true;
                }

                processedApps.Add(app);
            }

            return processedApps.ToArray();
        }
    }
}
