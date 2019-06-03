namespace formulate.app.Configuration
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// A "category" configuration element.
    /// </summary>
    public sealed class FieldCategoryConfigItem
    {
        #region Properties

        /// <summary>
        /// Gets or sets the type of the category.
        /// </summary>
        [Required]
        public string Kind { get; set; }

        /// <summary>
        /// Gets or sets the group for the category.
        /// </summary>
        public string Group { get; set; }

        #endregion
    }
}
