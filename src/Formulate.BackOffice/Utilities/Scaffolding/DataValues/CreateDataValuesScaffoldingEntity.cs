namespace Formulate.BackOffice.Utilities.DataValues
{
    using Formulate.Core.DataValues;
    using Formulate.Core.Folders;
    using Formulate.Core.Persistence;
    using System;
    using System.Collections.Generic;

    public sealed class CreateDataValuesScaffoldingEntity : ICreateDataValuesScaffoldingEntity
    {
        public IPersistedEntity? Create(CreateDataValuesScaffoldingEntityInput input)
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

            if (input.EntityType == EntityTypes.DataValues && input.KindId.HasValue)
            {
                return new PersistedDataValues()
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
