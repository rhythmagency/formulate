namespace Formulate.BackOffice.Utilities
{
    using Formulate.BackOffice.Configuration;
    using Microsoft.Extensions.Options;

    internal sealed class GetFolderIconOrDefault : IGetFolderIconOrDefault
    {
        private readonly bool _useDefaultFolderIcon;

        public GetFolderIconOrDefault(IOptions<FormulateBackOfficeOptions> options)
        {
            _useDefaultFolderIcon = options.Value.UseDefaultFolderIcon;
        }

        public string GetFolderIcon(TreeTypes input)
        {
            if (_useDefaultFolderIcon)
            {
                return Constants.Icons.Folders.Default;
            }

            return input switch
            {
                TreeTypes.DataValues => Constants.Icons.Folders.DataValues,
                TreeTypes.Forms => Constants.Icons.Folders.Forms,
                TreeTypes.Layouts => Constants.Icons.Folders.Layouts,
                TreeTypes.Validations => Constants.Icons.Folders.Validations,
                _ => Constants.Icons.Folders.Default
            };
        }
    }
}
