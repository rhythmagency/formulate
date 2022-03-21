namespace Formulate.BackOffice.ViewModels.Forms
{
    // Namespaces.
    using Core.FormHandlers;
    using Core.Types;
    using System;

    /// <summary>
    /// A view model that supplements the <see cref="PersistedFormHandler"/> class
    /// with additional data that is not persisted.
    /// </summary>
    internal class FormHandlerViewModel
    {
        /// <inheritdoc cref="IEntitySettings.Id"/>
        public Guid Id { get; set; }

        /// <inheritdoc cref="IEntitySettings.KindId"/>
        public Guid KindId { get; set; }

        /// <inheritdoc cref="IEntitySettings.Name"/>
        public string Name { get; set; }

        /// <inheritdoc cref="IEntitySettings.Data"/>
        public string Data { get; set; }

        /// <inheritdoc cref="IFormHandlerSettings.Alias"/>
        public string Alias { get; set; }

        /// <inheritdoc cref="IFormHandlerSettings.Enabled"/>
        public bool Enabled { get; set; }

        /// <summary>
        /// The icon for this handler.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="source">
        /// The persisted form handler to copy.
        /// </param>
        public FormHandlerViewModel(PersistedFormHandler source)
        {
            Id = source.Id;
            KindId = source.KindId;
            Name = source.Name;
            Data = source.Data;
            Alias = source.Alias;
            Enabled = source.Enabled;
            //TODO: Set icon.
        }
    }
}