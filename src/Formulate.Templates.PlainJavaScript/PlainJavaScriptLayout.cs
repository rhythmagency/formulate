namespace Formulate.Templates.PlainJavaScript
{
    using System;
    using System.Collections.Generic;

    public sealed class PlainJavaScriptLayout
    {
        public IReadOnlyCollection<PlainJavaScriptLayoutRow> Rows { get; set; } = Array.Empty<PlainJavaScriptLayoutRow>();
    }
}
