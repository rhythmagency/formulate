namespace Formulate.Core.Persistence
{
    /// <summary>
    /// A settings class required to create a <see cref="PersistenceUtility{TPersistedEntity}"/>.
    /// </summary>
    internal sealed class PersistenceUtilitySettings : IPersistenceUtilitySettings
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
        /// Settings used for data values entities.
        /// </summary>
        public static readonly IPersistenceUtilitySettings DataValues = new PersistenceUtilitySettings()
        {
            BasePath = "DataValues",
            Extension = ".json",
            Wildcard = "*.json"
        };

        /// <summary>
        /// Settings used for form entities.
        /// </summary>
        public static readonly IPersistenceUtilitySettings Forms = new PersistenceUtilitySettings()
        {
            BasePath = "forms",
            Extension = ".form",
            Wildcard = "*.form"
        };
        
        /// <summary>
        /// Settings used for folder entities.
        /// </summary>
        public static readonly IPersistenceUtilitySettings Folders = new PersistenceUtilitySettings()
        {
            BasePath = "folders",
            Extension = ".folder",
            Wildcard = "*.folder"
        };

        /// <summary>
        /// Settings used for layout entities.
        /// </summary>
        public static readonly IPersistenceUtilitySettings Layouts = new PersistenceUtilitySettings()
        {
            BasePath = "layouts",
            Extension = ".json",
            Wildcard = "*.json"
        };

        /// <summary>
        /// Settings used for validation entities.
        /// </summary>
        public static IPersistenceUtilitySettings Validations = new PersistenceUtilitySettings()
        {
            BasePath = "validations",
            Extension = ".validation",
            Wildcard = "*.validation"
        };
    }
}
