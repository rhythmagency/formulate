namespace Formulate.BackOffice.EditorModels.Forms
{
    // Namespaces.
    using Core.Forms;
    using Formulate.BackOffice.Persistence;
    using System;

    /// <summary>
    /// A view model that supplements the <see cref="PersistedForm"/> class
    /// with additional data that is not persisted.
    /// </summary>
    public sealed class FormEditorModel : EditorModel
    {
        public FormEditorModel(PersistedForm entity) : base(entity)
        {
            Alias = entity.Alias;
        } 

        /// <inheritdoc cref="PersistedForm.Fields"/>
        public FormFieldEditorModel[] Fields { get; set; } = Array.Empty<FormFieldEditorModel>();

        /// <inheritdoc cref="PersistedForm.Handlers"/>
        public FormHandlerEditorModel[] Handlers { get; set; } = Array.Empty<FormHandlerEditorModel>();

        public override EntityTypes EntityType => EntityTypes.Form;
    }
}