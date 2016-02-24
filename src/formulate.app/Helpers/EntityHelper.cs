namespace formulate.app.Helpers
{

    // Namespaces.
    using Entities;
    using System;
    using System.Linq;
    using Umbraco.Core;
    using CoreConstants = Umbraco.Core.Constants;
    using DataSourceConstants = formulate.app.Constants.Trees.DataSources;
    using DataValueConstants = formulate.app.Constants.Trees.DataValues;
    using FormConstants = formulate.app.Constants.Trees.Forms;
    using LayoutConstants = formulate.app.Constants.Trees.Layouts;
    using ValidationConstants = formulate.app.Constants.Trees.Validations;


    /// <summary>
    /// Helps with entities.
    /// </summary>
    internal class EntityHelper
    {

        #region Constants

        private const string InvalidId = "The specified ID was not recognized as a valid root ID.";

        #endregion


        #region Readonly Variables

        static readonly Guid FormId;
        static readonly Guid LayoutId;
        static readonly Guid ValidationId;
        static readonly Guid DataValueId;
        static readonly Guid DataSourceId;

        #endregion


        #region Constructors

        /// <summary>
        /// Static constructor.
        /// </summary>
        static EntityHelper()
        {
            FormId = GuidHelper.GetGuid(FormConstants.Id);
            LayoutId = GuidHelper.GetGuid(LayoutConstants.Id);
            ValidationId = GuidHelper.GetGuid(ValidationConstants.Id);
            DataValueId = GuidHelper.GetGuid(DataValueConstants.Id);
            DataSourceId = GuidHelper.GetGuid(DataSourceConstants.Id);
        }

        #endregion


        #region Methods

        /// <summary>
        /// Gets the tree icon for the root entity with the specified ID.
        /// </summary>
        /// <param name="id">
        /// The root entity ID.
        /// </param>
        /// <returns>The icon.</returns>
        public static string GetIconForRoot(Guid id)
        {
            if (id == FormId)
            {
                return FormConstants.TreeIcon;
            }
            else if (id == LayoutId)
            {
                return LayoutConstants.TreeIcon;
            }
            else if (id == ValidationId)
            {
                return ValidationConstants.TreeIcon;
            }
            else if (id == DataValueId)
            {
                return DataValueConstants.TreeIcon;
            }
            else if (id == DataSourceId)
            {
                return DataSourceConstants.TreeIcon;
            }
            else
            {
                throw new ArgumentOutOfRangeException("id", InvalidId);
            }
        }


        /// <summary>
        /// Gets the group tree icon for the entities under the root
        /// with the specified ID.
        /// </summary>
        /// <param name="id">
        /// The root entity ID.
        /// </param>
        /// <returns>
        /// The group icon.
        /// </returns>
        public static string GetGroupIconByRoot(Guid id)
        {
            if (id == FormId)
            {
                return FormConstants.GroupIcon;
            }
            else if (id == LayoutId)
            {
                return LayoutConstants.GroupIcon;
            }
            else if (id == ValidationId)
            {
                return ValidationConstants.GroupIcon;
            }
            else if (id == DataValueId)
            {
                return DataValueConstants.GroupIcon;
            }
            else if (id == DataSourceId)
            {
                return DataSourceConstants.GroupIcon;
            }
            else
            {
                throw new ArgumentOutOfRangeException("id", InvalidId);
            }
        }


        /// <summary>
        /// Gets the name for the root entity with the specified ID.
        /// </summary>
        /// <param name="id">
        /// The root entity ID.
        /// </param>
        /// <returns>
        /// The entity name.
        /// </returns>
        public static string GetNameForRoot(Guid id)
        {
            if (id == FormId)
            {
                return FormConstants.Title;
            }
            else if (id == LayoutId)
            {
                return LayoutConstants.Title;
            }
            else if (id == ValidationId)
            {
                return ValidationConstants.Title;
            }
            else if (id == DataValueId)
            {
                return DataValueConstants.Title;
            }
            else if (id == DataSourceId)
            {
                return DataSourceConstants.Title;
            }
            else
            {
                throw new ArgumentOutOfRangeException("id", InvalidId);
            }
        }


        /// <summary>
        /// Indicates whether or not the entity with the specified ID
        /// is a root entity.
        /// </summary>
        /// <param name="id">The entity ID.</param>
        /// <returns>
        /// True, if the entity is a root entity; otherwise, false.
        /// </returns>
        public static bool IsRoot(Guid id)
        {
            return id == FormId || id == LayoutId || id == ValidationId
                || id == DataValueId || id == DataSourceId;
        }


        /// <summary>
        /// Converts an entity kind to a string.
        /// </summary>
        /// <param name="kind">The entity kind.</param>
        /// <returns>
        /// The entity kind string.
        /// </returns>
        public static string GetString(EntityKind kind)
        {
            return Enum.GetName(typeof(EntityKind), kind);
        }


        /// <summary>
        /// Converts a server-side entity path to a client-side entity path.
        /// </summary>
        /// <param name="path">
        /// The server-side entity path.
        /// </param>
        /// <returns>
        /// The client-side entity path.
        /// </returns>
        /// <remarks>
        /// The client-side entity path expects an extra root ID of "-1",
        /// which this method includes.
        /// </remarks>
        public static string[] GetClientPath(Guid[] path)
        {
            var rootId = CoreConstants.System.Root.ToInvariantString();
            var clientPath = new[] { rootId }
                .Concat(path.Select(x => GuidHelper.GetString(x)))
                .ToArray();
            return clientPath;
        }

        #endregion

    }

}