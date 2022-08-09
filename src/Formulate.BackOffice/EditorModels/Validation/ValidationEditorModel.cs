namespace Formulate.BackOffice.EditorModels.Validation
{
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.Validations;
    using System;

    public sealed class ValidationEditorModel : EditorModel
    {
        public ValidationEditorModel(PersistedValidation entity, bool isNew) : base(entity, isNew)
        {
            Alias = entity.Alias;
            KindId = entity.KindId;
        }

        public object Data { get; set; }
        
        public Guid KindId { get; set; }

        public override EntityTypes EntityType => EntityTypes.Validation;

        public string Directive { get; set; }
    }
}
