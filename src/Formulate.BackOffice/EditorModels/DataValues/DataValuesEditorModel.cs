namespace Formulate.BackOffice.EditorModels.DataValues
{
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.DataValues;
    using System;

    public sealed class DataValuesEditorModel : EditorModel
    {
        public DataValuesEditorModel(PersistedDataValues entity, bool isNew) : base(entity, isNew)
        {
            Alias = entity.Alias;
            KindId = entity.KindId;
        }

        public object Data { get; set; }
        
        public Guid KindId { get; set; }

        public override EntityTypes EntityType => EntityTypes.DataValues;

        public string Directive { get; set; }

        public bool IsLegacy { get; set; }

    }
}
