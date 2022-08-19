namespace Formulate.BackOffice.Utilities.EditorModels.Templates
{
    using Formulate.BackOffice.EditorModels.Templates;
    using Formulate.Core.Templates;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class GetTemplateEditorModels : IGetTemplateEditorModels
    {
        private readonly TemplateDefinitionCollection _templateDefinitions;

        public GetTemplateEditorModels(TemplateDefinitionCollection templateDefinitions)
        {
            _templateDefinitions = templateDefinitions;
        }

        public IReadOnlyCollection<TemplateEditorModel> GetAll()
        {
            return _templateDefinitions.Select(x => new TemplateEditorModel(x)).ToArray();
        }
    }
}
