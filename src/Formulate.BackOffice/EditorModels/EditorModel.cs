namespace Formulate.BackOffice.EditorModels
{
    using Formulate.BackOffice.Persistence;
    using Formulate.BackOffice.Trees;
    using Formulate.Core.Persistence;
    using System;

    public abstract class EditorModel : IEditorModel
    {
        public EditorModel(IPersistedEntity entity, bool isNew)
        {
            Id = entity.Id;
            Path = entity.Path;
            Name = entity.Name;
            TreePath = entity.TreeSafePath();
            IsNew = isNew;            
        }

        public Guid Id { get; set; }

        public Guid[] Path { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public abstract EntityTypes EntityType { get; }

        public string[] TreePath { get; set; }

        public bool IsNew { get; set; }
    }
}
