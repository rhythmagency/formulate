namespace formulate.app.Persistence.Internal
{

    // Namespaces.
    using Entities;
    using Helpers;
    using Resolvers;
    using System;
    using System.Collections.Generic;
    using System.Linq;


    /// <summary>
    /// Handles persistence of entities.
    /// </summary>
    internal class DefaultEntityPersistence : IEntityPersistence
    {

        #region Properties

        /// <summary>
        /// Folder persistence.
        /// </summary>
        private IFolderPersistence Folders
        {
            get
            {
                return FolderPersistence.Current.Manager;
            }
        }


        /// <summary>
        /// Form persistence.
        /// </summary>
        private IFormPersistence Forms
        {
            get
            {
                return FormPersistence.Current.Manager;
            }
        }


        /// <summary>
        /// Configured form persistence.
        /// </summary>
        private IConfiguredFormPersistence ConfiguredForms
        {
            get
            {
                return ConfiguredFormPersistence.Current.Manager;
            }
        }


        /// <summary>
        /// Layout persistence.
        /// </summary>
        private ILayoutPersistence Layouts
        {
            get
            {
                return LayoutPersistence.Current.Manager;
            }
        }


        /// <summary>
        /// Validation persistence.
        /// </summary>
        private IValidationPersistence Validations
        {
            get
            {
                return ValidationPersistence.Current.Manager;
            }
        }


        /// <summary>
        /// Layout persistence.
        /// </summary>
        private IDataValuePersistence DataValues
        {
            get
            {
                return DataValuePersistence.Current.Manager;
            }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DefaultEntityPersistence()
        {
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Gets the entity with the specified ID.
        /// </summary>
        /// <param name="entityId">The ID of the entity.</param>
        /// <returns>
        /// The entity.
        /// </returns>
        public IEntity Retrieve(Guid entityId)
        {

            // Root-level node?
            if (EntityHelper.IsRoot(entityId))
            {
                return new EntityRoot()
                {
                    Id = entityId,
                    Path = new[] { entityId },
                    Name = EntityHelper.GetNameForRoot(entityId),
                    Icon = EntityHelper.GetIconForRoot(entityId)
                };
            }
            else
            {

                // Specific entities (e.g., forms or layouts).
                return Folders.Retrieve(entityId) as IEntity
                    ?? Forms.Retrieve(entityId) as IEntity
                    ?? ConfiguredForms.Retrieve(entityId) as IEntity
                    ?? Layouts.Retrieve(entityId) as IEntity
                    ?? Validations.Retrieve(entityId) as IEntity
                    ?? DataValues.Retrieve(entityId) as IEntity;

            }

        }


        /// <summary>
        /// Gets all the entities that are the children of the folder with the specified ID.
        /// </summary>
        /// <param name="parentId">The parent ID.</param>
        /// <returns>
        /// The entities.
        /// </returns>
        /// <remarks>
        /// You can specify a parent ID of null to get the root entities.
        /// </remarks>
        public IEnumerable<IEntity> RetrieveChildren(Guid? parentId)
        {
            var children = new List<IEntity>();
            children.AddRange(Folders.RetrieveChildren(parentId));
            children.AddRange(Forms.RetrieveChildren(parentId));
            if (parentId.HasValue)
            {
                children.AddRange(ConfiguredForms.RetrieveChildren(parentId.Value));
            }
            children.AddRange(Layouts.RetrieveChildren(parentId));
            children.AddRange(Validations.RetrieveChildren(parentId));
            children.AddRange(DataValues.RetrieveChildren(parentId));
            return children;
        }


        /// <summary>
        /// Gets all the entities that are the descendants of the folder with the specified ID.
        /// </summary>
        /// <param name="parentId">The parent ID.</param>
        /// <returns>
        /// The entities.
        /// </returns>
        public IEnumerable<IEntity> RetrieveDescendants(Guid parentId)
        {
            var descendants = new List<IEntity>();
            var folders = Folders.RetrieveChildren(parentId);
            var folderDescendants = folders.SelectMany(x => RetrieveDescendants(x.Id));
            descendants.AddRange(folders);
            descendants.AddRange(folderDescendants);
            descendants.AddRange(Forms.RetrieveChildren(parentId));
            descendants.AddRange(ConfiguredForms.RetrieveChildren(parentId));
            descendants.AddRange(Layouts.RetrieveChildren(parentId));
            descendants.AddRange(Validations.RetrieveChildren(parentId));
            descendants.AddRange(DataValues.RetrieveChildren(parentId));
            return descendants;
        }

        #endregion

    }

}