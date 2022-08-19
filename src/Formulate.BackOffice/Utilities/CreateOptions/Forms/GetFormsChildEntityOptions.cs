namespace Formulate.BackOffice.Utilities.CreateOptions.Forms
{
    using Formulate.BackOffice.Controllers;
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.Folders;
    using Formulate.Core.Forms;
    using Formulate.Core.Persistence;
    using System.Collections.Generic;

    public sealed class GetFormsChildEntityOptions : IGetFormsChildEntityOptions
    {
        private readonly string _folderIcon;

        public GetFormsChildEntityOptions(IGetFolderIconOrDefault getFolderIconOrDefault)
        {
            _folderIcon = getFolderIconOrDefault.GetFolderIcon(TreeRootTypes.Forms);
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
                options.AddConfiguredFormOption();

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
