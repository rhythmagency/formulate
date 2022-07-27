namespace Formulate.Website.Components
{
    using Formulate.Core.Configuration;
    using Formulate.Core.ConfiguredForms;
    using Formulate.Core.Templates;
    using Formulate.Website.Utilities;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Microsoft.Extensions.Options;

    [ViewComponent]
    public sealed class FormulateFormViewComponent : ViewComponent
    {
        private readonly TemplateDefinitionCollection _templateDefinitions;
        private readonly IBuildConfiguredFormRenderModel _buildConfiguredFormRenderModel;

        public FormulateFormViewComponent(TemplateDefinitionCollection templateDefinitions, IBuildConfiguredFormRenderModel buildConfiguredFormRenderModel)
        {
            _templateDefinitions = templateDefinitions;
            _buildConfiguredFormRenderModel = buildConfiguredFormRenderModel;
        }

        public IViewComponentResult Invoke(ConfiguredForm model)
        {
            if (model is null)
            {
                return EmptyResult();
            }

            var template = _templateDefinitions.FirstOrDefault(x => x.Id == model.TemplateId);

            if (template is null)
            {
                return EmptyResult();
            }

            var renderModel = _buildConfiguredFormRenderModel.Build(model);

            if (renderModel is null)
            {
                return EmptyResult();
            }

            return View(template.ViewName, renderModel);
        }

        private static IViewComponentResult EmptyResult()
        {
            return new HtmlContentViewComponentResult(HtmlString.Empty);
        }
    }
}