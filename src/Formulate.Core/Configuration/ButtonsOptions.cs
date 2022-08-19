namespace Formulate.Core.Configuration
{
    using System.Collections.Generic;

    /// <summary>
    /// The configuration options for form buttons.
    /// </summary>
    public sealed class ButtonsOptions : List<ButtonItem>
    {
        /// <summary>
        /// The name of the section to load this configuration from.
        /// </summary>
        public const string SectionName = "Formulate:Buttons";

        internal static readonly ButtonItem[] FallbackOptions = new[]
        {
            new ButtonItem()
            {
                Kind = "Next"
            },
            new ButtonItem()
            {
                Kind = "Previous"
            },
            new ButtonItem()
            {
                Kind = "Submit"
            }
        };

        public ButtonsOptions()
        {
        }
    }
}