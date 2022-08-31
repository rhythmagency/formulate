namespace Formulate.Templates.PlainJavaScript
{
    public sealed class PlainJavaScriptLayoutCell
    {
        public int ColumnSpan { get; init; }

        public IReadOnlyCollection<Guid> FieldIds { get; set; } = Array.Empty<Guid>();
    }
}