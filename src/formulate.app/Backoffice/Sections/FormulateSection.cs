namespace formulate.app.Backoffice
{
    using Umbraco.Core.Models.Sections;

    /// <summary>
    /// The <see cref="ISection"/> used by Formulate in the Umbraco backoffice.
    /// </summary>
    internal sealed class FormulateSection : ISection
    {
        /// <inheritdoc />
        public string Alias => "formulate";

        /// <inheritdoc />
        public string Name => "Formulate";
    }
}
