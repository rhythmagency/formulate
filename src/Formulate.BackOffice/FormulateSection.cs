using Umbraco.Cms.Core.Sections;

namespace Formulate.BackOffice
{
    public sealed class FormulateSection : ISection
    {
        public static class Constants
        {
            /// <summary>
            /// The alias.
            /// </summary>
            public const string Alias = "formulate";

            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "Formulate";
        }

        /// <inheritdoc />
        public string Alias => Constants.Alias;

        /// <inheritdoc />
        public string Name => Constants.Name;
    }
}
