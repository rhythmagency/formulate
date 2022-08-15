namespace Formulate.BackOffice.EditorModels.ConfiguredForm
{
    using Formulate.Core.Layouts;
    using System;
    using System.Runtime.Serialization;
    
    [DataContract]
    public sealed class ConfiguredFormLayoutEditorModel
    {
        public ConfiguredFormLayoutEditorModel()
        {
        }

        public ConfiguredFormLayoutEditorModel(PersistedLayout layout)
        {
            Id = layout.Id;
            Name = layout.Name;
        }

        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
