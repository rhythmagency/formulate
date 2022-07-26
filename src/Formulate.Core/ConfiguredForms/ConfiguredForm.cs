namespace Formulate.Core.ConfiguredForms
{
    using System;

    /// <summary>
    /// A configured form used by the front end.
    /// </summary>
    public sealed class ConfiguredForm
    {
        public Guid Configuration { get; init; }

        public Guid FormId { get; init; }

        public Guid? LayoutId { get; init; }

        public Guid? TemplateId { get; init; }
    }
}
