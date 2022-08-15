namespace Formulate.BackOffice.Composers
{
    using Formulate.BackOffice.DependencyInjection;
    using Umbraco.Cms.Core.Composing;
    using Umbraco.Cms.Core.DependencyInjection;

    /// <summary>
    /// Manages composing elements of the back office for Formulate.
    /// </summary>
    public class FormulateBackOfficeComposer : IComposer
    {
        /// <inheritdoc cref="FormulateBackOfficeComposer"/>
        public void Compose(IUmbracoBuilder builder)
        {
            builder.AddFormulateBackOffice();
        }
    }
}