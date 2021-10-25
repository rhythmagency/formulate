using Formulate.Core.Types;

namespace Formulate.Core.FormHandlers
{
    /// <summary>
    /// Creates a <see cref="IFormHandler"/>.
    /// </summary>
    public interface IFormHandlerFactory : IEntityFactory<IFormHandlerSettings, IFormHandler>
    {
    }
}