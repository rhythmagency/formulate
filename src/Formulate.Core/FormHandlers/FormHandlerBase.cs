using System;

namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// The base class for all form handlers.
    /// </summary>
    /// <remarks>Do not implement this definition directly. Instead implement <see cref="FormHandler"/> or <see cref="AsyncFormHandler"/>.</remarks>
    public abstract class FormHandlerBase : IFormHandler
    {
        /// <inheritdoc />
        public Guid DefinitionId { get; }

        /// <summary>
        /// Gets the ID.
        /// </summary>
        public Guid Id { get; }
        
        /// <summary>
        /// Gets the alias.
        /// </summary>
        public string Alias { get; }
        
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Gets a value indicating whether this is enabled.
        /// </summary>
        public bool Enabled { get; }

        /// <summary>
        /// The raw configuration.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is for reference only.
        /// </para>
        /// <para>
        /// Deserialization should typically happen in the overridden <see cref="FormHandlerDefinition"/> CreateHandler or the <see cref="AsyncFormHandler"/> CreateAsyncHandler methods.
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
            DefinitionId = settings.Id;
            Alias = settings.Alias;
            Name = settings.Name;
            Enabled = settings.Enabled;
            RawConfiguration = settings.Configuration;
        }
    }
}
