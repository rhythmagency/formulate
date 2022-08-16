namespace Formulate.BackOffice.Utilities.FormFields
{
    using Formulate.Core.FormFields;
    using Formulate.Core.Types;
    using System;

    public sealed class GetFormFieldScaffolding : IGetFormFieldScaffolding
    {
        private readonly FormFieldDefinitionCollection _formFieldDefinitions;

        public GetFormFieldScaffolding(FormFieldDefinitionCollection formFieldDefinitions)
        {
            _formFieldDefinitions = formFieldDefinitions;
        }

        public PersistedFormField? Get(Guid id)
        {
            var definition = _formFieldDefinitions.FirstOrDefault(id);

            if (definition is null)
            {
                return default;
            }

            var scaffolding = new PersistedFormField()
            {
                Id = Guid.NewGuid(),
                KindId = definition.KindId,
            };

            return scaffolding;
        }
    }

}
