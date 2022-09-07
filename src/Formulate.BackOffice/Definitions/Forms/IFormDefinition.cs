
namespace Formulate.BackOffice.Definitions.Forms
{
    using Formulate.Core.Forms;
    using Formulate.Core.Types;

    /// <summary>
    /// A contract for implementing a form definition.
    /// </summary>
    public interface IFormDefinition : IDefinition
    {
        /// <summary>
        /// Gets the icon.
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the sort order.
        /// </summary>
        int SortOrder { get; }

        /// <summary>
        /// Processes a form post save.
        /// </summary>
        /// <param name="form">
        /// The current form.
        /// </param>
        void PostSave(PersistedForm form);
    }
}