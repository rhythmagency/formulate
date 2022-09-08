namespace Formulate.Core.Templates
{
    using Formulate.Core.Types;

    /// <summary>
    /// Represents a contract for configuring template definitions.
    /// </summary>
    public interface ITemplateDefinition : IDefinition
    {
        /// <summary>
        /// The view name of the template definition.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is used to determine the file name used by the rendering View Component.
        /// </para>
        /// <para>
        /// This should be a valid file name without an extension. (e.g. "Plain JavaScript" not "Plain JavaScript.cshtml")
        /// </para>
        /// </remarks>
        string ViewName { get; }
    }
}
