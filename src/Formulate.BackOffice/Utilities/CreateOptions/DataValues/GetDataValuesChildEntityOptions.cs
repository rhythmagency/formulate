namespace Formulate.BackOffice.Utilities.CreateOptions.DataValues
{
    using Formulate.BackOffice.Controllers;
    using Formulate.Core.Folders;
    using Formulate.Core.DataValues;
    using Formulate.Core.Persistence;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class GetDataValuesChildEntityOptions : IGetDataValuesChildEntityOptions
    {
        private readonly DataValuesDefinitionCollection _dataValuesDefinitions;

        private readonly string _folderIcon;

        public GetDataValuesChildEntityOptions(IGetFolderIconOrDefault getFolderIconOrDefault, DataValuesDefinitionCollection dataValuesDefinitions)
        {
            _folderIcon = getFolderIconOrDefault.GetFolderIcon(TreeTypes.DataValues);
            _dataValuesDefinitions = dataValuesDefinitions;
        }

        public IReadOnlyCollection<CreateChildEntityOption> Get(IPersistedEntity? parent)
        {
            var options = new List<CreateChildEntityOption>();

            var dataValueOptions = _dataValuesDefinitions.Where(x => x.IsLegacy == false).Select(x => new CreateChildEntityOption()
            {
                Name = x.Name,
                KindId = x.KindId,
                EntityType = EntityTypes.DataValues,
                Icon = x.Icon
            }).OrderBy(x => x.Name)
            .ToArray();

            if (parent is null)
            {
                options.AddFolderOption(_folderIcon);
                options.AddRange(dataValueOptions);

                return options;
            }

            if (parent is not PersistedFolder)
            {
                return options;
            }

            options.AddFolderOption(_folderIcon);
            options.AddRange(dataValueOptions);

            return options;
        }
    }
}
