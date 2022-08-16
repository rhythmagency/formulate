namespace Formulate.BackOffice.Utilities.FormHandlers
{
    using Formulate.BackOffice.Controllers;
    using Formulate.Core.FormHandlers;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class GetFormHandlerOptions : IGetFormHandlerOptions
    {
        private readonly FormHandlerDefinitionCollection _formHandlerDefinitions;

        public GetFormHandlerOptions(FormHandlerDefinitionCollection formHandlerDefinitions)
        {
            _formHandlerDefinitions = formHandlerDefinitions;
        }

        public IReadOnlyCollection<CreateItemOption> Get()
        {
            return _formHandlerDefinitions.Select(x => new CreateItemOption() {
                Icon = x.Icon,
                KindId = x.KindId,
                Name = x.DefinitionLabel,
                Category = x.Category
            }).ToArray();
        }
    }
}
