namespace Formulate.BackOffice.ViewModels.Forms
{
    // Namespaces.
    using Core.FormFields;
    using Core.FormHandlers;
    using Core.Forms;
    using Core.Persistence;
    using Core.Validations;
    using System;
    using System.Linq;

    /// <summary>
    /// A view model that supplements the <see cref="PersistedForm"/> class
    /// with additional data that is not persisted.
    /// </summary>
    public sealed class FormEditorModel
    {
        /// <inheritdoc cref="IPersistedEntity.Id"/>
        public Guid Id { get; set; }

        /// <inheritdoc cref="IPersistedEntity.Path"/>
        public Guid[] Path { get; set; }

        /// <inheritdoc cref="IPersistedEntity.Name"/>
        public string Name { get; set; }

        /// <inheritdoc cref="PersistedForm.Alias"/>
        public string Alias { get; set; }

        /// <inheritdoc cref="PersistedForm.Fields"/>
        public FormFieldEditorModel[] Fields { get; set; }

        /// <inheritdoc cref="PersistedForm.Handlers"/>
        public FormHandlerEditorModel[] Handlers { get; set; }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="source">
        /// The persisted form to copy.
        /// </param>
        public FormEditorModel(PersistedForm source,
            IFormHandlerFactory formHandlerFactory,
            IFormFieldFactory formFieldFactory)
        {
            Id = source.Id;
            Path = source.Path;
            Name = source.Name;
            Alias = source.Alias;
            Fields = source.Fields
                ?.Select(x => new
                {
                    Original = x,
                    Mapped = formFieldFactory.Create(x) as FormField,
                })
                ?.Where(x => x.Mapped != null)
                ?.Select(x => new FormFieldEditorModel()
                {
                    Alias = x.Original.Alias,
                    Category = x.Original.Category,
                    Directive = x.Mapped.Directive,
                    Configuration = x.Mapped.BackOfficeConfiguration,
                    Icon = x.Mapped.Icon,
                    Id = x.Original.Id,
                    Name = x.Original.Name,
                    KindId = x.Original.KindId,
                    Label = x.Original.Label,
                    Validations = x.Mapped.Validations
                        .Select(y =>
                        {
                            return new FormFieldValidationEditorModel()
                            {
                                Configuration = (y as Validation).BackOfficeConfiguration,
                                Id = y.Id,
                                Name = y.Name,
                            };
                        })
                        .ToArray(),
                })
                ?.ToArray();
            Handlers = source.Handlers
                ?.Select(x => new
                {
                    Original = x,
                    Mapped = formHandlerFactory.Create(x) as FormHandler,
                })
                .Select(x => new FormHandlerEditorModel
                {
                    Alias = x.Original.Alias,
                    Enabled = x.Original.Enabled,
                    Icon = x.Mapped.Icon,
                    Id = x.Original.Id,
                    KindId = x.Original.KindId,
                    Name = x.Original.Name,
                    Configuration = x.Mapped.BackOfficeConfiguration,
                    Directive = x.Mapped.Directive,
                })
                ?.ToArray();
        }
    }
}