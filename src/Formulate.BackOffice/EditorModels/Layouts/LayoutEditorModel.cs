namespace Formulate.BackOffice.EditorModels.Layouts
{
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.Layouts;
    using System;

    public sealed class LayoutEditorModel : EditorModel
    {
        public LayoutEditorModel(PersistedLayout entity, bool isNew) : base(entity, isNew)
        {
            KindId = entity.KindId;
            Data = entity.Data;
        }

        public object Data { get; set; }
        
        public Guid KindId { get; set; }

        public override EntityTypes EntityType => EntityTypes.Layout;

        public string Directive { get; internal set; }
    }
}
