namespace Formulate.BackOffice.Utilities.FormHandlers
{
    using Formulate.Core.FormHandlers;
    using System;

    public interface IGetFormHandlerScaffolding
    {
        PersistedFormHandler? Get(Guid id);
    }
}
