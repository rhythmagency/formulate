namespace Formulate.BackOffice.Utilities.CreateOptions.Validations
{
    using Formulate.BackOffice.Controllers;
    using Formulate.Core.Folders;
    using Formulate.Core.Validations;
    using Formulate.Core.Persistence;
    using System.Collections.Generic;
    using System.Linq;
    using Formulate.BackOffice.Persistence;

    public sealed class GetValidationsChildEntityOptions : IGetValidationsChildEntityOptions
    {
        private readonly string _folderIcon;

        private readonly ValidationDefinitionCollection _validationDefinitions;

        public GetValidationsChildEntityOptions(IGetFolderIconOrDefault getFolderIconOrDefault, ValidationDefinitionCollection validationDefinitions)
        {
            _folderIcon = getFolderIconOrDefault.GetFolderIcon(TreeRootTypes.Validations);
            _validationDefinitions = validationDefinitions;
        }

        public IReadOnlyCollection<CreateChildEntityOption> Get(IPersistedEntity? parent)
        {
            var options = new List<CreateChildEntityOption>();
            var validationOptions = _validationDefinitions.Where(x => x.IsLegacy == false).Select(x => new CreateChildEntityOption()
            {
                Name = x.Name,
                KindId = x.KindId,
                EntityType = EntityTypes.Validation,
                Icon = Constants.Icons.Entities.Validation
            }).OrderBy(x => x.Name)
            .ToArray();

            if (parent is null)
            {
                options.AddFolderOption(_folderIcon);
                options.AddRange(validationOptions);

                return options;
            }

            if (parent is not PersistedFolder)
            {
                return options;
            }

            options.AddFolderOption(_folderIcon);
            options.AddRange(validationOptions);

            return options;
        }
    }
}
