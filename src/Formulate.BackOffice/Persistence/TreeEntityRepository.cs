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
    /// <summary>
    /// The default implementation of <see cref="ITreeEntityRepository"/>.
    /// </summary>
    internal sealed class TreeEntityRepository : ITreeEntityRepository
    {
        /// <summary>
        /// The configured form entity repository.
        /// </summary>
        private readonly IConfiguredFormEntityRepository _configuredFormEntityRepository;

        /// <summary>
        /// The form entity repository.
        /// </summary>
        private readonly IFormEntityRepository _formEntityRepository;

        /// <summary>
        /// The layout entity repository.
        /// </summary>
        private readonly ILayoutEntityRepository _layoutEntityRepository;

        /// <summary>
        /// The folder entity repository.
        /// </summary>
        private readonly IFolderEntityRepository _folderEntityRepository;

        /// <summary>
        /// The data values entity repository.
        /// </summary>
        private readonly IDataValuesEntityRepository _dataValuesEntityRepository;

        /// <summary>
        /// The validation entity repository.
        /// </summary>
        private readonly IValidationEntityRepository _validationEntityRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeEntityRepository"/> class.
        /// </summary>
        /// <param name="configuredFormEntityRepository">The configured form entity repository.</param>
        /// <param name="formEntityRepository">The form entity repository.</param>
        /// <param name="layoutEntityRepository">The layout entity repository.</param>
        /// <param name="folderEntityRepository">The folder entity repository.</param>
        /// <param name="dataValuesEntityRepository">The data values entity repository.</param>
        /// <param name="validationEntityRepository">The validation entity repository.</param>
        public TreeEntityRepository(IConfiguredFormEntityRepository configuredFormEntityRepository, IFormEntityRepository formEntityRepository, ILayoutEntityRepository layoutEntityRepository, IFolderEntityRepository folderEntityRepository, IDataValuesEntityRepository dataValuesEntityRepository, IValidationEntityRepository validationEntityRepository)
        {
            _configuredFormEntityRepository = configuredFormEntityRepository;
            _formEntityRepository = formEntityRepository;
            _layoutEntityRepository = layoutEntityRepository;
            _folderEntityRepository = folderEntityRepository;
            _dataValuesEntityRepository = dataValuesEntityRepository;
            _validationEntityRepository = validationEntityRepository;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public bool HasChildren(Guid parentId)
        {
            return GetChildren(parentId).Any();
        }

        /// <inheritdoc />
        public IReadOnlyCollection<IPersistedEntity> GetRootItems(TreeRootTypes treeRootType)
        {
            var rootId = GetRootId(treeRootType);
            var entities = new List<IPersistedEntity>(_folderEntityRepository.GetChildren(rootId));

            switch (treeRootType)
            {
                case TreeRootTypes.DataValues:
                    entities.AddRange(_dataValuesEntityRepository.GetRootItems());
                    break;
                case TreeRootTypes.Forms:
                    entities.AddRange(_formEntityRepository.GetRootItems());
                    break;
                case TreeRootTypes.Layouts:
                    entities.AddRange(_layoutEntityRepository.GetRootItems());
                    break;
                case TreeRootTypes.Validations:
                    entities.AddRange(_validationEntityRepository.GetRootItems());
                    break;
            }

            return entities.ToArray();
        }

        public Guid GetRootId(TreeRootTypes treeRootType)
        {
            var rootId = GetRootIdString(treeRootType);

            if (Guid.TryParse(rootId, out var parentId))
            {
                return parentId;
            }

            return default;
        }

        public IReadOnlyCollection<Guid> Delete(IPersistedEntity entity)
        {
            var children = GetChildren(entity.Id);
            var deleteIds = new List<Guid>();

            foreach (var child in children)
            {
                deleteIds.AddRange(Delete(child));
            }

            switch (entity)
            {
                case PersistedConfiguredForm:
                    _configuredFormEntityRepository.Delete(entity.Id);
                    break;

                case PersistedDataValues:
                    _dataValuesEntityRepository.Delete(entity.Id);
                    break;

                case PersistedFolder:
                    _folderEntityRepository.Delete(entity.Id);
                    break;

                case PersistedForm:
                    _folderEntityRepository.Delete(entity.Id);
                    break;

                case PersistedLayout:
                    _layoutEntityRepository.Delete(entity.Id);
                    break;

                case PersistedValidation:
                    _layoutEntityRepository.Delete(entity.Id);
                    break;
            }

            deleteIds.Add(entity.Id);

            return deleteIds;
        }
        public Guid[] Move(IPersistedEntity entity, Guid[] parentPath)
        {
            var updatedPath = new List<Guid>(parentPath).Append(entity.Id).ToArray();

            foreach (var child in GetChildren(entity.Id))
            {
                Move(child, updatedPath);
            }

            if (entity is not PersistedEntity persistedEntity)
            {
                throw new NotSupportedException($"{entity.GetType()} does not support being moved.");
            }

            persistedEntity.Path = updatedPath;

            switch (persistedEntity)
            {
                case PersistedConfiguredForm configuredForm:
                    return _configuredFormEntityRepository.Move(configuredForm, updatedPath);

                case PersistedDataValues dataValues:
                    return _dataValuesEntityRepository.Move(dataValues, updatedPath);

                case PersistedFolder folder:
                    return _folderEntityRepository.Move(folder, updatedPath);

                case PersistedForm form:
                    return _formEntityRepository.Move(form, updatedPath);

                case PersistedLayout layout:
                    return _layoutEntityRepository.Move(layout, updatedPath);

                case PersistedValidation validation:
                    return _validationEntityRepository.Move(validation, updatedPath);
            }

            throw new NotSupportedException($"{entity.GetType()} does not support being moved.");
        }

        /// <summary>
        /// Gets the Root ID for the current ent
        /// ity type/
        /// </summary>
        /// <param name="treeRootType">The tree root type.</param>
        /// <returns>A <see cref="string"/>.</returns>
        private static string GetRootIdString(TreeRootTypes treeRootType)
        {
            return treeRootType switch
            {
                TreeRootTypes.Forms => FormConstants.RootId,
                TreeRootTypes.DataValues => DataValuesConstants.RootId,
                TreeRootTypes.Layouts => LayoutConstants.RootId,
                TreeRootTypes.Validations => ValidationConstant.RootId,
                _ => throw new NotSupportedException($"{treeRootType} is not a supported root type.")
            };
        }
    }
}