namespace Formulate.BackOffice.EditorModels.DataValues
{
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.DataValues;
    using System;

    public sealed class DataValuesEditorModel : EditorModel
    {
        public DataValuesEditorModel(PersistedDataValues entity) : base(entity)
        {
            Alias = entity.Alias;
            KindId = entity.KindId;
            Data = entity.Data;
        }

        public string Data { get; set; }
        
        public Guid KindId { get; set; }

        public override EntityTypes EntityType => EntityTypes.DataValues;
    }
}
