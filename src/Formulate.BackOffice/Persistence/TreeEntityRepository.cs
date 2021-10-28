using System;
using System.Collections.Generic;
using System.Linq;
using Formulate.Core.ConfiguredForms;
using Formulate.Core.DataValues;
using Formulate.Core.Folders;
using Formulate.Core.Forms;
using Formulate.Core.Layouts;
using Formulate.Core.Persistence;
using Formulate.Core.Validations;

namespace Formulate.BackOffice.Persistence
{
    internal sealed class TreeEntityRepository : ITreeEntityRepository
    {
        private readonly IConfiguredFormEntityRepository _configuredFormEntityRepository;
        private readonly IFormEntityRepository _formEntityRepository;
        private readonly ILayoutEntityRepository _layoutEntityRepository;
        private readonly IFolderEntityRepository _folderEntityRepository;
        private readonly IDataValuesEntityRepository _dataValuesEntityRepository;
        private readonly IValidationEntityRepository _validationEntityRepository;

        public TreeEntityRepository(IConfiguredFormEntityRepository configuredFormEntityRepository, IFormEntityRepository formEntityRepository, ILayoutEntityRepository layoutEntityRepository, IFolderEntityRepository folderEntityRepository, IDataValuesEntityRepository dataValuesEntityRepository, IValidationEntityRepository validationEntityRepository)
        {
            _configuredFormEntityRepository = configuredFormEntityRepository;
            _formEntityRepository = formEntityRepository;
            _layoutEntityRepository = layoutEntityRepository;
            _folderEntityRepository = folderEntityRepository;
            _dataValuesEntityRepository = dataValuesEntityRepository;
            _validationEntityRepository = validationEntityRepository;
        }

        public IPersistedEntity Get(Guid id)
        {
            var actions = new Func<Guid, IPersistedEntity>[]
            {
                _configuredFormEntityRepository.Get,
                _dataValuesEntityRepository.Get,
                _folderEntityRepository.Get,
                _formEntityRepository.Get,
                _layoutEntityRepository.Get,
                _validationEntityRepository.Get,
            };

            foreach (var action in actions)
            {
                var entity = action.Invoke(id);

                if (entity is not null)
                {
                    return entity;
                }
            }

            return default;
        }

        public IReadOnlyCollection<IPersistedEntity> GetChildren(Guid parentId)
        {
            var children = new List<IPersistedEntity>();

            children.AddRange(_configuredFormEntityRepository.GetChildren(parentId));
            children.AddRange(_dataValuesEntityRepository.GetChildren(parentId));
            children.AddRange(_folderEntityRepository.GetChildren(parentId));
            children.AddRange(_formEntityRepository.GetChildren(parentId));
            children.AddRange(_layoutEntityRepository.GetChildren(parentId));
            children.AddRange(_validationEntityRepository.GetChildren(parentId));

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
                entities.AddRange(_folderEntityRepository.GetChildren(parentId));
            }

            switch (type)
            {
                case FormulateEntityTypes.DataValues:
                    entities.AddRange(_dataValuesEntityRepository.GetRootItems());
                    break;
                case FormulateEntityTypes.Forms:
                    entities.AddRange(_formEntityRepository.GetRootItems());
                    break;
                case FormulateEntityTypes.Layouts:
                    entities.AddRange(_layoutEntityRepository.GetRootItems());
                    break;
                case FormulateEntityTypes.Validations:
                    entities.AddRange(_validationEntityRepository.GetRootItems());
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