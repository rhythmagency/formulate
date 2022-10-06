namespace Formulate.Web.Persistence
{
    /// <summary>
    /// A settings class required to create a <see cref="WebHostRepositoryUtility{TPersistedEntity}"/>.
    /// </summary>
    internal sealed class WebHostRepositoryUtilitySettings
    {
        public WebHostRepositoryUtilitySettings(string basePath, string extension) : this(basePath, extension, $"*{extension}")
        {
        }

        public WebHostRepositoryUtilitySettings(string basePath, string extension, string wildcard)
        {
            BasePath = basePath;
            Extension = extension;
            Wildcard = wildcard;
        }

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
        public static readonly WebHostRepositoryUtilitySettings DataValues = new("DataValues", ".dataValue");

        /// <summary>
        /// Settings used for form entities.
        /// </summary>
        public static readonly WebHostRepositoryUtilitySettings Forms = new("forms", ".form");

        /// <summary>
        /// Settings used for folder entities.
        /// </summary>
        public static readonly WebHostRepositoryUtilitySettings Folders = new("folders", ".folder");

        /// <summary>
        /// Settings used for layout entities.
        /// </summary>
        public static readonly WebHostRepositoryUtilitySettings Layouts = new("layouts", ".layout");

        /// <summary>
        /// Settings used for validation entities.
        /// </summary>
        public static WebHostRepositoryUtilitySettings Validations = new("validations", ".validation");
    }
}
