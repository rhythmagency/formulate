namespace Formulate.BackOffice.Utilities.Validations
{
    using Formulate.BackOffice.Controllers;
    using Formulate.Core.Persistence;
    using System.Collections.Generic;

    public interface IGetValidationsChildEntityOptions
    {
        IReadOnlyCollection<CreateChildEntityOption> Get(IPersistedEntity? parent);
    }
}
