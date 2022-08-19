namespace Formulate.BackOffice.Utilities.Validations
{
    using Formulate.Core.Validations;
    using Formulate.Core.Folders;
    using Formulate.Core.Persistence;
    using System;
    using System.Collections.Generic;

    public sealed class CreateValidationsScaffoldingEntity : ICreateValidationsScaffoldingEntity
    {
        public IPersistedEntity? Create(CreateValidationsScaffoldingEntityInput input)
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

            if (input.EntityType == EntityTypes.Validation && input.KindId.HasValue)
            {
                return new PersistedValidation()
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
