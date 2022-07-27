namespace Formulate.Website.Components
{
    using Formulate.Core.Configuration;
    using Formulate.Core.ConfiguredForms;
    using Formulate.Website.Utilities;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Microsoft.Extensions.Options;

    [ViewComponent]
    public sealed class FormulateFormViewComponent : ViewComponent
    {
        private readonly TemplatesOptions _templatesConfig;
        private readonly IBuildConfiguredFormRenderModel _buildConfiguredFormRenderModel;

        public FormulateFormViewComponent(IOptions<TemplatesOptions> templatesConfig, IBuildConfiguredFormRenderModel buildConfiguredFormRenderModel)
        {
            _templatesConfig = templatesConfig.Value;
            _buildConfiguredFormRenderModel = buildConfiguredFormRenderModel;
        }

        public IViewComponentResult Invoke(ConfiguredForm model)
        {
            if (model is null)
            {
                return EmptyResult();
            }

            var template = _templatesConfig.Items.FirstOrDefault(x => x.Id == model.TemplateId);

            if (template is null)
            {
                return EmptyResult();
            }

            var renderModel = _buildConfiguredFormRenderModel.Build(model);

            if (renderModel is null)
            {
                return EmptyResult();
            }

            return View(template.Name, renderModel);
        }

        private static IViewComponentResult EmptyResult()
        {
            return new HtmlContentViewComponentResult(HtmlString.Empty);
        }
    }
}