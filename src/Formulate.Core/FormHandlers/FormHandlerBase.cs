using System;

namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// The base class for all form handlers.
    /// </summary>
    /// <remarks>Do not implement this type directly. Instead implement <see cref="FormHandler"/> or <see cref="AsyncFormHandler"/>.</remarks>
    public abstract class FormHandlerBase : IFormHandler
    {
        /// <summary>
        /// Gets or sets the type ID.
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        public string Alias { get; set; }
        
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether this handler is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// The raw configuration.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is for reference only.
        /// </para>
        /// <para>
        /// Deserialization should typically happen in the overriden <see cref="FormHandlerType"/> CreateHandler or the <see cref="AsyncFormHandler"/> CreateAsyncHandler methods.
        /// </para>
        /// </remarks>
        protected readonly string RawConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormHandlerBase"/> class.
        /// </summary>
        /// <param name="settings">The form handler settings.</param>
        /// <exception cref="ArgumentNullException">The settings parameter is null.</exception>
        protected FormHandlerBase(IFormHandlerSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            Id = settings.Id;
            TypeId = settings.Id;
            Alias = settings.Alias;
            Name = settings.Name;
            Enabled = settings.Enabled;
            RawConfiguration = settings.Configuration;
        }
    }
}
