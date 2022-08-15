namespace Formulate.BackOffice.Utilities
{
    using Formulate.BackOffice.EditorModels;
    using Formulate.Core.Persistence;

    public interface IMapEditorModel
    {
        IEditorModel? MapTo(MapToEditorModelInput input);

        TEntity? MapFrom<TEditorModel, TEntity>(TEditorModel input) where TEditorModel : IEditorModel
                                                                    where TEntity : IPersistedEntity;
    }
}
