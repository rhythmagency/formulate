namespace Formulate.BackOffice.Utilities.CreateOptions.Forms
{
    using Formulate.BackOffice.Controllers;
    using Formulate.BackOffice.Definitions.Forms;
    using Formulate.Core.Folders;
    using Formulate.Core.Forms;
    using Formulate.Core.Layouts;
    using Formulate.Core.Persistence;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class GetFormsChildEntityOptions : IGetFormsChildEntityOptions
    {
        private readonly string _folderIcon;
        private readonly FormDefinitionCollection _formDefinitions;
        private readonly LayoutDefinitionCollection _layoutDefinitions;

        public GetFormsChildEntityOptions(FormDefinitionCollection formDefinitions, LayoutDefinitionCollection layoutDefinitions, IGetFolderIconOrDefault getFolderIconOrDefault)
        {
            _formDefinitions = formDefinitions;
            _layoutDefinitions = layoutDefinitions;
            _folderIcon = getFolderIconOrDefault.GetFolderIcon(TreeTypes.Forms);
        }

        public IReadOnlyCollection<CreateChildEntityOption> Get(IPersistedEntity? parent)
        {
            var options = new List<CreateChildEntityOption>();

            if (parent is PersistedForm)
            {
                var layoutOptions = _layoutDefinitions.Where(x => x.IsLegacy == false).Select(x => new CreateChildEntityOption()
                {
                    Name = x.Name,
                    KindId = x.KindId,
                    EntityType = EntityTypes.Layout,
                    Icon = Constants.Icons.Entities.Layout
                }).OrderBy(x => x.Name)
                .ToArray();

                options.AddRange(layoutOptions);

                return options;
            }

            options.AddFolderOption(_folderIcon);

            var formDefinitions = _formDefinitions.OrderBy(x => x.SortOrder).ThenBy(x => x.Name).ToArray();

            foreach(var definition in formDefinitions)
            {
                options.Add(new CreateChildEntityOption()
                {
                    EntityType = EntityTypes.Form,
                    Name = definition.Name,
                    Description = definition.Description,
                    Icon = definition.Icon,
                    KindId = definition.KindId,
                });
            }

            return options;
        }
    }
}
