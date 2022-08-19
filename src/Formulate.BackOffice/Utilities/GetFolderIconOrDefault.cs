namespace Formulate.BackOffice.Utilities
{
    using Formulate.BackOffice.Configuration;
    using Formulate.BackOffice.Persistence;
    using Microsoft.Extensions.Options;

    internal sealed class GetFolderIconOrDefault : IGetFolderIconOrDefault
    {
        private readonly bool _useDefaultFolderIcon;

        public GetFolderIconOrDefault(IOptions<FormulateBackOfficeOptions> options)
        {
            _useDefaultFolderIcon = options.Value.UseDefaultFolderIcon;
        }

        public string GetFolderIcon(TreeRootTypes input)
        {
            if (_useDefaultFolderIcon)
            {
                return Constants.Icons.Folders.Default;
            }

            return input switch
            {
                TreeRootTypes.DataValues => Constants.Icons.Folders.DataValues,
                TreeRootTypes.Forms => Constants.Icons.Folders.Forms,
                TreeRootTypes.Layouts => Constants.Icons.Folders.Layouts,
                TreeRootTypes.Validations => Constants.Icons.Folders.Validations,
                _ => Constants.Icons.Folders.Default
            };
        }
    }
}
