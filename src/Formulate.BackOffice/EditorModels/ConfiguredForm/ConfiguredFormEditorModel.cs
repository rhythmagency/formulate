namespace Formulate.BackOffice.EditorModels.ConfiguredForm
{
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.ConfiguredForms;
    using System;

    public sealed class ConfiguredFormEditorModel : EditorModel
    {
        public ConfiguredFormEditorModel(PersistedConfiguredForm entity) : base(entity)
        {
            Alias = entity.Alias;
        }

        public Guid LayoutId { get; set; }
        
        public string LayoutName { get; set; }
        
        public Guid TemplateId { get; set; }
        
        public string TemplateName { get; set; }

        public override EntityTypes EntityType => EntityTypes.ConfiguredForm;
    }
}
