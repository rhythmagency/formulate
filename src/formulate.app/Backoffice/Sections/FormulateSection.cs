namespace formulate.app.Backoffice
{
    using Umbraco.Core.Models.Sections;

    /// <summary>
    /// The Umbraco CMS backoffice Formulate section
    /// </summary>
    internal sealed class FormulateSection : ISection
    {
        public string Alias => "formulate";

        public string Name => "Formulate";
    }
}
