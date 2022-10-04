namespace Formulate.Website.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class FormulateWebsiteOptions
    {
        public const string SectionName = "Formulate:Website";

        public FormulateWebsitePersistenceOptions Persistence { get; set; } = new FormulateWebsitePersistenceOptions();
    }
}
