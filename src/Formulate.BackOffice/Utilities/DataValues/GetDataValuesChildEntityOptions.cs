namespace Formulate.BackOffice.Utilities.DataValues
{
    using Formulate.BackOffice.Controllers;
    using Formulate.Core.Folders;
    using Formulate.Core.DataValues;
    using Formulate.Core.Persistence;
    using System.Collections.Generic;
    using System.Linq;
    using Formulate.BackOffice.Persistence;

    public sealed class GetDataValuesChildEntityOptions : IGetDataValuesChildEntityOptions
    {
        private readonly DataValuesDefinitionCollection _dataValuesDefinitions;

        public GetDataValuesChildEntityOptions(DataValuesDefinitionCollection dataValuesDefinitions)
        {
            _dataValuesDefinitions = dataValuesDefinitions;
        }

        public IReadOnlyCollection<CreateChildEntityOption> Get(IPersistedEntity? parent)
        {
            var options = new List<CreateChildEntityOption>();

            var dataValueOptions = _dataValuesDefinitions.Select(x => new CreateChildEntityOption()
            {
                Name = x.DefinitionLabel,
                KindId = x.KindId,
                EntityType = EntityTypes.DataValues,
                Icon = x.Icon
            }).OrderBy(x => x.Name)
            .ToArray();


            if (parent is null)
            {
                options.AddDataValuesFolderOption();
                options.AddRange(dataValueOptions);

                return options;
            }

            if (parent is not PersistedFolder)
            {
                return options;
            }

            options.AddDataValuesFolderOption();
            options.AddRange(dataValueOptions);

            return options;
        }
    }
}
