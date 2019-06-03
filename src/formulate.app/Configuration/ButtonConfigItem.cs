namespace formulate.app.Configuration
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// A "button" configuration item.
    /// </summary>
    public sealed class ButtonConfigItem
    {
        #region Properties

        /// <summary>
        /// Gets or sets the kind of the button.
        /// </summary>
        [Required]
        public string Kind { get; set; }

        #endregion
    }
}