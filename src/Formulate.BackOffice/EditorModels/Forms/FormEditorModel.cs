namespace Formulate.BackOffice.EditorModels.Forms
{
    // Namespaces.
    using Core.Forms;
    using Formulate.BackOffice.Persistence;
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// A view model that supplements the <see cref="PersistedForm"/> class
    /// with additional data that is not persisted.
    /// </summary>
    [DataContract]
    public sealed class FormEditorModel : EntityEditorModel
    {
        public FormEditorModel() : base()
        {
        }

        public FormEditorModel(PersistedForm entity, bool isNew) : base(entity, isNew, false)
        {
        }

        /// <inheritdoc cref="PersistedForm.Fields"/>
        [DataMember(Name = "fields")]
        public FormFieldEditorModel[] Fields { get; set; } = Array.Empty<FormFieldEditorModel>();

        /// <inheritdoc cref="PersistedForm.Handlers"/>
        [DataMember(Name = "handlers")]
        public FormHandlerEditorModel[] Handlers { get; set; } = Array.Empty<FormHandlerEditorModel>();

        public override EntityTypes EntityType => EntityTypes.Form;
    }
}