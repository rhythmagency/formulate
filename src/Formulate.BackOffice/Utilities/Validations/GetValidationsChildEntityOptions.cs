namespace Formulate.BackOffice.Utilities.Validations
{
    using Formulate.BackOffice.Controllers;
    using Formulate.Core.Folders;
    using Formulate.Core.Validations;
    using Formulate.Core.Persistence;
    using System.Collections.Generic;
    using System.Linq;
    using Formulate.BackOffice.Persistence;
    using Formulate.BackOffice.Trees;

    public sealed class GetValidationsChildEntityOptions : IGetValidationsChildEntityOptions
    {
        private readonly ValidationDefinitionCollection _validationDefinitions;

        public GetValidationsChildEntityOptions(ValidationDefinitionCollection validationDefinitions)
        {
            _validationDefinitions = validationDefinitions;
        }

        public IReadOnlyCollection<CreateChildEntityOption> Get(IPersistedEntity? parent)
        {
            var options = new List<CreateChildEntityOption>();
            var validationOptions = _validationDefinitions.Select(x => new CreateChildEntityOption()
            {
                Name = x.DefinitionLabel,
                KindId = x.KindId,
                EntityType = EntityTypes.Validation,
                Icon = FormulateValidationsTreeController.Constants.ItemNodeIcon
            }).OrderBy(x => x.Name)
            .ToArray();

            if (parent is null)
            {
                options.AddValidationsFolderOption();
                options.AddRange(validationOptions);

                return options;
            }

            if (parent is not PersistedFolder)
            {
                return options;
            }

            options.AddValidationsFolderOption();
            options.AddRange(validationOptions);

            return options;
        }
    }
}
