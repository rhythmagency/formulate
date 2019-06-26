namespace formulate.api.Controllers
{
    using System.Web.Mvc;

    using formulate.app.Helpers;
    using formulate.app.Types;
    using formulate.core.Models;

    using Umbraco.Web.Mvc;

    /// <summary>
    /// The rendering controller.
    /// </summary>
    public sealed class FormulateRenderingController : SurfaceController
    {
        /// <summary>
        /// Gets or sets the definition helper.
        /// </summary>
        private IDefinitionHelper DefinitionHelper { get; set; }

        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="definitionHelper">
        /// The definition helper.
        /// </param>
        public FormulateRenderingController(IDefinitionHelper definitionHelper)
        {
            DefinitionHelper = definitionHelper;
        }

        /// <summary>
        /// A Child Action which renders a configured Formulate form.
        /// </summary>
        /// <param name="form">
        /// The configured form info.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        /// <remarks>Example usage: @Html.Action("Render", "FormulateRendering", new { form = Model.Form })</remarks>
        [ChildActionOnly]
        public ActionResult Render(ConfiguredFormInfo form)
        {
            var viewModel = new FormViewModel()
            {
                FormDefinition = DefinitionHelper.GetFormDefinition(form.FormId),
                LayoutDefinition = DefinitionHelper.GetLayoutDefinition(form.LayoutId),
                TemplatePath = DefinitionHelper.GetTemplatePath(form.TemplateId),
                PageId = CurrentPage.Id
            };

            return PartialView("~/Views/Partials/Formulate/RenderForm.cshtml", viewModel);
        }
    }
}
