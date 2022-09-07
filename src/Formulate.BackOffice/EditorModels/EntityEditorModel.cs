namespace Formulate.BackOffice.EditorModels
{
    using Formulate.BackOffice.Trees;
    using Formulate.Core.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public abstract class EntityEditorModel : IEntityEditorModel
    {
        public EntityEditorModel()
        {
        }

        public EntityEditorModel(IPersistedEntity entity, bool isNew, bool isLegacy)
        {
            Id = entity.Id;
            Path = entity.Path;
            Name = entity.Name;
            Alias = entity.Alias;
            TreePath = entity.TreeSafePath();
            IsNew = isNew;
            IsLegacy = isLegacy;
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

        [DataMember(Name = "isLegacy")]
        public bool IsLegacy { get; set; }
    }
}
