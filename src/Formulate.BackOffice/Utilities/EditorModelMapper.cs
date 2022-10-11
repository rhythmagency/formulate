namespace Formulate.BackOffice.Utilities
{
    using Formulate.BackOffice.EditorModels;
    using Formulate.BackOffice.Mapping.EditorModels;
    using Formulate.BackOffice.Notifications;
    using Formulate.Core.Persistence;
    using Umbraco.Cms.Core.Mapping;
    using Umbraco.Cms.Core.Scoping;

    internal sealed class EditorModelMapper : IEditorModelMapper
    {
        private readonly IUmbracoMapper _umbracoMapper;

        private readonly ICoreScopeProvider _coreScopeProvider;

        public EditorModelMapper(IUmbracoMapper umbracoMapper, ICoreScopeProvider coreScopeProvider)
        {
            _umbracoMapper = umbracoMapper;
            _coreScopeProvider = coreScopeProvider;
        }

        public IEditorModel? MapToEditor(MapToEditorModelInput input)
        {
            if (input.Item is null)
            {
                return default;
            }

            var editorModel = _umbracoMapper.Map<IEditorModel>(input.Item, (context) => { context.SetIsNew(input.IsNew); });

            var editorModelPostNotification = NotifySendEditorModel(editorModel);

            return editorModelPostNotification;
        }

        private IEditorModel? NotifySendEditorModel(IEditorModel? editorModel)
        {
            if (editorModel is EntityEditorModel entityEditorModel == false)
            {
                return editorModel;
            }

            using (var scope = _coreScopeProvider.CreateCoreScope())
            {
                var notification = new SendingEditorModelNotification(entityEditorModel);

                scope.Notifications.Publish(notification);
                scope.Complete();

                return notification.EditorModel;
            }
        }

        public TEntity? MapToEntity<TEditorModel, TEntity>(TEditorModel input)
            where TEditorModel : IEditorModel
            where TEntity : IPersistedEntity
        {
            if (input is null)
            {
                return default;
            }

            return _umbracoMapper.Map<TEditorModel, TEntity>(input);
        }

        public TItem? MapToItem<TEditorModel, TItem>(TEditorModel input)
            where TEditorModel : IEditorModel
            where TItem : IPersistedItem
        {
            if (input is null)
            {
                return default;
            }

            return _umbracoMapper.Map<TEditorModel, TItem>(input);
        }
    }
}
