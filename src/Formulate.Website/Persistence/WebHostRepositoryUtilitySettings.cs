namespace Formulate.Website.Persistence
{
    /// <summary>
    /// A settings class required to create a <see cref="WebHostRepositoryUtility{TPersistedEntity}"/>.
    /// </summary>
    internal sealed class WebHostRepositoryUtilitySettings
    {
        /// <summary>
        /// Gets or sets the base path to JSON files.
        /// </summary>
        public string BasePath { get; init; }

        /// <summary>
        /// Gets or sets the extension of JSON files.
        /// </summary>
        public string Extension { get; init; }

        /// <summary>
        /// Gets or sets the wildcard used to match JSON files.
        /// </summary>
        public string Wildcard { get; init; }

        /// <summary>
        /// Settings used for data values entities.
        /// </summary>
        public static readonly WebHostRepositoryUtilitySettings DataValues = new WebHostRepositoryUtilitySettings()
        {
            BasePath = "DataValues",
            Extension = ".dataValue",
            Wildcard = "*.dataValue"
        };

        /// <summary>
        /// Settings used for form entities.
        /// </summary>
        public static readonly WebHostRepositoryUtilitySettings Forms = new WebHostRepositoryUtilitySettings()
        {
            BasePath = "forms",
            Extension = ".form",
            Wildcard = "*.form"
        };
        
        /// <summary>
        /// Settings used for folder entities.
        /// </summary>
        public static readonly WebHostRepositoryUtilitySettings Folders = new WebHostRepositoryUtilitySettings()
        {
            BasePath = "folders",
            Extension = ".folder",
            Wildcard = "*.folder"
        };

        /// <summary>
        /// Settings used for layout entities.
        /// </summary>
        public static readonly WebHostRepositoryUtilitySettings Layouts = new WebHostRepositoryUtilitySettings()
        {
            BasePath = "layouts",
            Extension = ".layout",
            Wildcard = "*.layout"
        };

        /// <summary>
        /// Settings used for validation entities.
        /// </summary>
        public static WebHostRepositoryUtilitySettings Validations = new WebHostRepositoryUtilitySettings()
        {
            BasePath = "validations",
            Extension = ".validation",
            Wildcard = "*.validation"
        };
    }
}
