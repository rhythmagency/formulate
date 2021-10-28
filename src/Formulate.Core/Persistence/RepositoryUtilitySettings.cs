namespace Formulate.Core.Persistence
{
    /// <summary>
    /// A settings class required to create a <see cref="RepositoryUtility{TPersistedEntity}"/>.
    /// </summary>
    internal sealed class RepositoryUtilitySettings : IRepositoryUtilitySettings
    {
        /// <summary>
        /// Gets or sets the base path to JSON files.
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// Gets or sets the extension of JSON files.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets the wildcard used to match JSON files.
        /// </summary>
        public string Wildcard { get; set; }

        /// <summary>
        /// Settings used for configured forms entities.
        /// </summary>
        public static readonly IRepositoryUtilitySettings ConfiguredForms = new RepositoryUtilitySettings()
        {
            BasePath = "ConfiguredForms",
            Extension = ".conform",
            Wildcard = "*.conform"
        };

        /// <summary>
        /// Settings used for data values entities.
        /// </summary>
        public static readonly IRepositoryUtilitySettings DataValues = new RepositoryUtilitySettings()
        {
            BasePath = "DataValues",
            Extension = ".json",
            Wildcard = "*.json"
        };

        /// <summary>
        /// Settings used for form entities.
        /// </summary>
        public static readonly IRepositoryUtilitySettings Forms = new RepositoryUtilitySettings()
        {
            BasePath = "forms",
            Extension = ".form",
            Wildcard = "*.form"
        };
        
        /// <summary>
        /// Settings used for folder entities.
        /// </summary>
        public static readonly IRepositoryUtilitySettings Folders = new RepositoryUtilitySettings()
        {
            BasePath = "folders",
            Extension = ".folder",
            Wildcard = "*.folder"
        };

        /// <summary>
        /// Settings used for layout entities.
        /// </summary>
        public static readonly IRepositoryUtilitySettings Layouts = new RepositoryUtilitySettings()
        {
            BasePath = "layouts",
            Extension = ".json",
            Wildcard = "*.json"
        };

        /// <summary>
        /// Settings used for validation entities.
        /// </summary>
        public static IRepositoryUtilitySettings Validations = new RepositoryUtilitySettings()
        {
            BasePath = "validations",
            Extension = ".validation",
            Wildcard = "*.validation"
        };
    }
}
