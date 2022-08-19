namespace Formulate.BackOffice.Utilities.Trees
{
    internal sealed class EntityTreeNodeMetaData
    {
        public EntityTreeNodeMetaData(string icon) : this(icon, false)
        {
        }

        public EntityTreeNodeMetaData(string icon, bool isLegacy)
        {
            Icon = icon;
            IsLegacy = isLegacy;
        }

        public string Icon { get; init; }

        public bool IsLegacy { get; init; }
    }
}
