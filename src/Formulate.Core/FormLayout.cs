namespace Formulate.Core
{
    using System;

    /// <summary>
    /// A configured form used by the front end.
    /// </summary>
    public sealed class FormLayout
    {
        public Guid FormId { get; init; }

        public Guid LayoutId { get; init; }

        public Guid? TemplateId { get; init; }
    }
}
