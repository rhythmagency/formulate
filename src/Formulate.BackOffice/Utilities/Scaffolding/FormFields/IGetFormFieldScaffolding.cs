namespace Formulate.BackOffice.Utilities.FormFields
{
    using Formulate.Core.FormFields;
    using System;

    public interface IGetFormFieldScaffolding
    {
        PersistedFormField? Get(Guid id);
    }

}
