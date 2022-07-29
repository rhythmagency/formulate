namespace Formulate.BackOffice.EditorModels.Validation
{
    using Formulate.Core.Validations;
    using System;

    public sealed class ValidationEditorModel : EditorModel
    {
        public ValidationEditorModel(PersistedValidation entity) : base(entity)
        {
            Alias = entity.Alias;
            KindId = entity.KindId;
            Data = entity.Data;
        }

        public string Data { get; set; }
        
        public Guid KindId { get; set; }
    }
}
