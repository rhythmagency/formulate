namespace Formulate.BackOffice.Utilities.CreateOptions.FormFields
{
    using Formulate.BackOffice.Controllers;
    using System.Collections.Generic;

    public interface IGetFormFieldOptions
    {
        IReadOnlyCollection<CreateItemOption> Get();
    }
}
