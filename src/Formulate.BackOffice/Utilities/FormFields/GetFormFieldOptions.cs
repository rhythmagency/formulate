namespace Formulate.BackOffice.Utilities.FormFields
{
    using Formulate.BackOffice.Controllers;
    using Formulate.Core.FormFields;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class GetFormFieldOptions : IGetFormFieldOptions
    {
        private readonly FormFieldDefinitionCollection _formFieldDefinitions;

        public GetFormFieldOptions(FormFieldDefinitionCollection formFieldDefinitions)
        {
            _formFieldDefinitions = formFieldDefinitions;
        }

        public IReadOnlyCollection<CreateItemOption> Get()
        {
            return _formFieldDefinitions.Select(x => new CreateItemOption() {
                Icon = x.Icon,
                KindId = x.KindId,
                Name = x.DefinitionLabel,
                Category = x.Category
            }).ToArray();
        }
    }
}
