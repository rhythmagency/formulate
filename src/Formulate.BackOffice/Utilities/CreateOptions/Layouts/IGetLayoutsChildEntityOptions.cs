namespace Formulate.BackOffice.Utilities.CreateOptions.Layouts
{
    using Formulate.BackOffice.Controllers;
    using Formulate.Core.Persistence;
    using System.Collections.Generic;

    public interface IGetLayoutsChildEntityOptions
    {
        IReadOnlyCollection<CreateChildEntityOption> Get(IPersistedEntity? parent);
    }
}
