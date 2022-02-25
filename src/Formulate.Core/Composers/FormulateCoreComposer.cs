using Formulate.Core.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Formulate.Core.Composers
{
    public class FormulateCoreComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.AddFormulateCore();
        }
    }
}