namespace formulate.app.Configuration
{
    using System.Collections.Generic;

    /// <summary>
    /// A configuration section for field categories.
    /// </summary>
    public sealed class FieldCategoriesConfig
    {
        #region Properties

        /// <summary>
        /// Gets or sets the field categories in this configuration section.
        /// </summary>
        public IEnumerable<FieldCategoryConfigItem> Categories { get; set; }

        #endregion
    }
}