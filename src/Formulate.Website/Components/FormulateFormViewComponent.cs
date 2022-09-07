namespace Formulate.Website.Components
{
    using Formulate.Core.Layouts;
    using Formulate.Core.Templates;
    using Formulate.Website.Utilities;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewComponents;

    [ViewComponent]
    public sealed class FormulateFormViewComponent : ViewComponent
    {
        private readonly TemplateDefinitionCollection _templateDefinitions;
        private readonly IBuildFormLayoutRenderModel _buildFormLayoutRenderModel;

        public FormulateFormViewComponent(TemplateDefinitionCollection templateDefinitions, IBuildFormLayoutRenderModel buildFormLayoutRenderModel)
        {
            _templateDefinitions = templateDefinitions;
            _buildFormLayoutRenderModel = buildFormLayoutRenderModel;
        }

        public IViewComponentResult Invoke(FormLayout model)
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

            var renderModel = _buildFormLayoutRenderModel.Build(model);

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