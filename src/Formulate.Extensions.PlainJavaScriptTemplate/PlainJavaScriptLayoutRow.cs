namespace Formulate.Extensions.PlainJavaScriptTemplate
{
    public sealed class PlainJavaScriptLayoutRow
    {
        public bool IsStep { get; set; } = false;

        public IReadOnlyCollection<PlainJavaScriptLayoutCell> Cells { get; set; }
    }
}