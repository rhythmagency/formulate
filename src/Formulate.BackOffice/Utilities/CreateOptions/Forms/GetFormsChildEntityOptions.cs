namespace Formulate.BackOffice.Utilities.CreateOptions.Forms
{
    using Formulate.BackOffice.Controllers;
    using Formulate.Core.Folders;
    using Formulate.Core.Forms;
    using Formulate.Core.Layouts;
    using Formulate.Core.Persistence;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class GetFormsChildEntityOptions : IGetFormsChildEntityOptions
    {
        private readonly string _folderIcon;

        private readonly LayoutDefinitionCollection _layoutDefinitions;

        public GetFormsChildEntityOptions(LayoutDefinitionCollection layoutDefinitions, IGetFolderIconOrDefault getFolderIconOrDefault)
        {
            _layoutDefinitions = layoutDefinitions;
            _folderIcon = getFolderIconOrDefault.GetFolderIcon(TreeTypes.Forms);
        }

        public IReadOnlyCollection<CreateChildEntityOption> Get(IPersistedEntity? parent)
        {
            var options = new List<CreateChildEntityOption>();

            if (parent is null)
            {
                options.AddFolderOption(_folderIcon);
                options.AddFormOption();

                return options;
            }

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

            if (parent is not PersistedFolder)
            {
                return options;
            }

            options.AddFolderOption(_folderIcon);
            options.AddFormOption();

            return options;
        }
    }
}
