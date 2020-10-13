namespace formulate.app.Backoffice
{
    using Umbraco.Core.Models.Sections;

    /// <summary>
    /// The Umbraco CMS backoffice Formulate section
    /// </summary>
    internal sealed class FormulateSection : ISection
    {
        /// <inheritdoc />
        public string Alias => "formulate";

        /// <inheritdoc />
        public string Name => "Formulate";
    }
}
