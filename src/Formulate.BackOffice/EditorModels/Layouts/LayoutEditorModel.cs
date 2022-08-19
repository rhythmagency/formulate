namespace Formulate.BackOffice.EditorModels.Layouts
{
    using Formulate.Core.Layouts;
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class LayoutEditorModel : EntityEditorModel
    {
        public LayoutEditorModel() : base()
        {
        }

        public LayoutEditorModel(PersistedLayout entity, bool isNew, bool isLegacy) : base(entity, isNew, isLegacy)
        {
            KindId = entity.KindId;
            Data = entity.Data;
        }

        [DataMember(Name = "data")]
        public object Data { get; set; }

        [DataMember(Name = "kindId")]
        public Guid KindId { get; set; }

        public override EntityTypes EntityType => EntityTypes.Layout;

        [DataMember(Name = "directive")]
        public string Directive { get; set; }
    }
}
