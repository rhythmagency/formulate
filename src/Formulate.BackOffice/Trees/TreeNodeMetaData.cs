namespace Formulate.BackOffice.Trees
{
    public sealed class TreeNodeMetaData
    {
        public TreeNodeMetaData(string icon) : this(icon, false)
        {
        }

        public TreeNodeMetaData(string icon, bool isLegacy)
        {
            Icon = icon;
            IsLegacy = isLegacy;
        }

        public string Icon { get; init; }

        public bool IsLegacy { get; init; }
    }
}
