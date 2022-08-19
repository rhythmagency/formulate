namespace Formulate.BackOffice.EditorModels.DataValues
{
    using Formulate.Core.DataValues;
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class DataValuesEditorModel : EntityEditorModel
    {
        public DataValuesEditorModel() : base()
        {
        }

        public DataValuesEditorModel(PersistedDataValues entity, bool isNew, bool isLegacy) : base(entity, isNew, isLegacy)
        {
            KindId = entity.KindId;
        }

        [DataMember(Name = "data")]
        public object Data { get; set; }

        [DataMember(Name = "kindId")]
        public Guid KindId { get; set; }

        public override EntityTypes EntityType => EntityTypes.DataValues;

        [DataMember(Name = "directive")]
        public string Directive { get; set; }
    }
}
