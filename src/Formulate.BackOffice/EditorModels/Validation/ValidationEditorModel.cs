namespace Formulate.BackOffice.EditorModels.Validation
{
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.Validations;
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class ValidationEditorModel : EntityEditorModel
    {
        public ValidationEditorModel() : base()
        {
        }

        public ValidationEditorModel(PersistedValidation entity, bool isNew, bool isLegacy) : base(entity, isNew, isLegacy)
        {
            KindId = entity.KindId;
        }

        [DataMember(Name = "data")]
        public object Data { get; set; }

        [DataMember(Name = "kindId")]
        public Guid KindId { get; set; }

        public override EntityTypes EntityType => EntityTypes.Validation;

        [DataMember(Name = "directive")]
        public string Directive { get; set; }
    }
}
