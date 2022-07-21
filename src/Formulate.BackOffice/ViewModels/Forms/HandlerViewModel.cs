namespace Formulate.BackOffice.ViewModels.Forms
{
    // Namespaces.
    using System;

    /// <summary>
    /// The data required by the back office for a form handler.
    /// </summary>
    internal class HandlerViewModel
    {
        /// <summary>
        /// The configuration for this handler.
        /// </summary>
        public object Configuration { get; set; }

        /// <summary>
        /// The ID of this handler.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The ID for the type of this handler.
        /// </summary>
        public Guid KindId { get; set; }

        /// <summary>
        /// The name of this handler.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The alias of this handler.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// The back office directive that renders this handler.
        /// </summary>
        public string Directive { get; set; }

        /// <summary>
        /// Is this handler enabled?
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// The icon representing the type for this handler.
        /// </summary>
        public string Icon { get; set; }
    }
}