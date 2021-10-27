using Formulate.Core.FormFields;
using Formulate.Core.FormHandlers;

namespace Formulate.Core.Forms
{
    public interface IFormEntitySettings
    {

        IFormFieldSettings[] Fields { get; }

        IFormHandlerSettings[] Handlers { get; }
    }
}
