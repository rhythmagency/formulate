namespace Formulate.BackOffice.EditorModels.ConfiguredForm
{
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.ConfiguredForms;
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class ConfiguredFormEditorModel : EditorModel
    {
        public ConfiguredFormEditorModel() : base()
        {
        }

        public ConfiguredFormEditorModel(PersistedConfiguredForm entity, bool isNew) : base(entity, isNew)
        {
            Alias = entity.Alias;
        }

        [DataMember(Name = "layoutId")]
        public Guid LayoutId { get; set; }

        [DataMember(Name = "layoutName")]
        public string LayoutName { get; set; }

        [DataMember(Name = "templateId")]
        public Guid TemplateId { get; set; }

        [DataMember(Name = "templateName")]
        public string TemplateName { get; set; }

        public override EntityTypes EntityType => EntityTypes.ConfiguredForm;
    }
}
