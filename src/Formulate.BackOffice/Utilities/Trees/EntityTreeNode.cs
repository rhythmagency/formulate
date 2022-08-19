namespace Formulate.BackOffice.Utilities.Trees
{
    using Formulate.BackOffice.Trees;
    using Formulate.Core.Persistence;

    public sealed class EntityTreeNode
    {
        public EntityTreeNode(IPersistedEntity entity, bool hasChildren, string icon, bool isLegacy)
        {
            Id = entity.BackOfficeSafeId();
            HasChildren = hasChildren;
            IsLegacy = isLegacy;
            Icon = icon;
            Name = entity.Name;
            NodeType = entity.EntityType().ToString();
            Path = entity.TreeSafePathString();
        }

        public bool HasChildren { get; init; }

        public string Id { get; init; }

        public string Icon { get; init; }

        public string NodeType { get; init; }

        public bool IsLegacy { get; init; }

        public string Name { get; init; }

        public string Path { get; init; }
    }
}
