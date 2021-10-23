namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// An abstract class for creating a form handler type.
    /// </summary>
    public abstract class FormHandlerType : FormHandlerTypeBase
    {
        /// <summary>
        /// Creates a Form Handler.
        /// </summary>
        /// <returns>A <see cref="FormHandler"/>.</returns>
        public abstract FormHandler CreateHandler();
    }
}