namespace Formulate.BackOffice.EditorModels.Validation
{
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.Validations;
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class ValidationEditorModel : EditorModel
    {
        public ValidationEditorModel() : base()
        {
        }

        public ValidationEditorModel(PersistedValidation entity, bool isNew) : base(entity, isNew)
        {
            Alias = entity.Alias;
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
