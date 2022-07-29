namespace Formulate.BackOffice.EditorModels.Layouts
{
    using Formulate.Core.Layouts;
    using System;

    public sealed class LayoutEditorModel : EditorModel
    {
        public LayoutEditorModel(PersistedLayout entity) : base(entity)
        {
            KindId = entity.KindId;
            Data = entity.Data;
        }

        public string Data { get; set; }
        
        public Guid KindId { get; set; }
    }
}
