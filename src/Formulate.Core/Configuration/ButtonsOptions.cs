namespace Formulate.Core.Configuration
{
    // Namespaces.
    using System.Collections.Generic;

    /// <summary>
    /// The configuration options for form buttons.
    /// </summary>
    public sealed class ButtonsOptions
    {
        /// <summary>
        /// The name of the section to load this configuration from.
        /// </summary>
        public const string SectionName = "Formulate:Buttons";

        
        public ButtonsOptions()
        {
        }

        public List<ButtonItem> Items { get; set; } = new List<ButtonItem>()
        {
            new ButtonItem() { Kind = "Next" },
            new ButtonItem() { Kind = "Previous" },
            new ButtonItem() { Kind = "Submit" },
        };
    }
}