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
    }
}
