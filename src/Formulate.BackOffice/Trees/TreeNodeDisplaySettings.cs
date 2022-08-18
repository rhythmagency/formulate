namespace Formulate.BackOffice.Trees
{
    public sealed class TreeNodeDisplaySettings
    {
        public TreeNodeDisplaySettings(string icon) : this(icon, false)
        {
        }

        public TreeNodeDisplaySettings(string icon, bool isLegacy)
        {
            Icon = icon;
            IsLegacy = isLegacy;
        }

        public string Icon { get; init; }

        public bool IsLegacy { get; init; }
    }
}
