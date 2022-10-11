namespace Formulate.BackOffice.EditorModels
{
    using Formulate.Core.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Umbraco.Cms.Core.Models.ContentEditing;

    [DataContract]
    public abstract class ItemEditorModel : IEditorModel
    {
        public ItemEditorModel()
        {
        }

        public ItemEditorModel(IPersistedItem item, bool isNew, bool isLegacy)
        {
            Id = item.Id;
            Alias = item.Alias;
            Name = item.Alias;
            IsNew = isNew;
            isLegacy = isLegacy;
        }


        [DataMember(Name = "id")]
        public Guid Id { get; set; }


        [DataMember(Name = "name")]
        public string Name { get; set; }


        [DataMember(Name = "alias")]
        public string Alias { get; set; }

        [DataMember(Name = "isNew")]
        public bool IsNew { get; set; }

        [DataMember(Name = "isLegacy")]
        public bool IsLegacy { get; set; }

        [DataMember(Name = "apps")]
        public IReadOnlyCollection<ContentApp> Apps { get; set; } = Array.Empty<ContentApp>();
    }
}
