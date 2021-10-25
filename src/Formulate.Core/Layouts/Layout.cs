using System;

namespace Formulate.Core.Layouts
{
    /// <summary>
    /// The extended base class for a layout with a configuration.
    /// </summary>
    /// <typeparam name="TConfig">The type of the layout configuration.</typeparam>
    public abstract class Layout<TConfig> : Layout
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public TConfig Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Layout"/> class.
        /// </summary>
        /// <param name="settings">The layout settings.</param>
        /// <param name="configuration">The layout configuration.</param>
        /// <exception cref="ArgumentNullException">The settings parameter is null.</exception>
        protected Layout(ILayoutSettings settings, TConfig configuration) : base(settings)
        {
            Configuration = configuration;
        }
    }

    /// <summary>
    /// The base class for all layouts.
    /// </summary>
    public abstract class Layout : ILayout
    {
        /// <inheritdoc />
        public Guid TypeId { get; }

        /// <inheritdoc />
        public Guid Id { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Directive { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Layout"/> class.
        /// </summary>
        /// <param name="settings">The layout settings.</param>
        /// <exception cref="ArgumentNullException">The settings parameter is null.</exception>
        protected Layout(ILayoutSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            TypeId = settings.TypeId;
            Id = settings.Id;
            Directive = settings.Directive;
            Name = settings.Name;
        }
    }
}
