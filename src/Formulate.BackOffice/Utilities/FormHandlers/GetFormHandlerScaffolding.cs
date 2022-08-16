namespace Formulate.BackOffice.Utilities.FormHandlers
{
    using Formulate.Core.FormHandlers;
    using Formulate.Core.Types;
    using System;

    public sealed class GetFormHandlerScaffolding : IGetFormHandlerScaffolding
    {
        private readonly FormHandlerDefinitionCollection _formHandlerDefinitions;

        public GetFormHandlerScaffolding(FormHandlerDefinitionCollection formHandlerDefinitions)
        {
            _formHandlerDefinitions = formHandlerDefinitions;
        }

        public PersistedFormHandler? Get(Guid id)
        {
            var definition = _formHandlerDefinitions.FirstOrDefault(id);

            if (definition is null)
            {
                return default;
            }

            var scaffolding = new PersistedFormHandler()
            {
                Id = Guid.NewGuid(),
                Enabled = true,
                KindId = definition.KindId,
            };

            return scaffolding;
        }
    }
}
