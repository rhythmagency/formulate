namespace Formulate.BackOffice.EditorModels.ConfiguredForm
{
    using Formulate.Core.ConfiguredForms;
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class ConfiguredFormEditorModel : EntityEditorModel
    {
        public ConfiguredFormEditorModel() : base()
        {
        }

        public ConfiguredFormEditorModel(PersistedConfiguredForm entity, bool isNew) : base(entity, isNew, false)
        {
        }

        [DataMember(Name = "layout")]
        public ConfiguredFormLayoutEditorModel Layout { get; set; }

        [DataMember(Name = "templateId")]
        public Guid TemplateId { get; set; }

        public override EntityTypes EntityType => EntityTypes.ConfiguredForm;
    }
}
