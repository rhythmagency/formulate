namespace Formulate.BackOffice.Utilities.FormFields
{
    using Formulate.BackOffice.Controllers;
    using System.Collections.Generic;

    public interface IGetFormFieldOptions
    {
        IReadOnlyCollection<CreateItemOption> Get();
    }
}
