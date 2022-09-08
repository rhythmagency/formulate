namespace Formulate.Core.Types
{
    /// <summary>
    /// For implementations that have a directive.
    /// </summary>
    public interface IHaveDirective
    {
        /// <summary>
        /// Gets the directive.
        /// </summary>
        string Directive { get; }
    }
}
