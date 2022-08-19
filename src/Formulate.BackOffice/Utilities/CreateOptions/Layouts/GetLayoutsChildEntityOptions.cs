namespace Formulate.BackOffice.Utilities.CreateOptions.Layouts
{
    using Formulate.BackOffice.Controllers;
    using Formulate.Core.Folders;
    using Formulate.Core.Layouts;
    using Formulate.Core.Persistence;
    using System.Collections.Generic;
    using System.Linq;
    using Formulate.BackOffice.Persistence;

    public sealed class GetLayoutsChildEntityOptions : IGetLayoutsChildEntityOptions
    {
        private readonly LayoutDefinitionCollection _layoutDefinitions;

        public GetLayoutsChildEntityOptions(LayoutDefinitionCollection validationDefinitions)
        {
            _layoutDefinitions = validationDefinitions;
        }

        public IReadOnlyCollection<CreateChildEntityOption> Get(IPersistedEntity? parent)
        {
            var options = new List<CreateChildEntityOption>();
            var validationOptions = _layoutDefinitions.Where(x => x.IsLegacy == false).Select(x => new CreateChildEntityOption()
            {
                Name = x.Name,
                KindId = x.KindId,
                EntityType = EntityTypes.Layout,
                Icon = Constants.Icons.Entities.Layout
            }).OrderBy(x => x.Name)
            .ToArray();

            if (parent is null)
            {
                options.AddLayoutsFolderOption();
                options.AddRange(validationOptions);

                return options;
            }

            if (parent is not PersistedFolder)
            {
                return options;
            }

            options.AddLayoutsFolderOption();
            options.AddRange(validationOptions);

            return options;
        }
    }
}
