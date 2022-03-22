namespace Formulate.BackOffice.ViewModels.Forms
{
    // Namespaces.
    using Core.FormFields;
    using Core.FormHandlers;
    using Core.Forms;
    using Core.Persistence;
    using System;
    using System.Linq;

    /// <summary>
    /// A view model that supplements the <see cref="PersistedForm"/> class
    /// with additional data that is not persisted.
    /// </summary>
    internal class FormViewModel
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
        public PersistedFormField[] Fields { get; set; }

        /// <inheritdoc cref="PersistedForm.Handlers"/>
        public IFormHandler[] Handlers { get; set; }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="source">
        /// The persisted form to copy.
        /// </param>
        public FormViewModel(PersistedForm source, IFormHandlerFactory formHandlerFactory)
        {
            Id = source.Id;
            Path = source.Path;
            Name = source.Name;
            Alias = source.Alias;
            Fields = source.Fields;
            Handlers = source.Handlers
                ?.Select(x => formHandlerFactory.Create(x))
                ?.ToArray();
        }
    }
}