using System;
using System.Collections.Generic;
using System.Linq;
using Formulate.Core.DataValues;
using Formulate.Core.Folders;
using Formulate.Core.Forms;
using Formulate.Core.Layouts;
using Formulate.Core.Persistence;
using Formulate.Core.Validations;

namespace Formulate.BackOffice.Persistence
{
    internal sealed class TreeEntityPersistence : ITreeEntityPersistence
    {
        private readonly IFormEntityPersistence _formEntityPersistence;
        private readonly ILayoutEntityPersistence _layoutEntityPersistence;
        private readonly IFolderEntityPersistence _folderEntityPersistence;
        private readonly IDataValuesEntityPersistence _dataValuesEntityPersistence;
        private readonly IValidationEntityPersistence _validationEntityPersistence;

        public TreeEntityPersistence(IFormEntityPersistence formEntityPersistence, ILayoutEntityPersistence layoutEntityPersistence, IFolderEntityPersistence folderEntityPersistence, IDataValuesEntityPersistence dataValuesEntityPersistence, IValidationEntityPersistence validationEntityPersistence)
        {
            _formEntityPersistence = formEntityPersistence;
            _layoutEntityPersistence = layoutEntityPersistence;
            _folderEntityPersistence = folderEntityPersistence;
            _dataValuesEntityPersistence = dataValuesEntityPersistence;
            _validationEntityPersistence = validationEntityPersistence;
        }

        public IReadOnlyCollection<IPersistedEntity> GetChildren(Guid parentId)
        {
            var children = new List<IPersistedEntity>();

            children.AddRange(_dataValuesEntityPersistence.GetChildren(parentId));
            children.AddRange(_folderEntityPersistence.GetChildren(parentId));
            children.AddRange(_formEntityPersistence.GetChildren(parentId));
            children.AddRange(_layoutEntityPersistence.GetChildren(parentId));
            children.AddRange(_validationEntityPersistence.GetChildren(parentId));

            return children.ToArray();
        }

        public bool HasChildren(Guid parentId)
        {
            return GetChildren(parentId).Any();
        }

        public IReadOnlyCollection<IPersistedEntity> GetRootItems(FormulateEntityTypes type)
        {
            var rootId = GetRootId(type);
            var entities = new List<IPersistedEntity>();

            if (Guid.TryParse(rootId, out var parentId))
            {
                entities.AddRange(_folderEntityPersistence.GetChildren(parentId));
            }

            switch (type)
            {
                case FormulateEntityTypes.DataValues:
                    entities.AddRange(_dataValuesEntityPersistence.GetRootItems());
                    break;
                case FormulateEntityTypes.Forms:
                    entities.AddRange(_formEntityPersistence.GetRootItems());
                    break;
                case FormulateEntityTypes.Layouts:
                    entities.AddRange(_layoutEntityPersistence.GetRootItems());
                    break;
                case FormulateEntityTypes.Validations:
                    entities.AddRange(_validationEntityPersistence.GetRootItems());
                    break;
            }

            return entities.ToArray();
        }

        private static string GetRootId(FormulateEntityTypes type)
        {
            return type switch
            {
                FormulateEntityTypes.Forms => FormConstants.RootId,
                FormulateEntityTypes.DataValues => DataValuesConstants.RootId,
                FormulateEntityTypes.Layouts => LayoutConstants.RootId,
                FormulateEntityTypes.Validations => ValidationsConstants.RootId,
                _ => default
            };
        }
    }
}