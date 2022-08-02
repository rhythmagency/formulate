namespace Formulate.BackOffice.Utilities.Layouts
{
    using Formulate.BackOffice.Controllers;
    using Formulate.Core.Folders;
    using Formulate.Core.Layouts;
    using Formulate.Core.Persistence;
    using System.Collections.Generic;
    using System.Linq;
    using Formulate.BackOffice.Persistence;
    using Formulate.BackOffice.Trees;

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
            var validationOptions = _layoutDefinitions.Select(x => new CreateChildEntityOption()
            {
                Name = x.DefinitionLabel,
                KindId = x.KindId,
                EntityType = EntityTypes.Layout,
                Icon = FormulateLayoutsTreeController.Constants.ItemNodeIcon
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
