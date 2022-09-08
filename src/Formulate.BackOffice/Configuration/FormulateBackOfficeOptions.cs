using System;
using Formulate.Core.Templates;

namespace Formulate.BackOffice.Configuration
{
    public sealed class FormulateBackOfficeOptions
    {
        public const string SectionName = "Formulate:BackOffice";

        /// <summary>
        /// Gets a value indicating where to use the default folder icon instead of tree specific icons.
        /// </summary>
        /// <remarks>This defaults to false.</remarks>
        public bool UseDefaultFolderIcon { get; set; } = false;


        /// <summary>
        /// Gets the default template ID to be used when creating new layouts.
        /// </summary>
        /// <remarks>If no value is provided the first registered <see cref="ITemplateDefinition"/>'s <see cref="KindId"/> will be used instead.</remarks>
        public Guid? DefaultTemplateId { get; set; }
    }
}
