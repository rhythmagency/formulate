namespace formulate.app.Handlers
{

    // Namespaces.
    using Umbraco.Core;


    /// <summary>
    /// Handles the application started event.
    /// </summary>
    internal class ApplicationStartedHandler : ApplicationEventHandler
    {

        #region Methods

        /// <summary>
        /// Application started.
        /// </summary>
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {
            var service = applicationContext.Services.SectionService;
            var existingSection = service.GetByAlias("formulate");
            if (existingSection == null)
            {
                service.MakeNew("Formulate", "formulate", "icon-formulate-clipboard", 6);
            }
        }

        #endregion

    }

}