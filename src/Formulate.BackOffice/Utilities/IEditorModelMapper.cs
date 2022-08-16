namespace Formulate.BackOffice.Utilities
{
    using Formulate.BackOffice.EditorModels;
    using Formulate.Core.Persistence;

    public interface IEditorModelMapper
    {
        IEditorModel? MapToEditor(MapToEditorModelInput input);

        TEntity? MapToEntity<TEditorModel, TEntity>(TEditorModel input) 
            where TEditorModel : IEditorModel
            where TEntity : IPersistedEntity;

        TItem? MapToItem<TEditorModel, TItem>(TEditorModel input) 
            where TEditorModel : IEditorModel
            where TItem : IPersistedItem;
    }
}
