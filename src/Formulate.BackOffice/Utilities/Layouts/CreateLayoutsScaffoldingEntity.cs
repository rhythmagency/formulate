namespace Formulate.BackOffice.Utilities.Layouts
{
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.Layouts;
    using Formulate.Core.Folders;
    using Formulate.Core.Persistence;
    using System;
    using System.Collections.Generic;

    public sealed class CreateLayoutsScaffoldingEntity : ICreateLayoutsScaffoldingEntity
    {
        public IPersistedEntity? Create(CreateLayoutsScaffoldingEntityInput input)
        {
            var parent = input.Parent;
            var id = Guid.NewGuid();
            var entityPath = new List<Guid>();
            
            if (parent is not null)
            {
                entityPath.AddRange(parent.Path);
            }
            else 
            {
                entityPath.Add(input.RootId);
            }

            entityPath.Add(id);

            if (input.EntityType == EntityTypes.Layout && input.KindId.HasValue)
            {
                return new PersistedLayout()
                {
                    Id = id,
                    Path = entityPath.ToArray(),
                    KindId = input.KindId.Value,
                };
            }
            else if (input.EntityType == EntityTypes.Folder)
            {
                return new PersistedFolder()
                {
                    Id = id,
                    Path = entityPath.ToArray(),
                };
            }

            return default;
        }
    }
}
