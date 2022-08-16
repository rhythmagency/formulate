namespace Formulate.BackOffice.EditorModels
{
    using Formulate.Core.Persistence;
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public abstract class ItemEditorModel : IEditorModel
    {
        public ItemEditorModel()
        {
        }

        public ItemEditorModel(IPersistedItem item, bool isNew)
        {
            Id = item.Id;
            Alias = item.Alias;
            Name = item.Alias;
            IsNew = isNew;
        }


        [DataMember(Name = "id")]
        public Guid Id { get; set; }


        [DataMember(Name = "name")]
        public string Name { get; set; }


        [DataMember(Name = "alias")]
        public string Alias { get; set; }

        [DataMember(Name = "isNew")]
        public bool IsNew { get; set; }
    }
}
