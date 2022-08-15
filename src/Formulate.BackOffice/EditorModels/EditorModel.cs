namespace Formulate.BackOffice.EditorModels
{
    using Formulate.BackOffice.Persistence;
    using Formulate.BackOffice.Trees;
    using Formulate.Core.Persistence;
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public abstract class EditorModel : IEditorModel
    {
        public EditorModel()
        {
        }

        public EditorModel(IPersistedEntity entity, bool isNew)
        {
            Id = entity.Id;
            Path = entity.Path;
            Name = entity.Name;
            Alias = entity.Alias;
            TreePath = entity.TreeSafePath();
            IsNew = isNew;            
        }

        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "path")]
        public Guid[] Path { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "alias")]
        public string Alias { get; set; }

        [DataMember(Name = "entityType")]
        public abstract EntityTypes EntityType { get; }

        [DataMember(Name = "treePath")]
        public string[] TreePath { get; set; }

        [DataMember(Name = "isNew")]
        public bool IsNew { get; set; }
    }
}
