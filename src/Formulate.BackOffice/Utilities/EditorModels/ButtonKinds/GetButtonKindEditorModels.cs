namespace Formulate.BackOffice.Utilities.EditorModels.Buttons
{
    using Formulate.BackOffice.Configuration;
    using Formulate.BackOffice.EditorModels.ButtonKinds;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class GetButtonKindEditorModels : IGetButtonKindEditorModels
    {
        private readonly ButtonsOptions _options;

        public GetButtonKindEditorModels(IOptions<ButtonsOptions> options)
        {
            _options = options.Value;
        }

        public IReadOnlyCollection<ButtonKindEditorModel> GetAll()
        {
            return _options.Select(x => new ButtonKindEditorModel(x.Kind)).ToArray();
        }
    }
}
