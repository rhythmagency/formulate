namespace Formulate.BackOffice.Utilities.FormHandlers
{
    using Formulate.BackOffice.Controllers;
    using System.Collections.Generic;

    public interface IGetFormHandlerOptions
    {
        IReadOnlyCollection<CreateItemOption> Get();
    }
}
