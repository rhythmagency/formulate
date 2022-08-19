namespace Formulate.BackOffice.Utilities.EditorModels.Forms
{
    using Formulate.BackOffice.Configuration;
    using Formulate.BackOffice.EditorModels.Forms;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class GetFormFieldCategoryEditorModels : IGetFormFieldCategoryEditorModels
    {
        private readonly FormFieldOptions _options;

        public GetFormFieldCategoryEditorModels(IOptions<FormFieldOptions> options)
        {
            _options = options.Value;
        }

        public IReadOnlyCollection<FormFieldCategoryEditorModel> GetAll()
        {
            return _options.Categories.Select(x => new FormFieldCategoryEditorModel(x.Kind, x.Group)).ToArray();
        }
    }
}
