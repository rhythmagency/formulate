namespace Formulate.BackOffice.Utilities.Forms
{
    using Formulate.BackOffice.Controllers;
    using Formulate.Core.Persistence;
    using System.Collections.Generic;

    public interface IGetFormsChildEntityOptions
    {
        IReadOnlyCollection<CreateChildEntityOption> Get(IPersistedEntity? parent);
    }
}
