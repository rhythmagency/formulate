namespace Formulate.BackOffice.Utilities.Forms
{
    using Formulate.BackOffice.Controllers;
    using Formulate.Core.Folders;
    using Formulate.Core.Forms;
    using Formulate.Core.Persistence;
    using System.Collections.Generic;

    public sealed class GetFormsChildEntityOptions : IGetFormsChildEntityOptions
    {
        public IReadOnlyCollection<CreateChildEntityOption> Get(IPersistedEntity? parent)
        {
            var options = new List<CreateChildEntityOption>();

            if (parent is null)
            {
                options.AddFormFolderOption();
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

            options.AddFormFolderOption();
            options.AddFormOption();

            return options;
        }
    }
}
