namespace Formulate.BackOffice.EditorModels.ConfiguredForm
{
    using Formulate.Core.ConfiguredForms;
    using System;

    public sealed class ConfiguredFormEditorModel : EditorModel
    {
        public ConfiguredFormEditorModel(PersistedConfiguredForm entity) : base(entity)
        {
            Alias = entity.Alias;
        }

        public Guid LayoutId { get; internal set; }
        public string LayoutName { get; internal set; }
        public Guid TemplateId { get; internal set; }
        public string TemplateName { get; internal set; }
    }
}
